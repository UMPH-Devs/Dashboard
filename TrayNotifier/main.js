const { app, Tray, shell } = require('electron')

// handle setupevents as quickly as possible
const setupEvents = require('./installers/setupEvents')
setupEvents.handleSquirrelEvent() // squirrel event handled and app will exit in 1000ms, so don't do anything else

const request = require('request')

const ElectronStore = require('electron-store')
const settings = new ElectronStore()

let memoryStore = require('./inMemoryStore')
let resources = require('./resources')
let interval = require('./interval')
let contextMenuBuilder = require('./contextMenuBuilder')
let notifications = require('./notifications')

let appIcon
let currentInterval
let snoozeInterval

let snoozeTimeInSeconds = 0
let updateTimeInSeconds = minutes => {
  return 1000 * 60 * (minutes === null ? settings.get('interval', 10) : minutes)
}
memoryStore.interval = settings.get('interval', 10)

// stay in main.js
function updateStatus () {
  request.get(resources.dashboardApiUrl, function (error, response, body) {
    if (error) {
      notifications.displayStatus('requestError', appIcon, error, dialogCallback)
    } else if (response === null || response === undefined) {
      notifications.displayStatus(
        'requestError',
        appIcon,
        'response is null or undefined',
        dialogCallback
      )
    } else if (response.statusCode !== 200) {
      notifications.displayStatus('requestError', appIcon, response.statusCode, dialogCallback)
    } else {
      notifications.displayStatus(JSON.parse(body), appIcon, null, dialogCallback)
    }
  })
}
function dialogCallback () {
  const contextMenu = contextMenuBuilder.buildMenu(
    updateCurrentInterval,
    openPrdInBrowser,
    openStgInBrowser,
    snoozeNotificationsClickCallback
  )
  appIcon.setContextMenu(contextMenu)
}
function clearSnooze () {
  memoryStore.snooze = false
  memoryStore.snoozeInterval = 0

  // recreating the menu because I can't figure out how to access it once it has been set.
  const contextMenu = contextMenuBuilder.buildMenu(
    updateCurrentInterval,
    openPrdInBrowser,
    openStgInBrowser,
    snoozeNotificationsClickCallback
  )
  appIcon.setContextMenu(contextMenu)
}

// START Callbacks
function openStgInBrowser () {
  shell.openExternal(resources.stgDashboardUrl)
}
function openPrdInBrowser () {
  shell.openExternal(resources.prdDashboardUrl)
}
function updateCurrentInterval (minutes) {
  memoryStore.interval = minutes
  settings.set('interval', minutes)
  currentInterval = interval.resetInterval(
    currentInterval,
    updateTimeInSeconds(minutes),
    updateStatus
  )
}
function snoozeNotificationsClickCallback (minutes) {
  memoryStore.snooze = true
  memoryStore.snoozeInterval = minutes
  snoozeTimeInSeconds = 1000 * 60 * minutes
  snoozeInterval = interval.resetInterval(snoozeInterval, snoozeTimeInSeconds, clearSnooze)
}
// END Callbacks

function createTrayIcon () {
  appIcon = new Tray(resources.appLoadingIconUrl)

  const contextMenu = contextMenuBuilder.buildMenu(
    updateCurrentInterval,
    openPrdInBrowser,
    openStgInBrowser,
    snoozeNotificationsClickCallback
  )
  appIcon.setContextMenu(contextMenu)

  appIcon.on('click', updateStatus)

  if (resources.mainDashboardEnvironment === 'stage') {
    appIcon.on('double-click', openStgInBrowser)
  } else {
    appIcon.on('double-click', openPrdInBrowser)
  }
  updateStatus()
  currentInterval = interval.newInterval(updateTimeInSeconds(null), updateStatus)
}

app.on('ready', createTrayIcon) // execute application when ready...

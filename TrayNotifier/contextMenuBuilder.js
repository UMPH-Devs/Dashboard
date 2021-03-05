const Menu = require('electron').Menu
const app = require('electron').app
const resources = require('./resources')

const AutoLaunch = require('auto-launch')
let notifierAutolauncher = new AutoLaunch({
  name: resources.apiSuccessTitle
})

const ElectronStore = require('electron-store')
const settings = new ElectronStore()

let memoryStore = require('./inMemoryStore')

const squelchIndex = 0
const warningBalloonIndex = 1
const errorBalloonIndex = 2
const snoozeIndex = 3
const startupIndex = 4
const intervalIndex = 5

function buildMenu (
  intervalClickCallback,
  openPrdInBrowser,
  openStgInBrowser,
  snoozeNotificationsClickCallback
) {
  function squelchErrorsClick (menuItem, browserWindow, event) {
    squelch = !!menuItem.checked
    memoryStore.squelch = squelch
  }
  function changeIntervalClick (menuItem, browserWindow, event) {
    intervalClickCallback(menuItem.id)
  }
  function snoozeNotificationsClick (menuItem, browserWindow, event) {
    snoozeNotificationsClickCallback(menuItem.id)
  }
  function runOnStartup (menuItem, browserWindow, event) {
    if (menuItem.checked === true) {
      notifierAutolauncher.enable()
    } else {
      notifierAutolauncher.disable()
    }
  }
  function balloonToggle (menuItem, browserWindow, event) {
    switch (menuItem.id) {
      case 'warningBallonToggle':
        menuItem.checked === true
          ? settings.set('displayWarningBalloon', true)
          : settings.set('displayWarningBalloon', false)

        break

      case 'errorBallonToggle':
        menuItem.checked === true
          ? settings.set('displayErrorBalloon', true)
          : settings.set('displayErrorBalloon', false)

        break

      default:
        console.error('Error: balloonToggle() called without valid id')
    }
  }

  let contextMenu = Menu.buildFromTemplate([
    {
      label: 'Squelch Error Messages',
      id: 'squelchErrors',
      type: 'checkbox',
      click: squelchErrorsClick
    },
    {
      label: 'Display Warning Balloon',
      id: 'warningBallonToggle',
      type: 'checkbox',
      click: balloonToggle
    },
    {
      label: 'Display Error Balloon',
      id: 'errorBallonToggle',
      type: 'checkbox',
      click: balloonToggle
    },
    {
      label: 'Snooze All Notifications',
      id: 'snoozeNotifications',
      submenu: [
        {
          label: 'None',
          type: 'radio',
          id: '0',
          click: snoozeNotificationsClick
        },
        {
          label: '10 minutes',
          type: 'radio',
          id: '10',
          click: snoozeNotificationsClick
        },
        {
          label: '30 minutes',
          type: 'radio',
          id: '30',
          click: snoozeNotificationsClick
        },
        {
          label: '60 minutes',
          type: 'radio',
          id: '60',
          click: snoozeNotificationsClick
        }
      ]
    },
    {
      label: 'Run On Startup',
      id: 'runOnstartup',
      type: 'checkbox',
      click: runOnStartup
    },
    {
      label: 'Set Request Interval',
      id: 'interval',
      submenu: [
        {
          label: '1 minute',
          type: 'radio',
          id: '1',
          click: changeIntervalClick
        },
        {
          label: '5 minutes',
          type: 'radio',
          id: '5',
          click: changeIntervalClick
        },
        {
          label: '10 minutes',
          type: 'radio',
          id: '10',
          click: changeIntervalClick,
          checked: true
        },
        {
          label: '15 minutes',
          type: 'radio',
          id: '15',
          click: changeIntervalClick
        },
        {
          label: '30 minutes',
          type: 'radio',
          id: '30',
          click: changeIntervalClick
        },
        {
          label: '60 minutes',
          type: 'radio',
          id: '60',
          click: changeIntervalClick
        }
      ]
    },
    {
      label: resources.mainDashboardEnvironment === 'stage'
        ? 'Open Stg In Browser'
        : 'Open Prd In Browser',
      type: 'normal',
      click: resources.mainDashboardEnvironment === 'stage' ? openStgInBrowser : openPrdInBrowser
    },
    {
      label: 'Close',
      type: 'normal',
      click () {
        app.quit()
      }
    }
  ])

  let squelchMenuItem = contextMenu.items.filter(x => x.id === 'squelchErrors')[squelchIndex]
  let squelch = memoryStore.squelch
  squelchMenuItem.checked = squelch

  notifierAutolauncher
    .isEnabled()
    .then(function (isEnabled) {
      if (isEnabled) {
        contextMenu.items[startupIndex].checked = true
      }
    })
    .catch(function (err) {
      if (err) {
        console.error('Error in isEnabled for autolaunch context menu initialization.')
      }
    })

  let warningBalloonMenuItem = contextMenu.items[warningBalloonIndex]
  warningBalloonMenuItem.checked = settings.get('displayWarningBalloon', false)
  let errorBalloonMenuItem = contextMenu.items[errorBalloonIndex]
  errorBalloonMenuItem.checked = settings.get('displayErrorBalloon', true)

  let snoozeInterval = memoryStore.snoozeInterval.toString()
  let snoozeMenuItem = contextMenu.items[snoozeIndex].submenu.items.find(
    x => x.id === snoozeInterval
  )
  snoozeMenuItem.checked = true

  let interval = memoryStore.interval.toString()
  let intervalMenuItem = contextMenu.items[intervalIndex].submenu.items.find(
    x => x.id === interval
  )
  intervalMenuItem.checked = true
  return contextMenu
}

module.exports = { buildMenu }

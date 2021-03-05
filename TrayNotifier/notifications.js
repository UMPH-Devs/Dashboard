const dialog = require('electron').dialog
const platform = require('os').platform()
const fs = require('fs')

const ElectronStore = require('electron-store')
const settings = new ElectronStore()

let memoryStore = require('./inMemoryStore')
let resources = require('./resources')

function displayWarningBalloon (title, message, appIcon) {
  appIcon.setToolTip(title)

  if (
    !memoryStore.squelch &&
    !memoryStore.snooze &&
    platform !== 'darwin' &&
    settings.get('displayWarningBalloon', false)
  ) {
    appIcon.displayBalloon({
      icon: fs.readFileSync(resources.dashboardWarningIconPath),
      title: title,
      content: message
    })
  }
}
function displayErrorBalloon (title, message, appIcon) {
  appIcon.setToolTip(title)

  if (
    !memoryStore.squelch &&
    !memoryStore.snooze &&
    platform !== 'darwin' &&
    settings.get('displayErrorBalloon', true)
  ) {
    appIcon.displayBalloon({
      icon: fs.readFileSync(resources.dashboardErrorIconPath),
      title: title,
      content: message
    })
  }
}
function displayDialog (title, message, icon, lastResponseFlag, type, dialogCallback) {
  if (!memoryStore.squelch && !memoryStore.snooze && !lastResponseFlag) {
    dialog.showMessageBox(
      {
        type: type,
        icon: icon,
        title: title,
        message: message,
        noLink: true,
        buttons: ['Snooze 10', 'Snooze 30', 'Snooze 60', 'OK']
      },
      response => {
        switch (response) {
          case 0:
            memoryStore.snoozeInterval = 10
            memoryStore.snooze = true
            dialogCallback()
            break

          case 1:
            memoryStore.snoozeInterval = 30
            memoryStore.snooze = true
            dialogCallback()
            break

          case 2:
            memoryStore.snoozeInterval = 60
            memoryStore.snooze = true
            dialogCallback()
            break

          case 3:
            // OK was selected. Close Dialog.
            break

          default:
            console.error('Error: bad case in displayDialog() button click.')
        }
      }
    )
  }
}
function displayStatus (statusCode, appIcon, responseCode, dialogCallback) {
  let message
  let title
  switch (statusCode) {
    case 'success':
      memoryStore.lastResponseContainedError = false
      memoryStore.lastResponseContainedWarning = false
      console.log(resources.apiSuccessMessage)

      appIcon.setImage(resources.dashboardSuccessIconUrl)
      appIcon.setToolTip(resources.apiSuccessTitle)
      break

    case 'warning':
      memoryStore.lastResponseContainedError = false
      title = resources.apiWarningTitle
      message = resources.apiWarningMessage
      console.warn(title + ' ' + message)

      appIcon.setToolTip(resources.apiWarningTitle)
      displayWarningBalloon(title, message, appIcon)
      displayDialog(
        title,
        message,
        resources.dashboardWarningIconPath,
        memoryStore.lastResponseContainedWarning,
        'warning',
        dialogCallback
      )

      memoryStore.lastResponseContainedWarning = true
      appIcon.setImage(resources.dashboardWarningIconUrl)
      break

    case 'error':
      memoryStore.lastResponseContainedWarning = false
      title = resources.apiErrorTitle
      message = resources.apiErrorMessage
      console.warn(title + ' ' + message)

      displayErrorBalloon(title, message, appIcon)
      displayDialog(
        title,
        message,
        resources.dashboardErrorIconPath,
        memoryStore.lastResponseContainedError,
        'error',
        dialogCallback
      )

      memoryStore.lastResponseContainedError = true
      appIcon.setImage(resources.dashboardErrorIconUrl)
      break

    case 'requestError':
      memoryStore.lastResponseContainedWarning = false
      title = resources.requestErrorTitle
      message = resources.requestErrorMessage(responseCode)
      console.error(title + ' ' + message)

      displayErrorBalloon(title, message, appIcon)
      displayDialog(
        title,
        message,
        resources.dashboardErrorIconPath,
        memoryStore.lastResponseContainedError,
        'error',
        dialogCallback
      )

      memoryStore.lastResponseContainedError = true
      appIcon.setImage(resources.appErrorIconUrl)
      break

    default:
      memoryStore.lastResponseContainedWarning = false
      title = resources.apiErrorTitle
      message = resources.appErrorMessage(statusCode)
      console.error(title + ' ' + message)

      displayErrorBalloon(title, message, appIcon)
      displayDialog(
        title,
        message,
        resources.dashboardErrorIconPath,
        memoryStore.lastResponseContainedError,
        'error',
        dialogCallback
      )
      memoryStore.lastResponseContainedError = true

      appIcon.setImage(resources.appErrorIconUrl)
      break
  }
}

module.exports = {
  displayStatus
}

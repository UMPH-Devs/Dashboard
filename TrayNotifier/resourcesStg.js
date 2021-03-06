const path = require('path')
const url = require('url')

const mainDashboardEnvironment = 'stage'

const stgDashboardUrl = 'http://stg-dashboard.YourDomain.com/'
const prdDashboardUrl = 'http://dashboard.YourDomain.com/'
const dashboardApiUrl = stgDashboardUrl + 'api/modules/status'

const dashboardSuccessIconPath = path.resolve(__dirname, 'images', 'green_icon.ico')
const dashboardWarningIconPath = path.resolve(__dirname, 'images', 'yellow_icon.ico')
const dashboardErrorIconPath = path.resolve(__dirname, 'images', 'red_icon.ico')
const appLoadingIconPath = path.resolve(__dirname, 'images', 'white_QLT_icon.ico')
const appErrorIconPath = path.resolve(__dirname, 'images', 'fail_R00_icon.ico')

const dashboardSuccessIconUrl = url.format({
  pathname: dashboardSuccessIconPath
})
const dashboardWarningIconUrl = url.format({
  pathname: dashboardWarningIconPath
})
const dashboardErrorIconUrl = url.format({
  pathname: dashboardErrorIconPath
})
const appLoadingIconUrl = url.format({ pathname: appLoadingIconPath })
const appErrorIconUrl = url.format({
  pathname: appErrorIconPath
})

const apiSuccessTitle = 'Stage Dashboard'
const apiSuccessMessage = 'Stage Dashboard Success, all statuses are green.'
const apiWarningTitle = 'Stage Dashboard Warning'
const apiWarningMessage =
  'StageUMPH Dashboard has one or more applications or services in an warning state!'
const apiErrorTitle = 'Stage Dashboard Alert!'
const apiErrorMessage =
  'The Stage Dashboard has one or more applications or services in an error state!'
const appErrorTitle = 'Dashboard Request Error'
const appErrorMessage = statusCode =>
  `Error: The request returned an bad api status code: ${statusCode}`
const requestErrorTitle = 'Http Error!'
const requestErrorMessage = responseCode =>
  `Error: The request to the server returned an unexpected result resulting in a HTTP status code of ${responseCode}`

const shortcutLinkPath = path.format({
  dir: 'C:\\Users\\%USERNAME%\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup',
  base: 'STG UMPH Dashboard Notifier.lnk'
})
const exePath = path.format({
  dir: 'C:\\Users\\%USERNAME%\\AppData\\Local\\umphdashboardtraynotifier',
  base: 'STG Dashboard Notifier.exe'
})

module.exports = {
  stgDashboardUrl,
  prdDashboardUrl,
  dashboardSuccessIconPath,
  dashboardWarningIconPath,
  dashboardErrorIconPath,
  dashboardSuccessIconUrl,
  dashboardWarningIconUrl,
  dashboardErrorIconUrl,
  dashboardApiUrl,
  apiErrorTitle,
  apiErrorMessage,
  appErrorTitle,
  appErrorMessage,
  apiSuccessMessage,
  apiWarningMessage,
  requestErrorMessage,
  requestErrorTitle,
  appErrorIconPath,
  appLoadingIconPath,
  appErrorIconUrl,
  appLoadingIconUrl,
  apiWarningTitle,
  apiSuccessTitle,
  shortcutLinkPath,
  exePath,
  mainDashboardEnvironment
}

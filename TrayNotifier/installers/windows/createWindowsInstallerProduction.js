const createWindowsInstaller = require('electron-winstaller').createWindowsInstaller
const path = require('path')
const url = require('url')

getInstallerConfig().then(createWindowsInstaller).catch(error => {
  console.error(error.message || error)
  process.exit(1)
})

function getInstallerConfig () {
  console.log('creating windows installer')
  const rootPath = path.join('./')
  console.log('rootPath: ' + rootPath)
  const outPath = path.join(rootPath, 'release-builds', 'production')
  console.log('outPath: ' + outPath)
  var iconPath = path.join(rootPath, 'images', 'green_inv_icon.ico')
  console.log('iconPath: ' + iconPath)

  const iconUrl = url.format({
    pathname: path.join('file:///', __dirname, '/../../', iconPath)
  })
  console.log('iconUrl: ' + iconUrl)

  return Promise.resolve({
    appDirectory: path.join(outPath, '/Dashboard Notifier-win32-ia32/'),
    authors: 'Lee Jones',
    noMsi: true,
    outputDirectory: path.join(outPath, 'windows-installer-production'),
    title: 'Dashboard Notifier PRD',
    name: 'umphdashboardtraynotifierprd',
    exe: 'Dashboard Notifier PRD.exe',
    setupExe: 'PRD Dashboard Notifier Installer.exe',
    setupIcon: iconPath,
    iconUrl: iconUrl
  })
}

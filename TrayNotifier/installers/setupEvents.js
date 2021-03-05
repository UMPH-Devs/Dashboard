const electron = require('electron')
const path = require('path')
const app = electron.app

module.exports = {
  handleSquirrelEvent: function () {
    if (process.argv.length === 1) {
      return false
    }

    const ChildProcess = require('child_process')

    const appFolder = path.resolve(process.execPath, '..')
    const rootAtomFolder = path.resolve(appFolder, '..')
    const updateDotExe = path.resolve(path.join(rootAtomFolder, 'Update.exe'))
    const exeName = path.basename(process.execPath)
    const spawn = function (command, args) {
      let spawnedProcess

      try {
        spawnedProcess = ChildProcess.spawn(command, args, { detached: true })
      } catch (error) {
        console.error('spawnedProcess Error: ' + error)
      }

      return spawnedProcess
    }

    const spawnUpdate = function (args) {
      return spawn(updateDotExe, args)
    }
    function installorUpdate () {
      spawnUpdate(['--createShortcut', exeName])
      setTimeout(app.quit, 1000)
    }

    const squirrelEvent = process.argv[1]
    switch (squirrelEvent) {
      case '--squirrel-install':
        installorUpdate()
        return true

      case '--squirrel-updated':
        installorUpdate()
        return true

      case '--squirrel-uninstall':
        spawnUpdate(['--removeShortcut', exeName])
        setTimeout(app.quit, 1000)
        return true

      case '--squirrel-obsolete':
        app.quit()
        return true
    }
  }
}

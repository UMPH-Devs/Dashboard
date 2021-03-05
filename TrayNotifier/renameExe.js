const program = require('commander')
const fs = require('fs')

const stgDirectory = './release-builds/stage/Dashboard Notifier-win32-ia32/'
const prdDirectory = './release-builds/production/Dashboard Notifier-win32-ia32/'

program
  .option('-s, --stage', 'stage monitor')
  .option('-p, --production', 'production monitor')
  .parse(process.argv)

if (program.stage) {
  fs.renameSync(
    stgDirectory + 'Dashboard Notifier.exe',
    stgDirectory + 'Dashboard Notifier STG.exe',
    error => {
      if (error) console.error('error: ' + error)
      else console.log('renamed to: Dashboard Notifier STG.exe')
    }
  )
} else if (program.production) {
  fs.renameSync(
    prdDirectory + 'Dashboard Notifier.exe',
    prdDirectory + 'Dashboard Notifier PRD.exe',
    error => {
      if (error) console.error('error: ' + error)
      else console.log('renamed to: Dashboard Notifier PRD.exe')
    }
  )
} else {
  console.error('must be run with -s or -p')
}

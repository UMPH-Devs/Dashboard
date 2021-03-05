const program = require('commander')
const fs = require('fs')
const path = require('path')
const archiver = require('archiver')

const stgInstaller = 'STG UMPH Dashboard Notifier Installer.exe'
const prdInstaller = 'PRD UMPH Dashboard Notifier Installer.exe'

program
  .option('-a, --all', 'stage and productions zipped')
  .option('-s, --stage', 'stage zipped')
  .option('-p, --production', 'production zipped')
  .parse(process.argv)

var output = fs.createWriteStream(path.join(__dirname, 'release-builds', 'DashInstaller.zip'))
var archive = archiver('zip', {
  zlib: { level: 9 }
})
output.on('close', function () {
  console.log(archive.pointer() + ' total bytes')
  console.log('archiver has been finalized and the output file descriptor has closed.')
})

archive.on('warnign', function (err) {
  if (err.code === 'ENOENT') {
    console.log('Erorr: ' + err.code)
  } else {
    throw err
  }
})

archive.on('error', function (err) {
  throw err
})

archive.pipe(output)

let file1 = path.join(
  __dirname,
  'release-builds',
  'stage',
  'windows-installer-stage',
  stgInstaller
)
let file2 = path.join(
  __dirname,
  'release-builds',
  'production',
  'windows-installer-production',
  prdInstaller
)

if (program.all || program.stage) {
  archive.append(fs.createReadStream(file1), { name: stgInstaller })
}
if (program.all || program.production) {
  archive.append(fs.createReadStream(file2), { name: prdInstaller })
}

archive.finalize()

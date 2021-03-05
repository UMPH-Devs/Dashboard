const program = require('commander')
const fs = require('fs')

const resourcesBase = './resources.js'
const resourcesStg = './resourcesStg.js'
const resourcesPrd = './resourcesPrd.js'

program
  .option('-s, --stage', 'stage monitor')
  .option('-p, --production', 'production monitor')
  .parse(process.argv)

if (program.stage) doStageCopy()
else if (program.production) doProdCopy()
else {
  console.error('must be run with -s or -p')
}

function doStageCopy () {
  let content = fs.readFileSync(resourcesStg)

  fs.writeFileSync(resourcesBase, content, () => {
    console.log('Copied stage resources')
  })
}

function doProdCopy () {
  let content = fs.readFileSync(resourcesPrd)

  fs.writeFileSync(resourcesBase, content, () => {
    console.log('Copied production resources')
  })
}

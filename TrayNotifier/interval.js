function resetInterval (currentInterval, updateTimeInSeconds, callback) {
  clearInterval(currentInterval)
  return setInterval(callback, updateTimeInSeconds)
}

function newInterval (updateTimeInSeconds, callback) {
  return setInterval(callback, updateTimeInSeconds)
}

module.exports = {
  newInterval,
  resetInterval
}

(function () {
	utils = {};

	//// Moment/Date Stuff
	utils.getDateStatus = function (module) {
        if (moment(module.CreateDate).add(module.MinutesUntilError, 'minutes').isBefore(moment())) { return "panel-danger" }
        if (moment(module.CreateDate).add(module.MinutesUntilWarning, 'minutes').isBefore(moment())) { return "panel-warning" }
        return "panel-success";
	}

	utils.timeSince = function (date) {
		return moment(date).fromNow();
	}

	utils.getPrettyDate = function (time) {
		return moment(time).format("MM/DD/YYYY: hh:mm a")
	}

	/// Module Functions

	utils.isWarning = function (module) {
		return module.ItemStatus === "warning";
	}

	utils.isError = function (module) {
		return module.ItemStatus === "error"
	}

	utils.isRed = function (module) {
		return !module.IsForcedSuccess && ( utils.getDateStatus(module) === "panel-danger" || R.any(utils.isError, module.StatusItems) );
	}

	utils.isYellow = function (module) {
		return !module.IsForcedSuccess && !utils.isRed(module) && (utils.getDateStatus(module) === "panel-warning" || R.any(utils.isWarning, module.StatusItems));
	}

	utils.isGreen = function (module) {
		return module.IsForcedSuccess || ( !utils.isRed(module) && !utils.isYellow(module) );
	}

	utils.moduleInProgress = function (module) {
		return R.any(function (i) { return i.InProgress }, module.StatusItems) || module.IsInProgress;
	}


	/// StatusItem Functions

	utils.isItemRed = function (item) {
		return item.ItemStatus === "error";
	}

	utils.isItemYellow = function (item) {
		return item.ItemStatus === "warning";
	}



	///Implement Plugin into Vue globally.
	DashUtils = {};

	DashUtils.install = function (Vue, options) {
		Vue.prototype.$utils = utils;
	}

	Vue.use(DashUtils)
})();


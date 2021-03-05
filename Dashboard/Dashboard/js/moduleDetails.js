var appId = document.getElementById("appId").value;

var app = new Vue({
    el: "#app",

    data: {
        appId: appId,
		module: {},
		history: [],
		isModalVisible: false,
		isUserModalVisible: false,
		user: '',
    },

    created: function () {
		this.onCreate();
	},

	computed:  {
		userName: function () {
			return this.$cookie.get('userName');
		}
	},

	methods: {
		onCreate: function () {
			this.$http.get("/api/modules/" + appId)
				.then(function (response) {
					this.module = response.body;
				});
			this.$http.get("/api/modules/history/" + appId)
				.then(function (res) {
					this.history = res.body;
				});
		},
		getItemStatus: function (item) {
			return {
				"list-group-item-success": item.ItemStatus === "success",
				"list-group-item-warning": item.ItemStatus === "warning",
				"list-group-item-danger": item.ItemStatus === "error",
				"inProgress": item.InProgress
			}
		},
		getTimeClass: function (module) {
			return this.$utils.isRed(module) ? "list-group-item-danger" : "list-group-item-warning";
		},
		moduleStatus: function (module) {
			var status = "list-group-item-" + module.Status;
			return status === "list-group-item-error" ? "list-group-item-danger" : status;
		},
		getStatusLink: function (module) {
			return "/modules/status/" + module.StatusId;
		},
		clickInProgress: function (item) {
			console.log(item);

			if (!this.userName) {
				this.isUserModalVisible = true;
			} else {
				this.$http.post("/api/statusitem/progress", item)
					.then(function (res) {
						console.log(res.body);
						item.InProgress = true;
					});
			}

		},
		untilError: function (module) {
			return moment(this.module.CreateDate).add(this.module.MinutesUntilError, 'minutes');
		},
		untilWarning: function (module) {
			return moment(this.module.CreateDate).add(this.module.MinutesUntilWarning, 'minutes');
		},
		makeModalVisible: function () {
			this.isModalVisible = true;
		},
		hideModal: function () {
			this.isModalVisible = false;
		},
		hideUserModal: function () {
			this.isUserModalVisible = false;
		},
		onForceGreen: function () {
			this.$http.patch('/api/modules/success', this.module).then(function (res) {
				this.onCreate();
				this.isModalVisible = false;
			});
		},
		onModuleInProgress: function () {
			if (!this.userName) {
				this.isUserModalVisible = true;
			} else {
				this.module.InProgressUserid = this.$cookie.get('userName');
				this.sendModuleInProgress();
			}
		},

		onSendUser: function () {
			if (this.user == '') {
				return
			} else {
				this.$cookie.set('userName', this.user);
				this.module.inProgressUserid = this.user;
				this.sendModuleInProgress();
				this.hideUserModal();
			}
		},

		sendModuleInProgress: function () {
			this.$http.patch('/api/modules/progress', this.module).then(function (res) {
				this.onCreate();
			});
		}
	}
})
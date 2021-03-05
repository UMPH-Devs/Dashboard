var _id = document.getElementById("id").value;

var app = new Vue({
	el: "#app",
	data: {
		statusId: _id,
		module: {}
	},

	created: function () {
		this.$http.get("/api/modules/status/" + this.statusId)
			.then(function (res) {
				this.module = res.body;
			});
	},

	methods: {
		prettyDate: function (module) {
			return utils.getPrettyDate(module);
		},
		statusClass: function (statusItem) {
			var status = "list-group-item-" + statusItem.ItemStatus;
			return status === "list-group-item-error" ? "list-group-item-danger" : status;
		}
	}


});
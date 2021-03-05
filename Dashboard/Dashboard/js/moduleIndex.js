var app = new Vue({
    el: "#app",

    data: {
        modules: []
    },

    created: function () {
        this.$http.get("/api/moduletypes")
            .then(function(res) {
                this.modules = res.body;
            });
    },

    methods: {
        getDate: function (date) {
            return moment(date).format("MM/DD/YYYY");
        },
        getEditRoute: function (id) {
            return "/modules/edit/" + id;
		},
		getTokenString: function (bool) {
			return bool === true ? "yes" : "no";
		}
    }
});
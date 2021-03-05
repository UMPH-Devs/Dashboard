var app = new Vue({
    el: "#app",

    data: {
        module: {
            appId: "",
            name: "",
            url: "",
			frequency: "",
			tokenRequired: true
        },
        submitClicked: false
    },

    computed: {
        appIdError: function() {
            return this.clickedAnd(this.isAppIdNull);
        },
        nameError: function() {
            return this.clickedAnd(this.isNameNull);
        },
        urlFrequencyError: function() {
            return this.clickedAnd(!this.formsMatch);
		},
		tokenError: function () {
			//currently tokens should never throw an error
			return false;
		},
        isAppIdNull: function () {
            return this.module.appId === "";
        },
        isNameNull: function () {
            return this.module.name === "";
        },
        isUrlNull: function () {
            return this.module.url === "";
        },
        isFrequencyNull: function () {
            return this.module.frequency === "";
        },
        isRequiredFieldNull: function () {
            return this.isAppIdNull || this.isNameNull;
        },
        isFormInvalid: function () {
            return this.isRequiredFieldNull || !this.formsMatch;
        },
        formsMatch: function() {
            return this.isUrlNull === this.isFrequencyNull;
        }

    },

    methods: {
        onSubmit: function(e) {
            this.submitClicked = true;
            if (!this.isFormInvalid) {
				this.$http.post('/api/moduletypes', this.module).then(function (res) {
					window.location = "/modules";
				}, function (res) {
					console.log(res);
				})
            }
		},
		onSelectChanged: function (e) {
			this.module.tokenRequired = !this.module.tokenRequired
		},
        clickedAnd: function(fn) {
            return this.submitClicked && fn;
        }


    }
});
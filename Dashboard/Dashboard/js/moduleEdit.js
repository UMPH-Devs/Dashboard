var moduleId = document.getElementById("moduleId").value;

var app = new Vue({
    el: "#app",

    data: {
        module: {},
        submitClicked: false,
        isModuleVisible: false,
        alert: {
            visible: false,
            text: "Module Type Updated.",
            status: "Success!"
        }
    },

    created: function () {
        this.$http.get("/api/moduletypes/" + moduleId)
            .then(function (res) {
                console.log(res.body);
                this.module = res.body;
            });
    },

    computed: {
        nameError: function () {
            return this.submitClicked && this.module.Name === "";
        },
        urlFrequencyError: function() {
            return this.submitClicked && !this.formsMatch;
        },
        urlIsNull: function() {
            return this.module.URL === "";
        },
        frequencyIsNull: function() {
            return this.module.Frequency === null || this.module.Frequency === "";
        },
        formsMatch: function() {
            return this.frequencyIsNull === this.urlIsNull;
        },
        isInValid: function() {
            return this.nameError || this.urlFrequencyError;
        }
    },

    methods: {
        onSubmit: function () {
            this.submitClicked = true;
            console.log(this.isInValid);
            if (!this.isInValid) {
                this.$http.put("/api/moduletypes/", this.module)
                    .then(function(res) {
                        this.module = res.body;
                        this.alert.visible = true;
                    }, function(err) {
                        console.log(err);
                    });
            }
        },
        onDelete: function () {
            this.$http.delete("/api/moduletypes/" + moduleId)
                .then(function(res) {
                    window.location = "/modules";
                });
        },
        showModule: function() {
            this.isModuleVisible = true;
        },
        hideModule: function() {
            this.isModuleVisible = false;
        }
    }
});
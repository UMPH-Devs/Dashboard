var app = new Vue({
	el: '#app',

	data: {
		modules: [],
		inactiveModules: [],
		filterText: ""
	},

	computed: {
		inactiveWarningCount: function () {
			let res = R.filter(this.$utils.isYellow, this.inactiveModules)
			return res.length;
		}
	},

	created: function () {
		this.fetchData();
		window.setInterval(this.fetchData, 30000);

	},

	methods: {
		inFilter: function (module) {
			return this.cleanText(module.Name).indexOf(this.cleanText(this.filterText)) !== -1;
		},

		cleanText: function (text) {
			return text !== undefined ? text.trim().toLowerCase() : text;
		},

		onUpdate: function (event) {
			console.log("Object moved from " + event.oldIndex + " to " + event.newIndex);
		},

		getTotalClass: function () {
			return R.cond([
				[R.any(this.$utils.isRed), R.always("panel-danger")],
				[R.any(this.$utils.isYellow), R.always("panel-warning")],
				[R.T, R.always("panel-success")]
			])(this.modules);
        },

        getwikilink: function (module) {
            return "http://umphwiki.umpublishing.org/Dashboard-Wiki/topic/" + module.Name;
        },

		getModuleClass: function (module) {

			return {
				"panel-danger": this.$utils.isRed(module),
				"panel-warning": this.$utils.isYellow(module),
				"panel-succcess": this.$utils.isGreen(module),
				inProgress: this.$utils.moduleInProgress(module)
			}
		},

		getLink: function (id) {
			return "/modules/details/" + id;
		},

		fetchData: function () {
			this.$http.get('/api/modules')
				.then(function (response) {
					this.modules = R.filter(function (x) { return x !== null }, response.body);
					this.sortModules();
					
					var iconstatus = this.getTotalClass();
					if (iconstatus == 'panel-danger') { $('.navbar-brand').removeClass('IconYellow').addClass('IconRed'); }
				    else if (iconstatus == 'panel-warning') { $('.navbar-brand').removeClass('IconRed').addClass('IconYellow'); }
				    else { $('.navbar-brand').removeClass('IconRed').removeClass('IconYellow'); }
				});
			
		},

		hasDisplayItems: function (module) {
			return R.any(function (x) { return x.Display }, module.StatusItems);
		},

		sortModules: function () {
			var t = this;
			var warnings = R.filter(this.$utils.isYellow, this.modules);
			var errors = R.filter(this.$utils.isRed, this.modules);
			var success = R.filter(this.$utils.isGreen, this.modules);
			var modules = errors.concat(warnings).concat(success);
			var list = this.$cookie.get('inactive');
			if (list) {
				this.inactiveModules = R.filter(function (x) { return list.indexOf(x.AppId) >= 0 }, modules);
				modules = R.filter(function (x) { return list.indexOf(x.AppId) < 0 }, modules);
			}
			this.modules = R.filter(function (x) { return x.Active || t.$utils.isRed(x) }, modules);
		},

		makeActive: function (module) {
			var mod = this.getModuleFromList(module, this.inactiveModules);
			this.inactiveModules = R.filter(function (x) { return x.AppId !== module.AppId }, this.inactiveModules);
			this.modules.push(mod);
			this.removeFromCookie(module.AppId);
		},

		makeInactive: function (module) {
			var mod = this.getModuleFromList(module, this.modules);
			this.modules = R.filter(function (x) { return x.AppId !== module.AppId }, this.modules);
			this.inactiveModules.push(mod);
			this.addToCookie(module.AppId);

		},

		listClass: function (module) {
			return	this.$utils.isRed(module) ?	"list-group-item-danger" :
					this.$utils.isYellow(module) ? "list-group-item-warning" :
											"list-group-item-success";
		},

		addToCookie: function (appId) {
			var cookie = this.$cookie.get('inactive')
			cookie = cookie ? cookie.split(';') : [];
			cookie.push(appId);
			this.$cookie.set('inactive', cookie.join(';'));
		}, 

		removeFromCookie: function (appId) {
			var cookie = this.$cookie.get('inactive').split(';');
			cookie = R.filter(function (x) { return x !== appId && x !==";" }, cookie);
			this.$cookie.set('inactive', cookie.join(";"));
		},

		getModuleFromList: function (module, list) {
			return R.pipe(
				R.filter(function (x) { return x.AppId === module.AppId }),
				R.head
			)(list);
		}
	}
});
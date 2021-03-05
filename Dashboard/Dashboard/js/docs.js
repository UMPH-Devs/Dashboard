var app = new Vue({
	el: "#app",
	data: {
		txt:
		"# UMPH Dashboard \n" +
		"--- \n" +
		"### Getting Started \n\n" +
		"--- \n" +
		"Before you can send your status you have to add your module. \n " +
		"To do this click on the \"Modules\" link and fill out the form. The form has four fields. \n" +
		"- App Id - required \n" +
		"- Name - required \n" +
		"- Token Required - required \n" +
		"- URL - Unimplemented \n" +
		"- Frequency - Unimplemented \n\n" +
		"#### App Id \n" +
		"This should be a title-case unique identifier. This will be used to access your module to send updates. \n" +
		"#### Name \n" +
		"This is the human readable friendly name, which will be displayed throughout the app. \n" +
		"#### Token Required \n" +
		"This signifies wether or not your module will require a token to send an update. this option must be set to NO to use the web api." +
		"#### URL \n" +
		"Currently Unimplemented. This will be used if your app exposes an API to get information. \n" +
		"#### Frequency \n" +
		"Currently Unimplemented. This will be used if your app exposes an API to get information. \n\n" +
		"Once your module is created, You will send your status' with the DashInterface DLL. \n\n" +
		"### DLL Usage \n" +
		"--- \n" +
		//"To update your module send a JSON post request to \"https://dashboard.umpublishing.org/api/modules\" with your JSON payload. \n\n" +
		//"Your request should have the following items in it: \n" +
		"Once you have added the DLL to your project create a ModuleStatus object with the following attributes. /n" + 
		"- AppId \n" +
		"- StatusLine \n" +
		"- CustomHtml \n" +
		"- MinutesUntilError \n" +
		"- MitutesUntilWarning \n" +
		"- CreateDate \n" + 
		"- StatusItems \n\n" +
		"#### AppId \n" +
		"**Required** - The AppId that you created when you made the module on the site. You can get this from \"https://dashboard.umpublishing.org/modules\"" +
		"#### StatusLine \n" +
		"A short message about overall module health. Or status of its main feature. \n" +
		"#### CustomHtml \n" +
		"Here you can add customized html that will appear it your module's panel. This can be used to show more or more detailed information than the StatusLine. \n" +
		"#### MachineName \n" + 
		"**Required** - The name of the machine that the application is running on. \n" +
		"#### MinutesUntilError \n" + 
		"**Required** - How many minutes until the status will show as an error. \n" +
		"#### MinutesUntilWarning \n" +
		"**Required** - How many minutes until the status will show as a warning. \n" +
		"#### CreateDate \n" +
		"**Required** - When the update was sent. \n" +
		"#### StatusItems \n" + 
		"A list of StatusItems used to track different things about your app. They have their own schema specified as followed: \n" +
		"- AppId \n" +
		"- Name \n" +
		"- Value \n" +
		"- Status \n\n" +
		"#### AppId \n" +
		"**Required** - This is **NOT** the AppId of the module. This is the appId of this StatusItem, for example every time the Automatic Shipment processor added its orders processed, it would be \"OrdersProcessed\" \n" +
		"#### Name \n" +
		"**Required** - Friendly name of this status item. \n" +
		"#### Value \n" +
		"**Required** - The value of whatever status you are sending. ie for the orders processed example it would be the number of orders. \n" +
		"#### Status \n" +
		"**Required** - The status of the item. Use the Enumerable ItemStatus included in the DLL. \n" +
		"##### **Example:** \n" +
		"```csharp \n" +
		"var shipmentsToday = new StatusItem \n" +
        "    { \n" +
		"        AppId = \"AutoShipOrders\", \n" +
		"        Name = \"Orders Today\", \n" +
		"        Value = \"20\", \n" +
		"        Status = ItemStatus.Success \n" +
		"    }; \n" +
		"var errorShipments = new StatusItem \n" +
		"	{ \n" +
		"		AppId = \"AutoShipFailures\", \n" +
		"		Name = \"Failed Orders\", \n" +
		"		Value = \"1\", \n" +
		"		Status = ItemStatus.Warning \n" +

		"}; \n" +
		"var status = new ModuleStatus() \n" +
		"{ \n" +
		"	AppId = \"AutoShip\", \n" +
		"	MinutesUntilError = 1000, \n" +
		"	MinutesUntilWarning = 500, \n" +
		"	StatusLine = \"20 Shipments successful\", \n" +
		"	CustomHtml = \"<ul><li>Orders Today: 20</li><li>Failed Orders: 0</li></ul>\", \n" +
		"	StatusItems = new List < StatusItem > { shipmentsToday, errorShipments } \n" +
		"}; \n" +
		"``` \n\n" +
		"#### Sending the object \n" +
		"Once you have created your objects, you can send them to the API using the Messenger.SendStatus method on the DLL. \n" +
		"This is an asyn method so you will need to await it. The method takes the target environment, as an Enum, and the object you created earlier, and returns a task of " +
		"the same object after being added to the database.  I write a helper function th wrap the async all, so that i can bind its result inline. \n" +
		"```csharp \n" +
		"public static async Task<ModuleStatus> pushStatus(ModuleStatus status) { \n" +
		"	var msg = await Messenger.SendStatus(Target.Prd, status); \n" +
		"	return msg; \n" +
		"}\n" +
		"``` \n" +
		"You can then bind the result to a variable like so: \n" +
		"`var res = pushStatus(status).Result;`"
	},
	computed: {
		compiled: function() {
			return marked(this.txt);
		}
	}
})
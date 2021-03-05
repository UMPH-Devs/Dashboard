# Installation

1. Download and unzip the packaged installers.
2. Unzip and run the exe files.
3. Your programs are now installed and should be running. You may delete the installer exe files.
4. You may find a link in the start menu and desktop to relaunch it if it is closed.
- Note: You can double click and icon to open the dashboard in your browser.
- Note: You can right click to use a context menu to silence errors, set the interval the app checks the dashboard, run on startup, or close the application
- Recommended: drag tray icon down by the date/time in your windows tray to always display it.
- Recommended: Right click and check `Run On Startup` to run the program on system startup.

# Usage and Features

* Single click to refresh immediately.
* Double click to open dashboard: Open the dashboard in your default internet browser.
* Right click for context menu:  
        1. Squelch Error Messages: Silence error notifications. The icon will still change colors but you wont get popup messages.  
        2. Run On Startup: Toggle whether or not the application runs at system startup.  
        3. Set Request Interval: Set how often you would like the application to check the api for statuses.  
        4. Open in Browser: Open the dashboard in your default internet browser.  
        5. Close: Close the application.  

# Development 

## Installation for Development:

### Install node and yarn
* https://nodejs.org/en/download/
* https://yarnpkg.com/en/docs/install

### install node packages
1. Navigate to your UmphDashboardTrayNotifier directory and run the below commands:  
        1. `yarn global add eslint`  
        2. `eslint --init` (choose the `standard` style)
        3. `yarn`  
2. (Optional) In Visual Studio Code you can install `ESLint` and `Prettier - ESLint` for linting and auto formatting
3. (Optional) In Visual Studio Code you can install  `Markdown Preview Enhanced` to previewing markdown files easier.

## Distribution:

##### Create Installer
[tutorial](https://www.christianengvall.se/electron-windows-installer/)

1. Navigate to the project directory
2. Run the appropriate build command:
        * For Stage, run the following command: `npm run distribute-stage`
        * For Production, run the following command: `npm run distribute-production`
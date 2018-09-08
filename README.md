# QV-ABU

The excercise platform for the ABU final test of the Berufsbildung Luzern.

## Setup

The setup instructions will refer to every role seperately. It's possible to assign all roles to one single computer.

### Database

On the DB server:

* Set up a MS SQL database
* Run "Create-Db.sql" of the given release

On any given win-x64 computer:

* Set the environment variable "QvAbuConnection" to the connection string to the previously set up database
* Install .NET Core 2.1 runtime
* Unzip QvAbu.Cli*.zip from the given release to any directory
* Create the database definition files (.CSV) as described when running `QvAbu.Cli.exe /?`
* Drag-and-drop the database definition files per questionnaire onto `QvAbu.Cli.exe` to apply them to the database specified earlier
  * Note: Ech time the CLI is executed represents one questionnaire. It is possible to drag multiple files of the same question type in the same execution. It is also possible to create questionnaires that have the same name, but different tag, to create questionnaire variants for 3 and 4 year long apprenticeships. It is not possible to delete them via the CLI, however. For that, the database has to be deleted and the process has to be started over (or you can delete all wrong SQL entries manually).
  * Repeat this step as many times as needed

### API

On the API server:

* Set the environment variable "QvAbuConnection" to the connection string to the previously set up database
* Install the contents of QvAbu.Api*.zip from the given release in IIS

### Website

On the webserver:

* Unzip QvAbu.Web*.zip from the given release to any directory
* Set the URL in "assets/api-url-base.json" to the URL of the previously set up API server
* Install the website in any given webserver
* Add a URL redirect: Instead of returning HTTP 404, index.html should be retuned. [More info](https://angular.io/guide/deployment#routed-apps-must-fallback-to-indexhtml)

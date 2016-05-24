Steps to prepare the source code to build properly:

Prerequisites : 
-------------------
Installed ASP.NET Framework 4.5 (https://www.microsoft.com/en-us/download/details.aspx?id=30653)
The Lopoca folder contains the Visual Studio 2013 solution file named Lopoca.sln
Start the solution file (or open Visual Studio 2013 and browse to sln file).
-------------------

Description:
---------------------
The solution file contains follow folders:
0. SQL                                  contains MS SQL database and db user creation scripts 
1. Web        Lopoca.Web           	the web application
2. Data       Lopoca.Data        	the library project contains DataBase Repository and Entities
3. Core       Lopoca.Core        	the library project contains business logic.

------------------------

Actions:
--------------------
Build the solution. The solution uses Nuget to respore packages from packages config files.
Set the Lopoca.Web project as start up project
--------------------

Steps to create and initialize the database:

Prerequisites : 
-------------------
You could see this link bellow before you start to initialize the database and the web application.
http://www.asp.net/mvc/overview/deployment/visual-studio-web-deployment/deploying-to-iis
-------------------
Install Database and the user:
The solution contains two sql script files:
1. CreateLopocaDB.sql
2. CreateLopocaAppPoolUser.sql
Please run the first script number 1.CreateLopocaDB.sql and then 2.CreateLopocaAppPoolUser.sql




# Netsparker
Downtime Alerter project

Downtime Alerter is a dashboard to montior target application's health.

I developed this project in .Net Core 3.1 framework and MVC architecture.

For authentication i used Asp.Net Identity library. Thus, i easily handled login, register and logout processes for multiple users.

I added Entity Framework as an ORM. Migrated tools and sql server packages. I prefer code first to integrate class and database models

For logging i used NLog to store all exceptions in given file directory in a text format. 

I created an Error controller to handle every type errors and write to log text file

I declared a model named as TargetApplication. I defined all CRUD processes for this model and enable to modify name, url and interval.

I used HangFire Reccuring jobs to send GET request to a given Url at given interval.

Hangfire Reccuring jobs work with Cron expression interval. So UI expecting Cron expression as an interval.

For notification i supported multiple notification types. For example if sms and email options are declared in Startup.cs, the application
will send notification for these two channel. I developed my architecture in this way.

For sending mail i used SendGrid library. 

If you have any question please let me now.

Thank you!

Configuration
--------------
The repository clone link given below;https://github.com/rehakasuto/Netsparker.git  
Please open visual studio and click Clone a repository to clone your project to proper directory
 

Then set your connection string from appsettings.json 
 

Please run "update-database" command to proceed all migration folders.  

Please check your new database from SQL Server Management studio 

TargetApplication should be exists among Identity tables. 

After complete package manager processes run the project 
After project loaded successfully check your database again to prove Hangfire tables occurred or did not occur  

In application you need to register a new user. Without this user you can not access pages except Hangfire reports 


Kullanıcızı oluşturun daha sonra önünüze düşen ekranda activate user linkine tıklayıp kullanıcıyı aktive edin. Aktive edilmemiş kullanıcı ile giriş yapamazsınız. Bu AspNet Identity nin kendi tasarımından gelmektedir. This is an AspNet Identity feature. 
 


After login you need to see Home Page.  
 

If you want to change the directory of error logs, you need to configure nlog.config file.
 


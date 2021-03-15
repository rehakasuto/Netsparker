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
Hangfire Reccuring jobs work with Cron expression interval. So expecting Cron expression in interval area.
For notification i supported multiple notification types. For example if sms and email options are declared in Startup.cs, the application
will send notification for these two channel. I developed my architecture in this way.
For sending mail i used SendGrid library. 

If you have any question please let me now.
Thank you!

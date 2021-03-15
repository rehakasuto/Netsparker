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
 ![image](https://user-images.githubusercontent.com/22036236/111178006-cecf4200-85bb-11eb-8739-d4123484ffc2.png)
 
Please open visual studio and click Clone a repository to clone your project to proper directory
![image](https://user-images.githubusercontent.com/22036236/111178091-de4e8b00-85bb-11eb-838a-897b38f7528a.png)

Then set your connection string from appsettings.json 
 ![image](https://user-images.githubusercontent.com/22036236/111178126-e6a6c600-85bb-11eb-9d95-4d61459cd4c7.png)

Please run "update-database" command to proceed all migration folders.  
![image](https://user-images.githubusercontent.com/22036236/111178149-ec041080-85bb-11eb-90d0-8ae26f6cb5ad.png)

Please check your new database from SQL Server Management studio 
![image](https://user-images.githubusercontent.com/22036236/111178182-f3c3b500-85bb-11eb-9edd-c1c3db5977cb.png)

TargetApplication should be exists among Identity tables. 
![image](https://user-images.githubusercontent.com/22036236/111178232-fc1bf000-85bb-11eb-9d50-99f30cd8e4f0.png)

After complete package manager processes run the project 
![image](https://user-images.githubusercontent.com/22036236/111178260-02aa6780-85bc-11eb-8f7b-e80e042d2d93.png)

After project loaded successfully check your database again to prove Hangfire tables occurred or did not occur  
![image](https://user-images.githubusercontent.com/22036236/111178279-08a04880-85bc-11eb-9953-f11cc4801566.png)


In application you need to register a new user. Without this user you can not access pages except Hangfire reports 
![image](https://user-images.githubusercontent.com/22036236/111178298-0ccc6600-85bc-11eb-99e8-897e37ae4883.png)

Create the user then click to activate user. You can not login with deactive users. This is an AspNet Identity feature. 
![image](https://user-images.githubusercontent.com/22036236/111178432-32596f80-85bc-11eb-90a8-ab88701e7366.png)


After login you need to see Home Page.  
 ![image](https://user-images.githubusercontent.com/22036236/111178364-1d7cdc00-85bc-11eb-9dfc-30123818e63d.png)

If you want to change the directory of error logs, you need to configure nlog.config file.
 ![image](https://user-images.githubusercontent.com/22036236/111178486-4309e580-85bc-11eb-9d3d-543101dfee29.png)



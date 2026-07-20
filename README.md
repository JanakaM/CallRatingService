# Call Rating Service

## How to Run the solution

1. If SQLlite is not available please install SQLLite. at the bottom of this page there is a section for instructions for how to install SQLite in local PC.
2. Clone the repository.
3. Once clone the repo update appsetting.json file connectionstring property value as your cloned repo path. Data.db file is located in SolutionItems folder.</br> 
Example  :   "ConnectionString": "Data Source=C:/Janaka/GitHub/CallRatingService/SolutionItems/Data.Db"
4. Initially Data.db is empty database. Once solution run first time Db tables wiil be created. 
5. Set CallRatingServiceApi project as start up project and run/debug the solution.
6. OpenAPi / Scalar link can be used to make APi requests. example link </br>
   https://localhost:7226/scalar/v1#tag/endpoints (change the port number as required)
7. To view prepopulated customers please use this endpoint. provide the   querystring parameter - page. Example <br>
   https://localhost:7226/api/customer?page=1  


## Assumptions

1. I have create Datbase schemas step in the program.cs to simplyfy the solution runing in another machine. Did not include any EF migration steps.  (await dbContext.Database.EnsureCreatedAsync();)

2. I have assume Customers table is having valid customers. (I have set up prepopulate 10 customers for demo perpose) .In real world application will create Add customer endpoint.   

3. Customer rate card update is deleting existing rates and Add rates as per request payload. So new rate card request payload represent the customer rate card.


## If I spend more time on this project Things I would do to Improve the Solution

1. I Will add a Logging framework and add logging in order to support application maintenance aspect.

2. I Will add Authentication / Authorizatio0n capabilities to all Api Endpoints.

3. Currently there are minimum request validations available and Will add more request validation for all endpoints.

4. I will Add Automapper package for object mapping inbetween layers. Currently use raw mapping to keep simple the solution.

5. I will add more unit tests to increase code coverage and cover more scenarios which can be available in real world applications.

6. I will add integration tests which can be run in deployment pipeline or run in local. For this I will configure in memory databse too. 

7. Add exception handling. Add try catch blocks. For example add exception handling where access external resorces. (DB)

8. I will do few rounds of refactoring to improve the code quality. 

9. I will add more folders to seperate diffrent sections within project. Currently try to keep files in flat structure to simplyfy.


### How to Set up SQLite in local PC instruction

1.	Download SQLite for windows from https://www.sqlite.org/download.html
-> Go to precompile binaries and download sqlite-tools-win-x64-3530300.zip
->. Create folder for SQLite c:\sqlite and extract all files into this folder.
2.	 Add this folder path to environment variable (recommended but optional)
3.	Add VS extension “SQL Server Compact Toolbox” to view SQLite Database and tables. 
4.	Once extension added. Find the window “Sqllite/Sql ServerCompact toolbox” in VS. Using this window. Right click on  “Data Connection“ -> Create (create Database.) 	
5.	For solutions which DB already has been created, use the option Add connection using solution.  This will add the DB connection to the Sqlite toolbox. This will enable view and run queries of tables. 



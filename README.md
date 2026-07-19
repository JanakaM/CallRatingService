# Call Rating Service

## How to Run the solution

1. If SQLlite is not available please install SQLLite. There is a section for instructions for installing SQLite in local PC.
2. Clone the repository.
3. Once clone the repo update appsetting.json file connectionstring property value as your cloned repo path. Data.db file is located in SolutionItems folder. 
Example  :   "ConnectionString": "Data Source=C:/Janaka/GitHub/CallRatingService/SolutionItems/Data.Db"
4. Initially Data.db is empty database. Once solution run Db schemas wiil be creted. 
5. OpenAPi / Scalar link can be used to make APi requests. example link
   https://localhost:7226/scalar/v1#tag/endpoints (change the port number as required)
6. To view prepopulated customers please use this endpoint. set 1 as page querystring parameter.
   https://localhost:7226/api/customer?page=1  


# Assumptions

1. I have create Datbase schemas step in the program.cs to simplyfy the solution runing in another machine. Did not use any migration steps.  (await dbContext.Database.EnsureCreatedAsync();)

2. I have assume Customers table is having valid customers. (I have set up prepopulate 10 customers for demo perpose) .In real world application will create Add customer endpoint.   


## If I spend more time on this project Things I would do to Improve the Solution

1. I Will add Logging frame work and add logging to support application maintenance aspect

2. I Will add Authentication / Authorizatio0n capabilities to the Api Endpoints

3. Currently there is minimum request validations available Will add request validation.

4. I will Add Automapper package for object mapping inbetween layers. Currently use raw mapping to keep simple the solution.

5. I will add unit tests increase code coverage and cover other scenarios in real world application.

6. I will add integration tests which can be run deployment pipeline or run in local. For this will configure in memory databse. 




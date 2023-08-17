# GiveFreely

1. The code was written on clean architecture with layers
2. Libaries:
    EF Core
    Framework 6
    FluentValidation
    Xunit
    Moc
    Swagger
4. DB
    SQL Server 2022 Developer Edition
    Entities
       Customeres
       Commisions
       Affiliates
6. Step to run
   a. Create a DB on SQL Server 2022
   b. Create a user DB as db_owner
   c. Reemplace the parameters on appsettings.json file
      "DefaultConnectionString": "Data Source=DBInstance;Database=DBName;User ID=DBUser;Password=DBPassword"
   d. Open the project on Visual Studio 2022 and open NuGer package Manager and run this command
       Update-DataBase
   e. Now you are going to have 3 tables with name of entities
   f. Commision table you are going to have 4 rows to determinate the commision to affilite depends the count of customer has

     IdCommusion	FromCount	ToCount	  Money
        1	          1	      100	      10.00
        2	          100	    500	      15.00
        3	          500	    1000	    20.00
        4	          1000	  NULL	    25.00

     For the first 99 customers the company if going to pay 10 dollars by Customer
     For next 400 customers the pay is 15 dollars by Customer
     Next 500 Customer the pay is 20 dollares by Customer
     And if the affiliate will can get more customer is going to be 25 by Customer

   8. From the VS 2022 you need to select Api Project as StartProject and run
   9. You can see Swagger http://localhost:65492/swagger/index.html
   10. The project has 3 controllers
        Customer
           Add (name and IdAffiliate are required)
           Get
           GetById
       Affiliate
           Add (name is required)
           Get
           GetById
       Report
           Generate

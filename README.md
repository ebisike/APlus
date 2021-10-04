# APlus
Inventory Manager

APlus inventory manager is an ASP.NET Core web api based project used in listing, creating, updating and deleteing item from inventory.
The API endpoint are all open without any form of authentication, EXCEPT the DELETE endpoint. This autheticates the user that is trying to delete an item, before the delete operation is carried out.

The Project was designed using the clean architecture model with separation of concerns. The different layers include:
1. Domain Layer
2. Data Layer
3. DataAccess Layer
4. DTO layer
5. Application Layer
6. Dependencies Layer
7. API Layer
8. Unit Test

TECHNOLOGIES USED
1. .NET 5
2. ENTITY FRAMEWORK
3. xUnit Test
4. C#

Code first appraoch was used in maintaining the database models and schemas. All migrations are applied when the project starts running.
Also the codebase is self-documented so minimal code comments were used; instead I tried to use SELF EXPLANATORY identifiers for the models, classes, methods and variables.

Challenges Faced:
The unit test fails.

HOW TO RUN:
1. Open the solution in Visual Studio
2. Set the Startup project to: APlus.API project
3. Press F5 on keybaord or click the RUN button on the Debug Tab

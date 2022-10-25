Landmark Remark Application
---------------------------
An application for allowing users to store and share their favourite locations.

Tech Stack:
- C# and Web API Core API
- SQL Server and Entity Framework Core DB
- React front-end (using functions)
- JWT Authentication

Test accounts:
--------------
- anthony.albanese@example.com / anthonyalbanese
- richard.marles@example.com / richardmarles
- jim.chalmers@example.com / jimchalmers
- penny.wong@example.com / pennywong
- mark.butler@example.com / markbulter

Tickets / user stories in this project (what user stories would be created in Jira):
------------------------------------------------------------------------------------
- LND-1: As a user, I would like to be able to login to the application.
- LND-2: As a user (of the application) I can see my current location on a map.
- LND-3: As a user I can save a short note at my current location.
- LND-4: As a user I can see notes that I have saved at the location they were saved on the map.
- LND-5: As a user I can see the location, text, and user-name of notes other users have saved.
- LND-6: As a user I have the ability to search for a note based on contained text or user-name.

How to run this project:
------------------------
1. Ensure SQL Server (Express is fine) is installed.
2. Run the update-database command in Package Manager Console with LandmarkRemark.API set as the startup project and LandmarkRemark.Entities as the default project to set up the database.
3. Set the LandmarkRemark.API and LandmarkRemark.UI projects as the startup projects.

Architectural Design:
---------------------
I've chosen to use a 3 tier architecture with an API layer (LandmarkRemark.API), business logic layer (LandmarkRemark.Services) and repository layer (LandmarkRemark.Repository). Dependencies are injected using the native DI functionality in .NET Core. NUnit is used for unit testing. Moq is used for mocking.

I've chosen to use this architecture to provide loose coupling and allow easy unit testing of different layers of functionality in isolation.

I'm using the Google Maps API for showing the map and the location's a user has saved.

Things I'd do if I had more time / if this was a commercial project:
--------------------------------------------------------------------
- I'd add support for editing and deleting landmarks.
- I'd add support for user registration.
- I'd add two factor authentication.
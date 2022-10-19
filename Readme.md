Landmark Remark Application
---------------------------
An application for allowing users to store and share their favourite locations.

Tech Stack:
- C# and Web API Core API
- SQL Server and Entity Framework Core DB
- React front-end

Tickets / user stories in this project (what user stories would be created in Jira):
----------------
- LND-1: As a user, I would like to be able to login to the application.
- LND-2: As a user (of the application) I can see my current location on a map.
- LND-3: As a user I can save a short note at my current location.
- LND-4: As a user I can see notes that I have saved at the location they were saved on the map.
- LND-5: As a user I can see the location, text, and user-name of notes other users have saved.
- LND-6: As a user I have the ability to search for a note based on contained text or user-name.

Design approaches for unfinished stories:
-----------------

LND-1: 
------
I would use OAuth tokens to store authentication credentials.

The token will be stored in memory in the React application and sent to all APIs. 

The user ID in this token will be stored against the user ID field on the landmarks table.

LND-4:
------
I will use the Google React Marker component to display the notes on the map.

LND-5:
------
I will use EF Core with a search similar to LND-4, except without a supplied user ID. These will be displayed as per LND-4.

LND-6:
------
I will use SQL Server Full Text Search on the notes field of the Landmarks table. 

I will use a standard equals search on the first name and last name of the Persons table.
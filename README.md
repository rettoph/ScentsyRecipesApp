

## Setup
Create a Microsoft SQL Server & import the [`recipe.sql`](https://github.com/rettoph/ScentsyRecipesApp/blob/master/recipe.sql) file. This will create the required tables and a single example recipe (I highly recommend this recipe as it is delicious).

To setup the web server's Microsoft SQL Server connection just edit the [`src/ScentsyRecipesApp.Server/Properties/launchSettings.json`](https://github.com/rettoph/ScentsyRecipesApp/blob/master/src/ScentsyRecipesApp.Server/Properties/launchSettings.json) file and add a `DATABASE_CONNECTION_STRING` environment variable to your profile. There is an example included with this project in the `IIS Express` profile.

Note, due to some design choices the connection string **must** contain [`MultipleActiveResultSets=true;`](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/enabling-multiple-active-result-sets) as seen in the example. If this is not included the server will not function as expected.
 
A live demo of this application can be viewed here: [http://scentsy.rettoph.io/](http://scentsy.rettoph.io/) (I'll probably keep this up for a week or two, after that this'll be a dead link).

 ## Info
You might notice there are 2 projects included within this solution. [`ScentsyRecipesApp.Server`](https://github.com/rettoph/ScentsyRecipesApp/tree/master/src/ScentsyRecipesApp.Server) and [`ScentsyRecipesApp.Library`](https://github.com/rettoph/ScentsyRecipesApp/tree/master/src/ScentsyRecipesApp.Library). Here is a short overview of the purpose behind each project.

[**`ScentsyRecipesApp.Server`**](https://github.com/rettoph/ScentsyRecipesApp/tree/master/src/ScentsyRecipesApp.Server)
This is the primary web server, and should be executed when viewing this code. It contains all primary Models, Views, & Controllers.

[**`ScentsyRecipesApp.Library`**](https://github.com/rettoph/ScentsyRecipesApp/tree/master/src/ScentsyRecipesApp.Library)
As EF Core was specifically disallowed, I decided to create my  own data binding method. All the base code for that resides here. Ultimately, this library defines all of the real database interactions. Here is a basic overview of what can be found within this library.
| Class | Description |
|---|---|
|[`ModelContext<TModel>`](https://github.com/rettoph/ScentsyRecipesApp/blob/master/src/ScentsyRecipesApp.Library/ModelContext.cs)| Provides some basic model binding in an effort to emulate EF Core. While its not nearly as functional, this app doesnt require any crazy actions so its fine. Essentially, this is where Selects, Inserts, & Updates are preformed, all by passing a TModel instance (an implementation of `BaseModel`) to the previously mentioned methods. |
| [`BaseModel`](https://github.com/rettoph/ScentsyRecipesApp/blob/master/src/ScentsyRecipesApp.Library/BaseModel.cs) | A simple scaffold containing default implementation & required method used by all custom models within the current WebApp. Most interfacing is done within the `ModelContext<TModel> `class. |


## Objective
Build a simple web application which lets users create edit and delete recipes and related data. The app must be a C# MVC Dotnet Core 3.1 application built in Visual Studio 2019. **Do not** use Entity Framework. The data access portion of the app should be built with [ADO.NET](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ado-net-overview). 

When completed, close Visual Studio, zip the entire solution directory and email to your Scentsy recruiter. The interview team will test your project by running the SQL scripts to set up the database and then running the app from visual studio.

Fulfill the following user stories using whichever library or APIs you need:

-   **User Story:** I can create recipes that have names and ingredients.
-   **User Story:** I can see an index view where the names of all the recipes are visible.
-   **User Story:** I can click into any of those recipes to view details about the recipe.
-   **User Story:** I can edit the recipes’ information and ingredients.
-   **User Story:** I can delete the recipes.

## WishList
If I had more time to work on this there are some things I would choose to include, such as:
- User logins
	- Authenticated Recipe editing/deleting
	- Public Recipe viewing
- Media Files
	- Images for Recipes
	- Optional images for RecipeSteps
- Remove the need for [`MultipleActiveResultSets=true;`](https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/enabling-multiple-active-result-sets) within the connection string

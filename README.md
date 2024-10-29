**Beer Inventory API**

**Overview**

The Beer Inventory API is a .NET Web API designed to manage a collection of beers, breweries, bars, and the relationships between them. This API provides endpoints to perform CRUD (Create, Read, Update, Delete) operations on beers, breweries, bars, and the associations between them.

**Features**

Manage Beers: Add, update, and retrieve beers with optional filtering by alcohol content.

Manage Breweries: Add and update breweries, as well as retrieve details about specific breweries. 

Manage Bars: Add and update bars, and retrieve information about bars including their associated beers. 

Manage Relationships: Link beers to breweries and bars, allowing for flexible queries to get all beers from a specific brewery or all beers available at a particular bar.

**Technologies Used** .NET 8 ASP.NET Core Web API Entity Framework Core SQL Server

API Endpoints 

**Beers**

GET /beer/{id}: Get a beer by ID.

GET /beer: Get all beers with optional filtering by alcohol content.

POST /beer: Add a new beer.

PUT /beer/{id}: Update a beer by ID.

**Breweries**

GET /brewery/{id}: Get a brewery by ID.

GET /brewery: Get all breweries.

POST /brewery: Add a new brewery.

PUT /brewery/{id}: Update a brewery by ID.

**Bars**

GET /bar/{id}: Get a bar by ID.

GET /bar: Get all bars.

POST /bar: Add a new bar.

PUT /bar/{id}: Update a bar by ID.

**Brewery Beer Relationships**

POST /brewery/beer: Link a beer to a brewery.

GET /brewery/{breweryId}/beer: Get a brewery with its associated beers.

GET /brewery/beer: Get all breweries with their associated beers.

**Bar Beer Relationships**

POST /bar/beer: Link a beer to a bar.

GET /bar/{barId}/beer: Get a bar with its associated beers.

GET /bar/beer: Get all bars with their associated beers.

Installation Prerequisites .NET SDK installed on your machine.

A compatible database ( SQL Server).

**Clone the Repository**

git clone https://github.com/yourusername/BeerInventoryAPI.git

cd BeerInventoryAPI

**Set Up the Database**

Update the connection string in appsettings.json to point to your database.

Run the following commands to create the database and apply migrations:

_dotnet ef migrations add InitialCreate

dotnet ef database update_

**Run the Application**

dotnet run

The API will be available at https://localhost:5001 .

**Testing**

The project includes unit tests for various services to ensure functionality. To run tests, use the following command:

dotnet test

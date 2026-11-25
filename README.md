### InventoryTracker



InventoryTracker is a full-stack application for inventory management, built with C# and .NET 10. It assists with managing inventory, organizing/categorizing items, monitoring stock levels, and searching existing inventory, all from a central dashboard.

The application uses ASP.NET Core, Entity Framework Core, SQLite, and Razor Pages. I also wanted to separate the different main functions of the application into separate projects for the domain, data access, API, and user interface. I built this project to gain hands-on experience developing a complete .NET application. I wanted to be able to incorporate skills such as database design, API development, web interfaces, and writing automated tests.





##### Features



###### Dashboard

Provides a quick overview of:

* Total inventory items
* Active inventory items
* Low-stock items
* Total categories



###### Manage/Organize Inventory

* Create, view, edit, and delete inventory items
* Create, edit, and delete categories

  * Prevent deletion of categories containing inventory items





###### Monitor Low-Stock Items

* Track inventory quantities and reorder levels
* Flag items that are active and are running low on stock



###### Search

* Search inventory items by name or description
* Support added for \* wildcards



###### Validation/Error Handling and Automated Testing

* Validate user input at both the Web and API layers
* Handle API errors
* Automated domain and API integration tests



###### Responsive Web Design

* Built UI around a responsive Razor Pages interface





##### Technology Stack



* C#
* .NET 10
* ASP.NET Core Web API
* ASP.NET Core Razor Pages
* Entity Framework Core
* SQLite
* LINQ
* xUnit
* Microsoft.Testing.Platform
* WebApplicationFactory
* Bootstrap
* Scalar API Reference
* Git and GitHub





##### Architecture



The solution has clear separation between the UI, API, data access, and domain logic, splitting each into its own project. This way, each project has a specific responsibility.



1. The user's browser is used to connect to the InventoryTracker.Web project, which houses the Razor Pages.
2. Once a user triggers an action involving data, it sends an HTTP request to InventoryTracker.Api, which hosts the ASP.NET Core REST API.
3. That API then uses Entity Framework Core through InventoryTracker.Data to read from and write to a SQLite database.



The .Web project communicates with the API through HttpClient instead of accessing the database directly. This keeps database operations and business rules separate from the user interface.





##### Solution Structure



###### InventoryTracker.Api

&#x09;REST API

&#x09;Controllers

&#x09;DTOs

&#x09;Validation



###### InventoryTracker.Console

&#x09;Console application used for early

&#x09;development and persistence testing



###### InventoryTracker.Data

&#x09;EF Core DbContext

&#x09;Entity Configuration

&#x09;Migrations

&#x09;Database Configuration



###### InventoryTracker.Domain

&#x09;Domain Entities and Relationships



###### InventoryTracker.Tests

&#x09;Domain and API Integration Tests



###### InventoryTracker.Web

&#x09;Razor Pages

&#x09;View Models

&#x09;API Communication

###### 



##### API Endpoints



###### Inventory Items

|GET|/api/InventoryItems|
|-|-|
|GET|/api/InventoryItems/{id}|
|POST|/api/InventoryItems|
|PUT|/api/InventoryItems/{id}|
|DELETE|/api/InventoryItems/{id}|
|GET|/api/InventoryItems/low-stock|
|GET|/api/InventoryItems/category/{categoryId}|
|GET|/api/InventoryItems/category/{categoryId}/low-stock|
|GET|/api/InventoryItems/search?term={searchTerm}|



###### Categories

|GET|/api/Categories|
|-|-|
|GET|/api/Categories/{id}|
|POST|/api/Categories|
|PUT|/api/Categories/{id}|
|DELETE|/api/Categories/{id}|



###### Dashboard

|GET|/api/Dashboard|
|-|-|





##### Search



Inventory searches match against item names and descriptions. By default, entering a search term returns items containing the supplied text. For example, the user can simply supply ***usb*** as a search term.



The use of simple wildcard patterns using '**\***' is also supported. For example:

|**SEARCH**|**BEHAVIOR**|
|-|-|
|usb|Contains "usb"|
|usb\*|Starts with "usb"|
|\*usb|Ends with "usb"|
|\*usb\*|Contains "usb"|



The application converts '**\*'** to the SQL '**LIKE**' wildcard '**\&'** when building the search query.





##### Validation



Both the Web application and API validate incoming data. Validation and business rules include:



* Inventory and category names are required
* Text fields have maximum lengths
* Quantities must be non-negative
* Inventory items must belong to a valid category
* Categories containing inventory items cannot be deleted



The Web interface provides feedback through error messages when users enter invalid information. The API performs its own validation to protect the database, including when requests originate outside the Web application.





##### Testing



I used a combination of domain and API integration tests to verify application behavior.

The integration tests use xUnit, WebApplicationFactory, and temporary SQLite in-memory databases. This allows tests to send HTTP requests to the API and verify responses against an isolated database.

Some of the behaviors covered include:



* New inventory items default to active
* Requests for missing inventory items return **404 Not Found**
* Requests for existing inventory items return the expected DTO
* Invalid category IDs return **400 Bad Request**
* Categories containing inventory items cannot be deleted
* Empty categories are deleted successfully
* Requests for deleted categories return **404 Not Found**
* The low-stock endpoint returns only active items meeting the low-stock criteria



Run the test suite from the solution directory:

**dotnet test**



I have also followed Microsoft's current guidance regarding VSTest-based **dotnet test** command. This allows MTP test projects with .NET 10 SDK to opt-in to the MTP-based **dotnet test** and disallowing running with the VSTest-based implementation. See documentation [here](https://learn.microsoft.com/sl-si/dotnet/core/testing/microsoft-testing-platform-migration-from-v1-to-v2).





##### Running Locally



###### Requirements

* .NET 10 SDK
* Git
* Visual Studio 2026 (or another .NET-compatible IDE)
* Entity Framework Core CLI tools



1. ###### Clone the Repository

&#x09;***git clone https://github.com/RobertMoore2052/InventoryTrackerv2***

&#x09;***cd InventoryTracker***



###### 2\. Restore Dependencies and Build

&#x09;***dotnet restore***

&#x09;***dotnet build***



###### 3\. Set Up the Database

InventoryTracker uses SQLite for database storage and Entity Framework Core migrations to manage the database schema.



Apply the migrations from the solution directory:

&#x09;***dotnet ef database update --project InventoryTracker.Data --startup-project InventoryTracker.Api***



###### 4\. Run the Application

Both the API and Web projects need to be running.



In Visual Studio, configure the solution to use multiple startup projects:

&#x09;1. Right-click the solution and select Properties

&#x09;2. Under Common Properties > Configure Startup Projects, choose the option for 'Multiple startup projects:'

&#x09;3. In the boxes below, you should see each project listed. Choose the following:

&#x09;	a. InventoryTracker.Api | Start | {blank}

&#x09;	b. InventoryTracker.Web | Start | {blank}

&#x09;4. All other option, leave 'Action' set to 'None'.



Start debugging to launch both applications (green 'Start'/'https' button OR F5)



The Web application uses the configured ApiBaseUrl to communicate with the API. Make sure this value matches the address of your running API instance.





##### Screenshots



###### Dashboard

*Dashboard screenshot coming soon.*



###### Inventory List

*Inventory list screenshot coming soon.*



###### Low Stock

*Low-stock inventory screenshot coming soon.*



###### Create and Edit Inventory

*Inventory form screenshot coming soon.*





##### What I Learned



My main goal with this project was to understand how the different parts of a .NET application work together.

Throughout development, I gained practical experience with:



* Designing relational domain models
* Configuring one-to-many relationships in EF Core
* Managing database schemas through migrations
* Writing asynchronous LINQ queries
* Building REST APIs using controllers and DTOs
* Working with HTTP status codes
* Configuring dependency injection
* Handling Razor Pages model binding
* Communicating with an API through HttpClient
* Implementing validation across application layers
* Enforcing business rules
* Writing integration tests with isolated databases
* Debugging issues across the Web, API, and data access layers



Working through an entire application gave me a better understanding of how architectural decisions affect maintainability, testing, and communication between projects.





##### Project Status



The core application is complete, including inventory management, category management, search, low-stock monitoring, dashboard reporting, validation, and automated testing.



Remaining work includes:



* Final user interface improvements
* Adding application screenshots
* Preparing the application for deployment
* Completing portfolio documentation



This project serves as a practical demonstration of my experience building, testing, and organizing a full-stack .NET application.




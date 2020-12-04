# Store Application

## Project Description

An Asp.Net web application representing a fantasy dungeon-crawling store. Allows users to purchase silly adventuring gear such as swords, magic potions, and spellbooks.

Uses the client-server model to provide a web UI and API for accessing store data in an online database.

## Technologies Used

* C#
* .NET Core SDK
* PostgreSQL
* Entity Framework Core 3.1.9
* ASP.NET MVC 
* BootstrapJS
* HTML5
* CSS3
* Serilog - version 2.10.0

## Features

* User can sign up for account or log in to an existing account
* User can select different store locations and view inventory for each location
* User can view product details, including remaining stock
* User can add multiple products to a cart and order them as a group
* User can view order history and organize by date or cost
* Admin can add to store location inventory
* Admin can add new products to store locations
* Admin can view order history for a location and organize by date or cost

## Getting Started
   
To Clone: `git clone https://github.com/201019-UiPath/Siles-LoayzaAndres-Project1.git`

* You will need an **appsettings.json** file in the StoreAPI folder containing a JSON object named **ConnectionStrings**. Include a connection string with your desired database host and credentials with the name **StoreDB**.
* To automatically generate the necessary database tables using EF Core, send the following commands in the terminal while in the StoreDB directory (this requires the .NET Core SDK): 
    * Installing EF commands: `dotnet tool install --global dotnet-ef`
    * Adding a migration: `dotnet ef migrations add initial -c StoreContext --startup-project ../StoreAPI/StoreAPI.csproj`
    * Updating the database: `dotnet ef database update --startup-project ../StoreAPI/StoreAPI.csproj`
* You must add store locations directly to the database. Products may be added directly to the database or from the admin controls on the website.

## Usage

* The StoreAPI must be be running on a server in order for the web client application to access the database.
* To run this project from Microsoft Visual Studio, follow these steps:
    * Open the **StoreApplicationAPI.sln** in VS and select *Start Without Debugging* from the Debug menu. This should start running the StoreAPI from a live server.
    * Open the **StoreWebUI.sln** in VS and select *Start Without Debugging* from the Debug menu. This will start the web client application.
* From the web client application, you can use the web interface to manage user accounts, view inventory, place orders, and so on.

## License

This project uses the following license: [MIT License](LICENSE).


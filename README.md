A simple warehouse product management system built with ASP.NET Core using a RESTful Web API and a minimal MVC frontend. This application allows users to manage products stored in a warehouse, including listing, searching, filtering, adding, updating, and deleting product records.

📌 Features
✅ Product Management (via Web API)
Add new products

Edit existing products

Delete products

List all products

Filter/search by name or category

🖥 MVC Frontend
View all products

View product details

Consume the Web API to display data

🔐 (Optional) Authentication
Basic login system to restrict access to product management features

📦 Product Model
Each product has the following properties:

Property	Description
ID	Unique identifier
Name	Required; name of the product
Description	Optional; product details
Category	Optional; product category
QuantityInStock	Required; must be >= 0
Price	Required; must be > 0
DateAdded	Date the product was added

🔧 Technologies Used
ASP.NET Core Web API (.NET 6 or higher)

ASP.NET Core MVC

Entity Framework Core (Code First)

SQL Server

Dependency Injection

AutoMapper (optional for DTO mapping)

Authentication (optional with Identity or cookie-based auth)

🏗 Architecture Overview
API Layer: Handles all product CRUD operations

MVC Frontend: Displays product information by consuming the API

Data Layer: EF Core Code-First with Repository pattern (optional)

Validation: Enforced via Data Annotations

🧑‍💻 Author

Igres111

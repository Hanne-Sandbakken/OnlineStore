Online Store API

Introduction
This project is a C# ASP.NET Code API for an online store. It utilizes Entity Framework and middleware to provide functionality for browsing products, managing a shopping cart, and checking out orders.

Models
Product: A product consists of a name, description, price, and a URL for a product image.
Cart: A shopping cart contains a list of products and the total price.
Order: An order includes a list of products, a total price, and a delivery address.

Endpoints
GET /api/products: Retrieves a list of products.
GET /api/products/{id}: Retrieves a single product by its ID.
POST /api/cart: Adds a product to the shopping cart.
GET /api/cart: Retrieves the customer's shopping cart.
POST /api/checkout: Processes the checkout for a customer and creates an order.

Middleware
Authentication: Requires user authentication to use the API (utilizes Microsoft Entra Id).
Logging: Logs all requests and responses.

"Test webhook"

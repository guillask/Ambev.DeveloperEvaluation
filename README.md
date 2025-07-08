# Developer Evaluation Project

`READ CAREFULLY`

## Instructions
**The test below will have up to 7 calendar days to be delivered from the date of receipt of this manual.**

- The code must be versioned in a public Github repository and a link must be sent for evaluation once completed
- Upload this template to your repository and start working from it
- Read the instructions carefully and make sure all requirements are being addressed
- The repository must provide instructions on how to configure, execute and test the project
- Documentation and overall organization will also be taken into consideration

## Use Case
**You are a developer on the DeveloperStore team. Now we need to implement the API prototypes.**

As we work with `DDD`, to reference entities from other domains, we use the `External Identities` pattern with denormalization of entity descriptions.

Therefore, you will write an API (complete CRUD) that handles sales records. The API needs to be able to inform:

* Sale number
* Date when the sale was made
* Customer
* Total sale amount
* Branch where the sale was made
* Products
* Quantities
* Unit prices
* Discounts
* Total amount for each item
* Cancelled/Not Cancelled

It's not mandatory, but it would be a differential to build code for publishing events of:
* SaleCreated
* SaleModified
* SaleCancelled
* ItemCancelled

If you write the code, **it's not required** to actually publish to any Message Broker. You can log a message in the application log or however you find most convenient.

### Business Rules

* Purchases above 4 identical items have a 10% discount
* Purchases between 10 and 20 identical items have a 20% discount
* It's not possible to sell above 20 identical items
* Purchases below 4 items cannot have a discount

These business rules define quantity-based discounting tiers and limitations:

1. Discount Tiers:
   - 4+ items: 10% discount
   - 10-20 items: 20% discount

2. Restrictions:
   - Maximum limit: 20 items per product
   - No discounts allowed for quantities below 4 items

# API Challenge

📌 **About the Project**

API to manage sales and products.

🛠️ **Technologies Used**

- .NET Core 9.0
- Entity Framework Core
- SQL Server
- Docker (Optional)
- Git

🚀 **How to Run the Project**

1️⃣ **Clone the Repository**

```bash
git clone https://github.com/guillask/Ambev.DeveloperEvaluation
cd your-repository
```

2️⃣ **Configure the Connection String in `appsettings.json`**

Edit the `appsettings.Development.json` file and add your database connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=DeveloperEvaluation;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}
```

3️⃣ **Install Dependencies**

```bash
dotnet restore
```

4️⃣ **Create the Database and Run Migrations** _(NuGet Package Manager)_

- Select the project: `Ambev.DeveloperEvaluation.ORM`
- Run the following command:

```bash
update-database
```

To create a new migration, use:

```bash
add-migration MigrationName
```

5️⃣ **Run the Project** _(Terminal)_

```bash
dotnet run
```

The API will be running at: [http://localhost:7181](http://localhost:7181)

📚 **Main Endpoints**

- `POST /api/Sales/AddSale` → Creates a new sale.
- `GET /api/Sales/GetById` → Retrieves a sale by ID.
- `PUT /api/Sales/AlterSaleStatus` → Updates the cancellation status of a sale.
- `POST /api/Products/Create` → Creates a new product.

📝 **License**

This project is open-source and can be freely used.

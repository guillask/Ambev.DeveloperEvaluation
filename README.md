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
git clone https://github.com/guillask/SalesAPI
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

# Readify

Readify is a **book rental application** built using **ASP.NET Core MVC (.NET 6)** with **Entity Framework Core** and **Microsoft SQL Server**.
Users can rent, pay, and return books. The Admin can manage categories, books, and rental transactions.

---

## Features

- Interactive renting, payment, and price viewing using **JavaScript** and **Razor Views**
- Responsive design using **Bootstrap**
- **Toastr notifications** and **jQuery library** validations
- Authentication and Authorization using **Identity Framework**
- **Repository Pattern** with **Dependency Injection**

---

## Installation Guide

1. **Clone readify repository:**
```
https://github.com/johndave-uganiza/readify.git
```

2. **Navigate to project directory:**
```
cd Readify
```

3. **Configure SQL Server connection string in *appsettings.json*:**
```
"DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=ReadifyDemoDB;Trusted_Connection=True;"
```

4. **Install Packages:**
```
dotnet tool install --global dotnet-ef --version 6.*
dotnet add package Microsoft.EntityFrameworkCore --version 6.*
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 6.*
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 6.*
dotnet add package Microsoft.EntityFrameworkCore.Design --version 6.*
```

4. **Build Application:**
```
dotnet clean
dotnet restore
dotnet build
```

6. **Add Migrations:**
```
dotnet ef migrations add init --project . --startup-project .
```

7. **Update Database:**
```
dotnet ef database update --project . --startup-project .
```

8. **Run Application:**
```
dotnet run
```

## Admin Demo Credentials

- **Username:** Admin_Demo
- **Password** Admin_Demo123

> ⚠️ These credentials are **for demo purposes only**.
> Please do **not** put real production credentials.

---

## Credits
This project was developed as a learning exercise by following YouTube tutorials and implementing selected parts as references.
- [Learn ASP.NET Core MVC (.NET 6) - Full Course](https://www.youtube.com/watch?v=hZ1DASYd9rk)
- [Role based authorization in dot net 6+ (MVC) | Asp.net Identity](https://www.youtube.com/watch?v=KylxLlXsKjE&t=4606s)

## Thank You
Thanks for checking out this project! ❤️

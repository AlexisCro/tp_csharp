# TP note C#
## Database Implementation
This project will use SQL Server. 

### Packages
- Microsoft.EntityFrameworkCore.SqlServer --version 8.0.1
- Microsoft.AspNetCore.Identity.EntityFrameworkCore --version 8.0.1
- Microsoft.AspNetCore.Identity.UI --version 8.0.1
- Microsoft.EntityFrameworkCore.Design --version 8.0.1

Launch command 
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.1`

### Config database options
In `Program.cs` add :
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(
  Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
```

Update the `appsettings.json` to add the `DefaultString` necessary to the connection to the database. 
Add the following code : 
```csharp
  "ConnectionStrings": {
    "DefaultConnection": "Server=<YOUR SERVER NAME>;Integrated Security=True;TrustServerCertificate=True;Trusted_Connection=Yes;MultipleActiveResultSets=true"
  }
```

### Migrations
To realize migrations we need to launch these commands : 
`dotnet ef migrations add <migration name>` to create the migration 
`dotnet ef database update` to launch the migration

## Identity
Identity permit us to manage authentication of users. 

In our project we manage two registration : 
- Teachers
- Students

So to that we have implement a new table between IdentityUser and TeacherModel or StudentModel. 

So `TeacherModel` and `StudentModel` inherit of `UserModel` which inherit of `IdentityUser`.

## Teacher Object
### Model

### Controller

## Student Object

## Event Object
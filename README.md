# TP note C#
## Database Implementation
This project will use SQL Server. 

### Packages
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.1
- 

Launch command 
`dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.1`

### Config
In `Program.cs` add :
```csharp
builder.Services.AddDbContext<ApplicationDbContext>(
  Options => Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
```

Update the `appsettings.json` to add the `DefaultString` necessary to the connection to the database. 

### Migrations
To realize migrations we need to launch this commands : 
`dotnet ef migrations add <migration name>` to create the migration 
`dotnet ef database update` to launch the migration

## Identity

## Teacher Object
### Model


### Controller

## Student Object

## Event Object
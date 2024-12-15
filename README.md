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

## Database
In this project our purpose is to have a student list, a teacher list and an event list. 

And each student can subscribe to an event. 

So thaht we have these tables in our database : 
- Students
- Teachers
- Events

And a join table `Participants` to register students who want to participate to the event. 
So we have a has_many has_many relation. 

Indeed, a student can participate on many events and an events can have many participants.

## Test to check the app
### Teacher
When the app start you will arrive on the Home page. This page will a bit empty until you register a teacher who can be register event. 

So the first step is to create a teacher, go to the Register page and create a teacher account. 
Then Login you as a teacher, go to the page Login and enter your credentials. 

Now you can create students and events. 

### Student
When a student is created, you can logout as a teacher and Login as a student. 

As a student you don't have access to all application. So it's normal to don't have the permission to edit, delete or create a teacher or an event or a student. 

You could try as a student to want to create a teacher and reach the `/Account/RegisterStudent`, but you will redirect with a message error. 

But as a student, you can subscribe to an event or unsubscribe to event where you're registered. 

### Event
A teacher can create an Event by filling the form, or he can edit an event.
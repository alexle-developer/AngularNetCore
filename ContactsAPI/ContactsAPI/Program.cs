using ContactsAPI.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add CORS policy

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:4200")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Add services to the container.

// If we want to use a real database, we can use the following code:
//      Add services for the database DbContext
//      Connection String => Server=(localdb)\\MSSQLLocalDB; Database=ContactsDb; Trusted_Connection=True; MultipleActiveResultSets=true
//      Migration Steps using Nuget Package Manager Console:
//       1. Add-Migration "InitialMigration"
//       2. Update-Database
builder.Services.AddDbContext<ContactsApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDatabaseConnectionString")));


builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.ConfigureSwaggerGen(setup =>
{
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Contact List",
        Version = "v1"
    });
});


// Build the application.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseSwagger();
    //app.UseSwaggerUI(x =>
    //{
    //    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact List API");
    //    x.RoutePrefix = string.Empty;

    //});
}

app.UseSwagger();
app.UseSwaggerUI(x =>
{
    // When run on localhost, removed the RoutePrefix swagger
    //  - http://localhost:7223/index.html
    //
    // When run on Azure, removed the RoutePrefix swagger
    //  - https://contactlistapi.azurewebsites.net/index.html
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact List API");
    x.RoutePrefix = string.Empty;

});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

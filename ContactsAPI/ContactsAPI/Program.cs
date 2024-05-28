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

    /* NOTE: 
     * Azure URL will NOT work if you place the app.useSwagger() and app.UseSwaggerUI() here 
     */

    // If we want the "swagger" to be part of the URL for Azure, we need to:
    //  - move app.UseSwagger() and app.UseSwaggerUI() outside of the if block
    //  - Comment out the x.SwaggerEndpoint
    //  - Change the RoutePrefix to "swagger"
    //  - Publish the app to Azure and run it
    //  -  local => http://localhost:7223/swagger/index.html
    //  -  Azure => https://contactlistapi.azurewebsites.net/swagger/index.html
    // x.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact List API");
    // x.RoutePrefix = "swagger";

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
    // When run on localhost, http://localhost:7223/index.html
    // When run on Azure, https://contactlistapi.azurewebsites.net/index.html
    x.SwaggerEndpoint("/swagger/v1/swagger.json", "Contact List API");
    x.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Check if there is any pending migration
// ApplyMigration();

app.Run();



void ApplyMigration()
{

    if (app.Environment.IsDevelopment())
    {
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<ContactsApiDbContext>();
            // if pending migration count is greater than 0, then apply migration
            if (db.Database.GetPendingMigrations().Any())
            {
                db.Database.Migrate();
            }

        }
    }
}
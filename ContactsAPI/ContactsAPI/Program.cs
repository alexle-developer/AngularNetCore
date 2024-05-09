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


// Add services for the database DbContext
// Connection String => Server=(localdb)\\MSSQLLocalDB; Database=ContactsDb; Trusted_Connection=True; MultipleActiveResultSets=true
// we do not need to provide the connection string as we are using in-memory database
// options.UseInMemoryDatabase("ContactsDb");
// Migration Steps using Nuget Package Manager Console:
// 1. Add-Migration "InitialMigration"
// 2. Update-Database
builder.Services.AddDbContext<ContactsApiDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AzureDatabaseConnectionString")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

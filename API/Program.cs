using API.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args); //adding web application in IIS kestrel webserver
 

// Add services to the container. DI container

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//di for storecontext
builder.Services.AddDbContext<StoreContext>(opt =>{
    opt.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//adding service for CORS, react (redering data from webapi)
builder.Services.AddCors();
var app = builder.Build(); //building app

// Configure the HTTP request pipeline. //adding middlewere(software to addd inside software)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); //middleware
    app.UseSwaggerUI();
}
//middleware
app.UseCors(opt =>
{
    opt.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:3000");
});
//app.UseHttpsRedirection(); for production

app.UseAuthorization();

app.MapControllers(); //Routing configuration

//create a db, need to hold of dbContext
var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<StoreContext>();
var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
try
{
    context.Database.Migrate();
    DbInitializer.Initialize(context);  //initialize db as it was created in static way  
}
catch (Exception ex)
{
    logger.LogError(ex, "A problem occurred during migration");
}
 
app.Run();

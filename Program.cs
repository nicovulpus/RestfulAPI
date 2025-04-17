// CREATE A RESTFUL API THAT TRACKS THE LOCATION OF A MOBILE USER. LOGS ALL THE ADDRESSES THE USER
// HAS BEEN TO.

// ALSO SHOULD BE ABLE TO SEE IF USERS ARE IN THE SAME LOCATION 

using RestfulAPI.Models;  
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;


// Load configs from appsettings , environment variables and command line arguments
// Lets me register services like DbContext, logging, CORS and such via builder.Services
var builder = WebApplication.CreateBuilder(args);
// Add support for API controllers
builder.Services.AddControllers();

// Register AppDbContext, prepares the connection to the database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Create the actual app instance
var app = builder.Build();

// Add authentication middleware to the request pipeline, who is the user?
app.UseAuthentication();
// Add the authorization middleware to the request pipeline, what is the user allowed to do?
app.UseAuthorization();
// Scan the controllers and make the endpoints reachable by HTTP requests
app.MapControllers(); 



app.Run(); 

using RestfulAPI.Models;  
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// CREATE A RESTFUL API THAT TRACKS THE LOCATION OF A MOBILE USER. LOGS ALL THE ADDRESSES THE USER
// HAS BEEN TO.

// ALSO SHOULD BE ABLE TO SEE IF TWO USERS ARE IN THE SAME LOCATION 

var app = builder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));


app.Run();

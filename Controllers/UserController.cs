using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestfulAPI.Models;
using System.Security.Cryptography;
using System;
using System.Text;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;



// STATE THAT THIS IS A WEB API CONTROLLER
[ApiController]

// THEN DEFINE THE API ENDPOINTS 

// BEGIN BY SETTING A BASE ROUTE CONTROLLER
[Route("api/[controller]")]

// DEFINE THE USER CONTROLLER

// Make the UserController inherit from the class ControllerBase 
public class UserController : ControllerBase
{
  // Set a new variable _context with type AppDbContext
  // Which allows the controller to access the database
  private readonly AppDbContext _context;
  private readonly IConfiguration _configuration; 
  
  // Explicitly create a constructor for the UserController class
  public UserController (AppDbContext context, IConfiguration configuration)
  {
    _context = context;
    _configuration = configuration; 
  }

  private string GenerateJwtToken(User user)
{
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.Email, user.Email),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var token = new JwtSecurityToken(
        issuer: _configuration["JwtSettings:Issuer"],
        audience: _configuration["JwtSettings:Audience"],
        claims: claims,
        expires: DateTime.Now.AddHours(3),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}


// POST REGISTER PATH 
// Create a path called register 
[HttpPost("register")]

// When client sends a POST request to this endpoint with a JSON body
// deserialize the body into a User object , then this method asynchronously to 
// return either a User or a status code 
        public async Task<ActionResult<User>> Register([FromBody] User newUser)
        {
            // Create a variable that awaits it's content from the User table in the database
            // gives the first user that matches the description
            
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == newUser.Email || u.Username == newUser.Username);
            // if existingsUser is not null, that means that this user already exists
            if (existingUser != null)
            {
                // Returns a 409 status code 
                return Conflict("User with same email or username already exists.");
            }

            // HASH

            // Hashes the plain-text password before storing
            newUser.PasswordHash = HashPassword(newUser.PasswordHash);
            // Add new user to DbContext 
            _context.Users.Add(newUser);
            // Save changes to database
            await _context.SaveChangesAsync();
            // Returns 201 , user created
            return CreatedAtAction(nameof(GetUser), new { id = newUser.Id }, newUser);
        }

        // HELPER TO HASH PASSWORD USING BCRYPT 
        private string HashPassword(string password)
        {
          return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // POST LOGIN PATH
        
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            if (user == null)
            return Unauthorized("Invalid credentials.");

            // CHECK PASSWORD
            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            return Unauthorized("Invalid credentials.");
            
            var token = GenerateJwtToken(user);

            return Ok(token);

        }
        // GET ID 
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return NotFound();

            return user;
        }

        // DATA TRANSFER OBJECT FOR LOGIN CREDENTIALS
        public class LoginRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }



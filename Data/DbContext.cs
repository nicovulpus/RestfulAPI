using Microsoft.EntityFrameworkCore;         
using RestfulAPI.Models;
using System.Text.Json.Serialization;


public class User
{
    public int Id { get; set; } // PRIMARY KEY
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    [JsonIgnore]
    public int? CurrentAddressId { get; set; } 
    [JsonIgnore]
    public Address? CurrentAddress { get; set; }
    [JsonIgnore]
    // ONE TO MANY RELATIONSHIP
    // USER CAN HAVE VISITED MANY ADDRESSES
    public ICollection<UserAddressLog> AddressLogs { get; set; } = new List<UserAddressLog>();
    
    
}

public class Address
{
    public int Id { get; set; } // PRIMARY KEY
    
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    
    public string? Street { get; set; }
    public string? Housenumber { get; set; }
    public string? Postcode { get; set; }
    public string? City { get; set; }
    public string? Country { get; set; }


  
    // MANY USERS CAN VISIT THE SAME ADDRESS
    public ICollection<UserAddressLog> UserVisits { get; set; } 
    

}


public class UserAddressLog
{
    public int Id { get; set; } // PRIMARY KEY 

    public int UserId { get; set; } //AUTOMATICALLY MAPPED AS FOREIGN KEY
    public User User { get; set; }

    public int AddressId { get; set; } // AUTOMATICALLY MAPPED AS FOREIGN KEY 
    public Address Address { get; set; }

    public DateTime VisitedAt { get; set; }
}

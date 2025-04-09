using Microsoft.EntityFrameworkCore;         
using RestfulAPI.Models;

public class User
{
    public int Id { get; set; } // PRIMARY KEY
    public string Email { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public int? CurrentAddressId { get; set; } 
    public Address CurrentAddress { get; set; }
    
    public ICollection<UserAddressLog> AddressLogs { get; set; } // ONE TO MANY RELATIONSHIP
    // USER CAN HAVE VISITED MANY ADDRESSES
}

public class Address
{
    public int Id { get; set; } // PRIMARY KEY
    public string Street { get; set; }
    public string City { get; set; }
    public string Country { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

  
    public ICollection<UserAddressLog> UserVisits { get; set; } // MANY USERS CAN VISIT THE SAME ADDRESS
    

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

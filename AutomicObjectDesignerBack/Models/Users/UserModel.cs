using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace AutomicObjectDesigner.Models.Registration;

public class UserModel
{
    [Key]
    public int Id { get; set; }

    //[Required]
    [StringLength(16)]
    [RegularExpression(@"[a-zA-Z]{2,16}", ErrorMessage = "Valid Charactors include (A-Z) (a-z), max length 16")]
    public string FirstName { get; set; }

    //[Required]
    [StringLength(32)]
    [RegularExpression("([a-zA-Z]{2,})?", ErrorMessage = "Valid Charactors include (A-Z) (a-z) (' space -), max length 32")]
    public string LastName { get; set; }
    public string Name => $"{FirstName} {LastName}";
    //[Required]
    [StringLength(16)]
    [RegularExpression(@"[a-zA-Z]{2,16}", ErrorMessage = "Valid Charactors include (A-Z) (a-z), max length 16")]
    public string UserName { get; set; }

    //[Required]
    [StringLength(32)]
    [RegularExpression(@"[a-z0-9]+@[a-z]+\.[a-z]{2,3}")]
    public string Email { get; set; }
    public byte[] PasswordHash { get; set; }
    public byte[] PasswordSalt { get; set; }
    public bool HasEmailConfirmed { get; set; }
    public string Role { get; set; }

}


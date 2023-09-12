using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace my_Ecommerce_App.ViewModels;

public class RegisterUser
{
    [Required]
    [RegularExpression(@"^[A-z]+[A-z0-9]*$", ErrorMessage = "Invalid username")]
    public string? UserName { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address")]
    public string? Email { get; set; }

    [Required]
    [RegularExpression(@"^\+[0-9]+$", ErrorMessage = "Invalid Phone Number")]
    public string? Phone { get; set; }

    [Required]
    public string? Address { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [RegularExpression(@"^[A-z0-9]{5,}$", ErrorMessage = "password must be :<br>at least 6 characters.<br>containing alphabetic characters (upper and lower).<br>containing numbers")]
    public string? Password { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "password confirmation does not match password")]
    public string? ConfirmPassword { get; set; }
}
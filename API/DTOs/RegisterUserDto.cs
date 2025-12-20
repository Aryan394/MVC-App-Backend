using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class RegisterUserDto
{
    [Required(ErrorMessage = "Full Name is required")]
    [RegularExpression(@"^\S(.*\S)?$", ErrorMessage = "Provide Full Name")]
    [MaxLength(30, ErrorMessage = "Full Name cannot exceed 30 characters")]
    public string FullName { get; set; } = "";

    [Required(ErrorMessage = "Email is required")]
    [MinLength(1, ErrorMessage = "Provide Email Address")]
    [MaxLength(30, ErrorMessage = "Email Address cannot exceed 30 characters")]
    public string Email { get; set; } = "";

    [Required(ErrorMessage = "Password is required")]
    [MinLength(8, ErrorMessage = "Password should be of at least 8 characters")]
    [MaxLength(16, ErrorMessage = "Password cannot exceed 16 characters")]
    public string Password { get; set; } = "";
}
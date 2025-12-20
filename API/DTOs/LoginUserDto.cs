using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class LoginUserDto
{
    [MaxLength(30, ErrorMessage = "Enter valid Email Address")]
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = "";

    [MaxLength(30)]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = "";
}
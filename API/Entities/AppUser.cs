using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class AppUser
{
    [Required]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    [MaxLength(50)]
    public string? DisplayName { get; set; } 
    [Required]
    [MaxLength(100)]
    public string? Email { get; set; }
    [MaxLength(200)]
    public string? ImageUrl { get; set; }
    [MaxLength(200)]
    public  string? Address { get; set; }
    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
    
    // Nav Property
    public Member Member { get; set; } = null!;
    
}
using System.ComponentModel.DataAnnotations;

namespace API.DTOs;

public class SeedUserDto
{
    public Guid Id {get; set;} 
    public required string Email {get; set;}
    public required DateOnly DateOfBirth {get; set;}
    [MaxLength(200)]
    public string? ImageUrl {get; set;}
    [MaxLength(50)]
    public required string FullName {get; set;}
    public DateTime CreationTime { get; set; } 
    public DateTime? LastModificationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    public DateTime LastActive {get; set;} 
    [MaxLength(6)]
    public string? Gender {get; set;}
    [MaxLength(50)]
    public string? City {get; set;}
    [MaxLength(50)]
    public string? Country {get; set;}

}
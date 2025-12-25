using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities;

public class Member
{
    public Guid Id {get; set;} 
    public required DateOnly DateOfBirth {get; set;}
    [MaxLength(200)]
    public string? ImageUrl {get; set;}
    [MaxLength(50)]
    public required string FullName {get; set;}
    public DateTime CreationTime { get; set; } = DateTime.UtcNow;
    public DateTime? LastModificationTime { get; set; }
    public DateTime? DeletionTime { get; set; }
    public DateTime LastActive {get; set;} = DateTime.UtcNow;
    [MaxLength(6)]
    public string? Gender {get; set;}
    [MaxLength(50)]
    public string? City {get; set;}
    [MaxLength(50)]
    public string? Country {get; set;}
    
    // Navigation Property
    [ForeignKey((nameof(Id)))]
    public AppUser User { get; set; } = null!;
}
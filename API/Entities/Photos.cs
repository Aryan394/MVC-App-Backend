using System.ComponentModel.DataAnnotations;

namespace API.Entities;

public class Photos
{
    public Guid Id { get; set; }
    [MaxLength(200)]
    public required string Url { get; set; }
    
    // Navigation Property
    public Member Member { get; set; } = null!;
}
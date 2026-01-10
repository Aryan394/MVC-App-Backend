using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Entities;

public class Photos
{
    [MaxLength(200)]
    public required string Id { get; set; }
    [MaxLength(200)]
    public required string Url { get; set; }
    
    // Navigation Property
    [JsonIgnore]
    public Member Member { get; set; } = null!;
    public Guid MemberId { get; set; } 
}
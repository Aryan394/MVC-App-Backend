namespace API.Helpers;

public class MemberParams : PagingParams
{
    public Guid? CurrentMemberId { get; set; }
    public string? Gender { get; set; }
    public string? Country { get; set; }
    public int MinAge { get; set; } = 5;
    public int MaxAge { get; set; } = 200;

}
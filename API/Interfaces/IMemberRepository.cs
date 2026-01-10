using API.Entities;

namespace API.Interfaces;

public interface IMemberRepository
{
    void Update(Member member);
    Task<bool> SaveAllAsync();
    Task<IReadOnlyList<Member>> GetAllAsync();
    Task<Member?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Photos>> GetPhotosForMemberAsync(Guid memberId);
    Task<Member?> GetMemberForUpdate(Guid memberId);
}
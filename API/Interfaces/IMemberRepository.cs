using API.Entities;
using API.Helpers;

namespace API.Interfaces;

public interface IMemberRepository
{
    void Update(Member member);
    Task<bool> SaveAllAsync();
    Task<PaginatedResults<Member>> GetAllAsync(MemberParams memberParams);
    Task<Member?> GetByIdAsync(Guid id);
    Task<IReadOnlyList<Photos>> GetPhotosForMemberAsync(Guid memberId);
    Task<Member?> GetMemberForUpdate(Guid memberId);
}
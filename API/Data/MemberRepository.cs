using API.Entities;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class MemberRepository(AppDbContext context) : IMemberRepository
{
    public void Update(Member member)
    {
        context.Entry(member).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync()>0;
    }

    public async Task<PaginatedResults<Member>> GetAllAsync(MemberParams memberParams)
    {
        var query = context.Members
            .OrderBy(m => m.Id)
            .AsQueryable();
        query = query.Where(x=> x.Id != memberParams.CurrentMemberId);
        if (memberParams.Gender != null)
        {
            query = query.Where(x=>x.Gender == memberParams.Gender);
        }

        var minDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-memberParams.MaxAge - 1));
        var maxDob = DateOnly.FromDateTime(DateTime.Today.AddYears(-memberParams.MinAge));
        query = query.Where(x => x.DateOfBirth >= minDob && x.DateOfBirth <= maxDob);
        return await PaginationHelper.CreateAsync(query, memberParams.PageNumber, memberParams.PageSize);
    }

    public async Task<Member?> GetByIdAsync(Guid id)
    {
        return await context.Members.FindAsync(id);
    }

    public async Task<IReadOnlyList<Photos>> GetPhotosForMemberAsync(Guid memberId)
    {
        return await context.Members.
            Where(p => p.Id == memberId).
            SelectMany(x=>x.Photos)
            .ToListAsync();
    }

    public async Task<Member?> GetMemberForUpdate(Guid memberId)
    {
        return await context.Members
            .Include(x=>x.User)
            .Include(x=>x.Photos)
            .SingleOrDefaultAsync(x => x.Id == memberId);
    }

    public async Task<IReadOnlyList<Member>> GetMembersAsync()
    {
        var query = context.Members.AsQueryable();
        return await context.Members.ToListAsync();
    }

    // public async Task<Member?> GetByIdAsync(string email)
    // {
    //     return await context.Members.FirstOrDefaultAsync(m => m.Email == email);
    // }
}
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext _context)
    {
        if (await _context.Users.AnyAsync()) return;
        var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json");
        var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);
        if (members == null || members.Count == 0)
        {
            Console.WriteLine("No members found in seed data");
            return;
        }
        
        foreach (var member in members)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                Email = member.Email,
                DisplayName = member.FullName,
                ImageUrl = member.ImageUrl,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                Member = new Member
                {
                FullName = member.FullName,
                DateOfBirth = member.DateOfBirth,
                ImageUrl = member.ImageUrl,
                Gender = member.Gender,
                City = member.City,
                Country = member.Country,
                LastActive =  member.LastActive,
                CreationTime =  member.CreationTime,
                }
            };
            user.Member.Photos.Add(new Photos
            {
                Url = member.ImageUrl,
                MemberId = member.Id,
            });
            _context.Users.Add(user);
        }
        await _context.SaveChangesAsync();
    }
}
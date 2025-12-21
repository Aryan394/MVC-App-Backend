using API.DTOs;
using API.Entities;
using API.Interfaces;
using API.Services;

namespace API.Extensions;

public static class AppUserExtensions
{
    public static UserResponseDto ToDto(this AppUser user, ITokenService tokenService)
    {
        return new UserResponseDto
        {
            Email = user.Email,
            Id = user.Id,
            DisplayName = user.DisplayName,
            Token = tokenService.CreateToken(user)
        };
    }
 }
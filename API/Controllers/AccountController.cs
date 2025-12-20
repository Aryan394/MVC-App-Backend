using System.Security.Cryptography;
using System.Text;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class AccountController : BaseAPIController
{
    
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    
    public AccountController(
        AppDbContext context,
        ITokenService tokenService)
    {
        _context = context;
        _tokenService = tokenService;
    }
    [HttpPost("register")]
    public async Task<ActionResult<UserResponseDto>> Register(RegisterUserDto registerUserDto)
    {
        try
        {
            if(string.IsNullOrWhiteSpace(registerUserDto.FullName))
                return BadRequest("Enter Full Name");
        
            if(string.IsNullOrWhiteSpace(registerUserDto.Email))
                return BadRequest("Enter Email");
        
            if(string.IsNullOrWhiteSpace(registerUserDto.Password))
                return BadRequest("Enter Password");
        
            if (! IsValidEmail(registerUserDto.Email))
                return BadRequest("Enter valid email address");
            
            if (await EmailExists(registerUserDto.Email))
                return Conflict("Email is already in use.");
        
            using var hmac = new HMACSHA512(); // 'using' releases all the resources used by the current instance of the hash algorithm class
            var user = new AppUser
            {
                DisplayName = registerUserDto.FullName,
                Email = registerUserDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerUserDto.Password)),
                PasswordSalt = hmac.Key
            };
        
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            var response = new UserResponseDto()
            {
                Email = user.Email,
                Id = user.Id,
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user)
            };
            return Ok(response);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                "An error occurred while processing your request. Please try again later."
            );
        }
    }

    private bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var addr = (new System.Net.Mail.MailAddress(email));
            return addr.Address == email;
        }
        catch
        {
            return false;
        } 
    }
    private async Task<bool> EmailExists(string email)
    {
        return await _context.Users.AnyAsync(u => 
            u.Email.ToLower() == email.ToLower());
    }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponseDto>> Login(LoginUserDto loginUserDto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x=> 
            x.Email.ToLower() == loginUserDto.Email.ToLower());
        if (user == null)
            return Unauthorized("Email or password incorrect");
        using var hmac = new HMACSHA512(user.PasswordSalt);
        var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginUserDto.Password));
        for (var i = 0; i < computedHash.Length; i++)
        {
            if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Email or password incorrect");
        }

        var response = new UserResponseDto()
        {
            Email = user.Email,
            Id = user.Id,
            DisplayName = user.DisplayName,
            Token = _tokenService.CreateToken(user)
        };
        return Ok(response);
    }
}
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers;

public class MembersController(AppDbContext context) : BaseAPIController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers() // public ActionResult allows us to return HTTP responses
    {
        var members = await context.Users.ToListAsync();
        return Ok(members);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AppUser>> GetMember(Guid id)
    {
        try
        {
            var member = await context.Users.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }

            return Ok(member);
            
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

        }
        
    }
}
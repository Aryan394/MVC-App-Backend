using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Authorize]
public class MembersController(IMemberRepository memberRepository, 
    IPhotoService photoService) : BaseApiController
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<Member>>> GetMembers([FromQuery]MemberParams memberParams) // public ActionResult allows us to return HTTP responses
    {
        memberParams.CurrentMemberId = User.GetMemberId();
        return Ok(await memberRepository.GetAllAsync(memberParams));
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetMember(Guid id)
    {
        try
        {
            var member = await memberRepository.GetByIdAsync(id);
            
            if (member == null)
                return NotFound();
            
            return Ok(member);
            
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, e.Message);

        }
        
    }

    [HttpGet("{id}/photos")]
    public async Task<ActionResult<IReadOnlyList<Photos>>> GetMemberPhotos(Guid id)
    {
        return Ok(await memberRepository.GetPhotosForMemberAsync(id));
    }

     // [HttpPost("add-photo")]
     // public async Task<ActionResult<Photos>> AddPhoto([FromForm] IFormFile file)
     // {
     //     var member = await memberRepository.GetMemberForUpdate (User.Id());
     //     if(member == null) return NotFound("Member not found or is not accessible");
     //     var result = await photoService.UploadPhotoAsync(file);
     //     if (result.Error != null) return BadRequest(result.Error.Message);
     //     var photo = new Photos
     //     {
     //         Url = result.SecureUrl.AbsoluteUri,
     //         Id = result.PublicId,
     //         MemberId = User.GetMemberId()
     //     };
     //     if (member.ImageUrl == null)
     //     {
     //         member.ImageUrl = photo.Url;
     //         member.User.ImageUrl = photo.Url;
     //     }
     //     member.Photos.Add(photo);
     //     if (await memberRepository.SaveAllAsync()) return photo;
     //     return BadRequest("Could not add a photo. Please try later :(");
     // }
}
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpPost("auth")]
    public IActionResult GetAuth()
    {
        return Unauthorized();
    }

    [HttpPost("not-found")]
    public IActionResult GetNotFound()
    {
        return NotFound();
    }
    [HttpPost("forbidden")]
    public IActionResult GetForbidden()
    {
        return Forbid();
    }
    [HttpGet("server-error")]
    public IActionResult GetServerError()
    {
       throw new Exception("This is a Server Error");
    }
    [HttpGet("bad-request")]
    public IActionResult GetBadRequest()
    {
        return BadRequest("This is a bad request ");
    }
}
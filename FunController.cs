namespace BugandFixFirstMinimalApi;

using Microsoft.AspNetCore.Mvc;



[ApiController]
[Route("api/[controller]")]
public class FunController : ControllerBase
{
    [HttpGet("joke")]
    public IActionResult GetJoke()
    {
        var joke = new
        {
            Setup = "Why don’t skeletons fight each other?",
            Punchline = "They don’t have the guts."
        };

        return Ok(joke);
    }

    [HttpPost("add-fun")]
    public IActionResult AddFun([FromBody] FunItem funItem)
    {
        if (funItem == null || string.IsNullOrEmpty(funItem.Name))
        {
            return BadRequest("Invalid fun item.");
        }

        return Created("api/fun/add-fun", funItem);
    }
}

public class FunItem
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

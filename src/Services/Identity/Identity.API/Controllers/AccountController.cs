namespace Identity.API.Controllers;

[Route("api/[controller]")]
public class AccountController : Controller
{
    private readonly IIdentityUserService _userService;
    public AccountController(IIdentityUserService userService)
    {
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
    {
        var result = await _userService.RegisterAsync(userRegister);

        if (!result.Succeeded)
            return BadRequest(result.Errors);

        return Ok();
    }
}
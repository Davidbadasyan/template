using UM.Application.Dtos;

namespace UM.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IUserQueryService _userQueryService;

    public UsersController(IMediator mediator, IUserQueryService userQueryService)
    {
        _mediator = mediator;
        _userQueryService = userQueryService;
    }

    [Route("register")]
    [HttpPost]
    public async Task<ActionResult> RegisterUserAsync([FromBody] UserRegistrationDto userRegistration)
    {
        var command = new RegisterUserCommand(userRegistration);
        var response = await _mediator.Send(command);

        return Ok(response);
    }

    [Route("me")]
    [HttpGet]
    public async Task<ActionResult> GetMeAsync()
    {
        var me = await _userQueryService.GetMeAsync();
        return Ok(me);
    }

    [Route("{id:long}")]
    [HttpGet]
    public async Task<ActionResult> GetByIdAsync([FromRoute] long id)
    {
        var user = await _userQueryService.GetByIdAsync(id);
        return Ok(user);
    }

    [Route("{id:long}")]
    [HttpPut]
    public async Task<ActionResult> UpdateAsync(
        [FromRoute] long id,
        [FromBody] UserUpdateDto userUpdate)
    {
        var response = await _mediator.Send(new UpdateUserCommand(id, userUpdate));
        return Ok(response);
    }
}
using BeautySalon.ContractService;
using BeautySalon.DTO.Auths;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers.User;

public class UsersController : MainController
{
    public UsersController(INotifier notifier) : base(notifier)
    {
    }

    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenDto>> Login()
    {
        
        return CustomResponse();
    }
}

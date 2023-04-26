using BeautySalon.ContractAppService;
using BeautySalon.ContractService;
using BeautySalon.DTO.Auths;
using BeautySalon.DTO.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.Controllers.User;

public class UsersController : MainController
{
    private readonly IUserAppService _appService;

    public UsersController(
        INotifier notifier, 
        IUserAppService appService) : base(notifier)
    {
        _appService = appService;
    }

    [HttpPost("auth")]
    [AllowAnonymous]
    public async Task<ActionResult<TokenDto>> Login(UserLoginDto dto)
    {
        if (dto == null || !ModelState.IsValid) return CustomResponse(ModelState);
        var result = await _appService.Login(dto);
        
        return CustomResponse(result);
    }
}

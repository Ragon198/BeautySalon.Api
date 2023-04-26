using BeautySalon.ContractAppService;
using BeautySalon.ContractService;
using BeautySalon.DTO.Auths;
using BeautySalon.DTO.Users;

namespace BeautySalon.AppService;

public class UserAppService : IUserAppService
{
    private readonly IAuthService _authService;

    public UserAppService(IAuthService authService)
    {
        _authService = authService;
    }

    public Task<TokenDto> Login(UserLoginDto dto)
    {
        return _authService.Login(dto);
    }
}

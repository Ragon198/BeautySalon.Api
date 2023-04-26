using BeautySalon.DTO.Auths;
using BeautySalon.DTO.Users;

namespace BeautySalon.ContractAppService;

public interface IUserAppService
{
    Task<TokenDto> Login(UserLoginDto dto);
}

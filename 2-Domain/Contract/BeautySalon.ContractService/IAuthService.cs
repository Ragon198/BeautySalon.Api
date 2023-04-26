using BeautySalon.DTO.Auths;
using BeautySalon.DTO.Users;

namespace BeautySalon.ContractService;

public interface IAuthService
{
    Task<TokenDto> Login(UserLoginDto dto);
}

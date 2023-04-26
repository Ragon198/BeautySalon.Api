using System.ComponentModel.DataAnnotations;

namespace BeautySalon.DTO.Users;

public class UserLoginDto
{
    [Required]
    public string Pin { get; set; }
    
    [DataType(DataType.Password)]
    [Required]
    public string Password { get; set; }
}

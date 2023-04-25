namespace BeautySalon.DTO.Auths;

public class TokenDto
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public List<Dictionary<string, string>> Claims { get; set; }
}

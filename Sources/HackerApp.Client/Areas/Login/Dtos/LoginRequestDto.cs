namespace HackerApp.Client.Areas.Login.Dtos
{
    public class LoginRequestDto
    {
        public string Password { get; set; } = null!;
        public string UserName { get; set; } = null!;
    }
}
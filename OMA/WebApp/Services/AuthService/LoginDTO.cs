namespace WebApp.Services.AuthService
{
    public class LoginDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }
    public class AuthResult
    {
        public string token { get; set; }
        public bool status { get; set; }

    }
}

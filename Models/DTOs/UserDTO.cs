namespace rentcar_api.Models.DTOs
{
    public class UserRegisterDto
    {
        public string nm_user { get; set; } = string.Empty;
        public string nm_email { get; set; } = string.Empty;
        public string nm_senha { get; set; } = string.Empty;
    }

    public class UserLoginDto
    {
        public string nm_email { get; set; } = string.Empty;
        public string nm_senha { get; set; } = string.Empty;
    }
}

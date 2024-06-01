namespace CodePulse.API.Models.DTO
{
    public class LoginResponseDto
    {
        public string Name { get; set; }
        public List<string> Roles{ get; set; }
        public string Token { get; set; }
    }
}

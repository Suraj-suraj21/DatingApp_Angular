using System.Security.Principal;

namespace DatingApp_WebApi.DTOs
{
    public class LoginDto
    {
        public required string Username {  get; set; }   
        public required string Password { get; set; }
    }
}

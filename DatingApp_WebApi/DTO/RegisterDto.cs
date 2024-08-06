using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace DatingApp_WebApi.DTO
{
    public class RegisterDto
    {
        [Required]        
        public required string Username { get; set; }

        [Required]
        public required string Password { get; set; }

    }
}

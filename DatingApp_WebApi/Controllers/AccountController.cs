using AngularAPI.Data;
using AngularAPI.Entities;
using DatingApp_WebApi.DTO;
using DatingApp_WebApi.DTOs;
using DatingApp_WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Security.Cryptography;
using System.Text;

namespace DatingApp_WebApi.Controllers
{
    public class AccountController(Datacontext context,ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")]

        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if(await UserExist(registerDto.Username))   return BadRequest("username is taken");
            using var hmca = new HMACSHA512();

            var user = new AppUser
            {
                UserName =  registerDto.Username.ToLower(),
                PasswordHash = hmca.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmca.Key
            };

            context.users.Add(user);
            await context.SaveChangesAsync();
            return new UserDto
            {
                Username = user.UserName,
                Token = tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public  async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await context.users.FirstOrDefaultAsync(x=>x.UserName.ToLower() == loginDto.Username.ToLower());
            if (user == null) return Unauthorized("Invalid username or password");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for(int i = 0; i < computedHash.Length; i++)
            {

                if (computedHash[i] != user.PasswordHash[i]) {
                    return Unauthorized("invalid password");
                }
            }
            
            return new UserDto { Username = user.UserName, Token = tokenService.CreateToken(user) };    
        }

        public async Task<bool> UserExist(string username)
        {
                return await context.users.AnyAsync(x=>x.UserName.ToLower() == username.ToLower());
        }       

    }
}

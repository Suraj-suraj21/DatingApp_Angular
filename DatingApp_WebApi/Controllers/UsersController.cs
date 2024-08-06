using AngularAPI.Data;
using AngularAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace DatingApp_WebApi.Controllers
{
    [Route("api/[controller]")]
   
    public class UsersController : BaseApiController
    {
        private Datacontext _context;
        public UsersController(Datacontext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUser()
        {
             var users = await _context.users.ToListAsync();
             return users;
        }


        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AppUser>> GetUser(int id)
        {
            var user =  await _context.users.FindAsync(id);

            if(user is null) return NotFound();
            return user;
        }

    }
}

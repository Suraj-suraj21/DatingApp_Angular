using AngularAPI.Entities;

namespace DatingApp_WebApi.Interfaces
{
    public interface ITokenService
    {

        string CreateToken(AppUser user);


    }
}

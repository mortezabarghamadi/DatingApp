using Domain.Entites.User;

namespace DatingApp.Api.Services.Interfaces
{
    public interface ITokenService
    {
        //ایجاد توکن 
        string CreateToken(User user);
    }
}

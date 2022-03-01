using JWT.Models;

namespace JWT.Respository
{
    public interface IManagerJWTRepository
    {
        Tokens Authenticate(Account account); 
    }
}

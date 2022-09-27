using Movie_API.Models;

namespace Movie_API.Services
{
    public interface IAuthManager
    {
        Task<bool> ValidateUser(LoginDTO userDTO);

        Task<string> CreateToken();
    }
}

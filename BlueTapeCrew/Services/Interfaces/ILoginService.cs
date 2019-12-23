using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface ILoginService
    {
        Task<SignInResult> Login(string username, string password);
        Task Logout();
    }
}

using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IEmailSubscriptionService
    {
        Task<string> Subscribe(string email);
    }
}

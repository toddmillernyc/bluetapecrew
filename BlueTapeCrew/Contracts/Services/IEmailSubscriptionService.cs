using System.Threading.Tasks;

namespace BlueTapeCrew.Contracts.Services
{
    public interface IEmailSubscriptionService
    {
        Task<string> Subscribe(string email);
    }
}

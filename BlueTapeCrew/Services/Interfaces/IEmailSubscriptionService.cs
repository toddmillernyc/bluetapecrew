using System.Threading.Tasks;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IEmailSubscriptionService
    {
        Task Subscribe(string email);
    }
}

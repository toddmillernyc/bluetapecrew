using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEmailSubscriptionService
    {
        Task Subscribe(string email);
    }
}

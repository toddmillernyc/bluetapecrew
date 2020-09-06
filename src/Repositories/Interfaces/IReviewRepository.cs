using System.Threading.Tasks;
using Repositories.Entities;

namespace Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task Create(Review review);
    }
}

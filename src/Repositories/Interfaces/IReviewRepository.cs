using System.Threading.Tasks;
using Entities;

namespace Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task Create(Review review);
    }
}

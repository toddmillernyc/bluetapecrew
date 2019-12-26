using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Repositories.Interfaces
{
    public interface IReviewRepository
    {
        Task Create(Review review);
    }
}

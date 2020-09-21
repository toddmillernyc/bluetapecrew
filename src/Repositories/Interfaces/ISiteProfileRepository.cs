using Entities;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface ISiteProfileRepository
    {
        Task<PublicSiteProfile> Get();
    }
}

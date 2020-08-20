using System.Collections.Generic;
using System.Threading.Tasks;
using Services.Models;

namespace Services.Interfaces
{
    public interface ICatalogService
    {
        Task<IEnumerable<CatalogModel>> GetCatalog();
    }
}

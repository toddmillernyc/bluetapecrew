using System.Collections.Generic;
using System.Threading.Tasks;
using Entities;

namespace BlueTapeCrew.Services.Interfaces
{
    public interface IStyleService
    {
        Task DeleteStyle(int styleId);
        Task CreateColor(Color color);
        Task CreateStyle(Style style);
        Task CreateSize(string sizeText);
        Task<IEnumerable<Color>> GetColors();
        Task<IEnumerable<Size>> GetSizes();
    }
}

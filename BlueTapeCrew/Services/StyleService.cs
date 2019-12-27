using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlueTapeCrew.Repositories.Interfaces;
using BlueTapeCrew.Services.Interfaces;
using Entities;

namespace BlueTapeCrew.Services
{
    public class StyleService : IStyleService
    {
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IStyleRepository _styleRepository;

        public StyleService(
            IColorRepository colorRepository,
            ISizeRepository sizeRepository,
            IStyleRepository styleRepository)
        {
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _styleRepository = styleRepository;
        }

        public Task CreateStyle(Style style) => _styleRepository.Create(style);
        public Task CreateColor(Color color) => _colorRepository.Create(color);
        public Task DeleteStyle(int styleId) => _styleRepository.Delete(styleId);
        public Task<IEnumerable<Color>> GetColors() => _colorRepository.Get();
        public Task<IEnumerable<Size>> GetSizes() => _sizeRepository.Get();

        public async Task CreateSize(string sizeText)
        {
            var sizes = await _sizeRepository.Get();
            var lastSize = sizes.OrderByDescending(x => x.SizeOrder).FirstOrDefault();
            var sizeOrder = lastSize?.SizeOrder ?? 0;
            var size = new Size {SizeText = sizeText, SizeOrder = sizeOrder + 1};
            await _sizeRepository.Create(size);
        }
    }
}

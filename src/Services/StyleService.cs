using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using Entity = Repositories.Entities;

namespace Services
{
    public class StyleService : IStyleService
    {
        private readonly IColorRepository _colorRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IStyleRepository _styleRepository;
        private readonly IMapper _mapper;

        public StyleService(
            IColorRepository colorRepository,
            ISizeRepository sizeRepository,
            IStyleRepository styleRepository,
            IMapper mapper)
        {
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _styleRepository = styleRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StyleView>> GetByProductId(int productId)
        {
            var entities = await _styleRepository.GetByProductId(productId);
            var model = _mapper.Map<IEnumerable<StyleView>>(entities);
            return model;
        }

        public Task CreateStyle(Style style)
        {
            var entity = _mapper.Map<Entity.Style>(style);
            return _styleRepository.Create(entity);
        }

        public Task CreateColor(Color color)
        {
            var entity = _mapper.Map<Entity.Color>(color);
            return _colorRepository.Create(entity);
        }

        public async Task CreateSize(string sizeText)
        {
            var sizes = await _sizeRepository.Get();
            var lastSize = sizes.OrderByDescending(x => x.SizeOrder).FirstOrDefault();
            var sizeOrder = lastSize?.SizeOrder ?? 0;
            var size = new Size { SizeText = sizeText, SizeOrder = sizeOrder + 1 };
            var entity = _mapper.Map<Entity.Size>(size);
            await _sizeRepository.Create(entity);
        }

        public Task DeleteStyle(int styleId) => _styleRepository.Delete(styleId);
        public async Task<IEnumerable<Color>> GetColors()
        {
            var entities = await _colorRepository.Get();
            var model = _mapper.Map<IEnumerable<Color>>(entities);
            return model;
        }

        public async Task<IEnumerable<Size>> GetSizes()
        {
            var entities = await _sizeRepository.Get();
            var model = _mapper.Map<IEnumerable<Size>>(entities);
            return model;
        }
    }
}

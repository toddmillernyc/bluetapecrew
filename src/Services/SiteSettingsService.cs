using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Linq;
using System.Threading.Tasks;
using Entity = Repositories.Entities;

namespace Services
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly ISiteSettingsRepository _repository;
        private readonly IMapper _mapper;

        public SiteSettingsService(
            ISiteSettingsRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<SiteSetting> Get()
        {
            var siteSettings = await _repository.GetAll();
            var entity = siteSettings.OrderByDescending(x => x.Id).FirstOrDefault();
            var model = _mapper.Map<SiteSetting>(entity);
            return model;
        }

        public async Task<SiteSetting> Set(SiteSetting siteSetting)
        {
            var entity = _mapper.Map<Entity.SiteSetting>(siteSetting);
            entity.Id = 0;
            await _repository.Create(entity);
            var model = _mapper.Map<SiteSetting>(entity);
            return model;
        }
    }
}
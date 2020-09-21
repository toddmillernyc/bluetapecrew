using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Threading.Tasks;

namespace Services
{
    public class SiteSettingsService : ISiteSettingsService
    {
        private readonly ISiteSettingsRepository _repository;
        private readonly IMapper _mapper;
        private readonly ISiteProfileRepository _profileRepository;

        public SiteSettingsService(
            ISiteSettingsRepository repository,
            IMapper mapper,
            ISiteProfileRepository profileRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _profileRepository = profileRepository;
        }

        public async Task<SiteSetting> Get()
        {
            var entity = await _repository.Get();
            var model = _mapper.Map<SiteSetting>(entity);
            return model;
        }

        public async Task Set(SiteSetting siteSetting)
        {
            var entity = _mapper.Map<Entities.SiteSetting>(siteSetting);
            entity.Id = 0;
            await _repository.Create(entity);
        }

        public async Task<SiteProfile> GetSiteProfile()
        {
            var entity = await _profileRepository.Get();
            var model = _mapper.Map<SiteProfile>(entity);
            return model;
        }

        public async Task SetSiteProfile(SiteProfile siteProfile)
        {
            var entity = _mapper.Map<Entities.PublicSiteProfile>(siteProfile);
            entity.Id = 0;
            await _profileRepository.Set(entity);
        }
    }
}
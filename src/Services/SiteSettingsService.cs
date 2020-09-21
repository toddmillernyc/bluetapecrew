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

        public async Task<SiteSetting> Set(SiteSetting siteSetting)
        {
            await _repository.DeleteAll();
            var entity = _mapper.Map<Entities.SiteSetting>(siteSetting);
            await _repository.Create(entity);
            var model = _mapper.Map<SiteSetting>(entity);
            return model;
        }

        public async Task<SiteProfile> GetSiteProfile()
        {
            var entity = await _profileRepository.Get();
            var model = _mapper.Map<SiteProfile>(entity);
            return model;
        }
    }
}
using AutoMapper;
using Repositories.Interfaces;
using Services.Interfaces;
using Services.Models;
using System.Threading.Tasks;
using Entity = Repositories.Entities;

namespace Services
{
    public class GuestUserService : IGuestUserService
    {
        private readonly IGuestUserRepository _guestUserRepository;
        private readonly IMapper _mapper;

        public GuestUserService(IGuestUserRepository guestUserRepository,
            IMapper mapper)
        {
            _guestUserRepository = guestUserRepository;
            _mapper = mapper;
        }

        public async Task<GuestUser> FindBy(string sessionId)
        {
            var userEntity =  await _guestUserRepository.FindBy(sessionId);
            var user = _mapper.Map<GuestUser>(userEntity);
            return user;
        }

        public async Task Create(GuestUser user)
        {
            var entity = _mapper.Map<Entity.GuestUser>(user);
            await _guestUserRepository.Create(entity);
        }
    }
}

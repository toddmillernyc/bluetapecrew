using AutoMapper;
using BlueTapeCrew.Models;
using BlueTapeCrew.ViewModels;
using Entities;

namespace BlueTapeCrew.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<ApplicationUser, User>();
            CreateMap<CheckoutRequest, ApplicationUser>()
                .ForMember(x => x.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(x => x.City, opt => opt.MapFrom(s => s.City))
                .ForMember(x => x.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(x => x.PhoneNumber, opt => opt.MapFrom(s => s.Phone))
                .ForMember(x => x.PostalCode, opt => opt.MapFrom(s => s.PostalCode))
                .ForMember(x => x.State, opt => opt.MapFrom(s => s.State))
                .ForMember(x => x.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForAllOtherMembers(x => x.Ignore());
            CreateMap<CheckoutRequest, GuestUser>();
        }
    }
}

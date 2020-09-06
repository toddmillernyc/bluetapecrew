using AutoMapper;
using BlueTapeCrew.Areas.Admin.Models;
using BlueTapeCrew.Extensions;
using BlueTapeCrew.Identity;
using Microsoft.AspNetCore.Http;
using Services.Models;
using System.Linq;

namespace BlueTapeCrew.Mappings
{
    public class WebMappings : Profile
    {
        public WebMappings()
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
                .ForMember(x => x.UserName, opt => opt.MapFrom(s => s.UserName));

            CreateMap<ApplicationUser, CheckoutRequest>()
                .ForMember(x => x.Address, opt => opt.MapFrom(s => s.Address))
                .ForMember(x => x.City, opt => opt.MapFrom(s => s.City))
                .ForMember(x => x.Email, opt => opt.MapFrom(s => s.Email))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(s => s.FirstName))
                .ForMember(x => x.LastName, opt => opt.MapFrom(s => s.LastName))
                .ForMember(x => x.Phone, opt => opt.MapFrom(s => s.PhoneNumber))
                .ForMember(x => x.PostalCode, opt => opt.MapFrom(s => s.PostalCode))
                .ForMember(x => x.State, opt => opt.MapFrom(s => s.State))
                .ForMember(x => x.UserName, opt => opt.MapFrom(s => s.UserName))
                .ForAllOtherMembers(x => x.Ignore());

            CreateMap<IFormFile, SaveImageRequest>()
                .ForMember(x => x.ImageData, opt => opt.MapFrom(s => s.ToBytes()))
                .ForMember(x => x.FileName, opt => opt.MapFrom(s => s.FileName))
                .ForMember(x => x.ContentType, opt => opt.MapFrom(s => s.ContentType));

            CreateMap<Category, AdminCategoryViewModel>()
                .ForMember(x => x.Products, opt => opt
                    .MapFrom(s => s.ProductCategories
                                   .Select(x => x.Product)
                                   .OrderBy(product => product.ProductName)
                                   .Select(product => new AdminProductViewModel
                                   {
                                       Description = product.Description,
                                       Id = product.Id,
                                       ImageId = product.ImageId,
                                       Name = product.ProductName
                                   })));
        }
    }
}

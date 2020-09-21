using AutoMapper;
using Model = Services.Models;

namespace Services.Mappings
{
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            // Entity -> Model
            CreateMap<Entities.CartImage, Model.CartImage>();
            CreateMap<Entities.Cart, Model.Cart>();
            CreateMap<Entities.CartView, Model.CartView>();
            CreateMap<Entities.Category, Model.Category>();
            CreateMap<Entities.Color, Model.Color>();
            CreateMap<Entities.GuestUser, Model.GuestUser>();
            CreateMap<Entities.Image, Model.Image>();
            CreateMap<Entities.Order, Model.Order>();
            CreateMap<Entities.OrderItem, Model.OrderItem>();
            CreateMap<Entities.Product, Model.Product>();
            CreateMap<Entities.ProductCategory, Model.ProductCategory>();
            CreateMap<Entities.ProductImage, Model.ProductImage>();
            CreateMap<Entities.PublicSiteProfile, Model.SiteProfile>();
            CreateMap<Entities.Review, Model.Review>();
            CreateMap<Entities.SiteSetting, Model.SiteSetting>();
            CreateMap<Entities.Size, Model.Size>();
            CreateMap<Entities.Style, Model.Style>();
            CreateMap<Entities.StyleView, Model.StyleView>();

            // Model -> Entity
            CreateMap<Entities.CartImage, Model.CartImage>();
            CreateMap<Model.Cart, Entities.Cart>();
            CreateMap<Model.Category, Entities.Category>();
            CreateMap<Model.Color, Entities.Color>();
            CreateMap<Model.GuestUser, Entities.GuestUser>();
            CreateMap<Model.Order, Entities.Order>();
            CreateMap<Model.OrderItem, Entities.OrderItem>();
            CreateMap<Model.Product, Entities.Product>();
            CreateMap<Model.ProductCategory, Entities.ProductCategory>();
            CreateMap<Model.ProductImage, Entities.ProductImage>();
            CreateMap<Model.Review, Entities.Review>();
            CreateMap<Model.SiteProfile, Entities.PublicSiteProfile>();

            CreateMap<Model.SiteSetting, Entities.SiteSetting>();
            CreateMap<Model.Size, Entities.Size>();
            CreateMap<Model.Style, Entities.Style>();

            //Viewmodels & DTO's
            CreateMap<Model.CheckoutRequest, Model.GuestUser>();
        }
    }
}
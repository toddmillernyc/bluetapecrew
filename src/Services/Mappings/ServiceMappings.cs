using AutoMapper;
using Entity = Repositories.Entities;
using Model = Services.Models;

namespace Services.Mappings
{
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            // Entity -> Model
            CreateMap<Entity.CartImage, Model.CartImage>();
            CreateMap<Entity.Cart, Model.Cart>();
            CreateMap<Entity.CartView, Model.CartView>();
            CreateMap<Entity.Category, Model.Category>();
            CreateMap<Entity.Color, Model.Color>();
            CreateMap<Entity.GuestUser, Model.GuestUser>();
            CreateMap<Entity.Image, Model.Image>();
            CreateMap<Entity.Order, Model.Order>();
            CreateMap<Entity.OrderItem, Model.OrderItem>();
            CreateMap<Entity.Product, Model.Product>();
            CreateMap<Entity.ProductCategory, Model.ProductCategory>();
            CreateMap<Entity.ProductImage, Model.ProductImage>();
            CreateMap<Entity.Review, Model.Review>();
            CreateMap<Entity.SiteSetting, Model.SiteSetting>();
            CreateMap<Entity.Size, Model.Size>();
            CreateMap<Entity.Style, Model.Style>();
            CreateMap<Entity.StyleView, Model.StyleView>();

            // Model -> Entity
            CreateMap<Model.Cart, Entity.Cart>();
            CreateMap<Model.Category, Entity.Category>();
            CreateMap<Model.Color, Entity.Color>();
            CreateMap<Model.GuestUser, Entity.GuestUser>();
            CreateMap<Model.Order, Entity.Order>();
            CreateMap<Model.OrderItem, Entity.OrderItem>();
            CreateMap<Model.Product, Entity.Product>();
            CreateMap<Model.ProductCategory, Entity.ProductCategory>();
            CreateMap<Model.ProductImage, Entity.ProductImage>();
            CreateMap<Model.Review, Entity.Review>();
            CreateMap<Model.SiteSetting, Entity.SiteSetting>();
            CreateMap<Model.Size, Entity.Size>();
            CreateMap<Model.Style, Entity.Style>();

            //Viewmodels & DTO's
            CreateMap<Model.CheckoutRequest, Model.GuestUser>();
        }
    }
}
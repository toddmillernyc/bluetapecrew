using AutoMapper;

namespace Services.Mappings
{
    public class ServiceMappings : Profile
    {
        public ServiceMappings()
        {
            // Entity -> Model
            CreateMap<Entities.CartImage, Models.CartImage>();
            CreateMap<Entities.Cart, Models.Cart>();
            CreateMap<Entities.CartView, Models.CartView>();
            CreateMap<Entities.Category, Models.Category>();
            CreateMap<Entities.Color, Models.Color>();
            CreateMap<Entities.GuestUser, Models.GuestUser>();
            CreateMap<Entities.Image, Models.Image>();
            CreateMap<Entities.Order, Models.Order>();
            CreateMap<Entities.OrderItem, Models.OrderItem>();
            CreateMap<Entities.Product, Models.Product>();
            CreateMap<Entities.ProductCategory, Models.ProductCategory>();
            CreateMap<Entities.ProductImage, Models.ProductImage>();
            CreateMap<Entities.Review, Models.Review>();
            CreateMap<Entities.SiteSetting, Models.SiteSetting>();
            CreateMap<Entities.Size, Models.Size>();
            CreateMap<Entities.Style, Models.Style>();
            CreateMap<Entities.StyleView, Models.StyleView>();

            // Model -> Entity
            CreateMap<Models.Cart, Entities.Cart>();
            CreateMap<Models.Category, Entities.Category>();
            CreateMap<Models.Color, Entities.Color>();
            CreateMap<Models.GuestUser, Entities.GuestUser>();
            CreateMap<Models.Order, Entities.Order>();
            CreateMap<Models.OrderItem, Entities.OrderItem>();
            CreateMap<Models.Product, Entities.Product>();
            CreateMap<Models.ProductCategory, Entities.ProductCategory>();
            CreateMap<Models.ProductImage, Entities.ProductImage>();
            CreateMap<Models.Review, Entities.Review>();
            CreateMap<Models.SiteSetting, Entities.SiteSetting>();
            CreateMap<Models.Size, Entities.Size>();
            CreateMap<Models.Style, Entities.Style>();

            //Viewmodels & DTO's
            CreateMap<Models.CheckoutRequest, Models.GuestUser>();
        }
    }
}
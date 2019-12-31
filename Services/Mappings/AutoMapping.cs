using AutoMapper;
using System.Collections.Generic;
using Entity = Repositories.Entities;
using Model = Services.Models;

namespace Services.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            // Entity -> Model
            CreateMap<Entity.Category, Model.Category>();
            CreateMap<Entity.Image, Model.Image>();
            CreateMap<Entity.Order, Model.Order>();
            CreateMap<Entity.Product, Model.Product>();
            CreateMap<Entity.SiteSetting, Model.SiteSetting>();
            CreateMap<Entity.GuestUser, Model.GuestUser>();

            // Entity -> Model Collections
            CreateMap<List<Entity.CartView>, List<Model.CartView>>();
            CreateMap<IEnumerable<Entity.Category>, IEnumerable<Model.Category>>();
            CreateMap<IEnumerable<Entity.Color>, IEnumerable<Model.Color>>();
            CreateMap<IEnumerable<Entity.Order>, IEnumerable<Model.Order>>();
            CreateMap<IEnumerable<Entity.Product>, IEnumerable<Model.Product>>();
            CreateMap<IEnumerable<Entity.Size>, IEnumerable<Model.Size>>();
            CreateMap<IEnumerable<Entity.StyleView>, IEnumerable<Model.StyleView>>();

            // Model -> Entity
            CreateMap<Model.Cart, Entity.Cart>();
            CreateMap<Model.Category, Entity.Category>();
            CreateMap<Model.Color, Entity.Color>();
            CreateMap<Model.GuestUser, Entity.GuestUser>();
            CreateMap<Model.Order, Entity.Order>();
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
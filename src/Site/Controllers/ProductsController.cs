﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Models;
using Site.Services.Interfaces;

namespace Site.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICookieService _cookieService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IViewModelService _viewModelService;

        public ProductsController(IProductService productService,
                                  ICookieService cookieService,
                                  IHttpContextAccessor httpContextAccessor,
                                  IViewModelService viewModelService)
        {
            _productService = productService;
            _cookieService = cookieService;
            _httpContextAccessor = httpContextAccessor;
            _viewModelService = viewModelService;
        }

        [Route("products/{name}")]
        [HttpGet]
        public async Task<IActionResult> Details(string name)
        {
            var productViewModel = await _viewModelService.GetProductViewModel(name);
            if (productViewModel == null) return RedirectToAction("Error", "Home", new { statusCode = 404 });
            _cookieService.SetCurrentProduct(_httpContextAccessor.HttpContext, productViewModel.Id);
            _cookieService.SetCurrentCategory(_httpContextAccessor.HttpContext, productViewModel.Category);
            ViewBag.ReturnUrl = HttpContext.Request.Path.ToString();
            return View(productViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [Route("addreview")]
        public async Task<IActionResult> AddReview(Review review) => RedirectToAction("Details", new { name = await _productService.AddReview(review) });

        public async Task<string> GetStylePrice(int id) => await _productService.GetStylePrice(id);
    }
}
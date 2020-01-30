using AutoMapper;
using BlueTapeCrew.Extensions;
using BlueTapeCrew.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PayPal;
using Services.Interfaces;
using Services.Models;
using System;
using System.Threading.Tasks;
using OrderMsg = Services.Models.Constants.Orders;

namespace BlueTapeCrew.Controllers
{
    [RequireHttps]
    public class CheckoutController : Controller
    {
        private readonly bool _isSandbox;
        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;
        private readonly IMapper _mapper;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly ISessionService _session;

        public CheckoutController(
            ICartService cartService,
            ICheckoutService checkoutService,
            IOrderService orderService,
            IUserService userService,
            ISessionService session,
            IMapper mapper)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
            _orderService = orderService;
            _userService = userService;
            _session = session;
            _mapper = mapper;
#if DEBUG
            _isSandbox = true;
#endif
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var returnUrl = HttpContext.Request.Path.ToString();
            var user = await _userService.Find(User.Identity.Name);
            var sessionId = _session.SessionId();
            var checkoutModel = await _checkoutService.CreateCheckoutRequest(user, sessionId, returnUrl);
            return checkoutModel.Cart.IsEmpty ? View("EmptyCart") : View(checkoutModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutRequest model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                //todo, post cart data with model
                model.Cart = await _cartService.GetCartViewModel(_session.SessionId());
                model.UserName = User.Identity.Name;
                model.SessionId = _session.SessionId();
                if (User.Identity.IsAuthenticated)
                {
                    await _userService.UpdateUser(model);
                }
                else
                {
                    var guestUser = _mapper.Map<GuestUser>(model);
                    TryValidateModel(guestUser);
                    if (!ModelState.IsValid) return View(model);
                    await _userService.CreateGuestUser(guestUser);
                }

                var path = HttpContext.Request.GetDisplayUrl();
                var uri = new Uri(path);
                var redirectUrl = await _checkoutService.Start(model.SessionId, uri, _isSandbox);
                if (!string.IsNullOrEmpty(redirectUrl)) return Redirect(redirectUrl);
            }
            catch (PaymentsException ex)
            {
                return Content(ex.Response);
            }
            return View(model);
        }

        [Route("checkoutreview")]
        public async Task<ActionResult> CheckoutReview(string paymentId, string token, string payerId, string cancel = "false")
        {
            if (cancel == "true") return View("CheckoutCancel");
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(payerId))
                return RedirectToAction("Index", "Checkout");

            ViewBag.Token = token;
            ViewBag.PayerId = payerId;
            ViewBag.PaymentId = paymentId;

            var cart = await _cartService.GetCartViewModel(_session.SessionId());
            CheckoutRequest model;
            if (User.Identity.IsAuthenticated)
            {
                model = new CheckoutRequest(await _userService.Find(User.Identity.Name), cart, HttpContext.Request.Path.ToString());
            }
            else
            {
                var guestUser = await _userService.GetGuestUser(_session.SessionId());
                model = new CheckoutRequest(guestUser, cart);
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Complete(CompletePaymentRequest completePaymentRequest)
        {
            try
            {
                //todo: send cart data with order
                ViewBag.PaymentConfirmation = await _checkoutService.Complete(completePaymentRequest, _isSandbox);
                
                var cart = await _cartService.GetCartViewModel(_session.SessionId());
                var orderId = await _orderService.Create(await GetOrderModel(), cart);
                await _cartService.EmptyCart(_session.SessionId());
                return RedirectToAction("OrderConfirmation", "Checkout", new { id = orderId });
            }
            catch (PaymentsException ex)
            {
                return Content(ex.Response);
            }
        }

        private async Task<Order> GetOrderModel()
        {
            var order = new Order { IpAddress = Request.Host.Host, SessionId = _session.SessionId() };
            if (User.Identity.IsAuthenticated)
            {
                var user = await _userService.Find(User.Identity.Name);
                order.UpdateUser(user);
            }
            else
            {
                order.UpdateUser(await _userService.GetGuestUser(_session.SessionId()));
            }
            return order;
        }

        public async Task<ActionResult> OrderConfirmation(int id) => View(await _orderService.SendConfirmationEmail(id));

        public async Task<ActionResult> OrderError()
        {
            ModelState.AddModelError(string.Empty, OrderMsg.ErrorMessage);
            var cart = await _cartService.GetCartViewModel(_session.SessionId());
            return View(cart);
        }

        protected override void Dispose(bool disposing) { }
    }
}
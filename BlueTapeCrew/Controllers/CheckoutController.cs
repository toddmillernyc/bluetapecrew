using AutoMapper;
using BlueTapeCrew.Email;
using BlueTapeCrew.Extensions;
using BlueTapeCrew.Models;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using Entities;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using PayPal;
using System;
using System.Threading.Tasks;
using OrderMsg = BlueTapeCrew.Models.Constants.Orders;

namespace BlueTapeCrew.Controllers
{
    [RequireHttps]
    public class CheckoutController : Controller
    {

        private readonly bool _isSandbox;

        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;
        private readonly IEmailService _emailService;
        private readonly IOrderService _orderService;
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IUserService _userService;
        private readonly ISessionService _session;
        private readonly IMapper _mapper;

        public CheckoutController(
            ICartService cartService,
            ICheckoutService checkoutService,
            IEmailService emailService,
            IOrderService orderService,
            ISiteSettingsService siteSettingsService,
            IUserService userService,
            ISessionService session,
            IMapper mapper)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
            _emailService = emailService;
            _orderService = orderService;
            _siteSettingsService = siteSettingsService;
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
            var checkoutModel = await _checkoutService.CreateCheckoutRequest(User.Identity.Name, returnUrl);
            return checkoutModel.Cart.IsEmpty
                ? View("EmptyCart")
                : View(checkoutModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutRequest model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                model.Cart = await Cart;
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
            var model = User.Identity.IsAuthenticated
                ? new CheckoutRequest(await _userService.Find(User.Identity.Name), await Cart, HttpContext.Request.Path.ToString())
                : new CheckoutRequest(await GuestUser, await Cart);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Complete(CompletePaymentRequest completePaymentRequest)
        {
            try
            {
                ViewBag.PaymentConfirmation = await _checkoutService.Complete(completePaymentRequest, _isSandbox);
                var orderId = await _orderService.Create(await GetOrderModel(), await Cart);
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
                order.UpdateUser(await _userService.Find(User.Identity.Name));
            else
                order.UpdateUser(await GuestUser);
            return order;
        }

        public async Task<ActionResult> OrderConfirmation(int id)
        {
            var order = await _orderService.GetOrder(id);
            var emailRequest = await GetSmtpRequest(order);
            await _emailService.SendEmail(emailRequest);
            return View(order);
        }

        public async Task<ActionResult> OrderError()
        {
            ModelState.AddModelError(string.Empty, OrderMsg.ErrorMessage);
            return View(await Cart);
        }

        //private helper methods
        private Task<CartViewModel> Cart => _cartService.GetCartViewModel(_session.SessionId());
        private async Task<SmtpRequest> GetSmtpRequest(Order order)
        {
            var settings = await _siteSettingsService.Get();
            var textBody = EmailTemplates.GetOrderConfirmationTextBody(order, User.Identity.IsAuthenticated);
            var htmlBody = EmailTemplates.GetOrderConfirmationHtmlBody(order);
            return new SmtpRequest(settings, htmlBody, textBody, order.Email, OrderMsg.EmailSubject);
        }

        private Task<GuestUser> GuestUser => _userService.GetGuestUser(_session.SessionId());
        protected override void Dispose(bool disposing) { }
    }
}
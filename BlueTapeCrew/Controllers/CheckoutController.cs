using BlueTapeCrew.Extensions;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.ViewModels;
using PayPal;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlueTapeCrew.Email;

namespace BlueTapeCrew.Controllers
{
    [RequireHttps]
    public class CheckoutController : Controller
    {
        private const string OrderErrorMessage = "Your order was not placed, there was an issue.  Please contact us.";
        private readonly bool _isSandbox;

        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;
        private readonly IEmailService _emailService;
        private readonly IOrderService _orderService;
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IUserService _userService;

        public CheckoutController(
            ICartService cartService,
            ICheckoutService checkoutService,
            IEmailService emailService,
            IOrderService orderService,
            ISiteSettingsService siteSettingsService,
            IUserService userService)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
            _emailService = emailService;
            _orderService = orderService;
            _siteSettingsService = siteSettingsService;
            _userService = userService;
#if DEBUG
            _isSandbox = true;
#endif
        }

        [HttpGet]
        public async Task<ViewResult> Index()
        {
            var cart = await Cart;
            if (cart.IsEmpty) return View("EmptyCart");
            var model = new CheckoutViewModel(await GetUserBy(User.Identity.Name), cart, HttpContext.Request.Url?.ToString());
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            try
            {
                model.UserName = User.Identity.Name;
                model.SessionId = Session.SessionID;
                if (Request.IsAuthenticated) await _userService.UpdateUser(model);
                else await _userService.CreateGuestUser(model);
                var redirectUrl = await _checkoutService.Start(Session.SessionID, HttpContext.Request.Url, _isSandbox);
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
            var model = Request.IsAuthenticated
                ? new CheckoutViewModel(await GetUserBy(User.Identity.Name), await Cart, HttpContext.Request.Url?.ToString())
                : new CheckoutViewModel(await GuestUser, await Cart);
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
                await _cartService.EmptyCart(Session.SessionID);
                return RedirectToAction("OrderConfirmation", "Checkout", new { id = orderId });
            }
            catch (PaymentsException ex)
            {
                return Content(ex.Response);
            }
        }

        private async Task<Order> GetOrderModel()
        {
            var order = new Order { IpAddress = Request.UserHostAddress, SessionId = Session.SessionID };
            if (Request.IsAuthenticated)
                order.UpdateUser(await GetUserBy(User.Identity.Name));
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
            ViewBag.Message = OrderErrorMessage;
            return View(await Cart);
        }

        //private helper methods
        private Task<CartViewModel> Cart => _cartService.GetCartViewModel(Session?.SessionID);
        private async Task<SmtpRequest> GetSmtpRequest(Order order)
        {
            var settings = await _siteSettingsService.Get();
            var textBody = EmailTemplates.GetOrderConfirmationTextBody(order, User.Identity.IsAuthenticated);
            var htmlBody = EmailTemplates.GetOrderConfirmationHtmlBody(order);
            return new SmtpRequest(settings, htmlBody, textBody, order.Email);
        }
        private Task<AspNetUser> GetUserBy(string name) => _userService.GetUserByName(name);
        private Task<GuestUser> GuestUser => _userService.GetGuestUser(Session.SessionID);
    }
}
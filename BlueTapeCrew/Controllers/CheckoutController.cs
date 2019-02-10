using System.Linq;
using BlueTapeCrew.Extensions;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;
using BlueTapeCrew.Utils;
using BlueTapeCrew.ViewModels;
using PayPal;
using System.Threading.Tasks;
using System.Web.Mvc;

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

        public CheckoutController(
            ICartService cartService,
            ICheckoutService checkoutService,
            IUserService userService,
            IEmailService emailService,
            IOrderService orderService,
            ISiteSettingsService siteSettingsService)
        {
            _cartService = cartService;
            _userService = userService;
            _emailService = emailService;
            _orderService = orderService;
            _checkoutService = checkoutService;
            _siteSettingsService = siteSettingsService;
#if DEBUG
            _isSandbox = true;
#endif
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var cart = await _cartService.GetCartViewModel(Session.SessionID);
            if (cart.IsEmpty) return View("EmptyCart");
            var user = await _userService.GetUserByName(User.Identity.Name);
            var model = new CheckoutViewModel(user, cart);
            ViewBag.ReturnUrl = HttpContext.Request.Url?.ToString();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.UserName = User.Identity.Name;
                    model.SessionId = Session.SessionID;
                    if (Request.IsAuthenticated) await _userService.UpdateUser(model);
                    else await _userService.CreateGuestUser(model);
                    var redirectUrl = await _checkoutService.Start(Session.SessionID, HttpContext.Request.Url, _isSandbox);
                    if (!string.IsNullOrEmpty(redirectUrl)) Response.Redirect(redirectUrl, false);
                }
                catch (PaymentsException ex)
                {
                    return Content(ex.Response);
                }
            }
            ViewBag.Errors = true;
            return View(model);
        }

        [Route("checkoutreview")]
        public async Task<ActionResult> CheckoutReview(string paymentId, string token, string payerId, string cancel = "false")
        {
            if (cancel == "true") return RedirectToAction("CheckoutCancel");
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(payerId))
                return RedirectToAction("Index", "Checkout");

            ViewBag.Token = token;
            ViewBag.PayerId = payerId;
            ViewBag.PaymentId = paymentId;
            var cart = await _cartService.GetCartViewModel(Session.SessionID);
            var model = Request.IsAuthenticated
                ? new CheckoutViewModel(await _userService.GetUserByName(User.Identity.Name), cart)
                : new CheckoutViewModel(await _userService.GetGuestUser(Session.SessionID), cart);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Complete(CompletePaymentRequest completePaymentRequest)
        {
            try
            {
                ViewBag.PaymentConfirmation = await _checkoutService.Complete(completePaymentRequest, _isSandbox);
                var cartViewModel = await _cartService.GetCartViewModel(Session.SessionID);
                var orderId = await _orderService.Create(await GetOrderModel(), cartViewModel);
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
                order.UpdateUser(await _userService.GetUserByName(User.Identity.Name));
            else
                order.UpdateUser(await _userService.GetGuestUser(Session.SessionID));
            return order;
        }

        public ActionResult CheckoutCancel()
        {
            return View();
        }

        public async Task<ActionResult> OrderConfirmation(int id)
        {
            var order = await _orderService.GetOrder(id);
            var emailRequest = await GetSmtpRequest(order);
            await _emailService.SendEmail(emailRequest);
            return View(order);
        }

        private async Task<SmtpRequest> GetSmtpRequest(Order order)
        {
            var settings = await _siteSettingsService.Get();
            var textBody = EmailTemplates.GetOrderConfirmationTextBody(order, User.Identity.IsAuthenticated);
            var htmlBody = EmailTemplates.GetOrderConfirmationHtmlBody(order);
            return new SmtpRequest(settings, htmlBody, textBody, order.Email);
        }

        public async Task<ActionResult> OrderError()
        {
            ViewBag.Message = "Your order was not placed, there was an issue.  Please contact us.";
            return View(await _cartService.GetCartViewModel(Session.SessionID));
        }
    }
}
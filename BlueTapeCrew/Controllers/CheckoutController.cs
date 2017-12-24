using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using BlueTapeCrew.Contracts.Services;
using BlueTapeCrew.Models;
using BlueTapeCrew.Paypal;
using BlueTapeCrew.Utils;
using BlueTapeCrew.ViewModels;
using PayPal;

namespace BlueTapeCrew.Controllers
{
    [RequireHttps]
    public class CheckoutController : Controller
    {
        private readonly bool _isSandbox;

        private readonly ICartService _cartService;
        private readonly IInvoiceService _invoiceService;
        private readonly IOrderService _orderService;
        private readonly IPaypalService _paypalService;
        private readonly ISiteSettingsService _siteSettingsService;
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;

        public CheckoutController(
            IUserService userService,
            ICartService cartService,
            IEmailService emailService,
            IOrderService orderService,
            IPaypalService paypalService,
            ISiteSettingsService siteSettingsService,
            IInvoiceService invoiceService)
        {
            _cartService = cartService;
            _userService = userService;
            _orderService = orderService;
            _invoiceService = invoiceService;
            _paypalService = paypalService;
            _siteSettingsService = siteSettingsService;
            _emailService = emailService;
#if DEBUG
            _isSandbox = true;
#endif
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var cart = await _cartService.GetCartViewModel(Session.SessionID);
            if (cart == null) return RedirectToAction("EmptyCart");
            var user = await _userService.GetUserByName(User.Identity.Name);
            var model = new CheckoutViewModel(user, cart);
            ViewBag.ReturnUrl = HttpContext.Request.Url?.ToString();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CheckoutViewModel model,string itemamt,string shipping,string amt)
        {
            if (ModelState.IsValid)
            {
                if (Request.IsAuthenticated)
                {
                    await
                        _userService.UpdateUser(User.Identity.Name, model.FirstName, model.LastName, model.Address,
                            model.City, model.State, model.Zip, model.Phone, model.Email);
                }
                else
                {
                    await
                        _userService.CreateGuestUser(Session.SessionID, model.FirstName, model.LastName, model.Address,
                            model.City, model.State, model.Zip, model.Phone, model.Email);
                }

                var settings = await _siteSettingsService.Get();
                var cart = await _cartService.GetCartViewModel(Session.SessionID);
                
                //todo: impliment token store and get token if not expired
                var accessToken = string.Empty;

                var invoice = await _invoiceService.Create(Session.SessionID);

                try
                {
                    var paymentRequest = new PaymentRequest(HttpContext.Request.Url, settings, cart.Items, invoice.Id, accessToken, _isSandbox);
                    var redirectUrl = _paypalService.PaywithPaypal(paymentRequest);
                    if (!string.IsNullOrEmpty(redirectUrl)) Response.Redirect(redirectUrl);
                }
                catch (PaymentsException ex)
                {
                    return Content(ex.Response);
                }
            }
            ViewBag.Errors = true;
            model.Cart = await _cartService.GetCartViewModel(Session.SessionID);
            return View(model);
        }

        public ViewResult EmptyCart()
        {
            return View();
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
            var model = new CheckoutViewModel();
            if (Request.IsAuthenticated)
            {
                var user = await _userService.GetUserByName(User.Identity.Name);
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.Phone = user.PhoneNumber;
                model.Address = user.Address;
                model.City = user.City;
                model.State = user.State;
                model.Zip = user.PostalCode;
            }
            else
            {
                var user = await _userService.GetGuestUser(Session.SessionID);
                model.FirstName = user.FirstName;
                model.LastName = user.LastName;
                model.Email = user.Email;
                model.Phone = user.PhoneNumber;
                model.Address = user.Address;
                model.City = user.City;
                model.State = user.State;
                model.Zip = user.PostalCode;
            }
            model.Cart = await _cartService.GetCartViewModel(Session.SessionID);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Complete(CompletePaymentRequest completePaymentRequest)
        {
            try
            {
                var clientId = "";
                var clientSecret = "";
                var settings = await _siteSettingsService.Get();

                if (_isSandbox)
                {
                    clientId = settings.PaypalSandBoxClientId;
                    clientSecret = settings.PaypalSandBoxSecret;
                }
                else
                {
                    clientId = settings.PaypalClientId;
                    clientSecret = settings.PaypalClientSecret;
                }
                completePaymentRequest.Token = _paypalService.GetAccessToken(clientId, clientSecret, _isSandbox ? "sandbox" : "mode");
                var completedPayment = _paypalService.CompletePayment(completePaymentRequest);
                ViewBag.PaymentConfirmation = completedPayment.id;
            }
            catch (PaymentsException ex)
            {
                return Content(ex.Response);
            }



           var cartView = await _cartService.Get(Session.SessionID);
            var subtotal = cartView.Sum(x => x.SubTotal);
            var shipping = 0;
            if (subtotal < 50)
            {
                shipping = 6;
            }
                    
            var total = subtotal + shipping;

            var order = new Order
            {
                IpAddress = Request.UserHostAddress,
                SessionId = Session.SessionID,
                Shipping = shipping,
                SubTotal = subtotal,
                Total = total,
                DateCreated = DateTime.UtcNow
            };

            if (Request.IsAuthenticated)
            {
                var user = await _userService.GetUserByName(User.Identity.Name);
                if (user != null)
                {
                    order.UserName = user.UserName;
                    order.Email = user.Email;
                    order.FirstName = user.FirstName;
                    order.LastName = user.LastName;
                    order.Address = user.Address;
                    order.City = user.City;
                    order.State = user.State;
                    order.Zip = user.PostalCode;
                    order.Phone = user.PhoneNumber;
                }
            }
            else
            {
                var user = await _userService.GetGuestUser(Session.SessionID);
                order.Email = user.Email;
                order.FirstName = user.FirstName;
                order.LastName = user.LastName;
                order.Address = user.Address;
                order.City = user.City;
                order.State = user.State;
                order.Zip = user.PostalCode;
                order.Phone = user.PhoneNumber;
            }

            order.OrderItems = new List<OrderItem>();
            foreach (var item in cartView.ToList())
            {
                order.OrderItems.Add(new OrderItem
                {
                    Description = item.ProductName + " " + item.StyleText,
                    Price = item.Price,
                    SubTotal = item.SubTotal,
                    Quantity = item.Quantity
                });
            }
            await _orderService.AddOrder(order);
            await _cartService.EmptyCart(Session.SessionID);
            return RedirectToAction("OrderConfirmation", "Checkout", new { id = order.Id });
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
            return View( await _cartService.GetCartViewModel(Session.SessionID));
        }
    }
}
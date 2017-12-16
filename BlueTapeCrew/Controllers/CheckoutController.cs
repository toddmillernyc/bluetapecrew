using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BlueTapeCrew.Interfaces;
using BlueTapeCrew.Models;
using BlueTapeCrew.Utils;
using BlueTapeCrew.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace BlueTapeCrew.Controllers
{
    [RequireHttps]
    public class CheckoutController : Controller
    {
        private readonly ICartService _cartService;
        private readonly ICheckoutService _checkoutService;
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IPaypalService _paypalService;

        private ApplicationUserManager _userManager;

        public CheckoutController(
            ICheckoutService checkoutService,
            IUserService userService,
            ICartService cartService,
            IOrderService orderService,
            IPaypalService paypalService,
            ApplicationUserManager userManager)
        {
            _cartService = cartService;
            _checkoutService = checkoutService;
            _userService = userService;
            _orderService = orderService;
            _paypalService = paypalService;
            _userManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var cart = await _cartService.GetCartViewModel(Session.SessionID);
            if (cart == null) return RedirectToAction("EmptyCart");
            var user = await _userService.GetUserByName(User.Identity.Name);
            var model = new CheckoutViewModel(user, cart);
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
                var retMsg = "";
                var token = "";

                if (amt != null)
                {
                    var ret = _paypalService.ShortcutExpressCheckout(itemamt, shipping, amt, ref token, ref retMsg, Session.SessionID);
                    if (ret)
                    {
                        Session["token"] = token;
                        Response.Redirect(retMsg);
                    }
                    else
                    {
                        return Content("CheckoutError?" + retMsg);
                    }
                }
                else
                {
                    return Content("CheckoutError?ErrorCode=AmtMissing");
                }
                return Content("something went wrong");
            }
            ViewBag.Errors = true;
            model.Cart = await _cartService.GetCartViewModel(Session.SessionID);
            return View(model);
        }

        public ViewResult EmptyCart()
        {
            return View();
        }

        public async Task<ActionResult> CheckoutReview(string token, string payerId)
        {
            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(payerId))
                return RedirectToAction("Index", "Checkout");

            ViewBag.Token = token;
            ViewBag.PayerId = payerId;
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
        public async Task<ActionResult> CheckoutReview(string finalPaymentAmount, string token, string payerId)
        {
            var decoder = new NvpCodec();
            var retMsg = "";
            var ret = _paypalService.DoCheckoutPayment(finalPaymentAmount, token, payerId, ref decoder, ref retMsg);
            if (!ret) return Content(retMsg);

            // Retrieve PayPal confirmation value.
            var paymentConfirmation = decoder["PAYMENTINFO_0_TRANSACTIONID"];
            ViewBag.PaymentConfirmation = paymentConfirmation;

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
            await _orderService.AddOrder(order);
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
            await _cartService.EmptyCart(Session.SessionID);
            return RedirectToAction("OrderConfirmation", "Checkout", new { id = order.Id });
        }
        
        public ActionResult CheckoutCancel()
        {
            return View();
        }


        public async  Task<ActionResult> OrderConfirmation(int id)
        {
            using (var db = new BtcEntities())
            {
                var mailSettings = await db.MailSettings.FirstOrDefaultAsync();

                var adminEmail = mailSettings.Username;
                var order = await _orderService.GetOrder(id);

                var myMessage = new IdentityMessage();
                myMessage.Destination = order.Email;
                myMessage.Subject = "Your BlueTapeCrew.com order";

                myMessage.Body = "Your BlueTapeCrew.com order has been placed.\r\n\r\n Shipping Info:\r\n\r\n";
                myMessage.Body += order.FirstName + " " + order.LastName + "\r\n";
                myMessage.Body += order.Email + "\r\n";
                myMessage.Body += order.Phone + "\r\n";
                myMessage.Body += order.Address + "\r\n";
                myMessage.Body += order.City + ", " + order.State + " " + order.Zip + "\r\n\r\n";

                myMessage.Body += "Items: ";

                foreach (var item in order.OrderItems)
                {
                    myMessage.Body += "Item: " + item.Description + "\r\n";
                    myMessage.Body += "Price: " + item.Price + "\r\n";
                    myMessage.Body += "Quantity: " + item.Quantity + "\r\n";
                }

                myMessage.Body += "\r\n\r\n";
                myMessage.Body += "Subtotal: " + order.SubTotal;
                myMessage.Body += "Shipping: " + order.Shipping;
                myMessage.Body += "Total: " + order.Total;
                myMessage.Body += "\r\n\r\n";
                if (Request.IsAuthenticated)
                {
                    myMessage.Body += "Check your order status at https://bluetapecrew.com";
                }
                else
                {
                    myMessage.Body += "email info@bluetapecrew.com for order status.";
                }
                const string br = "<br/>";
                const string sp = " ";

                var html = "<h1>BlueTapeCrew.com Order #" + order.Id + "</h1>";
                html += "<h2>Shipping Info:</h2>";
                html += "<p>" + order.FirstName + sp + order.LastName + br;
                html += order.Email + br;
                html += order.Phone + br;
                html += order.Address + br;
                html += order.City + ", " + order.State + sp + order.Zip + "</p>";


                foreach (var item in order.OrderItems)
                {
                    html += "<h3> Order Items:</h3>";
                    html += "<b>" + item.Description + "</b>";
                    html += "<p>" + item.Quantity + "</p>";
                    html += "<p>Price: " + $"{item.Price:n2}" + "</p>";
                    html += "<p>Subtotal: " + $"{item.SubTotal:n2}" + "</p>";
                }
                html += br + br;
                html += "<p>" + " Order Subototal: " + $"{order.SubTotal:n2}" + "</p>";
                html += "<p>" + "Shipping: " + $"{order.Shipping:n2}" + "</p>";
                html += "<p>" + "Total: " + $"{order.Total:n2}" + "</p>";

                html += "<p>Check your order status at <a href='https://bluetapecrew.com/account'>bluetapecrew.com/account</a> or email <a href='mailto:info@bluetapecrew.com'>info@bluetapecrew.com</a></p>";

                myMessage.Body = html;
                await UserManager.SmsService.SendAsync(myMessage);

                return View(order);
            }

        }

        public async Task<ActionResult> OrderError()
        {
            ViewBag.Message = "Your order was not placed, there was an issue.  Please contact us.";
            return View( await _cartService.GetCartViewModel(Session.SessionID));
        }
    }
}
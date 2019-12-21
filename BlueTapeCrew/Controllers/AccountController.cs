using System;
using BlueTapeCrew.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using BlueTapeCrew.Models;
using BlueTapeCrew.Models.Entities;
using BlueTapeCrew.Services.Interfaces;

namespace BlueTapeCrew.Controllers
{
    [Authorize]
    [RequireHttps]
    public class AccountController : Controller
    {
        private const string LoginFailMessage = "Invalid login attempt.";

        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailSender;
        private readonly ISiteSettingsService _settings;

        public AccountController(
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IEmailService emailSender,
            ISiteSettingsService settings,
            RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailSender = emailSender;
            _settings = settings;
            var role = new IdentityRole
            {
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            _ = roleManager.CreateAsync(role).Result;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Require the user to have a confirmed email before they can log on.
                // var user = await UserManager.FindByNameAsync(model.Email);
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (!await _userManager.IsEmailConfirmedAsync(user))
                    {
                        await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account-Resend");

                        // Uncomment to debug locally  
                        //ViewBag.Link = callbackUrl;
                        ViewBag.errorMessage =
                            "Please confirm your email before logging in.  The email has been re-sent to your account.";
                        return View("Error");
                    }
                }
                else
                {
                    ModelState.AddModelError("", LoginFailMessage);
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to lockOutOnFailure: true
                var result =
                    await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (string.IsNullOrEmpty(returnUrl) || returnUrl.Contains("confirmemail"))
                        return RedirectToAction("Index", "Home");
                    return !Url.IsLocalUrl(returnUrl)
                        ? Redirect(returnUrl)
                        : RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut) return View("Lockout");
                if (result.Succeeded == false) ModelState.AddModelError("", LoginFailMessage);
                return View(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                //  Comment the following line to prevent log in until the user is confirmed.
                //  await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account");


                ViewBag.Message = "Check your email and confirm your account, you must be confirmed "
                                  + "before you can log in.";

                return View("Info");
                //return RedirectToAction("Index", "Home");
            }
            AddErrors(result);


            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                if (Request.Path == null) return RedirectToAction("ForgotPasswordConfirmation", "Account");
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code }, Request.Scheme);
                var settings = await _settings.Get();
                var htmlBody = $"Please reset your password by clicking <a href=\"{callbackUrl}\">here</a>";
                await _emailSender.SendEmail(new SmtpRequest(settings, htmlBody, htmlBody, user.UserName));
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await _userManager.ResetPasswordAsync(user, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        private async Task SendEmailConfirmationTokenAsync(string userId, string subject)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.Action("ConfirmEmail", "Account",
               new {userId, code }, protocol: Request?.Scheme);
            var settings = await _settings.Get();
            var htmlBody = $"Please confirm your account by clicking <a href=\"{callbackUrl}\">here</a>";
            await _emailSender.SendEmail(new SmtpRequest(settings, htmlBody, htmlBody, user.Email));
        }

        protected override void Dispose(bool disposing)
        {
            _userManager?.Dispose();
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(error.Code, error.Description);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
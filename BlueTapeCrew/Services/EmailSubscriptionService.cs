using System;
using BlueTapeCrew.Interfaces;
using MailChimp;
using MailChimp.Helper;

namespace BlueTapeCrew.Services
{
    public class EmailSubscriptionService : IEmailSubscriptionService
    {
        public string Subscribe(string email)
        {
            var mc = new MailChimpManager("b747576e31c781186892432095f6c7d9-us10");
            var mcEmail = new EmailParameter { Email = email };
            try
            {
                var results = mc.Subscribe("a663c9f79b", mcEmail);
                return results.Email;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }

}
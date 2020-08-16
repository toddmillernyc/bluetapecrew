namespace Services.Models
{
    public static class Constants
    {
        public static class Account
        {
            public const string ConfirmationCodeCantBeNullError = "Confirmation code can't be null";
            public const string EmailConfirmationError = "There was an error confirming your email";
            public const string EmailConfirmationLinkSentMessage = "Check your email and confirm your account, you must be confirmed before you can log in.";
            public const string LoginFailMessage = "Invalid login attempt.";
            public const string ResetPasswordEmailSubject = "Reset your bluetapecrew.com password";
            public const string ResetPasswordError = "There was an error resetting the password or the user was not found";
            public const string SendPasswordRestLinkError = "There user was not found or there was an error sending the password reset email";
            public const string UserIdCantBeNullError = "User Id Can't be null";
            public const string UnconfirmedEmailMessage = "Please confirm your email before logging in.  The email has been re-sent to your account.";
        }

        public static class Orders
        {
            public const string ErrorMessage = "Your order was not placed, there was an issue.  Please contact us.";
            public const string EmailSubject = "Your BlueTapeCrew.com order";
        }
    }
}

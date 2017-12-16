using System.Collections.Generic;
using BlueTapeCrew.Paypal.Models;

namespace BlueTapeCrew.Tests.Stubs
{
    public static class PaypalStubs
    {
        public static Payment TestPayment => new Payment
        {
            Intent = "sale",
            Payer = new Payer("paypal"),
            Transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        Amount = new Amount
                        {
                            Total = "30.11",
                            Currency = "USD",
                            Details = new Details
                            {
                                Subtotal = "30.00",
                                Tax = "0.07",
                                Shipping = "0.03",
                                HandlingFee = "1.00",
                                ShippingDiscount = "-1.00",
                                Insurance = "0.01"
                            }
                        },
                        Description = "The payment transaction description.",
                        Custom = "EBAY_EMS_90048630024435",
                        InvoiceNumber = "48787589673",
                        PaymentOptions = new PaymentOptions
                        {
                            AllowedPaymentMethod = "INSTANT_FUNDING_SOURCE"
                        },
                        SoftDescriptor = "ECHI5786786",
                        ItemList = new ItemList
                        {
                            Items = new List<Item>
                            {
                                new Item
                                {
                                    Name = "hat",
                                    Description = "Brown hat.",
                                    Quantity = "5",
                                    Price = "3",
                                    Tax = "0.01",
                                    Sku = "1",
                                    Currency = "USD"
                                },
                                new Item
                                {
                                    Name = "handbag",
                                    Description = "Black handbag.",
                                    Quantity = "1",
                                    Price = "15",
                                    Tax = "0.02",
                                    Sku = "product34",
                                    Currency = "USD"
                                }
                            },
                            ShippingAddress = new ShippingAddress
                            {
                                RecipientName = "Brian Robinson",
                                Line1 = "4th Floor",
                                Line2 = "Unit #34",
                                City = "San Jose",
                                CountryCode = "US",
                                PostalCode = "95131",
                                Phone = "011862212345678",
                                State = "CA"
                            }
                        }
                    }
                },
            NoteToPayer = "Contact us for any questions on your order.",
            RedirectUrls = new RedirectUrls
            {
                ReturnUrl = "https://www.example.com/return",
                CancelUrl = "https://www.example.com/cancel"
            }
        };

    }
}

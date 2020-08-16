using Services.Models;
using System.Collections.Generic;

namespace Stubs
{
    public static class OrderStubs
    {
        public static IEnumerable<Order> Orders(string email)
        {
            return new List<Order> { new Order { Id = 1, Email = email } };
        }
    }
}

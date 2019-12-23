using Entities;
using System.Collections.Generic;

namespace Btc.Tests.Stubs
{
    public static class OrderStubs
    {
        public static IEnumerable<Order> Orders(string email)
        {
            return new List<Order> { new Order { Id = 1, Email = email } };
        }
    }
}

using Entities;

namespace BlueTapeCrew.Email
{
    public static class EmailTemplates
    {
        public static string GetOrderConfirmationHtmlBody(Order order)
        {
            const string br = "<br/>";
            const string sp = " ";

            var html = "<h1>Your BlueTapeCrew.com Order #" + order.Id + " Has Been Placed</h1>";
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
            return html;
        }

        public static string GetOrderConfirmationTextBody(Order order, bool isAuthenticated)
        {
            var message = "Your BlueTapeCrew.com order has been placed.\r\n\r\n Shipping Info:\r\n\r\n";
            message += order.FirstName + " " + order.LastName + "\r\n";
            message += order.Email + "\r\n";
            message += order.Phone + "\r\n";
            message += order.Address + "\r\n";
            message += order.City + ", " + order.State + " " + order.Zip + "\r\n\r\n";

            message += "Items: ";

            foreach (var item in order.OrderItems)
            {
                message += "Item: " + item.Description + "\r\n";
                message += "Price: " + item.Price + "\r\n";
                message += "Quantity: " + item.Quantity + "\r\n";
            }

            message += "\r\n\r\n";
            message += "Subtotal: " + order.SubTotal;
            message += "Shipping: " + order.Shipping;
            message += "Total: " + order.Total;
            message += "\r\n\r\n";
            if (isAuthenticated)
                message += "Check your order status at https://bluetapecrew.com";
            else
                message += "email info@bluetapecrew.com for order status.";
            return message;
        }
    }
}
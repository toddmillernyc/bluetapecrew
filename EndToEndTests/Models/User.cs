using System;

namespace EndToEndTests.Models
{
    public class User
    {
        public User()
        {
            var id = Guid.NewGuid().ToString().Substring(0, 5);
            Email = $"BTC-integration-test-user-{id}@mailinator.com";
            Password = "A" + Guid.NewGuid().ToString().Substring(5);
        }
        
        public string Email { get; }
        public string Password { get; }
    }
}

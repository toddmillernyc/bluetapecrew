using System;
using React.Site.Models;

namespace React.Site.GraphQL
{
    public class Mutation
    {
        public TokenResponse Login(string email, string password)
        {
            return new TokenResponse
            {
                Token = Guid.NewGuid() + email
            };
        }
    }
}

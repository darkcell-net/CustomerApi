using System;

namespace CustomerRepository
{
    public class IdentityGenerator : IIdentityGenerator
    {
        public string GenerateId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
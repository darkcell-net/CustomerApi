using CustomerRepository;
using System.Collections.Generic;

namespace CustomerApi.Tests.TestDoubles
{
    public class FakeIdentityGenerator : IIdentityGenerator
    {
        private Queue<string> _identities = new Queue<string>();

        public string GenerateId()
        {
            return _identities.Dequeue();
        }

        public FakeIdentityGenerator WithGeneratedIdentities(params string[] identities)
        {
            _identities = new Queue<string>(identities);

            return this;
        }
    }
}
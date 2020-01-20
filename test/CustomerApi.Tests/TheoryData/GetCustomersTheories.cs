using System;
using Xunit;
using Api = CustomerApi.Models;
using Entities = CustomerRepository.Entities;

namespace CustomerApi.Tests.TheoryData
{
    public class GetCustomersTheories : TheoryData<GetCustomersTheoryData>
    {
        public GetCustomersTheories()
        {
            AddMatchOnFirstName();
            AddMatchOnLastName();
            AddMatchOnBothFirstAndLastName();
        }

        private void AddMatchOnFirstName()
        {
            const string firstName = nameof(firstName);
            const string lastName = nameof(lastName);
            string id1 = Guid.NewGuid().ToString();
            string id2 = Guid.NewGuid().ToString();
            string id3 = Guid.NewGuid().ToString();
            const string notMatched1 = nameof(notMatched1);
            const string notMatched2 = nameof(notMatched2);
            DateTime dateOfBirth1 = DateTime.UtcNow;
            DateTime dateOfBirth2 = dateOfBirth1.AddDays(-1);

            Add(new GetCustomersTheoryData
            {
                FirstName = firstName,
                LastName = lastName,
                ExistingCustomers = new Entities.Customer[]
                    {
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = firstName,
                            Id = id1,
                            LastName = notMatched1
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = firstName,
                            Id = id2,
                            LastName = notMatched2
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = "no match",
                            Id = id3,
                            LastName = "no match"
                        }
                    },
                ExpectedResponse = new Api.Customer[]
                    {
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = firstName,
                            Id = id1,
                            LastName = notMatched1
                        },
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = firstName,
                            Id = id2,
                            LastName = notMatched2
                        }
                    }
            });
        }

        private void AddMatchOnLastName()
        {
            const string firstName = nameof(firstName);
            const string lastName = nameof(lastName);
            string id1 = Guid.NewGuid().ToString();
            string id2 = Guid.NewGuid().ToString();
            string id3 = Guid.NewGuid().ToString();
            const string notMatched1 = nameof(notMatched1);
            const string notMatched2 = nameof(notMatched2);
            DateTime dateOfBirth1 = DateTime.UtcNow;
            DateTime dateOfBirth2 = dateOfBirth1.AddDays(-1);

            Add(new GetCustomersTheoryData
            {
                FirstName = firstName,
                LastName = lastName,
                ExistingCustomers = new Entities.Customer[]
                    {
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = notMatched1,
                            Id = id1,
                            LastName = lastName
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = notMatched2,
                            Id = id2,
                            LastName = lastName
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = "no match",
                            Id = id3,
                            LastName = "no match"
                        }
                    },
                ExpectedResponse = new Api.Customer[]
                    {
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = notMatched1,
                            Id = id1,
                            LastName = lastName
                        },
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = notMatched2,
                            Id = id2,
                            LastName = lastName
                        }
                    }
            });
        }

        private void AddMatchOnBothFirstAndLastName()
        {
            const string firstName = nameof(firstName);
            const string lastName = nameof(lastName);
            string id1 = Guid.NewGuid().ToString();
            string id2 = Guid.NewGuid().ToString();
            string id3 = Guid.NewGuid().ToString();
            DateTime dateOfBirth1 = DateTime.UtcNow;
            DateTime dateOfBirth2 = dateOfBirth1.AddDays(-1);

            Add(new GetCustomersTheoryData
            {
                FirstName = firstName,
                LastName = lastName,
                ExistingCustomers = new Entities.Customer[]
                    {
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = firstName,
                            Id = id1,
                            LastName = lastName
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = firstName,
                            Id = id2,
                            LastName = lastName
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = "no match",
                            Id = id3,
                            LastName = "no match"
                        }
                    },
                ExpectedResponse = new Api.Customer[]
                    {
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = firstName,
                            Id = id1,
                            LastName = lastName
                        },
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = firstName,
                            Id = id2,
                            LastName = lastName
                        }
                    }
            });
        }
    }
}
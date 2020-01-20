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
            AddMatchOnBorthFirstAndLastName();
        }

        private void AddMatchOnFirstName()
        {
            const string firstName = nameof(firstName);
            const string lastName = nameof(lastName);
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
                            LastName = notMatched1
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = firstName,
                            LastName = notMatched2
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = "no match",
                            LastName = "no match"
                        }
                    },
                ExpectedResponse = new Api.Customer[]
                    {
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = firstName,
                            LastName = notMatched1
                        },
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = firstName,
                            LastName = notMatched2
                        }
                    }
            });
        }

        private void AddMatchOnLastName()
        {
            const string firstName = nameof(firstName);
            const string lastName = nameof(lastName);
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
                            LastName = lastName
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = notMatched2,
                            LastName = lastName
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = "no match",
                            LastName = "no match"
                        }
                    },
                ExpectedResponse = new Api.Customer[]
                    {
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = notMatched1,
                            LastName = lastName
                        },
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = notMatched2,
                            LastName = lastName
                        }
                    }
            });
        }

        private void AddMatchOnBorthFirstAndLastName()
        {
            const string firstName = nameof(firstName);
            const string lastName = nameof(lastName);
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
                            LastName = lastName
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = firstName,
                            LastName = lastName
                        },
                        new Entities.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = "no match",
                            LastName = "no match"
                        }
                    },
                ExpectedResponse = new Api.Customer[]
                    {
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth1,
                            FirstName = firstName,
                            LastName = lastName
                        },
                        new Api.Customer
                        {
                            DateOfBirth = dateOfBirth2,
                            FirstName = firstName,
                            LastName = lastName
                        }
                    }
            });
        }
    }
}
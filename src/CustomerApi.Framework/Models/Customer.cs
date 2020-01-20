using System;
using System.ComponentModel.DataAnnotations;

namespace CustomerApi.Models
{
    public class Customer
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime? DateOfBirth { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string Id { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
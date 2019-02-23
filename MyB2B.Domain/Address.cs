using System.ComponentModel.DataAnnotations;

namespace MyB2B.Domain
{
    public class Address : ApplicationEntity
    {
        [MaxLength(255)]
        public string Street { get; set; }

        [MaxLength(10)]
        public string Number { get; set; }

        [MaxLength(255)]
        public string City { get; set; }

        [MaxLength(10)]
        public string ZipCode { get; set; }

        [MaxLength(255)]
        public string Country { get; set; }
    }
}
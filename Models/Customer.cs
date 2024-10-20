using Microsoft.AspNetCore.Mvc;

namespace PinewoodTaskApp.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public double Invested { get; set; }

        public static explicit operator Customer(ActionResult<IEnumerable<Customer>> v)
        {
            throw new NotImplementedException();
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PinewoodTaskApp.Models;
using System.Diagnostics.Metrics;
using System.Net;
using System.Numerics;
using System.Xml.Linq;

namespace PinewoodTaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        //Using a list instead of a DB for storing the customers, added a few customers to initialise the list
        private static readonly List<Customer> _customers = new List<Customer>
        {
            new Customer { ID = 1, Name = "Dusty Carr", Email = "dusty.carr@gmail.com", Phone = "07735970428", Address = "34 New Dover Road", City = "Canterbury", Region = "Kent", Postcode = "CT1 3BH", Country = "UK", Invested = 4500.00 },
            new Customer { ID = 2, Name = "Barb Dwyer", Email = "barb.dwyer@outlook.com", Phone = "07757410268", Address = "12 Swanpool Walk", City = "Worcester", Region = "Worcestershire", Postcode = "WR2 4EL", Country = "UK", Invested = 500.00 },
            new Customer { ID = 3, Name = "Carrie Oakey", Email = "carrie123@gmail.com", Phone = "07725068143", Address = "30 Blackpool Road", City = "Bryanston", Region = "Johannesburg", Postcode = "BR322", Country = "South Africa", Invested = 0 },
            new Customer { ID = 4, Name = "Annette Curtain", Email = "annette.curtain2@gmail.com", Phone = "07712975308", Address = "30 Crofters Mead", City = "Croydon", Region = "Surrey", Postcode = "CR9 4ER", Country = "UK", Invested = 2590.00 },
            new Customer { ID = 5, Name = "Eileen Dover", Email = "eileendover@gmail.com", Phone = "07791534860", Address = "Wogan Lodge, Sansome Road", City = "Edinburgh", Region = "West Lothian", Postcode = "EH4 8ER", Country = "UK", Invested = 45.99 },
            new Customer { ID = 6, Name = "Matt Tress", Email = "matthewtress@outlook.com", Phone = "07703167482", Address = "145 Bristol Road South", City = "Longbridge", Region = "Birmingham", Postcode = "B45 2TR", Country = "UK", Invested = 3500.00 },
            new Customer { ID = 7, Name = "Rick Shaw", Email = "rickshaw45@gmail.com", Phone = "07706478931", Address = "Unit 2 Trade Park", City = "Penarth Road", Region = "Cardiff", Postcode = "CF11 8TQ", Country = "UK", Invested = 450 },
            new Customer { ID = 8, Name = "Russel Sprout", Email = "russ.sprout@gmail.com", Phone = "07709524837", Address = "1 Frederick Road, Worcester Road", City = "Kidderminster", Region = "Worcestershire", Postcode = "DY11 7RA", Country = "UK", Invested = 3790.00 },
            new Customer { ID = 9, Name = "Woody Forrest", Email = "woody15@gmail.com", Phone = "07792860741", Address = "35 Victoria Street", City = "Aberdeen", Region = "Aberdeenshire ", Postcode = "AB10 7PB", Country = "UK", Invested = 45.66 },
            new Customer { ID = 10, Name = "Tom Katt", Email = "thomas.katt@gmail.com", Phone = "07715647302", Address = "23 Cowley Road", City = "Oxford", Region = "Oxfordshire", Postcode = "OX4 2WS", Country = "UK", Invested = 700.00 }
        };

        //Get the full list of customers
        [HttpGet]
        public ActionResult<IEnumerable<Customer>> GetCustomerList()
        {
            return Ok(_customers);
        }

        //Get a single customer
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomer(int id)
        {
            var customer = _customers.FirstOrDefault(x => x.ID == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        //add a customer
        [HttpPost]
        public ActionResult AddCustomer([FromBody] Customer customer)
        {
            //get the max id and increment by 1 before adding
            int maxID = _customers.Max(x => x.ID);
            customer.ID = maxID + 1;

            _customers.Add(customer);

            return CreatedAtAction(nameof(AddCustomer), new { id = customer.ID }, customer);
        }

        //update a customer
        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, [FromBody] Customer customer)
        {
            if (id != customer.ID)
            {
                return BadRequest();
            }

            var customerToUpdate = _customers.FirstOrDefault(x => x.ID == id);
            if (customerToUpdate == null)
            {
                return NotFound();
            }

            customerToUpdate.Name = customer.Name;
            customerToUpdate.Phone = customer.Phone;
            customerToUpdate.Email = customer.Email;
            customerToUpdate.City = customer.City;
            customerToUpdate.Region = customer.Region;
            customerToUpdate.Postcode = customer.Postcode;
            customerToUpdate.Country = customer.Country;
            customerToUpdate.Invested = customer.Invested;

            return NoContent();
        }

        //delete a customer
        [HttpDelete("{id}")]
        public ActionResult DeleteCustomer(int id)
        {
            var customerToUpdate = _customers.FirstOrDefault(x => x.ID == id);
            if (customerToUpdate == null)
            {
                return NotFound();
            }

            _customers.Remove(customerToUpdate);

            return NoContent();
        }

    }
}

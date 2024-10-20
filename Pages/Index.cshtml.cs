using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using PinewoodTaskApp.Controllers;
using PinewoodTaskApp.Models;
using PinewoodTaskApp.Data;
using System.Diagnostics.Eventing.Reader;
using System.Text.Json;

namespace PinewoodTaskApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IConfiguration configuration;

        public IndexModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public List<Customer> Customers { get; set; }
        public string StatusMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            StatusMessage = "";
            var api = new APIData(configuration);

            try
            {
                Customers = await api.GetCustomerList();
            }
            catch (Exception e)
            {
                //need to set the BaseAddress in app settings for the Web API
                StatusMessage = "API Failure - Please set the BaseAPIAddress value in app settings for the Web API address. e.g. for visual studio is will be something like \"https://localhost:7291/\".";
                Customers = new List<Customer>();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostUpdateCustomer()
        {
            StatusMessage = "";
            var api = new APIData(configuration);

            //update customer details
            var cust = new Customer();
            cust.ID = Convert.ToInt32(Request.Form["modal_hidden_id"].ToString());
            cust.Name = Request.Form["txtName"].ToString();
            cust.Phone = Request.Form["txtPhone"].ToString();
            cust.Email = Request.Form["txtEmail"].ToString();
            cust.Address = Request.Form["txtAddress"].ToString();
            cust.City = Request.Form["txtCity"].ToString();
            cust.Region = Request.Form["txtRegion"].ToString();
            cust.Postcode = Request.Form["txtPostcode"].ToString();
            cust.Country = Request.Form["txtCountry"].ToString();
            cust.Invested = (float)Convert.ToDouble(Request.Form["txtInvested"].ToString());

            if (cust.ID != 0)
            {
                _ = await api.UpdateCustomer(cust);
            } else {
                _ = await api.SaveCustomer(cust);
            }

            //refresh customer list
            Customers = await api.GetCustomerList();

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteCustomer2()
        {
            StatusMessage = "";
            var api = new APIData(configuration);
            _ = await api.DeleteCustomer(Convert.ToInt32(Request.Form["modal_hidden_id_delete"].ToString()));

            //refresh customer list
            Customers = await api.GetCustomerList();

            return Page();
        }

    }
}

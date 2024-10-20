using Newtonsoft.Json;
using PinewoodTaskApp.Models;

namespace PinewoodTaskApp.Data
{
    public class APIData
    {
        private readonly IConfiguration configuration;

        public APIData(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<List<Customer>> GetCustomerList()
        {
            List<Customer> customers = new List<Customer>();

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(configuration["BaseAPIAddress"]);

                using (HttpResponseMessage response = await httpClient.GetAsync("api/Customer"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        customers = JsonConvert.DeserializeObject<List<Customer>>(apiResponse);
                    }
                }
            }
            return customers;
        }

        public async Task<string> SaveCustomer(Customer cust)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(configuration["BaseAPIAddress"]);

                string json = JsonConvert.SerializeObject(cust);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PostAsync("api/Customer", httpContent))
                {
                    if (response.IsSuccessStatusCode)
                    {

                    }
                }
            }
            return "";
        }

        public async Task<string> UpdateCustomer(Customer cust)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(configuration["BaseAPIAddress"]);

                string json = JsonConvert.SerializeObject(cust);
                StringContent httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                using (HttpResponseMessage response = await httpClient.PutAsync("api/Customer/" + cust.ID.ToString(), httpContent))
                {
                    if (response.IsSuccessStatusCode)
                    {

                    }
                }
            }
            return "";
        }

        public async Task<string> DeleteCustomer(int ID)
        {
            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(configuration["BaseAPIAddress"]);

                using (HttpResponseMessage response = await httpClient.DeleteAsync("api/Customer/" + ID.ToString()))
                {
                    if (response.IsSuccessStatusCode)
                    {

                    }
                }
            }
            return "";
        }

    }
}

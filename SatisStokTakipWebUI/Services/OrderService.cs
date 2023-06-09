using Newtonsoft.Json;
using SatisStokTakipWebUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SatisStokTakipWebUI.Services
{
    public class OrderService
    {
        private readonly string _baseUrl;

        public OrderService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        // Order CRUD operations
        public async Task<List<Order>> GetOrders()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/orders");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<List<Order>>(stringResponse);
            return orders;
        }

        public async Task<Order> GetOrder(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/orders/" + id);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(stringResponse);
            return order;
        }

        public async Task CreateOrder(Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PostAsync(_baseUrl + "/api/orders", data);
        }

        public async Task UpdateOrder(int id, Order order)
        {
            var json = JsonConvert.SerializeObject(order);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PutAsync(_baseUrl + "/api/orders/" + id, data);
        }

        public async Task DeleteOrder(int id)
        {
            using var client = new HttpClient();
            await client.DeleteAsync(_baseUrl + "/api/orders/" + id);
        }
    }

}

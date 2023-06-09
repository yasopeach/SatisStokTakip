using Newtonsoft.Json;
using SatisStokTakipWebUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SatisStokTakipWebUI.Services
{
    public class OrderDetailService
    {
        private readonly string _baseUrl;

        public OrderDetailService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        // OrderDetail CRUD operations
        public async Task<List<OrderDetail>> GetOrderDetails()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/orderdetails");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var orderDetails = JsonConvert.DeserializeObject<List<OrderDetail>>(stringResponse);
            return orderDetails;
        }

        public async Task<OrderDetail> GetOrderDetail(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/orderdetails/" + id);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var orderDetail = JsonConvert.DeserializeObject<OrderDetail>(stringResponse);
            return orderDetail;
        }

        public async Task CreateOrderDetail(OrderDetail orderDetail)
        {
            var json = JsonConvert.SerializeObject(orderDetail);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PostAsync(_baseUrl + "/api/orderdetails", data);
        }

        public async Task UpdateOrderDetail(int id, OrderDetail orderDetail)
        {
            var json = JsonConvert.SerializeObject(orderDetail);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PutAsync(_baseUrl + "/api/orderdetails/" + id, data);
        }

        public async Task DeleteOrderDetail(int id)
        {
            using var client = new HttpClient();
            await client.DeleteAsync(_baseUrl + "/api/orderdetails/" + id);
        }
    }

}

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SatisStokTakipWebUI.Models;

namespace SatisStokTakipWebUI.Services
{
    public class ProductService
    {
        private readonly string _baseUrl;

        public ProductService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        // Product CRUD operations
        public async Task<List<Product>> GetProducts()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/products");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<List<Product>>(stringResponse);
            return products;
        }

        public async Task<Product> GetProduct(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/products/" + id);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(stringResponse);
            return product;
        }

        public async Task CreateProduct(Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PostAsync(_baseUrl + "/api/products", data);
        }

        public async Task UpdateProduct(int id, Product product)
        {
            var json = JsonConvert.SerializeObject(product);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PutAsync(_baseUrl + "/api/products/" + id, data);
        }

        public async Task DeleteProduct(int id)
        {
            using var client = new HttpClient();
            await client.DeleteAsync(_baseUrl + "/api/products/" + id);
        }

    }

}

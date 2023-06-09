using Newtonsoft.Json;
using SatisStokTakipWebUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SatisStokTakipWebUI.Services
{
    public class UserService
    {
        private readonly string _baseUrl;

        public UserService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        // User CRUD operations
        public async Task<List<User>> GetUsers()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/users");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<List<User>>(stringResponse);
            return users;
        }

        public async Task<User> GetUser(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/users/" + id);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var user = JsonConvert.DeserializeObject<User>(stringResponse);
            return user;
        }

        public async Task CreateUser(User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PostAsync(_baseUrl + "/api/users", data);
        }

        public async Task UpdateUser(int id, User user)
        {
            var json = JsonConvert.SerializeObject(user);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PutAsync(_baseUrl + "/api/users/" + id, data);
        }

        public async Task DeleteUser(int id)
        {
            using var client = new HttpClient();
            await client.DeleteAsync(_baseUrl + "/api/users/" + id);
        }
    }

}

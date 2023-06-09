using Newtonsoft.Json;
using SatisStokTakipWebUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SatisStokTakipWebUI.Services
{
    public class SessionService
    {
        private readonly string _baseUrl;

        public SessionService(string baseUrl)
        {
            _baseUrl = baseUrl;
        }

        // Session CRUD operations
        public async Task<List<Session>> GetSessions()
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/sessions");
            var stringResponse = await response.Content.ReadAsStringAsync();
            var sessions = JsonConvert.DeserializeObject<List<Session>>(stringResponse);
            return sessions;
        }

        public async Task<Session> GetSession(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync(_baseUrl + "/api/sessions/" + id);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var session = JsonConvert.DeserializeObject<Session>(stringResponse);
            return session;
        }

        public async Task CreateSession(Session session)
        {
            var json = JsonConvert.SerializeObject(session);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PostAsync(_baseUrl + "/api/sessions", data);
        }

        public async Task UpdateSession(int id, Session session)
        {
            var json = JsonConvert.SerializeObject(session);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            using var client = new HttpClient();
            await client.PutAsync(_baseUrl + "/api/sessions/" + id, data);
        }

        public async Task DeleteSession(int id)
        {
            using var client = new HttpClient();
            await client.DeleteAsync(_baseUrl + "/api/sessions/" + id);
        }
    }

}

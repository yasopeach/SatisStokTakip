using Microsoft.AspNetCore.Mvc;
using SatisStokTakipWebUI.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text;

namespace SatisStokTakipWebUI.Controllers
{
    public class OrdersController : Controller
    {
        private HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/orders");
            if (response.IsSuccessStatusCode)
            {
                var orders = await response.Content.ReadAsAsync<IEnumerable<Order>>();
                return View(orders);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/orders/" + id);
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadAsAsync<Order>();
                return View(order);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,UserId,OrderDate,OrderStatus")] Order order)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44308/api/orders", order);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync($"https://localhost:44308/api/orders/{id}");
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadAsAsync<Order>();
                if (order == null)
                {
                    return NotFound();
                }
                return View(order);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UserId,OrderDate,OrderStatus")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var orderContent = new StringContent(JsonConvert.SerializeObject(order), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"https://localhost:44308/api/orders/{id}", orderContent);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return NotFound();
                }
            }

            return View(order);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/orders/" + id);
            if (response.IsSuccessStatusCode)
            {
                var order = await response.Content.ReadAsAsync<Order>();
                return View(order);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:44308/api/orders/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }
    }
}

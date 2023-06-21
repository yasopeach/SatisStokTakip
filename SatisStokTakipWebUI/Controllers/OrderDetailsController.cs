using System;
using System.Net.Http;
using System.Threading.Tasks;
using SatisStokTakipWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;

namespace SatisStokTakipWebUI.Controllers
{
    public class OrderDetailsController : Controller
    {
        private HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/orderdetails");
            if (response.IsSuccessStatusCode)
            {
                var orderDetails = await response.Content.ReadAsAsync<IEnumerable<OrderDetail>>();
                return View(orderDetails);
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

            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/orderdetails/" + id);
            if (response.IsSuccessStatusCode)
            {
                var orderDetails = await response.Content.ReadAsAsync<OrderDetail>();
                return View(orderDetails);
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
        public async Task<IActionResult> Create([Bind("OrderDetailId,OrderId,ProductId,Quantity")] OrderDetail orderDetail)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44308/api/orderdetails", orderDetail);
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

            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/orderdetails/" + id);
            if (response.IsSuccessStatusCode)
            {
                var orderDetails = await response.Content.ReadAsAsync<OrderDetail>();
                return View(orderDetails);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,UserId,OrderDate,OrderStatus")] OrderDetail orderDetail)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:44308/api/orderdetails/" + id, orderDetail);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/orderdetails/" + id);
            if (response.IsSuccessStatusCode)
            {
                var orderDetails = await response.Content.ReadAsAsync<OrderDetail>();
                return View(orderDetails);
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
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:44308/api/orderdetails/" + id);
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
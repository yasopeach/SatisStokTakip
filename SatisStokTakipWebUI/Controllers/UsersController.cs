using System;
using System.Net.Http;
using System.Threading.Tasks;
using SatisStokTakipWebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace SatisStokTakipWebUI.Controllers
{
    public class UsersController : Controller
    {
        private HttpClient client = new HttpClient();

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/users");
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadAsAsync<IEnumerable<User>>();
                return View(users);
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

            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/users/" + id);
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadAsAsync<User>();
                return View(users);
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
        public async Task<IActionResult> Create([Bind("UserName,Password")] User user)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44308/api/users", user);
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

            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/users/" + id);
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadAsAsync<User>();
                return View(users);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductCode,Name,Description,Price,Stock")] User user)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync("https://localhost:44308/api/users/" + id, user);
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

            HttpResponseMessage response = await client.GetAsync("https://localhost:44308/api/users/" + id);
            if (response.IsSuccessStatusCode)
            {
                var users = await response.Content.ReadAsAsync<User>();
                return View(users);
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
            HttpResponseMessage response = await client.DeleteAsync("https://localhost:44308/api/users/" + id);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Login(User user)
        //{
        //    HttpResponseMessage response = await client.PostAsJsonAsync("https://localhost:44308/api/users/login", user);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction(nameof(Index)); // giriş başarılıysa, Index'e yönlendir
        //    }
        //    else
        //    {
        //        ModelState.AddModelError(string.Empty, "Invalid Login Attempt"); // giriş başarısızsa, hata mesajını göster
        //        return View(user);
        //    }
        //}







    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SatisStokTakipAPI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SatisStokTakipAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly SatisStokTakipContext _context;

        public ProductsController(SatisStokTakipContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.ProductId)
            {
                return BadRequest();
            }

            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }


        [HttpPost("UploadFile")]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            // XML'i oku ve ürünleri elde et
            List<Product> products;
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                products = ReadProductsFromXml(stream);
            }

            // Ürünleri veritabanına ekle
            await AddProducts(products);

            return NoContent();
        }

        private List<Product> ReadProductsFromXml(StreamReader streamReader)
        {
            XDocument xdoc = XDocument.Load(streamReader);
            List<Product> products = xdoc.Root.Elements("Product")
                .Select(e => new Product
                {
                    Id = (int)e.Element("Id"),
                    Name = (string)e.Element("Name"),
                    Description = (string)e.Element("Description"),
                    Price = (decimal)e.Element("Price"),
                    Stock = (int)e.Element("Stock")
                }).ToList();

            return products;
        }

        private async Task AddProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                _context.Products.Add(product);
            }
            await _context.SaveChangesAsync();
        }
    }


}

}

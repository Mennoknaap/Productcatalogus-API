using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogus
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        private int _pageSize = 10;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        //-----------------------------\\
        //-------------GET-------------\\
        //-----------------------------\\

        // Get all products (sorted if wanted)
        [HttpGet("GetProduct")]
        public IActionResult Get(
            [FromQuery] string? Naam,
            [FromQuery] int? PotMaatMin,
            [FromQuery] int? PotMaatMax,
            [FromQuery] string? Kleur,
            [FromQuery] int? Pagina,
            [FromQuery] string? Sort,
            [FromQuery] string? SortOrder
        )
        {
            List<Product> filteredProducts = _context.Products.ToList();

            // Filter by name
            if (Naam != null)
            {
                 filteredProducts = Filter(Naam, product => product.Naam, filteredProducts);
            }

            // Filter by color
            if (Kleur != null)
            {
                 filteredProducts = Filter(Naam, product => product.Kleur, filteredProducts);
            }


            // Filter by potmaat
            if (PotMaatMin != null || PotMaatMax != null)
            {
                filteredProducts = filteredProducts
                    .Where(product =>
                        (PotMaatMin == null || product.PotMaat >= PotMaatMin) &&
                        (PotMaatMax == null || product.PotMaat <= PotMaatMax))
                    .ToList();
            }

            // Filter by page
            if (Pagina != null)
            {
                filteredProducts = filteredProducts.Skip(_pageSize * (Pagina.Value - 1)).Take(_pageSize).ToList();
            }

            // Sort
            if (Sort != null)
            {
                PropertyInfo property = typeof(Product).GetProperty(Sort);
                if (property != null)
                {
                    if (SortOrder != null && SortOrder.ToLower() == "desc")
                    {
                        filteredProducts = filteredProducts.OrderByDescending(product => property.GetValue(product, null)).ToList();
                    }
                    else
                    {
                        filteredProducts = filteredProducts.OrderBy(product => property.GetValue(product, null)).ToList();
                    }
                }
                else
                {
                    return BadRequest("Sorteer op een bestaande property");
                }
            }


            // Return all products
            return Ok(filteredProducts);
        }

        //-----------------------------\\
        //-------------POST------------\\
        //-----------------------------\\

        // Add a new product
        [HttpPost("PostProduct")]
        public IActionResult Post(
            [FromBody] Product product
        )
        {
            // Create a new Code with 13 random characters
            product.Code = RandomString(13);

            // Check if the productcode already exists
            while (_context.Products.Find(product.Code) != null)
            {
                product.Code = RandomString(13);
            }

            if (product.Naam.Length > 50)
            {
                return BadRequest("De naam mag niet langer zijn dan 50 karakters");
            }

            product.Kleur = product.Kleur ?? "Geen kleur";
            _context.Products.Add(product);

            _context.SaveChanges();
            return Ok("Product met code " + product.Code + " is toegevoegd");
        }



        //-----------------------------\\
        //-------------PUT-------------\\
        //-----------------------------\\

        // Change a product by code
        [HttpPut("PutProduct")]
        public IActionResult Put(
            [FromQuery] string code,
            [FromBody] Product product
        )
        {
            var productToChange = _context.Products.Find(code);

            if (productToChange == null)
            {
                return NotFound("Product niet gevonden");
            }
            
            productToChange.Naam = product.Naam;
            productToChange.PotMaat = product.PotMaat;
            productToChange.PlantHoogte = product.PlantHoogte;
            productToChange.Kleur = product.Kleur;
            productToChange.ProductGroep = product.ProductGroep;

            _context.SaveChanges();
            return Ok("Product met code " + code + " is aangepast");
        }


        //-----------------------------\\
        //------------DELETE-----------\\
        //-----------------------------\\

        // Delete a product by code
        [HttpDelete("DeleteProduct")]
        public IActionResult Delete(
            [FromQuery] string code
        )
        {
            var productToDelete = _context.Products.Find(code);

            if (productToDelete == null)
            {
                return NotFound("Product niet gevonden");
            }

            _context.Products.Remove(productToDelete);
            _context.SaveChanges();
            return Ok("Product met code " + code + " is verwijderd");
        }



        //-----------------------------\\
        //-----------METHODS-----------\\
        //-----------------------------\\

        // Create a random string with a given size
        private string RandomString(int v)
        {
            string random = "";
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            for (int i = 0; i < v; i++)
            {
                random += chars[new Random().Next(0, chars.Length)];
            }
            return random;
        }


        // Filter the products on String
        private List<Product> Filter<T>(string value, Func<Product, T> propertySelector, List<Product> products)
        {
            return products.Where(product => propertySelector(product).ToString().Contains(value)).ToList();
        }
    }
}
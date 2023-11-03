using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogus
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

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
            [FromQuery] int? Pagina
        )
        {
            System.Console.WriteLine(Naam);

            // Sorteer op naam
            if (Naam != null)
            {
                return(Ok(Filter(Naam, "Naam")));
            }

            // Sorteer op kleur
            if (Kleur != null)
            {
                return Ok(Filter(Kleur, "Kleur"));
            }


            // Sorteer op potmaat
            if (PotMaatMin != null || PotMaatMax != null)
            {
                var filteredProducts = _context.Products
                    .Where(product =>
                        (PotMaatMin == null || product.PotMaat >= PotMaatMin) &&
                        (PotMaatMax == null || product.PotMaat <= PotMaatMax))
                    .ToList();

                return Ok(filteredProducts);
            }

            return Ok(_context.Products);
        }

        //-----------------------------\\
        //-------------POST------------\\
        //-----------------------------\\

        [HttpPost("PostProduct")]
        public IActionResult Post(
            [FromBody] Product product
        )
        {
            // Create a new Code with 13 random characters
            product.Code = RandomString(13);

            // TODO: Check if the product already exists


            product.Kleur = product.Kleur ?? "Geen kleur";
            _context.Products.Add(product);

            _context.SaveChanges();
            return Ok();
        }



        //-----------------------------\\
        //-------------PUT-------------\\
        //-----------------------------\\

        //-----------------------------\\
        //------------DELETE-----------\\
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
        private List<Product> Filter(string propertyName, string value)
        {
            var products = _context.Products.AsQueryable();

            if (propertyName == "Naam")
            {
                products = products.Where(product => product.Naam.Contains(value));
            }
            else if (propertyName == "Kleur")
            {
                products = products.Where(product => product.Kleur.Contains(value));
            }

            return products.ToList();
        }



    }
}
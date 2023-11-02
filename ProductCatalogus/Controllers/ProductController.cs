using Microsoft.AspNetCore.Mvc;

namespace ProductCatalogus.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    //-----------------------------\\
    //-------------GET-------------\\
    //-----------------------------\\
    
    [HttpGet("GetProduct")]
    public IActionResult Get(
        [FromQuery] string ?Naam,
        [FromQuery] int ?Pagina
    )
    {
        System.Console.WriteLine(Naam);
        return Ok();
    }

    //-----------------------------\\
    //-------------POST------------\\
    //-----------------------------\\

    //-----------------------------\\
    //-------------PUT-------------\\
    //-----------------------------\\

    //-----------------------------\\
    //------------DELETE-----------\\
    //-----------------------------\\

}
using Microsoft.AspNetCore.Mvc;
using Vektorel.Api.Context;

namespace Vektorel.Api.Controllers;

// REST API
// REST API Naming Convention

[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly NorthwindContext context;

    public ProductController(NorthwindContext context)
    {
        this.context = context;
    }

    [HttpGet("{id}")]
    public IActionResult GetProduct([FromRoute] int id)
    {
        var product = context.Products.FirstOrDefault(f => f.ProductID == id);

        if (product is null)
        {
            return BadRequest("Ürün bulunamadı");
        }

        return Ok(product);
    }

    [HttpGet("filter")]
    public IActionResult FilterProduct([FromQuery] string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return BadRequest("Arama kriteri zorunlu");
        }

        var product = context.Products.FirstOrDefault(f => f.ProductName.StartsWith(name));
        if (product is null)
        {
            return BadRequest("Ürün bulunamadı");
        }

        return Ok(product);
    }


    [HttpGet("count")]
    public IActionResult GetProductCount()
    {
        var count = context.Products.Count();
        return Ok(count);
    }

    [HttpGet("category/{categoryId}")]
    public IActionResult GetProductsByCategoryId([FromRoute] int categoryId)
    {
        var products = context.Products.Where(f => f.CategoryId == categoryId).ToList();
        if (!products.Any())
        {
            return BadRequest("Kategori eşleştirilemedi");
        }

        return Ok(products);
    }
}

//[ApiController]
//public class SupplierController : ControllerBase
//{

//}
using Microsoft.AspNetCore.Mvc;
using Vektorel.Api.Context;
using Vektorel.Api.Entities;
using Vektorel.Api.Models;

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

    [HttpPost("create")]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        context.Products.Add(product);
        context.SaveChanges();
        return Ok(product.ProductName + " Eklendi");
    }

    [HttpPatch("add-stock")]
    public IActionResult UpdateProduct([FromBody] AddStockDto product)
    {
        //Bu kod artık çalışmaz çünkü AddStockDto'da Rande DataAnnotation var. Bakınız
        if (product.ProductID <= 0)
        {
            return BadRequest("Uygunsuz ürün kimliği");
        }
        var p = context.Products.FirstOrDefault(f => f.ProductID == product.ProductID);
        if (p is null)
        {
            return BadRequest("Ürün bulunamadı");
        }
        p.UnitsInStock += product.UnitCount;

        context.SaveChanges();
        return Ok(p.ProductName + " Stok güncellendi...: " + p.UnitsInStock);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete([FromRoute] int id)
    {
        var p = context.Products.FirstOrDefault(f => f.ProductID == id);
        if (p is null)
        {
            return BadRequest("Ürün bulunamadı");
        }
        context.Products.Remove(p);
        context.SaveChanges();
        return Ok(p.ProductName + " Silindi");
    }
}

//[ApiController]
//public class SupplierController : ControllerBase
//{

//}
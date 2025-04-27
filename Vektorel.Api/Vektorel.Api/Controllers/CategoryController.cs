using Microsoft.AspNetCore.Mvc;
using Vektorel.Api.Context;

namespace Vektorel.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController : ControllerBase
{
    private readonly NorthwindContext context;

    public CategoryController(NorthwindContext context)
    {
        this.context = context;
    }

    [HttpGet]
    public IActionResult Categories()
    {
        var categories = context.Categories.ToList();
        return Ok(categories);
    }
}

//[ApiController]
//public class SupplierController : ControllerBase
//{

//}
using System.ComponentModel.DataAnnotations;

namespace Vektorel.Api.Models;

public class AddStockDto
{
    [Range(1, int.MaxValue)]
    public int ProductID { get; set; }
    [Range(1, short.MaxValue)]
    public short UnitCount { get; set; }
}

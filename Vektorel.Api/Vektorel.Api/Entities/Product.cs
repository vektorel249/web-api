using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Vektorel.Api.Entities;

[Table("Products")]
public class Product
{
    [Key]
    public int ProductID { get; set; }

    [Required]
    [MaxLength(40)]
    public string ProductName { get; set; }
    public short? UnitsInStock { get; set; }
    public decimal? UnitPrice { get; set; }

    public int? CategoryId { get; set; }
}

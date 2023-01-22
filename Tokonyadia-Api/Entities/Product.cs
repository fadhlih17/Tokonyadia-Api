using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Tokonyadia_Api.Entities;

[Table(name:"m_product")]
public class Product
{
    [Key, Column(name:"id")] 
    public Guid Id { get; set; }
    
    [Column(name:"product_name", TypeName = "NVarchar(50)")]
    public string ProductName { get; set; }
    
    [Column(name:"description", TypeName = "NVarchar(100)")]
    public string Description{ get; set; }
    
    public ICollection<ProductPrice>? ProductPrices { get; set; }
}
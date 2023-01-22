using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokonyadia_Api.Entities;

[Table(name:"m_product_price")]
public class ProductPrice
{
    [Key, Column(name:"id")] public Guid Id { get; set; }
    
    [Column(name:"price")] public long Price { get; set; }
    
    [Column(name:"stock")] public int Stock { get; set; }
    
    [Column(name:"store_id")] public Guid StoreId { get; set; }
    
    [Column(name:"product_id")] public Guid ProductId { get; set; }
    
    public virtual Store? Store { get; set; }
    public virtual Product? Product { get; set; }
}
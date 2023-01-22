using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokonyadia_Api.Entities;

[Table(name:"t_purchase_detail")]
public class PurchaseDetail
{
    [Key, Column(name:"Id")] public Guid Id { get; set; }
    
    [Column(name:"qty")] public int Qty { get; set; }
    
    [Column(name:"purchase_id")] public Guid PurchaseId { get; set; }
    
    [Column(name:"product_price_id")] public Guid ProductPriceId { get; set; }
    
    public virtual Purchase? Purchase { get; set; }
    public virtual ProductPrice? ProductPrice { get; set; }
}
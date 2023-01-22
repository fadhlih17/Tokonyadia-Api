using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokonyadia_Api.Entities;

[Table(name:"t_purchase")]
public class Purchase
{
    [Key, Column(name:"id")] public Guid Id { get; set; }
    
    [Column(name:"trans_date")] public DateTime TransDate { get; set; }
    
    [Column(name:"customer_id")] public Guid CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }
    public ICollection<PurchaseDetail> PurchaseDetails { get; set; }
}
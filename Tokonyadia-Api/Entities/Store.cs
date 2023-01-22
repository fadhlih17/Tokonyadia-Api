using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokonyadia_Api.Entities;

[Table(name:"m_store")]
public class Store
{
    [Key, Column(name:"id")] 
    public Guid Id { get; set; }
    
    [Column(name:"store_name",TypeName = "NVarchar(50)")]
    public string StoreName { get; set; }
    
    [Column(name:"siup_number", TypeName = "NVarchar(9)")] 
    public string SiupNumber { get; set; }
    
    [Column(name:"address", TypeName = "NVarchar(100)")] 
    public string Address { get; set; }
    
    [Column(name:"phone_number", TypeName = "NVarchar(14)")]
    public string PhoneNumber { get; set; }
}
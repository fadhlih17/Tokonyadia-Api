using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokonyadia_Api.Entities;

[Table(name:"m_role")]
public class Role
{
    [Key,Column(name:"id")]
    public Guid Id { get; set; }
    
    [Column(name:"role")] public ERole ERole { get; set; }
}
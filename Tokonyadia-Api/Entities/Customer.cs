using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokonyadia_Api.Entities;

[Table(name:"m_customer")]
public class Customer
{
    [Key, Column(name: "id")] public Guid Id { get; set; }

    [Column(name: "customer_name", TypeName = "Varchar(48)")]
    public string CustomerName { get; set; }

    [Column(name: "phone_number", TypeName = "Varchar(14)")]
    public string PhoneNumber { get; set; }

    [Column(name: "address", TypeName = "Varchar(100)")]
    public string Address { get; set; }

    [Column(name: "user_credential_id")] public Guid UserCredentialId { get; set; }

    public virtual UserCredential UserCredential { get; set; }

}
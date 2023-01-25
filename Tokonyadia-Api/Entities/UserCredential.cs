using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tokonyadia_Api.Entities;

[Table(name:"m_user_credential")]
public class UserCredential
{
    [Key, Column(name:"id")]
    public Guid Id { get; set; }

    [Column(name: "email"), Required, EmailAddress]
    public string Email { get; set; } = null!; // menghilangkan warning krn sudah pasti tdk boleh kosong

    [Column(name: "password"), Required, StringLength(maximumLength: int.MaxValue, MinimumLength = 6)]
    public string Password { get; set; } = null!; // menghilangkan warning krn sudah pasti tdk boleh kosong

    // untuk autorisasi (Authorization)
    [Column(name:"role_id")]
    public Guid RoleId { get; set; }

    public virtual Role Role { get; set; }
}
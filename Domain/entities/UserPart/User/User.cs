using Domain.entities.Cart;
using Domain.entities.UserPart.Roles;
using System.ComponentModel.DataAnnotations;


namespace Domain.entities.UserPart.User;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(30)]
    public string UserName { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }
    [Required]
    public string PhoneNumber { get; set; }
    public DateTime Created { get; set; } = DateTime.Now;
    public string? UserAvatar { get; set; }
    public bool IsAdmin { get; set; }
    public bool SuperAdmin { get; set; }
    public bool IsDelete { get; set; }

    #region Rels
    public ICollection<UserSelectedRole> UserSelectedRoles { get; set; }
    public List<Carts> cart { get; set; }
    public ICollection<Comments.Comments> Comments { get; set; }
    #endregion

}

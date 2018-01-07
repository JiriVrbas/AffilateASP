using Models.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string Password { get; set; }
        [ForeignKey("Supperior")]
        public int? SupperiorId { get; set; }
        public virtual User Supperior { get; set; }
        [Required]
        public decimal Balance { get; set; }
        public string ConfirmationLink { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }
        [ForeignKey("FacebookAccount")]
        public int? FacebookAccountId { get; set; }
        public virtual FacebookIdentity FacebookAccount { get; set; }
    }
}

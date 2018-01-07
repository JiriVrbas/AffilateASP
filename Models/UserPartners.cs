using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("user_partners")]
    public class UserPartners
    {
        [Column(Order = 0),Key, ForeignKey("User")]
        public int UserId { get; set; }
        [Column(Order = 1),Key, ForeignKey("Partner")]
        public int PartnerId { get; set; }
        [Required]
        public string LinkToAffilate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DefaultValue("true")]
        public bool IsActive { get; set; }

        public virtual AffilatePartner Partner { get; set; }
        public virtual User User { get; set; }
    }
}

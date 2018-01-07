using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("affilate_partners")]
    public class AffilatePartner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartnerId { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageLink { get; set; }
    }
}

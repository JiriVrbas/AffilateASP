using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("cashouts")]
    public class Cashout
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CashoutId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }
        [Required]
        [MaxLength(50)]
        public string AccountNumber { get; set; }
        [Required]
        [MaxLength(50)]
        public string BankCode { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public bool IsPaid { get; set; }
        [Required]
        public DateTime Apply { get; set; }
        public DateTime? Paid { get; set; }
    }
}

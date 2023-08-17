using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GiveFreely.DataAccess.Schema
{
    public class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdCustomer { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int IdAffiliate { get; set; }
        public virtual Affiliate Affiliate { get; set; }
    }
}

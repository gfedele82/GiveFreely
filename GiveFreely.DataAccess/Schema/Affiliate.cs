using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace GiveFreely.DataAccess.Schema
{
    public class Affiliate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdAffiliate { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual List<Customer> Customers { get; set; }
    }
}

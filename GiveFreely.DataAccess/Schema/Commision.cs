using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GiveFreely.DataAccess.Schema
{
    public class Commision
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int IdCommusion { get; set; }
        [Required]
        public int  FromCount { get; set; }
        public int? ToCount { get; set; }
        [Required]
        public decimal Money { get; set; }
    }
}

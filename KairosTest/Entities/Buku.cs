using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KairosTest.Entities
{
    public class Buku
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required, MaxLength(250)]
        public string JudulBuku { get; set; }
        [Required, MaxLength(250)]
        public string Pengarang { get; set; }
        [Required, MaxLength(250)]
        public string JenisBuku { get; set; }
        [Required]
        public decimal HargaSewa { get; set; }
    }
}

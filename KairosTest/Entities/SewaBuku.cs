using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KairosTest.Entities
{
    public class SewaBuku
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public DateTime MulaiSewa { get; set; }
        public DateTime SelesaiSewa { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int JumlahHari { get; set; }

        [ForeignKey(nameof(Buku))]
        public int BukuId { get; set; }
        public virtual Buku Buku { get; set; }

        [ForeignKey(nameof(IdentityUser))]
        public string IdentityUserId { get; set; }
        public virtual IdentityUser IdentityUser { get; set; }
    }
}

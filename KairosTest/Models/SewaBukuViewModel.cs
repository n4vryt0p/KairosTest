using System;

namespace KairosTest.Models
{
    public class SewaBukuViewModel
    {
        public long Id { get; set; }
        public string JudulBuku { get; set; }
        public string Pengarang { get; set; }
        public string JenisBuku { get; set; }
        public int JumlahHari { get; set; }
        public decimal HargaSewa { get; set; }
        public decimal TotalSewa { get; set; }
        public string UserId { get; set; }
    }

    public class AddSewaBukuViewModel
    {
        public int BukuId { get; set; }
        public DateTime MulaiSewa { get; set; }
        public DateTime SelesaiSewa { get; set; }
    }
}

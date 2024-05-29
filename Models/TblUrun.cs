using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication15.Models
{
    [Table("Tbl_Urun")]
    public partial class TblUrun
    {
        [Key]
        public int UrunId { get; set; }
        public int? KategoriId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string? UrunAd { get; set; }
        [Column(TypeName = "money")]
        public decimal? UrunFiyat { get; set; }
        public short? UrunAdet { get; set; }
        [StringLength(100)]
        [Unicode(false)]
        public string? UrunPhoto { get; set; }
        [NotMapped]
        [DisplayName("Upload Image File")]
        public IFormFile? ImageFile { get; set; }
        public bool? FavoriUrun { get; set; }
        [ForeignKey("KategoriId")]
        [InverseProperty("TblUruns")]
        public virtual TblKategoriler? TblKategoriler  { get; set; }
    }
}

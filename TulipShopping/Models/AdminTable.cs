namespace TulipShopping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdminTable")]
    public partial class AdminTable
    {
        public int Id { get; set; }

        [StringLength(500)]
        public string KategoriFoto { get; set; }

        [Required]
        [StringLength(100)]
        public string KategoriAdi { get; set; }


        [Required]
        public decimal Dolar { get; set; }


        [Required]
        public decimal Dinar { get; set; }

        [Required]
        public decimal TransferMiktari { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal Kargo { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal Nakliye { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:0.000}")]
        public decimal Katki { get; set; }
    }
}
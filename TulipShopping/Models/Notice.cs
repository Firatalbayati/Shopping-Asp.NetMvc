namespace TulipShopping.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web.Mvc;

    [Table("Notice")]
    public partial class Notice
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }


        [Required]
        //Shared/EditorTemplates tinymce_full_compressed
        [UIHint("tinymce_full_compressed"), AllowHtml]
        public string Text { get; set; }
    }
}

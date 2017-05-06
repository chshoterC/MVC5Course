using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
	public class ListProducts
	{
        [Required]
        public int ProductId { get; set; }

        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public Nullable<decimal> Price { get; set; }
        public Nullable<decimal> Stock { get; set; }
		
    }
}
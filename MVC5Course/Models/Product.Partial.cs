namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using System.Linq;

    [MetadataType(typeof(ProductMetaData))]

    public partial class Product
    {
        public int 訂單數量
        {
            get
            {
                return this.OrderLine.Count(p => p.Qty > 4000);
            }
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(80, ErrorMessage = "欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}

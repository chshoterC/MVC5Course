namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using System.Linq;
    using ValidationAttributes;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public int 訂單數量
        {
            get
            {
                return this.OrderLine.Count();
            }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Price > 5 && this.Stock < 5)
            {

                yield return new ValidationResult("庫存小於5價錢大於5", new string[] { "Price", "Stock" });
            }

            if (this.Active == false)
            {
                yield return new ValidationResult("false", new string[] { "Active" });

            }
            yield break;
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }

        [StringLength(33)]

        [商品名稱必須包含特定名稱]
        [DisplayName("商品名稱")]
        public string ProductName { get; set; }
        [DisplayFormat(DataFormatString = "{0:0}")]
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }

        public virtual ICollection<OrderLine> OrderLine { get; set; }

        //[DataType(DataType.Date)] //不會改變資料型態，只會改變輸出到VIEW的資料型態
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public object CreateOn { get; set; }
    }
}

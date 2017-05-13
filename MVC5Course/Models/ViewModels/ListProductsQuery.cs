using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ListProductsQuery:IValidatableObject
    {
        public string q { get; set; }
        public int stock1 { get; set; }
        public int stock2 { get; set; }


        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(this.stock1 > this.stock2)
            {
                yield return new ValidationResult("庫存查詢條件錯誤", new string[] { "stock1", "stock2" });
            }
        }
    }

     
}
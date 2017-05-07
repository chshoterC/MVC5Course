using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttributes
{
    public class 商品名稱必須包含特定名稱 : DataTypeAttribute
    {
        string str1 = "Cloud";
        public 商品名稱必須包含特定名稱() : base(DataType.Text)
        {
            ErrorMessage = "商品名稱必須包含" + str1;
        }


        public override bool IsValid(object value)
        {
            var str = (string)value;

            return str.Contains(str1);
        }
    }
}
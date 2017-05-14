using System;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    internal class SharedViewBagAttribute : ActionFilterAttribute
    {
        public string MyProperty { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            if (MyProperty != "")
                filterContext.Controller.ViewBag.Message = "我不是空值";
            else
                filterContext.Controller.ViewBag.Message = "空的";
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
    }
}
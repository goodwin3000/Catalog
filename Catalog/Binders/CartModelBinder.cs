using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Catalog.Binders
{
   public class CartModelBinder:IModelBinder
    {
        private const string key = "Cart";
       

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            Cart cart = null;
            if (controllerContext.HttpContext.Session != null)
            {
                cart = (Cart)controllerContext.HttpContext.Session[key];
            }
            if (cart == null)
            {
                cart = new Cart();
                controllerContext.HttpContext.Session[key] = cart;
            }
            return cart;

        }
    }
}

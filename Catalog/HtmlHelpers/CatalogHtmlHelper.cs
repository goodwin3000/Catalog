using CatalogLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Catalog.HtmlHelpers
{
  public static  class CatalogHtmlHelper
    {
        
        public static MvcHtmlString CatalogTreeMenu(this HtmlHelper htmlHelper,
           List<Category> list, string urlPath)
        {
            //declare the html helper 
            //  string html = @"<ul id=""menu""><li>";
            string html = "";
           
            if (list.Count > 0)
            {
                var root = list.Where(c => c.ParentId == null).SingleOrDefault();
                CreateMenu(root.SubCategories, ref html, urlPath);
            }
         //   html += "</li></ul>";
            return MvcHtmlString.Create(html);
        }
        static void CreateMenu(ICollection<Category> list,ref string html, string urlPath)
        {
            if (list.Count == 0)
            {
                return;
            }

            // html += @"<ul class=""nav nav-list tree"">" + Environment.NewLine;
            if (html.Length == 0)
            {
                html += @"<ul id=""menu"">" + Environment.NewLine;
            }
            else
            {
                html += @"<ul>" + Environment.NewLine;
            }
            foreach (var item in list)
            {
                if (item.Published)
                {
                    html += @"<li><span>" + item.CategoryName +"</span>" +Environment.NewLine;
                    //   html += @"<li><a  href=""" + urlPath + item.CategoryLinkName + @""">" + item.CategoryName + "</a></li>" + Environment.NewLine;
                    if (item.SubCategories.Count == 0)
                    {
               
                    }
                    else
                    {                      
                        CreateMenu(item.SubCategories, ref html, urlPath);
                        html += "</li>";
                    }
                    //  CreateMenu(item.SubCategories, ref html, urlPath);

                }
            }
           
            html += "</ul>" + Environment.NewLine;
        }
    }
}

using Catalog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Catalog.HtmlHelpers
{
    public static class PaginationHelper
    {
        public static MvcHtmlString Pagination(this HtmlHelper htmlHelper,
          PaginationModel pmodel, Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= pmodel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pmodel.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }
                tag.AddCssClass("btn btn-default");
                sb.Append(tag.ToString());
              
            }
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}

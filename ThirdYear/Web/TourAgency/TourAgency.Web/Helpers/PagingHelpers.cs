using System;
using System.Text;
using System.Web.Mvc;
using TourAgency.Web.Models.Paginations;

namespace TourAgency.Web.Helpers
{
    public static class PagingHelpers
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
        PageInfo pageInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pageInfo.TotalPages; i++)
            {
                TagBuilder li = new TagBuilder("li");
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                if (i == pageInfo.PageNumber)
                {
                    tag.AddCssClass("selected");
                }
                tag.AddCssClass("page-link");
                li.AddCssClass("page-item");
                li.InnerHtml = tag.ToString();
                result.Append(li.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }
    }
}
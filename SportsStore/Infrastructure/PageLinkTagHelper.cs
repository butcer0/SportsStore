using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper
    {
        private IUrlHelperFactory urlHelperFactory;

        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        public PagingInfo PageModel { get; set; }

        public string PageAction { get; set; }

        #region Receive Additional Information from View
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; } = new Dictionary<string, object>();
        #endregion

        #region Styling Attributes Properties
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        #endregion


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            #region Depricated - Base not called
            //base.Process(context, output);
            #endregion

            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");

                #region Receive Additional Information from View
                PageUrlValues["productPage"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                #endregion

                #region Depricated - Introduced PageUrlValues
                //tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
                #endregion

                #region Styling Attributes
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                #endregion

                tag.InnerHtml.Append(i.ToString());
                result.InnerHtml.AppendHtml(tag);
            }

            #region Depricated - Introduced Class Styling
            //for (int i = 1; i <= PageModel.TotalPages; i++)
            //{
            //    TagBuilder tag = new TagBuilder("a");
            //    tag.Attributes["href"] = urlHelper.Action(PageAction, new { productPage = i });
            //    tag.InnerHtml.Append(i.ToString());
            //    result.InnerHtml.AppendHtml(tag);
            //}
            #endregion

            output.Content.AppendHtml(result.InnerHtml);
        }
    }
}

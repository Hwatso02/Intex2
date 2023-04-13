using Intex2.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Intex2.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PaginationTagHelper : TagHelper
    {
        private IUrlHelperFactory uhf;
        public PaginationTagHelper(IUrlHelperFactory temp)
        {
            uhf = temp;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext vc { get; set; }
        public PageInfo PageModel { get; set; }
        public string PageAction { get; set; }
        public string PageClass { get; set; }
        public bool PageClassesEnabled { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public string ShowOnlyWithColor { get; set; }
        public string SexFilter { get; set; }
        public string TextileFunctionFilter { get; set; }
        public string TextileStructureFilter { get; set; }
        public string ShowDepthNotNull { get; set; }
        public string ShowAgeAtDeathNotNull { get; set; }

        public override void Process(TagHelperContext thc, TagHelperOutput tho)
        {
            IUrlHelper uh = uhf.GetUrlHelper(vc);
            TagBuilder final = new TagBuilder("div");

            for (int i = 1; i <= PageModel.TotalPages; i++)
            {
                TagBuilder tb = new TagBuilder("a");
                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i,
                    showOnlyWithColor = ShowOnlyWithColor,
                    sexFilter = SexFilter,
                    textileFunctionFilter = TextileFunctionFilter,
                    textileStructureFilter = TextileStructureFilter,
                    showDepthNotNull = ShowDepthNotNull,
                    showAgeAtDeathNotNull = ShowAgeAtDeathNotNull
                });

                if (PageClassesEnabled)
                {
                    tb.AddCssClass(PageClass);
                    tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
                }
                tb.AddCssClass(PageClass);
                tb.InnerHtml.Append(i.ToString());
                final.InnerHtml.AppendHtml(tb);
            }

            tho.Content.AppendHtml(final.InnerHtml);
        }
    }
}





//using Intex2.Models.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.AspNetCore.Mvc.Routing;
//using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Razor.TagHelpers;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Intex2.Infrastructure
//{
//    [HtmlTargetElement("div", Attributes = "page-model")]
//    public class PaginationTagHelper : TagHelper
//    {
//        //Dynamically creat page links
//        private IUrlHelperFactory uhf;
//        public PaginationTagHelper(IUrlHelperFactory temp)
//        {
//            uhf = temp;
//        }

//        [ViewContext]
//        [HtmlAttributeNotBound]
//        public ViewContext vc { get; set; }
//        public PageInfo PageModel { get; set; }
//        public string PageAction { get; set; }
//        public string PageClass { get; set; }
//        public bool PageClassesEnabled { get; set; }
//        public string PageClassNormal { get; set; }
//        public string PageClassSelected { get; set; }

//        public override void Process(TagHelperContext thc, TagHelperOutput tho)
//        {
//            IUrlHelper uh = uhf.GetUrlHelper(vc);
//            TagBuilder final = new TagBuilder("div");

//            for (int i = 1; i <= PageModel.TotalPages; i++)
//            {
//                TagBuilder tb = new TagBuilder("a");
//                tb.Attributes["href"] = uh.Action(PageAction, new { pageNum = i });

//                if (PageClassesEnabled)
//                {
//                    tb.AddCssClass(PageClass);
//                    tb.AddCssClass(i == PageModel.CurrentPage ? PageClassSelected : PageClassNormal);
//                }
//                tb.AddCssClass(PageClass);
//                tb.InnerHtml.Append(i.ToString());
//                final.InnerHtml.AppendHtml(tb);
//            }

//            tho.Content.AppendHtml(final.InnerHtml);
//        }
//    }
//}

#pragma checksum "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\Shared\_SearchBarProductPartial.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7f26153e86b0b3a458b318e43411b2854c55a8b8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__SearchBarProductPartial), @"mvc.1.0.view", @"/Views/Shared/_SearchBarProductPartial.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\_ViewImports.cshtml"
using Resq.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\_ViewImports.cshtml"
using Resq.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7f26153e86b0b3a458b318e43411b2854c55a8b8", @"/Views/Shared/_SearchBarProductPartial.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8cc00bc76957b7c524f236855c4d46922c4bf0c", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__SearchBarProductPartial : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Resq.Web.ViewModels.SearchVendorViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<div class=\"search-bar\">\r\n");
#nullable restore
#line 3 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\Shared\_SearchBarProductPartial.cshtml"
     using (Html.BeginForm("SearchProductVendors", "BackOffice"))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <input id=\"VendorName\" name=\"VendorName\" type=\"text\"");
            BeginWriteAttribute("value", " value=\"", 210, "\"", 218, 0);
            EndWriteAttribute();
            WriteLiteral(" class=\"form-control\" placeholder=\"Vendor Name\" required/>\r\n        <input class=\"btn btn-primary\" type=\"submit\" value=\"Search\" id=\"search\"/>\r\n");
#nullable restore
#line 7 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\Shared\_SearchBarProductPartial.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Resq.Web.ViewModels.SearchVendorViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

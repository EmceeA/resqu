#pragma checksum "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b20a3543bee9e74b6c23bd685af67b5acc6d7b61"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BackOffice_VendorRequestHistory), @"mvc.1.0.view", @"/Views/BackOffice/VendorRequestHistory.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b20a3543bee9e74b6c23bd685af67b5acc6d7b61", @"/Views/BackOffice/VendorRequestHistory.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8cc00bc76957b7c524f236855c4d46922c4bf0c", @"/Views/_ViewImports.cshtml")]
    public class Views_BackOffice_VendorRequestHistory : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Resqu.Core.Dto.HistoryDto>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
  
    ViewData["Title"] = "VendorRequestHistory";
    Layout = "~/Views/Shared/_LayoutServices.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"

    <div class=""main-content"">
        <div class=""container-fluid"">
            <div class=""row"">
                <div class=""col-lg-12 mt-4"">
                    <h4>Completed Request History</h4>
                </div>
            </div>
            <div class=""row mt-4"">
                <div class=""col-lg-12"">
                    <div class=""p-4"">
                        <div class=""row align-items-center justify-content-end"">
                            <div class=""col-lg-3"">
                                <h4 class=""text-center"">TOTAL: ");
#nullable restore
#line 20 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                                          Write(ViewBag.Counter);

#line default
#line hidden
#nullable disable
            WriteLiteral(" Services</h4>\r\n                            </div>\r\n");
            WriteLiteral(@"                        </div>
                    </div>
                </div>
            </div>

            <div class=""row mt-4"">
                <div class=""col-lg-12"">
                    <div class=""bg-white add-service-div py-5 mb-5"" style=""height:73vh"">
                        <div class=""table-responsive"">
                            <table class=""table table-vendors"" id=""table_id"">
                                <thead>
                                    <tr>
                                        <th>S/N</th>
                                        <th>Service Start Date</th>
                                        <th>Service End Date</th>
                                        <th>Service Category</th>
                                        <th>Sub-Category</th>
                                        <th>Amount</th>
                                        <th>Name of Customer</th>
                                        <th>Location</th>
                              ");
            WriteLiteral("      </tr>\r\n                                </thead>\r\n                                <tbody>\r\n");
#nullable restore
#line 55 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                      int index = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 56 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                     foreach (var item in Model)
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <tr>\r\n                                            <td>\r\n");
#nullable restore
#line 60 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                                  index++;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                                                        ");
#nullable restore
#line 61 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                                                                   Write(index);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                                                                    </td>\r\n                                            <td>");
#nullable restore
#line 63 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                           Write(item.StartDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("<span class=\"px-2\"></span></td>\r\n                                            <td>");
#nullable restore
#line 64 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                           Write(item.EndDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("<span class=\"px-2\"></span></td>\r\n                                            <td>");
#nullable restore
#line 65 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                           Write(item.ServiceName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 66 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                           Write(item.SubCategory);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 67 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                           Write(item.Amount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 68 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                           Write(item.CustomerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                            <td>");
#nullable restore
#line 69 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                           Write(item.Location);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        </tr>\r\n");
#nullable restore
#line 71 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorRequestHistory.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </tbody>\r\n                            </table>\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Resqu.Core.Dto.HistoryDto>> Html { get; private set; }
    }
}
#pragma warning restore 1591

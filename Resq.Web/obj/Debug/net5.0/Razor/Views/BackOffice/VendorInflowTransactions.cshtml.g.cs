#pragma checksum "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "50acd035e5ea9019a02e6d081e08102e2b2fe2f1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BackOffice_VendorInflowTransactions), @"mvc.1.0.view", @"/Views/BackOffice/VendorInflowTransactions.cshtml")]
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
#nullable restore
#line 2 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
using Resq.Web.ViewModels;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"50acd035e5ea9019a02e6d081e08102e2b2fe2f1", @"/Views/BackOffice/VendorInflowTransactions.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8cc00bc76957b7c524f236855c4d46922c4bf0c", @"/Views/_ViewImports.cshtml")]
    public class Views_BackOffice_VendorInflowTransactions : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Resqu.Core.Entities.Transaction>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
  
    ViewData["Title"] = "VendorInflowTransactions";
    Layout = "~/Views/Shared/_LayoutServices.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<style>\r\n    ");
            WriteLiteral(@"@media screen and (min-width: 676px) {
        .modal-dialog {
            max-width: 1500px;
            /* New width for default modal */
        }
    }
</style>
<div class=""main-content"">
    <div class=""container-fluid"">
        <div class=""row"">
            <div class=""col-lg-12 mt-4"">
                <h4>Vendor Inflows</h4>
            </div>
        </div>
        <div class=""row mt-4"">
            <div class=""col-lg-12"">
                <div class=""bg-white add-service-div p-4"">
                    <div class=""row align-items-center justify-content-between"">
                        <div class=""col-lg-4 col-xl-3"">

                        </div>
                        <div class=""col-lg-3"">
                            <h4 class=""text-center"">TOTAL: ");
#nullable restore
#line 31 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                                      Write(ViewBag.Transactions);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </h4>\r\n                        </div>\r\n");
#nullable restore
#line 33 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                          
                            var models = new BackOfficeInflowSearch();
                            await Html.RenderPartialAsync("_SearchBarBacOfficeInflowPartial", models);

                        

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class=""row mt-4"">
        <div class=""col-lg-12"">
            <div class=""bg-white py-5 mb-5"">
                <div class=""table-responsive"">

                    <table class=""table table-bordered table-striped"">
                        <thead>
                            <tr>
                                <th>S/N</th>
                                <th>Type</th>
                                <th>Category</th>
                                <th>Product Type</th>
                                <th>Service Date</th>
                                <th>Amount</th>
                                <th>Customer Name</th>
                                <th>Vendor Name</th>
");
            WriteLiteral("                                <th>Payment Type</th>\r\n                            </tr>\r\n                        </thead>\r\n                        <tbody>\r\n");
#nullable restore
#line 64 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                              int index = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 65 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                             foreach (var item in Model)
                            {



#line default
#line hidden
#nullable disable
            WriteLiteral("                                <tr>\r\n                                    <td>\r\n");
#nullable restore
#line 71 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                          index++;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        ");
#nullable restore
#line 72 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                   Write(index);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    </td>\r\n\r\n");
#nullable restore
#line 75 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                     if (item.BackOfficeTransactionType == "CR" && item.VendorTransactionType == "CR")
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>\r\n                                            Inflow\r\n                                        </td>\r\n");
#nullable restore
#line 80 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"

                                    }
                                    else if (item.BackOfficeTransactionType == "DR")
                                    {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                        <td>\r\n                                            Outflow\r\n                                        </td>\r\n");
#nullable restore
#line 87 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                    <td>");
#nullable restore
#line 89 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                   Write(item.SubCategory);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 90 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                   Write(item.ServiceType);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 91 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                   Write(item.ServiceDate);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 92 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                   Write(item.VendorAmount);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 93 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                   Write(item.CustomerName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 94 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                   Write(item.VendorName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 95 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                                   Write(item.PaymentType);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                </tr>\r\n");
#nullable restore
#line 97 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\VendorInflowTransactions.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                        </tbody>\r\n                    </table>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Resqu.Core.Entities.Transaction>> Html { get; private set; }
    }
}
#pragma warning restore 1591

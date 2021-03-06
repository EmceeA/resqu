#pragma checksum "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0f1352f868b425f7f78c212db8a084a9a24e56ac"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_BackOffice_GetAllCustomers), @"mvc.1.0.view", @"/Views/BackOffice/GetAllCustomers.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f1352f868b425f7f78c212db8a084a9a24e56ac", @"/Views/BackOffice/GetAllCustomers.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e8cc00bc76957b7c524f236855c4d46922c4bf0c", @"/Views/_ViewImports.cshtml")]
    public class Views_BackOffice_GetAllCustomers : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Resqu.Core.Entities.Customer>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
  
    ViewData["Title"] = "GetAllCustomers";
    Layout = "~/Views/Shared/_LayoutServices.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""main-content"">
    <div class=""container-fluid"">
        <div class=""row"">
            <div class=""col-lg-12 mt-4"">
                <h4>Customers</h4>
            </div>
        </div>
        <div class=""row mt-4"">
            <div class=""col-lg-12"">
                <div class=""bg-white add-service-div p-4"">
                    <div class=""row align-items-center justify-content-between"">
                        <div class=""col-lg-4 col-xl-3"">
");
            WriteLiteral("                        </div>\r\n                        <div class=\"col-lg-3\">\r\n                            <h4 class=\"text-center\">TOTAL: ");
#nullable restore
#line 32 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                                      Write(Model.Count());

#line default
#line hidden
#nullable disable
            WriteLiteral(" Customers</h4>\r\n                        </div>\r\n                        <div class=\"col-lg-5 col-xl-5\">\r\n");
            WriteLiteral(@"                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class=""row mt-4"">
            <div class=""col-lg-12"">
                <div class=""bg-white py-5 mb-5"">
                    <div class=""table-responsive"">
                        <!--<table>-->
");
            WriteLiteral(@"                        <!--</table>-->
                        <table class=""table table-vendors"" id=""table_id"">
                            <thead>
                                <tr>
                                    <th>S/N</th>
                                    <th>Customer???s Name</th>
                                    <th>Gender</th>
                                    <th>Phone Number</th>
                                    <th>Email Address</th>
");
            WriteLiteral("                                    <th>Last Logged In</th>\r\n                                    <th>Last Service Date</th>\r\n                                </tr>\r\n                            </thead>\r\n                            <tbody>\r\n");
#nullable restore
#line 79 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                  int index = 0;

#line default
#line hidden
#nullable disable
#nullable restore
#line 80 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                 foreach (var item in Model)
                                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                    <tr>\r\n                                        <td>\r\n");
#nullable restore
#line 84 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                              index++;

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            ");
#nullable restore
#line 85 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                       Write(index);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                        </td>\r\n                                        <td>");
#nullable restore
#line 87 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                       Write(item.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("   ");
#nullable restore
#line 87 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                                         Write(item.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 88 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                       Write(item.Gender);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 89 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                       Write(item.PhoneNumber);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <td>");
#nullable restore
#line 90 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                       Write(item.EmailAddress);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                                        <!--<td>-->");
            WriteLiteral("<!--</td>-->\r\n                                        <td>");
#nullable restore
#line 92 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                       Write(item.LastLoginDate.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" <span class=\"px-2\">");
#nullable restore
#line 92 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                                                                                     Write(item.LastLoginDate.TimeOfDay.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                                        <td>");
#nullable restore
#line 93 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                       Write(item.LastServiceDate.ToString("dd/MM/yyyy"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" <span class=\"px-2\">");
#nullable restore
#line 93 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                                                                                       Write(item.LastServiceDate.TimeOfDay.ToString());

#line default
#line hidden
#nullable disable
            WriteLiteral("</span></td>\r\n                                    </tr>\r\n");
#nullable restore
#line 95 "C:\Users\HFET\source\repos\Resqu.API\Resq.Web\Views\BackOffice\GetAllCustomers.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            </tbody>\r\n                        </table>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Resqu.Core.Entities.Customer>> Html { get; private set; }
    }
}
#pragma warning restore 1591

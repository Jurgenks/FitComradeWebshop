#pragma checksum "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "55fa909ce8d75e93e737373dc6115aafbab969dd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(FitComrade.Pages.Account.Orders.Pages_Account_Orders_Details), @"mvc.1.0.razor-page", @"/Pages/Account/Orders/Details.cshtml")]
namespace FitComrade.Pages.Account.Orders
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
#line 1 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\_ViewImports.cshtml"
using FitComrade;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"55fa909ce8d75e93e737373dc6115aafbab969dd", @"/Pages/Account/Orders/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7f654989a22e15144000bf7f5f027b30bfbd9afc", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Account_Orders_Details : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-page", "./Invoice", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 4 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
  
    ViewData["Title"] = "Details";
    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Factuur</h1>\r\n\r\n<div>\r\n    \r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-2\">\r\n            Gebruiker:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 19 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Customer.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Voornaam:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 25 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Customer.CustomerSurName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Achternaam:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 31 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Customer.CustomerName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Adres:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 37 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.CustomerAdress.Where(a=>a.CustomerAdressID.Equals(model.Order.CustomerAdress.CustomerAdressID)).FirstOrDefault().Adress));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>        \r\n        <dt class=\"col-sm-2\">\r\n            Postcode:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 43 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.CustomerAdress.Where(a => a.CustomerAdressID.Equals(model.Order.CustomerAdress.CustomerAdressID)).FirstOrDefault().PostalCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>        \r\n        <dt class=\"col-sm-2\">\r\n            Telefoon:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 49 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Customer.CustomerPhone));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            ");
#nullable restore
#line 52 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Customer.CustomerEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 55 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Customer.CustomerEmail));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Prijs:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            €");
#nullable restore
#line 61 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
        Write(Html.DisplayFor(model => model.Order.OrderPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Betaling:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 67 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.Payment.PaymentMethod));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>        \r\n        <dt class=\"col-sm-2\">\r\n            Datum:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 73 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.OrderDate));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-2\">\r\n            Status:\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 79 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
       Write(Html.DisplayFor(model => model.Order.OrderStatus));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </dd>
    </dl>

    <table class=""table"">
        <thead>
            <tr>
                <th>
                    Product
                </th>
                <th>
                    Aantal
                </th>
                <th>
                    Totaal
                </th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 98 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
             foreach (var item in Model.OrderDetail)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        Art. ");
#nullable restore
#line 102 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
                        Write(Html.DisplayFor(modelItem => item.ProductID));

#line default
#line hidden
#nullable disable
            WriteLiteral(", ");
#nullable restore
#line 102 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
                                                                       Write(Html.DisplayFor(modelItem=>item.Product.ProductName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 105 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Quantity));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
#nullable restore
#line 108 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
                   Write(Html.DisplayFor(modelItem => item.TotalPrice));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    </td>                                   \r\n                </tr>\r\n");
#nullable restore
#line 111 "C:\Users\jurge\Documents\MEGAsync\Laptop\Fontys\Semester 2\Project\GitHub\FitComradeWebshop\FitComrade\Pages\Account\Orders\Details.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>\r\n</div>\r\n<div>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "55fa909ce8d75e93e737373dc6115aafbab969dd12127", async() => {
                WriteLiteral("Ga terug");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Page = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<FitComrade.Pages.Account.Orders.DetailsModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<FitComrade.Pages.Account.Orders.DetailsModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<FitComrade.Pages.Account.Orders.DetailsModel>)PageContext?.ViewData;
        public FitComrade.Pages.Account.Orders.DetailsModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591

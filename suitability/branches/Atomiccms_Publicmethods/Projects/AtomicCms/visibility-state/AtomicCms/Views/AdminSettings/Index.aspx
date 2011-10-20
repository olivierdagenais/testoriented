<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<ICollection<ISiteAttribute>>" %>

<%@ Import Namespace="AtomicCms.Core.DomainObjectsImp" %>
<%@ Import Namespace="AtomicCms.Web.Controllers" %>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<%@ Import Namespace="AtomicCms.Common.Extensions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent"
    runat="server">
    Site settings
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="server">
    <table class="dataGrid">
        <caption>
            Site settings</caption>
        <tbody>
            <tr>
                <th>
                    &nbsp;
                </th>
                <th>
                    Key
                </th>
                <th>
                    Value
                </th>
            </tr>
            <%
                foreach (SiteAttribute attr in Model)
                {
            %>
            <tr>
                <td>
                    <%=Html.ActionLink("Edit", ControllerNaming.Action<AdminSettingsController>(x => x.EditSetting(0)) + "/", ControllerNaming.Name<AdminSettingsController>(), new { id = attr.Id }, new { @class = "edit" })%>
                </td>
                <td>
                    <%=attr.Key%>
                </td>
                <td>
                    <%=Html.Encode(attr.Value.Ellipsis(100))%>
                </td>
            </tr>
            <%
                } %>
        </tbody>
    </table>
</asp:Content>

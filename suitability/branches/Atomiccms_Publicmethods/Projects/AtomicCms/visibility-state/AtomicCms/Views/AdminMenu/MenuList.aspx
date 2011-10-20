<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<AtomicCms.Common.Abstract.DomainObjects.IMenu>>" %>
<%@ Import Namespace="AtomicCms.Web.Controllers"%>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    MenuList
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
 
    <table class="dataGrid">
    <caption>Menu list</caption>
        <tr>
            <th>
            </th>
            <th>
                Id
            </th>
            <th>
                Title
            </th>
            <th>
                Type
            </th>
            <th>
                Description
            </th>
        </tr>
        <% foreach (var item in Model)
           { %>
        <tr>
            <td>
                <%=Html.ActionLink("Edit", ControllerNaming.Action<AdminMenuController>(x=>x.EditMenu(null)), ControllerNaming.Name<AdminMenuController>(), new{id=item.Id}, new{@class="edit"}) %>
            </td>
            <td>
                <%= Html.Encode(item.Id) %>
            </td>
            <td>
                <%= Html.Encode(item.Title) %>
            </td>
            <td>
                <%= Html.Encode(item.Type) %>
            </td>
            <td>
                <%= Html.Encode(item.Description) %>
            </td>
        </tr>
        <% } %>
    </table>
</asp:Content>

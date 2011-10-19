<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<ICollection<IUser>>" %>
<%@ Import Namespace="AtomicCms.Web.Controllers"%>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers"%>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent"
    runat="server">
    Users list
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="server">
            <table class="dataGrid">
        <caption>
            Cms users</caption>
        <tbody>
            <tr>
                <th>&nbsp;
                </th>
                <th>
                    Login
                </th>
                <th>
                    Display Name
                </th>
                <th>
                    Email
                </th>
                <th>
                    Registered
                </th>
            </tr>
            <%
                foreach (IUser user in Model)
                {
            %>
            <tr>
                <td>
                    <%=Html.ActionLink("Edit", ControllerNaming.Action<AdminUserController>(x=>x.EditUser(0)) + "/", ControllerNaming.Name<AdminUserController>(), new {id=user.Id}, new{@class="edit"}) %>
                </td>
                <td>
                    <%=user.Login %>
                </td>
                <td>
                    <%=user.DisplayName %>
                </td>
                <td>
                    <%=user.Email%>
                </td>
                <td>
                    <%=user.CreatedAt.ToShortDateString()%>
                </td>
            </tr>
            <%
                } %>
        </tbody>
    </table>
        
</asp:Content>

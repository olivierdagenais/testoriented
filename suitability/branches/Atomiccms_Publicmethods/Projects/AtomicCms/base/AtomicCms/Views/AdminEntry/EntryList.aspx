<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<IEntry>>" %>

<%@ Import Namespace="AtomicCms.Web.Controllers" %>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Pages
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <table class="dataGrid">
        <caption>
            All pages</caption>
        <tbody>
            <tr>
                <th>
                    &nbsp;
                </th>
                <th>
                    &nbsp;
                </th>
                <th>
                    Id
                </th>
                <th>
                    Alias
                </th>
                <th>
                    Title
                </th>
                <th>
                    Last modified
                </th>
            </tr>
            <%
                foreach (IEntry entry in Model)
                {
            %>
            <tr>
                <td>
                    <%=Html.ActionLink("Edit", ControllerNaming.Action<AdminEntryController>(x=>x.EditEntry(null)), ControllerNaming.Name<AdminEntryController>(), new{entry.Id}, new{@class="edit"}) %>
                </td>
                <td>
                    <%  using (Html.BeginForm("Delete", "AdminEntry", FormMethod.Post, null))
                        {%>
                            <%=Html.Hidden("id", entry.Id) %>  
                            <input type="submit" value="delete" class="delete smallButton" onclick="return confirmDeleting();" />
                      <%}%>
                </td>
                <td>
                    <%=entry.Id %>
                </td>
                <td>
                    <%=entry.Alias %>
                </td>
                <td>
                    <%=entry.EntryTitle%>
                </td>
                <td>
                    <%=entry.ModifiedAt.ToShortDateString()%>
                </td>
            </tr>
            <%
                } %>
        </tbody>
    </table>
</asp:Content>

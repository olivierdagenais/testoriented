<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<IMenu>" %>
<%@ Import Namespace="AtomicCms.Web.Controllers"%>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers"%>

<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent"
    runat="server">
    EditMenu
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="server">

    <script type="text/javascript">
        $(document).ready(function()
        {
            $('#toolbar').show();
            $('#toolbar_button_save').click(function()
            {
                $('#editMenuForm').submit();
            });

            $('#editMenuForm').validate();
        });

    </script>

    <h2>
        Edit Menu</h2>
    <%=TempData["SaveResult"]??""%>
    <%
        using (Html.BeginForm("EditMenu", "Admin", FormMethod.Post, new { id = "editMenuForm" }))
        { %>
    <fieldset>
        <legend>Fields</legend>
        <div class="editPageMain">
            <div class="leftControls">
                <div class="field">
                    <%=Html.Hidden("Id", Model.Id)%>
                </div>
                <div class="field">
                    <span class="label">Title</span>
                    <%=Html.TextBox("Title", Model.Title, new { @class = "wide textbox required" })%>
                </div>
                <div class="field">
                    <span class="label">Type</span>
                    <%
                        if (string.IsNullOrEmpty(Model.Type))
                        {%>
                    <%=Html.TextBox("Type", Model.Type, new { @class = "small textbox required" })%>
                    <%}
            else
            {%>
                    <%=Html.Hidden("Type", Model.Type) %>
                    <span><strong>
                        <%=Model.Type %></strong></span>
                    <%} %>
                </div>
                <div class="field">
                    <span class="label">Description</span>
                    <%=Html.TextBox("Description", Model.Description, new { @class = "wide textbox required" })%>
                </div>
                <div style="margin-top: 20px">
                    <span class="label">&nbsp;</span>
                    <table class="dataGrid">
                        <caption>
                            Menu items</caption>
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
                                Navigate Url
                            </th>
                            <th>
                                Is external
                            </th>
                            <th>
                                Entry id
                            </th>
                            <th>
                                Visible
                            </th>
                        </tr>
                        <%=Html.Hidden("MenuItems", Model.MenuItems) %>
                        <%
                            foreach (IMenuItem item in Model.MenuItems)
                            {
                        %>
                        <tr>
                            <td>
                                <%=Html.ActionLink("Edit", ControllerNaming.Action<AdminMenuItemController>(x=>x.EditMenuItem(null)), ControllerNaming.Name<AdminMenuItemController>(), new {id=item.Id}, new {@class="edit"}) %>
                            </td>
                            <td>
                                <%=item.Id %>
                            </td>
                            <td>
                                <%=item.Title %>
                            </td>
                            <td>
                                <%=item.NavigateUrl %>
                            </td>
                            <td>
                                <%=item.IsExternalUrl %>
                            </td>
                            <td>
                                <%=item.Entry == null ? "" : item.Entry.Id.ToString() %>
                            </td>
                            <td>
                                <%=item.Visible %>
                            </td>
                        </tr>
                        <%
                            } %>
                        <tr>
                            <td colspan="7">
                                <div style="float: right;">
                                    <%
                                        if (Model.Id > 0)
                                        {%>
                                    <%=Html.ActionLink("Add", ControllerNaming.Action<AdminMenuItemController>(x => x.EditMenuItem(null)), ControllerNaming.Name<AdminMenuItemController>(), new{id=string.Empty}, new { @class = "add" })%>
                                    <%} %>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </fieldset>
    <%} %>
</asp:Content>

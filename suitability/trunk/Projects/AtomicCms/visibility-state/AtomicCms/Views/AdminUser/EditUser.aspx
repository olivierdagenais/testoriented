<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<IUser>" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	EditUser
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<script type="text/javascript">
    $(document).ready(function()
    {
        $('#toolbar').show();
        $('#toolbar_button_save').click(function()
        {
            $('#editUserForm').submit();
        });
    });
</script>

    <% using (Html.BeginForm("EditUser", "AdminUser", FormMethod.Post, new { id = "editUserForm" }))
       {%>
    <fieldset>
        <legend>Edit user data</legend>
        <div class="editPageMain">
            <div class="leftControls">
                <div class="field">
                    <%=Html.Hidden("CreatedAt", Model.CreatedAt)%>
                </div>
                <div class="field">
                    <%=Html.Hidden("Id", Model.Id)%>
                </div>
                <div class="field">
                    <%=Html.Hidden("Hash", Model.Hash)%>
                </div>
                <div class="field">
                    <span class="label">Login name</span>
                    <%=Html.TextBox("Login", Model.Login, new { @class = "wide textbox" })%>
                </div>
                <div class="field">
                    <span class="label">Display name</span>
                    <%=Html.TextBox("DisplayName", Model.DisplayName, new { @class = "wide textbox" })%>
                </div>
                <div class="field">
                    <span class="label">Email</span>
                    <%=Html.TextBox("Email", Model.Email, new { @class = "wide textbox" })%>
                </div>
                <div class="field">
                    <span class="label">Password</span>
                    <%=Html.TextBox("Password", "", new { @class = "wide textbox" })%>
                </div>
            </div>
        </div>
        <div class="editPagePropertyies">
            <div class="leftControls" style="width: 96%">
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </fieldset>
    <% } %>
</asp:Content>

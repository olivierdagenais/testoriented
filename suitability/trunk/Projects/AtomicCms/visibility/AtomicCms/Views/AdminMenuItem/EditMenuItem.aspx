<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<MenuItemViewModel>" %>

<%@ Import Namespace="AtomicCms.Web.Controllers" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent"
    runat="server">
    EditMenuItem
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="server">

    <script type="text/javascript">
        $(document).ready(function()
        {
            $('#toolbar').show();
            $('#toolbar_button_save').click(function()
            {
                $('#editMenuItemForm').submit();
            });

            $('#editMenuItemForm').validate();

            $('#MenuItem_IsExternalUrl').click(manipulateEnabling);

            setIsExternalCheckBox();
            manipulateEnabling();

            function setIsExternalCheckBox()
            {

                $('#MenuItem_IsExternalUrl').attr('checked', !!$('#MenuItem_NavigateUrl').val());
            }
            function manipulateEnabling()
            {
                if ($('#MenuItem_IsExternalUrl').is(':checked'))
                {
                    $('#MenuItem_NavigateUrl').removeAttr('disabled');
                    $('#MenuItem_EntryId').attr('disabled', 'disabled');
                }
                else
                {
                    $('#MenuItem_NavigateUrl').attr('disabled', 'disabled');
                    $('#MenuItem_EntryId').removeAttr('disabled');
                }
            }
        });

    </script>

    <h2>
        EditMenuItem</h2>
    <%=TempData["SaveResult"]??""%>
    <%
        using (Html.BeginForm("EditMenuItem", "Admin", FormMethod.Post, new { id = "editMenuItemForm" }))
        { %>
    <fieldset>
        <legend>Fields</legend>
        <div class="editPageMain">
            <div class="leftControls">
                <div class="field">
                    <%=Html.Hidden("MenuItem.Id", Model.MenuItem.Id)%>
                </div>
                <div class="field">
                    <span class="label">Title</span>
                    <%=Html.TextBox("MenuItem.Title", Model.MenuItem.Title, new { @class = "wide textbox " })%>
                </div>
                <div class="field">
                    <span class="label"></span>
                    <%=Html.CheckBox("MenuItem.IsExternalUrl", Model.MenuItem.IsExternalUrl)%>
                    <label for="IsExternalUrl">
                        Is external url? (if yes, type it with <strong>http://</strong> prefix)</label>
                </div>
                <div class="field">
                    <span class="label">Navigate Url</span>
                    <%=Html.TextBox("MenuItem.NavigateUrl", Model.MenuItem.NavigateUrl, new { @class = "textbox "})%>
                </div>
                <div class="field">
                    <span class="label">Entry Id</span>
                    <%=Html.TextBox("MenuItem.EntryId", Model.MenuItem.Entry.Id, new { @class = "small textbox " })%><span><a href="#" class="submit" style="float:none; margin-left:10px; right:20%;">Find entry</a></span>
                </div>
                <div class="field">
                    <span class="label">Menu Type</span>
                    <%=Html.DropDownList("MenuItem.MenuId", Model.MenuTypes)%>
                </div>
                <div class="field">
                    <span class="label"></span>
                    <%=Html.CheckBox("MenuItem.Visible", Model.MenuItem.Visible)%>
                    <label for="Visible">
                        Visible?</label>
                </div>
            </div>
        </div>
    </fieldset>
    <%} %>
</asp:Content>

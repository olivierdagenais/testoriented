<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<ISiteAttribute>" %>

<%@ Import Namespace="AtomicCms.Core.ControllerHelpers" %>
<%@ Import Namespace="AtomicCms.Web.Controllers" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent"
    runat="server">
    Edit site attribute
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="server">

    <script type="text/javascript">
        $(document).ready(function()
        {
            $('#toolbar').show();
            $('#toolbar_button_save').click(function()
            {
                $('#editSiteSettingForm').submit();
            });
        });
    </script>

    <% using (Html.BeginForm(ControllerNaming.Action<AdminSettingsController>(x => x.EditSetting(0)), ControllerNaming.Name<AdminSettingsController>(), FormMethod.Post, new { id = "editSiteSettingForm" }))
       {%>
    <fieldset>
        <legend>Edit site setting</legend>
        <div class="editPageMain">
            <div class="leftControls">
                <div class="field">
                    <%=Html.Hidden("CreatedAt", Model.CreatedAt)%>
                </div>
                <div class="field">
                    <%=Html.Hidden("Id", Model.Id)%>
                </div>
                <div class="field">
                    <span class="label">Key</span>
                    <%=Html.TextBox("Key", Model.Key, new { @class = "wide textbox" })%>
                </div>
                <div class="field">
                    <span class="label">Value</span>
                    <%=Html.TextArea("Value", Model.Value, new { @class = "description wide long" })%>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </fieldset>
    <% } %>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master"
    Inherits="System.Web.Mvc.ViewPage<Entry>" %>

<%@ Import Namespace="AtomicCms.Core.DomainObjectsImp"%>
<%@ Import Namespace="AtomicCms.Core.ViewModels" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent"
    runat="server">
    EditEntry
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent"
    runat="server">

    <script type="text/javascript">
        $(document).ready(function()
        {
            $('#toolbar').show();
            $('#toolbar_button_save').click(function()
            {
                $('#editEntryForm').submit();
            });

            $('#editEntryForm').validate({
                rules:
                {
                    SeoTitle:
                    {
                        required:true,
                        minlength:5,
                        maxlength:65
                    },
                    Alias:
                    {
                        required: true,
                        minlength: 2,
                        maxlength: 50
                    },
                    MetaKeywords:
                    {
                        required: true,
                        minWords: 1,
                        maxWords: 25
                    },
                    MetaDescription:
                    {
                        required:true,
                        minlength:25,
                        maxlength:150
                    },
                    EntryTitle:
                    {
                        required:true,
                        maxlength:100
                    }
                },
                messages:
                {
                    SeoTitle:
                    {
                        required: "Please enter title for the page",
                        minlength: "Minimum lenght is 5 letters",
                        maxlength: "It should not be greater than 65 letters"
                    },
                    Alias:
                    {
                        required: "Please enter alias for the page",
                        minlength: "Minimum lenght is 2 letters",
                        maxlength: "It should not be greater than 50 letters"
                    },
                    MetaKeywords:
                    {
                        minWords: "Minimum number of words is 1",
                        maxWords: "Maximum number of words is 25"
                    },
                    MetaDescription:
                    {
                        minlength:"Minimum length is 25 characters",
                        maxlength: "Maximum length is 150 characters"
                    },
                    EntryTitle:
                    {
                        maxlength: "Maximum length is 100 letters"
                    }
                }


            });
        });
    </script>

    <h2>
        EditEntry</h2>
    <%=TempData["SaveResult"] ?? string.Empty %>
    <%= Html.ValidationSummary("Edit was unsuccessful. Please correct the errors and try again.") %>
    <% using (Html.BeginForm("EditEntry", "Admin", FormMethod.Post, new { id = "editEntryForm" }))
       {%>
    <fieldset>
        <legend>Fields</legend>
        <div class="editPageMain">
            <div class="leftControls">
                <div class="field">
                    <%=Html.Hidden("CreatedAt", Model.CreatedAt)%>
                </div>
                <div class="field">
                    <span class="label">Seo Page title</span> (must be unique)
                    <%=Html.TextBox("SeoTitle", Model.SeoTitle, new { @class = "wide textbox" })%>
                </div>
                <div class="field">
                    <span class="label">Alias</span> (it mast contain keywords)
                    <%=Html.TextBox("Alias", Model.Alias, new { @class = "wide textbox" })%>
                </div>
                <div class="field">
                    <span class="label">Entry title</span> (it must contain
                    keywords)
                    <%=Html.TextBox("EntryTitle", Model.EntryTitle, new { @class = "wide textbox" })%>
                </div>
                <div class="field">
                    <span class="label">Entry body</span> (contain keywords specified for the
                    page.) Keyword density must me > 2% of text. 250-700 words <br /><br />
                    <%=Html.TextArea("EntryBody", Model.EntryBody, new { @class = "description htmlEditor wysiwyg" })%>
                </div>
            </div>
            <br />
            <div style="margin-top: 5px; background-color: #fff;">
            </div>
        </div>
        <div class="editPagePropertyies">
            <div class="leftControls" style="width: 96%">
                <div class="field">
                    <span class="label">Meta keywords</span> (contain
                    5-10 keywords or phrases. They must be found in page content
                    (read body text). Keyword phrase must be less 6 words. )
                    <%=Html.TextArea("MetaKeywords", Model.MetaKeywords, new { @class = "description" })%>
                </div>
                <div class="field">
                    <span class="label">Meta description</span> (it must
                    be unique)
                    <%=Html.TextArea("MetaDescription", Model.MetaDescription, new { @class = "description" })%>
                </div>
            </div>
        </div>
        <div style="clear: both;">
        </div>
    </fieldset>
    <% } %>
</asp:Content>

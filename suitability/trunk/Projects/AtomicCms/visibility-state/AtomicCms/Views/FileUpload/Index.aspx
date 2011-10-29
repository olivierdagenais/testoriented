<%@ Import Namespace="System.IO" %>

<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" Inherits="System.Web.Mvc.ViewPage<ICollection<FileInfo>>" %>

<%@ Import Namespace="AtomicCms.Web.Controllers" %>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Uploads
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%using (Html.BeginForm(ControllerNaming.Action<FileUploadController>(x => x.UploadFile(null)), ControllerNaming.Name<FileUploadController>(), FormMethod.Post, new { enctype = "multipart/form-data" })) %>
    <%{%>
    <div style="margin-bottom: 10px;">
        <input type="file" name="fileUpload" id="fileUpload" />
        <input type="submit" value="Upload" />
    </div>
    <%}%>
    <table class="dataGrid">
        <caption>
            Uploads</caption>
        <tbody>
            <tr>
                <th>
                    &nbsp;
                </th>
                <th>
                    Name
                </th>
                <th>
                    Url
                </th>
            </tr>
            <%
                foreach (FileInfo fileInfo in Model)
                {
            %>
            <tr>
                <td>
                    <%using (Html.BeginForm(ControllerNaming.Action<FileUploadController>(x => x.DeleteFile(null)), ControllerNaming.Name<FileUploadController>(), new { fileName = fileInfo.Name }, FormMethod.Post)) %>
                    <%{%>
                    <input type="submit" value="Delete" class="delete" style="height: 17px; padding-top: 0px;" />
                    <%}%>
                </td>
                <td>
                    <%=fileInfo.Name %>
                </td>
                <td>
                    <a href="<%=ResolveUrl("~/") + AtomicCms.Web.Core.Configuration.Configuration.UploadsFolder + "/" + fileInfo.Name %>"
                        class="publish">dowload</a>
                </td>
            </tr>
            <%
                } %>
        </tbody>
    </table>
</asp:Content>

<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AtomicCms.Web.Controllers"%>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers"%>
<ul id="navmenu">
    <li>
        <%=Html.ActionLink("Dashboard", ControllerNaming.Action<AdminController>(x=>x.Dashboard()), ControllerNaming.Name<AdminController>())%></li>
    <li>
        <%=Html.ActionLink("Pages", ControllerNaming.Action<AdminEntryController>(x=>x.EntryList()), ControllerNaming.Name<AdminEntryController>()) %></li>
    <li>
        <%=Html.ActionLink("Menues", ControllerNaming.Action<AdminMenuController>(x=>x.MenuList()), ControllerNaming.Name<AdminMenuController>()) %></li>
    <li>
        <%=Html.ActionLink("Users", ControllerNaming.Action<AdminUserController>(x=>x.ListAll()), ControllerNaming.Name<AdminUserController>()) %></li>
    <li>
        <%=Html.ActionLink("Settings", ControllerNaming.Action<AdminSettingsController>(x => x.Index()), ControllerNaming.Name<AdminSettingsController>())%></li>
        <li>
        <%=Html.ActionLink("File upload", ControllerNaming.Action<FileUploadController>(x => x.Index()), ControllerNaming.Name<FileUploadController>())%></li>
</ul>

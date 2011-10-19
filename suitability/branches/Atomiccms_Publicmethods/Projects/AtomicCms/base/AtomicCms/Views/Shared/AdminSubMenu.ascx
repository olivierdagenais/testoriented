<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%@ Import Namespace="AtomicCms.Web.Controllers"%>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers"%>
<div id="submenu">
    <ul>
        <li>
            <%=Html.ActionLink("New page", ControllerNaming.Action<AdminEntryController>(x => x.EditEntry(null)), ControllerNaming.Name<AdminEntryController>(), new { id = string.Empty }, null)%>
        </li>
        <li>
            <%=Html.ActionLink("New Menu", ControllerNaming.Action<AdminMenuController>(x=>x.EditMenu(null)), ControllerNaming.Name<AdminMenuController>(), new{id=string.Empty}, null) %>
        </li>
        <li>
            <%=Html.ActionLink("Create Menu item", ControllerNaming.Action<AdminMenuItemController>(x =>x.EditMenuItem(null)), ControllerNaming.Name<AdminMenuItemController>(), new{id=string.Empty}, null)%>
        </li>
        <li>
            <%=Html.ActionLink("New site attbibute", ControllerNaming.Action<AdminSettingsController>(x=>x.EditSetting(0)), ControllerNaming.Name<AdminSettingsController>(), new{id=string.Empty}, null)%>
        </li>
    </ul>
</div>

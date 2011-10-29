<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ICollection<IEntry>>" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions"%>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<%@ Import Namespace="AtomicCms.Core.ControllerHelpers" %>
<%@ Import Namespace="AtomicCms.Core.DomainObjectsImp" %>
<%@ Import Namespace="AtomicCms.Web.Controllers" %>
<div class="module_menu">
    <div>
        <div>
            <div>
                <h3>
                <%=Html.Resource("Resources, LastAdded")%>
                </h3>
                <ul class="menu">
                    <%
                        foreach (IEntry entry in Model)
                        {
                    %>
                    <li>
                        <%=Html.ActionLink(entry.EntryTitle, ControllerNaming.Action<HomeController>(x=>x.Content(0,"")), ControllerNaming.Name<HomeController>(), new{id=entry.Id, name = entry.Alias}, new{title=entry.SeoTitle} )%>
                    </li>
                    <%
                        } %>
                </ul>
            </div>
        </div>
    </div>
</div>

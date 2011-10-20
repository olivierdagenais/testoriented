<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AtomicCms.Core.DomainObjectsImp.Menu>" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions" %>
<div class="pill_m">
    <div id="pillmenu">
        <ul id="mainlevel-nav">
            <%
                foreach (IMenuItem item in Model.MenuItems)
                {%>
            <li><a href="<%=Url.BuildMenuLink(item)%>" class="mainlevel-nav">
                <%=Html.Encode(item.Title)%></a> </li>
            <%
                }%>
        </ul>
    </div>
</div>

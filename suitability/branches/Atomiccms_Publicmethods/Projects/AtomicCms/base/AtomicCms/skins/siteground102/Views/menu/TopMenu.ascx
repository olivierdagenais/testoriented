<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AtomicCms.Core.DomainObjectsImp.Menu>" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions" %>
<%@ Import Namespace="AtomicCms.Core.DomainObjectsImp" %>
<ul>
    <%
        foreach (AtomicCms.Core.DomainObjectsImp.MenuItem item in Model.MenuItems)
        {%>
    <li><a href="<%=Url.BuildMenuLink(item) %>" class="mainlevel">
        <%=Html.Encode(item.Title)%></a> </li>
    <%
        }%>
</ul>

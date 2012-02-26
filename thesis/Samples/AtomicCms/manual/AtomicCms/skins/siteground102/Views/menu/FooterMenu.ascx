<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AtomicCms.Core.DomainObjectsImp.Menu>" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions"%>
<%@ Import Namespace="AtomicCms.Core.DomainObjectsImp" %>
|<%
    foreach (AtomicCms.Core.DomainObjectsImp.MenuItem item in Model.MenuItems)
    {%>
        <a href="<%=Url.BuildMenuLink(item) %>" class="copyright">
        <%=Html.Encode(item.Title)%></a>|
  <%}%>

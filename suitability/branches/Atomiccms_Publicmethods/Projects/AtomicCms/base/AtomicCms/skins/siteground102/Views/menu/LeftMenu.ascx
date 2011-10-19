<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AtomicCms.Core.DomainObjectsImp.Menu>" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions"%>
<%@ Import Namespace="AtomicCms.Core.DomainObjectsImp" %>
<h2 style="text-align: center; padding-top: 3px;">
    <%=Model.Title %>
</h2>
<table width="100%" border="0" cellpadding="0" cellspacing="0">
    <%
        foreach (AtomicCms.Core.DomainObjectsImp.MenuItem item in Model.MenuItems)
        {
    %>
    <tr align="left">
        <td>
            <a href="<%=Url.BuildMenuLink(item) %>" class="mainlevel">
                <%=Html.Encode(item.Title)%></a>
        </td>
    </tr>
    <%
        } %>
</table>

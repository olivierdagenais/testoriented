<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AtomicCms.Core.DomainObjectsImp.Menu>" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<%@ Import Namespace="AtomicCms.Core.DomainObjectsImp" %>
<table cellspacing="0" cellpadding="0" style="margin: 0 auto;">
    <tr>
        <td>
            <ul id="mainlevel-nav">
                <%
                    foreach (IMenuItem item in Model.MenuItems)
                    {%>
                <li><a href="<%=Url.BuildMenuLink(item) %>" class="mainlevel-nav">
                    <%=Html.Encode(item.Title)%></a> </li>
                <%
                    }%>
            </ul>
        </td>
    </tr>
</table>

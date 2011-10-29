<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AtomicCms.Core.DomainObjectsImp.Menu>" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions" %>
<table cellspacing="0" cellpadding="0" style="margin: 0 auto;">
    <tr>
        <td>
            <ul id="mainlevel-nav">
                <%
                    foreach (IMenuItem item in Model.MenuItems)
                    {
                %>
                <li><a class="mainlevel-nav" href="<%=Url.BuildMenuLink(item)%>" title="<%=Html.Encode(item.Title)%>">
                    <%=Html.Encode(item.Title)%></a></li>
                <%
                    }%>
            </ul>
        </td>
    </tr>
</table>

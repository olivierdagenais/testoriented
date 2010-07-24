<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AtomicCms.Core.DomainObjectsImp.Menu>" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions" %>
<div style="padding: 10px 0pt 0pt; text-align: center;">
    <%
        foreach (IMenuItem item in Model.MenuItems)
        {
%>| <a class="sgfooter" href="<%=Url.BuildMenuLink(item)%>" title="<%=Html.Encode(item.Title)%>">
        <span>
            <%=Html.Encode(item.Title)%></span></a>
    <%
        }%>
    |
</div>

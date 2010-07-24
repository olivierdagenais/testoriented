<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<AtomicCms.Core.DomainObjectsImp.Menu>" %>
<%@ Import Namespace="AtomicCms.Common.Abstract.DomainObjects" %>
<%@ Import Namespace="AtomicCms.Web.Core.Extensions" %>
<div class="module_menu">
    <div>
        <div>
            <div>
                <h3>
                    <%=Model.Title%></h3>
                <ul class="menu">
                    <%
                        foreach (IMenuItem item in Model.MenuItems)
                        {
                    %>
                    <li><a href="<%=Url.BuildMenuLink(item)%>" title="<%=Html.Encode(item.Title)%>"><span>
                        <%=Html.Encode(item.Title)%></span></a> </li>
                    <%
                        }%>
                </ul>
            </div>
        </div>
    </div>
</div>

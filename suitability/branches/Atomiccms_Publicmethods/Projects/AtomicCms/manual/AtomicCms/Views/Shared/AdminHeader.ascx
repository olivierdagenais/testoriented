<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<span style="float:left;"><%=Html.ActionLink("Visit website", "Default", "Home") %> </span>
    
    <span style="float: right;">
        Welcome [Name]
        /
        <%= Html.ActionLink("Log Off", "LogOff", "Account") %>
    </span>
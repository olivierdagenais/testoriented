<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ViewModelHome>" %>
<%@ Import Namespace="AtomicCms.Core.ViewModels"%>
<h1>
    <%=Model.Attributes.GetValue("NewsFlashTitle")%></h1>
<%=Model.Attributes.GetValue("NewsFlashBody")%>

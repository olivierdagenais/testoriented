<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ViewModelHome>" %>
<%@ Import Namespace="AtomicCms.Core.ViewModels"%>
<%=Model != null ? Model.Attributes.GetValue("TrackingScripts") : ""%>
<%@ Import Namespace="System.Diagnostics"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ViewModelHome>" %>
<%@ Import Namespace="AtomicCms.Core.ViewModels"%>
<%@ Import Namespace="AtomicCms.Common.Dto"%>
<% Debugger.Break(); %>
<%=Model.Attributes.GetValue("Copyrights")%>
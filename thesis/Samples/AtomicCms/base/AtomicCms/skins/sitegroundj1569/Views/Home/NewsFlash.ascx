<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ViewModelHome>" %>
<%@ Import Namespace="AtomicCms.Core.ViewModels" %>
<h3>
    <%=Model.Attributes.GetValue("NewsFlashTitle")%></h3>
<table class="contentpaneopen">
    <tbody>
        <tr>
            <td valign="top">
                <p>
                    <%=Model.Attributes.GetValue("NewsFlashBody")%>
                </p>
            </td>
        </tr>
    </tbody>
</table>

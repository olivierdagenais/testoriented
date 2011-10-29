<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ViewModelHome>" %>
<%@ Import Namespace="AtomicCms.Core.ViewModels"%>
<h3>
    <%=Model!= null ?Model.Attributes.GetValue("NewsFlashTitle") : ""%></h3>
<table class="contentpaneopen">
    <tbody>
        <tr>
            <td valign="top">
                <p>
                    <%=Model != null? Model.Attributes.GetValue("NewsFlashBody") : ""%>
                </p>
            </td>
        </tr>
        <tr>
            <td valign="top">
            </td>
        </tr>
    </tbody>
</table>

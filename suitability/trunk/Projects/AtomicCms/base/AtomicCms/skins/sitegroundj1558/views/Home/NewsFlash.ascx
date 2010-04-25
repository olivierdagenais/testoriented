<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ViewModelHome>" %>
<%@ Import Namespace="AtomicCms.Core.ViewModels" %>
<div id="header">
    <div id="logo">
        <a href="/">
            <%=Model!= null ?Model.Attributes.GetValue("NewsFlashTitle") : ""%></a>
    </div>
    <div id="newsflash">
        <table class="contentpaneopen">
            <tbody>
                <tr>
                    <td valign="top">
                        <p>
                            <%=Model != null? Model.Attributes.GetValue("NewsFlashBody") : ""%></p>
                    </td>
                </tr>
                <tr>
                    <td valign="top">
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
</div>

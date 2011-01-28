<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<dynamic>" %>
<ul>
    <li><%= Html.ActionLink("Home", "Index", "Home") %></li>
    <li><%= Html.ActionLink("Redirect back home", "BackToHome", "Home")%></li>
    <li><%= Html.ActionLink("Add a message", "Message", "Home")%></li>
</ul>

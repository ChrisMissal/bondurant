<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add a message</title>
</head>
<body>
    <h1>Add a message</h1>
    <% Html.RenderPartial("Links"); %>
    <form action="/home/message" method="post">
        <fieldset>
            <legend>Add a message</legend>
            <label>Message<input type="text" value="" name="message" /></label>
            <input type="submit" value="Add" />
        </fieldset>
    </form>
    <% Html.RenderAction("Scripts"); %>
</body>
</html>

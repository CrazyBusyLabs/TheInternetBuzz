<!DOCTYPE html>
<%@ Page Language="C#" Async="true" Inherits="TheInternetBuzz.Web.Pages.Admin.Error" %>
<html>
<head>
    <title></title>
</head>
<body>
<% DisplayError(); %>
<hr />
Current time: <%= DateTime.UtcNow %>
</body>
</html>

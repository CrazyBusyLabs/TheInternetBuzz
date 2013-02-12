<!DOCTYPE html>
<%@ Page Language="C#" Async="true" Inherits="TheInternetBuzz.Web.Pages.Admin.Cache" %>
<html>
<head>
    <title></title>
</head>
<body>
<% DisplayCache(); %>
<hr />
Current time: <%= DateTime.UtcNow %>
</body>
</html>

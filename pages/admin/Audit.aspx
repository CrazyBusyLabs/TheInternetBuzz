<!DOCTYPE html>
<%@ Page Language="C#" Async="true" Inherits="TheInternetBuzz.Web.Pages.Admin.Audit" %>
<html>
<head>
    <title></title>
</head>
<body>
<% DisplayAudit(); %>
<hr />
Current time: <%= DateTime.UtcNow %>
</body>
</html>

<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.HtmlPostBodyControl" Codebehind="HtmlPostBodyControl.ascx.cs" %>
<% if (IncludeFacebookSDKScript) { %>
<div id="fb-root"></div>
<script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "//connect.facebook.net/en_US/all.js#xfbml=1&appId=<%= FacebookAPIKey %>";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));</script>
<% } %>
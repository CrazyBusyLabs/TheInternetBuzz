<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.NavigationControl" Codebehind="NavigationControl.ascx.cs" %>
<div class="navigation">
<div class="navigationMenu">
<ul>
<li><a href="<%= TheInternetBuzz.Web.URLBuilder.BaseURL %>">Home</a></li>
<li><a href="<%= TheInternetBuzz.Web.URLBuilder.BuildURL("theinternetbuzz","TopBuzz") %>">Top Buzz</a></li>
<li><a href="<%= TheInternetBuzz.Web.URLBuilder.BuildURL("theinternetbuzz","Credits") %>">Credits</a></li>
</ul>
</div>
</div>
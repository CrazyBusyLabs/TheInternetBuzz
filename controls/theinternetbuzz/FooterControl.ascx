<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.FooterControl" Codebehind="FooterControl.ascx.cs" %>
<div class="footer"><a href="<%= TheInternetBuzz.Web.URLBuilder.BaseURL %>">TheInternetBuzz.com</a> - Copyright &copy; 2006-2013 Crazy Busy Management - All rights reserved.<br />
<% if (Location != null) { %>User <%= Location.city%>, <%= Location.countryName%> (<%= Location.longitude%>, <%= Location.latitude%>) <% } %>
</div>
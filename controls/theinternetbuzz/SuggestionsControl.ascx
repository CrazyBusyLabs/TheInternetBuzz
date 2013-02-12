<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.SuggestionsControl" Codebehind="SuggestionsControl.ascx.cs" %>
<div class="sidebarBox">
<div class="sidebarTitle"><%= Title %></div>
<div class="sidebarContent paddedSidebarContent">
<div class="trendsContent"><% DisplaySuggestions(); %></div>
</div>
</div>
<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.BuzzListControl" Codebehind="BuzzListControl.ascx.cs" %>
<div class="sidebarBox trendsListBox">
<div class="sidebarTitle trendsListTitle"><%= Title %></div>
<div class="sidebarContent paddedSidebarContent">
<div class="trendsContent trendsList"><% DisplayBuzzList(); %></div>
</div>
</div>
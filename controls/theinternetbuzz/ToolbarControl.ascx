<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.HeaderToolbarControl" Codebehind="ToolbarControl.ascx.cs" %>
<div class="toolbar">
<div class="toolbarLogo"><a href="<%= TheInternetBuzz.Web.URLBuilder.BaseURL %>"><img src="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("images/logo.png") %>" width="204" height="54" alt="TheInternetBuzz.org - social trending"/></a></div>
<div class="toolbarSearch">
    <form action="<%= TheInternetBuzz.Web.URLBuilder.BaseURL %>topic/" id="searchForm">
        <div class="toolbarSearchInput">
            <input type="text" class="toolbarSearchQuery" name="topic" value="<%= DefaultSearchValue %>" onfocus="this.select()" title="Enter your search term"/>
            <input type="submit" class="toolbarSearchSubmit" title="Search" value="" tabindex="0"/>
            <div class="clear"></div>
        </div>
    </form>
</div>
<div class="clear"></div>
</div>
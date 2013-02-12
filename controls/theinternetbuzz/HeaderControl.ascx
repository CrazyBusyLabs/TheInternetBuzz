<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.NavigationControl" Codebehind="NavigationControl.ascx.cs" %>
<div class="navigation">
<div class="navigationMenu">
<ul>
<li><a href="<%= TheInternetBuzz.Web.URLBuilder.BaseURL %>">Home</a></li>
<li><a href="<%= TheInternetBuzz.Web.URLBuilder.BuildURL("theinternetbuzz","TopBuzz") %>">Top Buzz</a></li>
<li><a href="<%= TheInternetBuzz.Web.URLBuilder.BuildURL("theinternetbuzz","Credits") %>">Credits</a></li>
</ul>
</div>


<div class="toolbarSearch">
    <form action="<%= TheInternetBuzz.Web.URLBuilder.BaseURL %>topic/" id="searchForm">
        <div class="toolbarSearchInput">
            <input type="text" class="toolbarSearchQuery" name="topic" value="<%= "hello" %>" onfocus="this.select()" title="Enter your search term"/>
            <input type="submit" class="toolbarSearchSubmit" title="Search" value="" tabindex="0"/>
            <div class="clear"></div>
        </div>
    </form>
</div>

</div>
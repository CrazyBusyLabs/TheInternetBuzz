<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.TopicControl" Codebehind="TopicControl.ascx.cs" %>
<div class="topic mainBox">
<div class="topicContent mainContent">
<div id="freebaseHtml" runat="server">
<div class="topicTitle"><h2><%= DisplayQuery %></h2></div>
<div id="topicAliasesHtml" class="topicAliasesText" runat="server">Also know as <% DisplayAliases(); %></div>
<div class="topicSummary">
    <div id="topicSummaryThumbmailHtml" class="topicThumbmail" runat="server"><img src="<%=TopicItem.FreebaseImageURL %>" alt="<%= DisplayQuery %>"/></div>
    <div class="topicSummaryText"><%=TopicItem.FreebaseSummary %></div>
</div>
<div class="topicLinks"><% DisplayLinks(); %></div>
<div class="topicSource">
Source: <%= DisplayQuery %> on <a href="http://www.freebase.com/">Freebase</a>, licensed under <a href="http://creativecommons.org/licenses/by/2.5/">CC-BY</a><br/>
Other content from <a href="<%= TopicItem.WikipediaURL %>">Wikipedia</a>, licensed under the <a href="http://www.gnu.org/copyleft/fdl.html">GFDL</a>
</div>
</div>
<div id="wikipediaHtml" runat="server">
<div class="topicTitle"><h2><%= DisplayQuery %></h2></div>
<div class="topicSummary">
    <div class="topicSummaryText"><%=TopicItem.WikipediaSummary %></div>
</div>
<div class="topicSource">
Source:
<a href="http://www.wikipedia.com">Wikipedia</a>, licensed under the <a href="http://www.gnu.org/copyleft/fdl.html">GFDL</a>
</div>
</div>
</div>
</div>
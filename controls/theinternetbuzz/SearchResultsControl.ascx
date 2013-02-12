<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.SearchResultsControl" Codebehind="SearchResultsControl.ascx.cs" %>
<%@ Import Namespace="TheInternetBuzz.Services.Config" %>
<%@ Import Namespace="TheInternetBuzz.Data.Search" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Collections" %>
<div class="searchResults mainBox">
<div class="mainContent">
<%
    int maxURLLength = ConfigService.GetConfig(ConfigKeys.THEINTERNETBUZZ_SEARCH_URL_DISPLAY_LENGTH, 80);
    if (SearchResultList != null)
    {
        foreach (SearchResultItem resultItem in SearchResultList)
        {
            string displayURL = resultItem.DisplayURL;
            if (displayURL != null && displayURL.Length > maxURLLength)
            {
                displayURL = displayURL.Substring(0, maxURLLength) + "...";
            }        
%>
        <div class="searchResult">
            <div class="resultTitle"><h2><a href="<%= resultItem.URL %>" onmousedown="trackEvent('<%= TrackSection %>','click','<%: TrackTopic.Replace("'", @"\'") %>');"><%= resultItem.Title %></a></h2></div>
<% if (DisplayImageURL)
   { %><span class="resultImageURL"><a href="<%= resultItem.URL %>" onmousedown="trackEvent('<%= TrackSection %>','click','<%: TrackTopic.Replace("'", @"\'") %>');"><img src="<%= resultItem.ImageURL %>" alt="<%= resultItem.Title %>" /></a></span><% } %>
<% if (resultItem.Abstract != null && resultItem.Abstract.Length > 0)
   { %><span class="resultAbstract"><%= resultItem.Abstract %></span><br /><% } %>
<% if (DisplayURL)
   { %><span class="resultURL"><a href="<%= resultItem.URL %>" onmousedown="trackEvent('<%= TrackSection %>','click','<%: TrackTopic.Replace("'", @"\'") %>');"><%= displayURL%></a></span><br /><% } %>
<% if (DisplayDate)
   { %><span class="resultDate"><%= resultItem.PublishedDate.ToUniversalTime().ToString("r")%></span><br /><% } %>
<% if (DisplaySource && resultItem.Source != null && resultItem.Source.Length > 0)
   { %><span class="resultSource">Source: <%= resultItem.Source %></span><br /><% } %>
<!-- Provider: <%= resultItem.Provider %> -->
        </div>
<%
        }
        if (DisplayPagination)
        {
%>
<div class="pagination">
<%
            string baseURL = TheInternetBuzz.Web.URLBuilder.BaseURL + PaginationSection + "?topic=" + HttpUtility.UrlEncode(PaginationQuery) + "&amp;page=";
            for (int i = 1; i <= PaginationMax; i++)
            {
                if (PaginationIndex == i)
                {
%>
<span class="paginationBox paginationSelected"><%= i%></span>
<%
                }
                else
                {
                    string url = HttpUtility.UrlPathEncode(baseURL + i);
%>
<span class="paginationBox paginationNonSelected"><a href="<%= url %>"><%= i%></a></span>       
<% 
                }
            }   
%>
</div>
<%
        }
    }
%>
</div>
</div>
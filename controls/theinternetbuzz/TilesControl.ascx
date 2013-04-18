<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.TilesControl" Codebehind="TilesControl.ascx.cs" %>
<%@ Import Namespace="TheInternetBuzz.Data.Trends" %>
<%@ Import Namespace="TheInternetBuzz.Web" %>
<div class="tilesBox mainBox">
  <div id="tiles" class="tilesContent mainContent">
<% 
    if (TrendsList != null)
    {
        foreach (TrendItem trendItem in TrendsList)
        {
            if (trendItem.TileImageURL != null && trendItem.TileImageURL.Length > 0)
            {
                string htmlEncodedTrend = HttpUtility.HtmlEncode(trendItem.Title);
                string url = URLBuilder.BuildURL("topic", trendItem.Title);
                string cssCloudClass = "tile" + trendItem.Weight;
%>
<div class="tile">
<div class="tileImage"><a href="<%= url %>"><img src="<%= trendItem.TileImageURL %>" alt="<%= htmlEncodedTrend  %>"></a></div>
<div class="tileTitle <%= cssCloudClass %>"><a href="<%= url %>"><%= htmlEncodedTrend  %></a></div>
</div>
<%

            }
        }
    }   
%>
  </div>
</div>
<script>
    var $container = $('#tiles');
    $container.imagesLoaded(function(){
        $container.isotope({
            itemSelector: '.tile',
            masonry: {
                columnWidth: 170
            }
        });
    });
</script>
<!DOCTYPE html>
<%@ Page Language="C#" Async="true" Inherits="TheInternetBuzz.Web.Pages.TopBuzz" %>
<%@ OutputCache Duration="3600" VaryByParam="none" Location="Any"  %> 
<html>
<head>
<theinternetbuzz:head runat="server" ID="htmlHead" Title="The Internet Buzz | Top Buzz" 
    Description="TheInternetBuzz.com Top Buzz provides the top trends for the last year." 
    Keywords="news, business, entertainment, movies, music, events, sports, soccer, football, baseball, basketball, technology, travel, weather"
     RobotIndexing="true" />
</head>
<body>
<theinternetbuzz:navigation runat="server" ID="navigationHtml"/>
<div class="page">
<div class="one-columns-main">
<theinternetbuzz:toolbar runat="server" ID="toolbarHtml"/>
<theinternetbuzz:title runat="server" ID="titleHtml"/>
<theinternetbuzz:cloud runat="server" ID="topTrendsCloudHtml"/>
<theinternetbuzz:category runat="server" ID="topTrendsMapHtml"/>
<theinternetbuzz:footer runat="server" ID="footerHtml"/>
</div>
</div>
</body>
</html>
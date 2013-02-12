<!DOCTYPE html>
<%@ Page Language="C#" Async="true" Inherits="TheInternetBuzz.Web.Pages.Ads.Default"%>
<%@ Import Namespace="TheInternetBuzz.Data.Ads" %>
<html>
<head>
<title></title>
</head>
<body>
<%
    switch (AdsType)
    {
        case AdsTypeEnum.Box250x250:
%>
<a href="http://click.linksynergy.com/fs-bin/click?id=tMwHFr2pIZY&offerid=149849.10000065&subid=0&type=4" target="_top"><img border="0" src="http://ad.linksynergy.com/fs-bin/show?id=tMwHFr2pIZY&bids=149849.10000065&subid=0&type=4&gridnum=14" /></a>
<%
            break;

        case AdsTypeEnum.Box160x600:
%>
<a href="http://click.linksynergy.com/fs-bin/click?id=tMwHFr2pIZY&offerid=184280.10000024&subid=0&type=4" target="_top"><img border="0" alt="Blue_160x600" src="http://ad.linksynergy.com/fs-bin/show?id=tMwHFr2pIZY&bids=184280.10000024&subid=0&type=4&gridnum=9"></a>
<%
            break;

        case AdsTypeEnum.Box728x90:
%>
<script type="text/javascript">
<!--    amazon_ad_tag = "crazybusy-20"; amazon_ad_width = "728"; amazon_ad_height = "90"; amazon_ad_link_target = "new"; amazon_ad_border = "hide"; //-->
</script>
<script type="text/javascript" src="http://www.assoc-amazon.com/s/ads.js"></script>
<%
            break;
    }
 %>
</body>
</html>
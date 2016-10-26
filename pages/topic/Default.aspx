<!DOCTYPE html>
<%@ Page Language="C#" Async="true" AsyncTimeout="3" Inherits="TheInternetBuzz.Web.Pages.Topic.Default" %>
<html>
<head>
<theinternetbuzz:head runat="server" ID="headHtml" IncludePrettyPhotoScript="true" IncludeJQueryScript="true" 
    IncludeSliderScript="true" IncludeGooglePlusOneScript="true" IncludeUserVoice="true"
    IncludeTwitterScript="true"/>
</head>
<body>
<theinternetbuzz:postbody runat="server" ID="postBodyHtml" IncludeFacebookSDKScript="true" />
<theinternetbuzz:navigation runat="server" ID="navigationHtml"/>
<div class="page">
<div class="one-columns-main">
<theinternetbuzz:toolbar runat="server" ID="toolbarHtml"/>
<theinternetbuzz:title runat="server" ID="titleHtml"/>
</div>
<div class="two-columns-main">
<whatthetrend:explanation runat="server" ID="explanationHtml"/>
<theinternetbuzz:topic runat="server" ID="topicHtml"/>
<theinternetbuzz:results runat="server" ID="tweetsResultsHtml"/>
</div>
<div class="two-columns-side">
<amazon:mp3 runat="server" ID="MP3Html"/>
<google:ads runat="server" AdsType="Box250x250" ID="googleAd1Html"/>
<theinternetbuzz:buzzlist runat="server" ID="buzzListHtml"/>
<google:ads runat="server" AdsType="Box160x600" ID="googleAd2Html"/>
</div>
<div class="clear"></div>
<div class="one-columns-main">
<theinternetbuzz:video runat="server" ID="videoHtml"/>
<google:ads runat="server" AdsType="Box728x90" ID="googleAd0Html"/>
<theinternetbuzz:share runat="server" ID="shareThisHtml"/>
<theinternetbuzz:footer runat="server" ID="footerHtml"/>
</div>
</div>
<theinternetbuzz:track runat="server" ID="trackerHtml"/>
</body>
</html>

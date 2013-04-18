<!DOCTYPE html>
<%@ Page Language="C#" Async="true" Inherits="TheInternetBuzz.Web.Pages.Default" %>
<%@ OutputCache Duration="120" VaryByParam="none" Location="Any"  %>
<html>
<head>
<theinternetbuzz:head runat="server" ID="htmlHead" Title="The Internet Buzz" 
    Description="TheInternetBuzz.com provides the current popular topics on the Internet. Stay in touch with what is happening in our world by using TheInternetBuzz.com." 
    Keywords="news, celebrities, movies, music, soccer, football, baseball, basketball, weather"
    IncludePrettyPhotoScript="true" IncludeJQueryScript="true" IncludeIsotope="true" IncludeSliderScript="true" RobotIndexing="true"
    IncludeGooglePlusOneScript="true" IncludeUserVoice="true"/>
</head>
<body>
<theinternetbuzz:postbody runat="server" ID="postBodyHtml" IncludeFacebookSDKScript="true" />
<theinternetbuzz:navigation runat="server" ID="navigationHtml"/>
<div class="page">
<div class="one-columns-main">
<theinternetbuzz:toolbar runat="server" ID="toolbarHtml"/>
<theinternetbuzz:title runat="server" ID="titleHtml"/>
<theinternetbuzz:cloud runat="server" ID="cloudHtml"/>
<theinternetbuzz:tiles runat="server" ID="tilesHtml"/>
<theinternetbuzz:video runat="server" ID="videoHtml"/>
<google:ads runat="server" AdsType="Box728x90" ID="googleAd2Html"/>
<theinternetbuzz:share runat="server" ID="shareThisHtml"/>
<theinternetbuzz:footer runat="server" ID="footerHtml"/>
</div>
</div>
</body>
</html>
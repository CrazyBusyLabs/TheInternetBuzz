<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.Amazon.AdsControl" Codebehind="AdsControl.ascx.cs" %>
<%@ Import Namespace="TheInternetBuzz.Data.Ads" %>
<div class="adsBox">
<div class="adsContent">
<div class="adsContentCenter" style="width:728px;">

<%
    switch (AdsType)
    {
        case AdsTypeEnum.Box728x90:
%>
<iframe src="http://rcm.amazon.com/e/cm?t=crazybusy-20&o=1&p=48&l=ur1&category=electronics&banner=1PWXNPPRG6D2NYYKV782&f=ifr" width="728" height="90" scrolling="no" border="0" marginwidth="0" style="border:none;" frameborder="0"></iframe>
<%
            break;
    }
 %>
 </div>
 </div>
 </div>
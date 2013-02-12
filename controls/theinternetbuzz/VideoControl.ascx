<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.VideoControl" Codebehind="VideoControl.ascx.cs" %>
<div class="videoTrends mainBox darkBlue">
    <div class="videoTrendsContent mainContent">
        <div class="contentSliderLeft"><button class="contentSliderLeftButton"></button></div>
        <div class="contentSlider" style="float:left;">
            <ul class="items"><% DisplayTrends(); %></ul>
        </div>
        <div class="contentSliderRight"><button class="contentSliderRightButton"></button></div>
        <div class="clear"></div>
    </div>
 </div>
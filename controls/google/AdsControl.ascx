<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.Google.AdsControl" Codebehind="AdsControl.ascx.cs" %>
<div class="adsBox">
<div class="adsContent">
<div class="adsContentCenter" style="width:<%= Width %>px;">
<script type="text/javascript">
google_ad_client = "<%= GoogleAdsenseAccount %>";
google_ad_slot = '<%= Slot %>';
google_ad_width = <%= Width %>;
google_ad_height = <%= Height %>;
</script>
<script type="text/javascript" src="http://pagead2.googlesyndication.com/pagead/show_ads.js"></script>
</div>
</div>
</div>
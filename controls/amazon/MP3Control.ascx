<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.Amazon.MP3Control" Codebehind="MP3Control.ascx.cs" %>
<div class="adsBox">
<div class="adsContent">
<div class="adsContentCenter">
<script type='text/javascript'>
var amzn_wdgt={widget:'MP3Clips'};
amzn_wdgt.tag='crazybusy-20';
 amzn_wdgt.widgetType='SearchAndAdd';
amzn_wdgt.keywords='<%= Query.Replace("'", @"\'") %>';
amzn_wdgt.title='Music about <%= Query.Replace("'", @"\'") %>...';
amzn_wdgt.width='250';
amzn_wdgt.height='250';
amzn_wdgt.shuffleTracks='True';
amzn_wdgt.marketPlace='US';
 </script>
<script type='text/javascript' src='http://wms.assoc-amazon.com/20070822/US/js/swfobject_1_5.js'></script>
</div>
</div>
</div>

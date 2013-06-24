<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.HtmlHeadControl" Codebehind="HtmlHeadControl.ascx.cs" %>
<meta charset="utf-8">
<title><%: Title %></title>
<meta name="description" content="<%: Description %>" />
<meta name="keywords" content="<%= Keywords %>" />
<% if (RobotIndexing) { %>
<meta name="robots" content="index,follow,noarchive" />
<% } else { %>
<meta name="robots" content="noindex,nofollow,noarchive" />
<% } %>
<link rel="stylesheet" type="text/css" href="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("styles/styles.css")%>"/>
<link rel="shortcut icon" href="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("images/icon.ico")%>"/>
<link rel="image_src" type="image/jpeg" href="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("images/logo_thumb.jpg")%>"/>
<meta property="fb:app_id" content="<%= FacebookAPIKey %>"/>
<% if (IncludeJQueryScript) { %>
<script type="text/javascript" src="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("javascript/jquery-1.9.0.min.js")%>"></script>
<% if (IncludeIsotope)
   { %>
<script type="text/javascript" src="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("javascript/jquery.imagesloaded.min.js")%>"></script>
<script type="text/javascript" src="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("javascript/jquery.isotope.min.js")%>"></script>
<% } } %>
<% if (IncludePrettyPhotoScript) { %>
<link rel="stylesheet" type="text/css" href="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("styles/prettyPhoto.css")%>"/>
<script type="text/javascript" src="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("javascript/jquery.prettyPhoto.js")%>"></script>
<script type="text/javascript">
$(document).ready(function() {
    $("a[class^='videoTrendsLink']").prettyPhoto({ theme: 'dark_rounded' });
});
</script><% } %>
<% if (IncludeSliderScript) { %><script type="text/javascript" src="<%= TheInternetBuzz.Web.URLBuilder.BuildResourceURL("javascript/slider.js")%>"></script><% } %>
<script type="text/javascript">
var _gaq = _gaq || [];
_gaq.push(['_setAccount', '<%= GoogleAnalyticsAccount %>']);
_gaq.push(['_trackPageview']);
(function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
})(); 

function trackEvent(category, action, label) {
    try {
        _gaq.push(['_trackEvent', category, action, label]);
    } catch (err) { }
}
</script>
<% if (IncludeGooglePlusOneScript) { %><script type="text/javascript">
    (function () {
        var po = document.createElement('script'); po.type = 'text/javascript'; po.async = true;
        po.src = 'https://apis.google.com/js/plusone.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(po, s);
    })();
</script><% } %>
<% if (IncludeUserVoice) { %><script type="text/javascript">
var uvOptions = {};
(function() {
    var uv = document.createElement('script'); uv.type = 'text/javascript'; uv.async = true;
    uv.src = ('https:' == document.location.protocol ? 'https://' : 'http://') + 'widget.uservoice.com/J1dwKSDjKcSosH7WMEL1g.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(uv, s);
})();
</script><% } %>
<% if (IncludeTwitterScript) { %><script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0], p = /^http:/.test(d.location) ? 'http' : 'https'; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = p + "://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script><% } %>



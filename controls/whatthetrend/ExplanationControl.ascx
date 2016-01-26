<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.WhatTheTrend.ExplanationControl" Codebehind="ExplanationControl.ascx.cs" %>

<div class="explanation mainBox">
<div class="explanationContent mainContent">
<div class="explanationTitle"><h2>Why is <%= DisplayQuery %> buzzing?</h2></div>
<% if (ExplanationItem != null && ExplanationItem.Explanation != null && ExplanationItem.Explanation.Length > 0) { %>
<div class="explanationText"><%=ExplanationItem.Explanation %></div>
<div class="explanationSource"><a href="http://api.whatthetrend.com/"><img src="http://api.whatthetrend.com/images/wtt_api_badge_120.png" alt="whatthetrend"></a></div>
<% } %>
<div class="explanationTitle"><h5>Do you know why <%= DisplayQuery %> is buzzing?</h5></div>
<div class="fb-comments" data-href="<%= TopicURL %>" data-width="550" data-num-posts="10" data-colorscheme="light"></div>
<script type="text/javascript">
    window.fbAsyncInit = function () {
        FB.Event.subscribe('comment.create',
            function (response) {
                trackEvent('<%= TrackerCategory %>', '<%= TrackerAction %>', '<%: TrackerLabel.Replace("'", @"\'") %>');
            });
        };
</script>
</div>
</div>
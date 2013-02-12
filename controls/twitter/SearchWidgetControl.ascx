<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.Twitter.SearchWidgetControl" Codebehind="SearchWidgetControl.ascx.cs" %>
<div class="twitterSearchWidgetBox">
<script src="http://widgets.twimg.com/j/2/widget.js" type="text/javascript"></script>
<script type="text/javascript">
new TWTR.Widget({
  version: 2,
  type: 'search',
  search: '<%= WidgetQuery.Replace("'", @"\'") %>',
  interval: 6000,
  title: 'Live Twitter Tweets',
  subject: '<%= WidgetSubject.Replace("'", @"\'") %>',
  width: 250,
  height: 400,
  theme: {
    shell: {
      background: '#ECEFF5',
      color: '#3B5998'
    },
    tweets: {
      background: '#FAFAFA',
      color: '#333333',
      links: '#1985b5'
    }
  },
  features: {
    scrollbar: false,
    loop: true,
    live: true,
    hashtags: true,
    timestamp: true,
    avatars: true,
    behavior: 'default'
  }
}).render().start();
</script>
</div>
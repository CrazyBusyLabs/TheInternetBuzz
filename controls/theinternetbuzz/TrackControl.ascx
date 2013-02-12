<%@ Control Language="C#" Inherits="TheInternetBuzz.Web.Controls.TrackControl" Codebehind="TrackControl.ascx.cs" %>
<script type="text/javascript">
    trackEvent('<%= TrackerCategory %>', '<%= TrackerAction %>', '<%: TrackerLabel.Replace("'", @"\'") %>');
</script>



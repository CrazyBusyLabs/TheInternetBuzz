using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TheInternetBuzz.Data.Ads;
using TheInternetBuzz.Services.Config;

namespace TheInternetBuzz.Web.Controls.Google
{
    public partial class AdsControl : UserControl
    {
        public AdsTypeEnum AdsType
        {
            private get;
            set;
        }

        protected string Slot
        {
            get;
            private set;
        }

        protected int Width
        {
            get;
            private set;
        }

        protected int Height
        {
            get;
            private set;
        }

        protected string GoogleAdsenseAccount
        {
            get;
            set;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            switch (AdsType)
            {
                case AdsTypeEnum.Box250x250:
                    Slot = ConfigService.GetConfig(ConfigKeys.GOOGLE_ADSENSE_SLOT_250x250, "");
                    Width = 250;
                    Height = 250;
                    break;

                case AdsTypeEnum.Box160x600:
                    Slot = ConfigService.GetConfig(ConfigKeys.GOOGLE_ADSENSE_SLOT_160x600, "");
                    Width = 160;
                    Height = 600;
                    break;

                case AdsTypeEnum.Box728x90:
                    Slot = ConfigService.GetConfig(ConfigKeys.GOOGLE_ADSENSE_SLOT_728x90, "");
                    Width = 728;
                    Height = 90;
                    break;
            }

            GoogleAdsenseAccount = ConfigService.GetConfig(ConfigKeys.GOOGLE_ADSENSE_ACCOUNT, "");
        }
    }
}
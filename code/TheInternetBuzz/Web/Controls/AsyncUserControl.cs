using System;
using System.Web;
using System.Web.UI;

using TheInternetBuzz.Services.Error;
using TheInternetBuzz.Services.Logging;

namespace TheInternetBuzz.Web.Controls
{
    public abstract class AsyncUserControl : UserControl
    {
        protected delegate void AsyncTaskDelegate();
        private AsyncTaskDelegate _runnerDelegate = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsAsync)
            {
                Page.RegisterAsyncTask(new PageAsyncTask(new BeginEventHandler(BeginHandler), new EndEventHandler(EndHandler), new EndEventHandler(TimeoutHandler), null, true));
            }
            else
            {
                LoadData();
                this.Visible = isDisplayControl();
            }
        }

        public IAsyncResult BeginHandler(object src, EventArgs e, AsyncCallback cb, object state)
        {
            IAsyncResult result = null;
            try 
            {
                _runnerDelegate = new AsyncTaskDelegate(this.LoadData);
               result = _runnerDelegate.BeginInvoke(cb, state);
            }
            catch (Exception exception)
            {
                ErrorService.Log("AsyncUserControl", "BeginHandler", "", exception);
                Trace.Write("AsyncUserControl", "EndHandler", exception);
            }
            return result;
        }

        public void EndHandler(IAsyncResult ar)
        {
            try
            {
                if (_runnerDelegate != null) _runnerDelegate.EndInvoke(ar);
                this.Visible = isDisplayControl();
            }
            catch (Exception exception)
            {
                ErrorService.Log("AsyncUserControl", "EndHandler", "", exception);
                Trace.Write("AsyncUserControl", "EndHandler", exception);
            }
        }

        public void TimeoutHandler(IAsyncResult ar)
        {
            LogService.Warn(typeof(TheInternetBuzz.Web.Controls.AsyncUserControl), "TimeoutHandler");
            Trace.Write("AsyncUserControl", "TimeoutHandler");
            this.Visible = isDisplayControl();
        }

        public Boolean isBot() {
            Boolean result = false;

            String userAgent = Request.UserAgent;
            if (userAgent != null) {
                if (userAgent.IndexOf("Googlebot") >= 0)
                {
                    result = true;
                }
                else if (userAgent.IndexOf("Twitterbot") >= 0) 
                {
                    result = true;
                }
                else if (userAgent.IndexOf("Mediapartners-Google") >= 0)
                {
                    result = true;
                }
            }

            return result;
        }

        abstract protected void LoadData();
        abstract protected bool isDisplayControl();
    }
}

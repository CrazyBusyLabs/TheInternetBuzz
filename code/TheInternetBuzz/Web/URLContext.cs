using System.Web;

namespace TheInternetBuzz.Web
{
    public class URLContext
    {
        static public URLContext GetURLContext()
        {
            return (URLContext) HttpContext.Current.Items["context"];
        }

        static public void StoreURLContext(URLContext urlContext)
        {
            HttpContext.Current.Items.Add("context", urlContext);
        }

        public string Scheme
        {
            get;
            set;
        }

        public string Host
        {
            get;
            set;
        }

        public string Virtual
        {
            get;
            set;
        }

        public string Section
        {
            get;
            set;
        }

        public string Topic
        {
            get;
            set;
        }

        public string Page
        {
            get;
            set;
        }

        public string Resource
        {
            get;
            set;
        }
    }
}
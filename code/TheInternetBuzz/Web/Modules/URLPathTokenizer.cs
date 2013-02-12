namespace TheInternetBuzz.Web.Modules
{
    public class URLPathTokenizer
    {
        private string[] tokens = null;
        private int index = 0;

        public URLPathTokenizer(string filePath)
        {
            tokens = filePath.Split('/');
        }

        public string nextToken()
        {
            string token = null;
            if (index < tokens.Length) 
            {
                token = tokens[index];
                index++;
                if (token.Length == 0)
                {
                    token = nextToken();
                }
            }

            return token;
        }
    }
}
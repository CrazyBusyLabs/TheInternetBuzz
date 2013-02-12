using System;

using TheInternetBuzz.Data.Explanation;
using TheInternetBuzz.Services.Explanation;

namespace TheInternetBuzz.Commands.Explanation
{
    public class ExplanationCommand : IExplanationCommand
    {
        private string query = null;

        public ExplanationCommand(string query)
        {
            this.query = query;
        }

        public ExplanationItem GetExplanation()
        {
            return new ExplanationService().GetExplanation(query);
        }
    }
}
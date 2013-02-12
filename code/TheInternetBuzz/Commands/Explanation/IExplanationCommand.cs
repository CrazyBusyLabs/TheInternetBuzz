using System;

using TheInternetBuzz.Data.Explanation;

namespace TheInternetBuzz.Commands.Explanation
{
    public interface IExplanationCommand
    {
        ExplanationItem GetExplanation();
    }
}
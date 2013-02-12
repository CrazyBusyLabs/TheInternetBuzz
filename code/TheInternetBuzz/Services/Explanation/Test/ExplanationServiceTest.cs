using System;
using System.Web;

using TheInternetBuzz.Data.Explanation;

using NUnit.Framework;

namespace TheInternetBuzz.Services.Explanation.Test
{
    [TestFixture]
    public class ExplanationServiceTest
    {
        [Test]
        public void GetUnknowExplanation()
        {
            ExplanationItem explanationItem = new ExplanationService().GetExplanation("unknown");
            Assert.That(explanationItem.Explanation.Length > 0);
        }

        [Test]
        public void GetLadyGagaExplanation()
        {
            ExplanationItem explanationItem = new ExplanationService().GetExplanation("Lady Gaga");
            Assert.That(explanationItem.Explanation.Length > 0);
        }
    }
}
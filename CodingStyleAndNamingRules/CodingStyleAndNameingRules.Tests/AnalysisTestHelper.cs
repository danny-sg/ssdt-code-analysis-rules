using System;
using System.Linq;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    public class AnalysisTestHelper
    {
        public static CodeAnalysisService CreateCodeAnalysisService(TSqlModel model, string ruleIdToRun)
        {
            var factory = new CodeAnalysisServiceFactory();

            var ruleSettings = new CodeAnalysisRuleSettings
            {
                new RuleConfiguration(ruleIdToRun)
            };

            ruleSettings.DisableRulesNotInSettings = true;

            var service = factory.CreateAnalysisService(model, new CodeAnalysisServiceSettings()
            {
                RuleSettings = ruleSettings
            });

            Assert.IsTrue(service.GetRules()
                .Any(rule => rule.RuleId
                    .Equals(ruleIdToRun, StringComparison.OrdinalIgnoreCase)),
                $"Expected rule '{ruleIdToRun}' not found by the service");
            return service;
        }
    }
}
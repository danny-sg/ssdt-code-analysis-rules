using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SqlServer.Dac.CodeAnalysis;
using Microsoft.SqlServer.Dac.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodingStyleAndNameingRules.Tests
{
    public abstract class RuleTests
    {
        protected CodeAnalysisService CreateCodeAnalysisService(TSqlModel model, string ruleIdToRun)
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

        protected TSqlModel ModelFromSql(string sql)
        {
            var model = new TSqlModel(SqlServerVersion.Sql130, new TSqlModelOptions());

            model.AddObjects(sql);

            return model;
        }

        protected void OutputProblems(IReadOnlyCollection<SqlRuleProblem> problems)
        {
            if (!problems.Any())
            {
                System.Diagnostics.Debug.Print("No problems");
            }

            foreach (var sqlRuleProblem in problems)
            {
                System.Diagnostics.Debug.Print(sqlRuleProblem.ErrorMessageString);
            }
        }
    }
}
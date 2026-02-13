using LogicBuilder.RulesDirector;
using LogicBuilder.Workflow.Activities.Rules;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Contoso.Test.Flow.Rules
{
    public class RulesCache(ConcurrentDictionary<string, RuleEngine> ruleEngines, ConcurrentDictionary<string, string> resourceStrings) : IRulesCache
    {
        private readonly ConcurrentDictionary<string, RuleEngine> concurrentRuleEngines = ruleEngines;
        private readonly ConcurrentDictionary<string, string> concurrentResourceStrings = resourceStrings;

        #region Properties
        public IDictionary<string, RuleEngine> RuleEngines => concurrentRuleEngines;
        public IDictionary<string, string> ResourceStrings => concurrentResourceStrings;
        #endregion Properties

        #region Methods
        public RuleEngine GetRuleEngine(string ruleSet)
        {
            if (this.RuleEngines == null)
                return null;

            return this.concurrentRuleEngines.TryGetValue(ruleSet.ToLowerInvariant(), out RuleEngine ruleEngine) ? ruleEngine : null;
        }
        #endregion Methods
    }
}

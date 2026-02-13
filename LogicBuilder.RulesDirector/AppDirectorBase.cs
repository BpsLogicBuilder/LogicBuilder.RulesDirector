using LogicBuilder.Workflow.Activities.Rules;
using System;
using System.Globalization;

namespace LogicBuilder.RulesDirector
{
    abstract public class AppDirectorBase : DirectorBase
    {

        #region Properties
        /// This property is used by the auto-generated rules and should
        /// not be used otherwise.
        /// Gets the name of the current module or Sets the name of the newly invoked module
        /// </summary>
        public sealed override string ModuleBeginName
        {
            get { return _moduleBeginName; }
            set
            {
                _callingModuleStack.Push(_moduleBeginName);
                _moduleBeginName = value;
                if (_moduleBeginName.Length != 0)
                {
                    _callingModuleDriverStack.Push(this._driver);

                    this._driver = string.Empty;
                    this._moduleEndName = string.Empty;

                    UpdateProgressList();

                    RuleEngine ruleEngine = RulesCache.GetRuleEngine(_moduleBeginName) ?? throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.moduleNotFoundFormat, _moduleBeginName));
                    ruleEngine.Execute(this.FlowActivity);
                }
            }
        }

        /// <summary>
        /// This property is used by the auto-generated rules and should
        /// not be used otherwise.
        /// Gets the name of the most recently terminated module or
        /// Sets the name of the terminating module
        /// </summary>
        public sealed override string ModuleEndName
        {
            get { return _moduleEndName; }
            set
            {
                _moduleEndName = value;
                _moduleBeginName = _callingModuleStack.Pop().ToString();
                if (_moduleBeginName.Length != 0)
                {
                    this._driver = _callingModuleDriverStack.Pop().ToString();

                    UpdateProgressList();

                    RuleEngine ruleEngine = RulesCache.GetRuleEngine(_moduleBeginName) ?? throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.moduleNotFoundFormat, _moduleBeginName));
                    ruleEngine.Execute(this.FlowActivity);
                }
            }
        }
        #endregion Properties

        #region Methods
        /// <summary>
        /// Executes the current ruleset
        /// </summary>
        public sealed override void ExecuteRulesEngine()
        {
            UpdateProgressList();

            RuleEngine ruleEngine = RulesCache.GetRuleEngine(_moduleBeginName) ?? throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.moduleNotFoundFormat, _moduleBeginName));
            ruleEngine.Execute(this.FlowActivity);
        }

        /// <summary>
        /// Sets the current module and executes the rules engine
        /// </summary>
        public sealed override void StartInitialFlow(string module)
        {
            if (string.IsNullOrEmpty(module))
                throw new ArgumentException(Strings.ruleSetCannotBeNull);

            ClearFlowVariables();

            UpdateProgressList(module);

            RuleEngine ruleEngine = RulesCache.GetRuleEngine(module) ?? throw new InvalidOperationException(string.Format(CultureInfo.CurrentCulture, Strings.moduleNotFoundFormat, module));
            ruleEngine.Execute(this.FlowActivity);
        }
        #endregion Methods
    }
}

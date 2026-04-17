using Contoso.Domain.Entities;
using System.Threading.Tasks;

namespace Contoso.Test.Flow.Rules
{
    public interface IRulesLoader
    {
        Task LoadRulesOnStartUp(RulesModuleModel module, RulesCache cache);
        void LoadRules(RulesModuleModel module, RulesCache cache);
    }
}

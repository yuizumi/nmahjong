using System.Collections.Generic;
using NMahjong.Aux;

namespace NMahjong.Japanese
{
    public interface IRuleAdvisor
    {
        int RequiredFan { get; }
        ImmutableList<IPlayerAction> GetValidActions();
    }
}

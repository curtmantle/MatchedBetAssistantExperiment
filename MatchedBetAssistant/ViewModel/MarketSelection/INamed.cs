using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public interface ISelectableMarket
    {
        string Id { get; }
        string Name { get; }
    }
}

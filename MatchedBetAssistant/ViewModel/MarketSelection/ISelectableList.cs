using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public interface ISelectableList
    {
        ISelectableMarket SelectedItem { get; set; }

        string BreadcrumbString { get; }

        ISelectableList PreviousList { get; }

        event MarketSelectedEventHandler MarketSelected;
    }

    public delegate void MarketSelectedEventHandler(object sender, MarketSelectedEventArgs args);

    public class MarketSelectedEventArgs : EventArgs
    {
        public MarketSelectedEventArgs(ISelectableList market)
        {
            this.SelectedMarketList = market;
        }
        public ISelectableList SelectedMarketList { get; private set; }
    }
}

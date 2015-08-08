using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class CompetitionSelectorViewModel : ViewModelBase, ISelectableMarket
    {
        private Competition country;

        internal CompetitionSelectorViewModel(Competition country)
        {
            this.country = country;
        }

        public string Id { get { return this.country.Id; } }

        public string Name { get { return this.country.Name; } }

    }
}

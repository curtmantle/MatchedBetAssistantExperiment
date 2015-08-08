using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class CountrySelectorViewModel : ViewModelBase, INamed
    {
        private Country country;

        internal CountrySelectorViewModel(Country country)
        {
            this.country = country;
        }

        public string Name { get { return this.country.CountryCode; } }

    }
}

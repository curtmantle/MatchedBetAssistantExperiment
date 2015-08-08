using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{
    public class CountryCollection : SelectorCollection<CountrySelectorViewModel>
    {
        public CountryCollection() { }



        public CountryCollection(IEnumerable<CountrySelectorViewModel> countries, ISelectableList previous)
            : base(countries, previous)
        {

        }

        public CountryCollection(IEnumerable<Country> countries, ISelectableList previous)
        {
            this.AddRange(countries.Select(x => new CountrySelectorViewModel(x)).ToList());

            this.PreviousList = previous;
        }


    }
}

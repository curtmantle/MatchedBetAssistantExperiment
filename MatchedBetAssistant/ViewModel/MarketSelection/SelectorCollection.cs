using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.ViewModel.MarketSelection
{


    public abstract class SelectorCollection<T> : List<T>, ISelectableList where T : INamed
    {
        private INamed selectedItem;
        private ISelectableList previousList;

        protected SelectorCollection()
        { }

        protected SelectorCollection(IEnumerable<T> collection)
            : base(collection)
        {
            this.previousList = null;
        }

        protected SelectorCollection(IEnumerable<T> collection, ISelectableList previousList)
            : base(collection)
        {
            this.previousList = previousList;
        }

        protected SelectorCollection(ISelectableList list, ISelectableList previousList)
        {
            this.previousList = previousList;
            var newList = list as SelectorCollection<T>;
            this.AddRange(newList);
        }

        public virtual INamed SelectedItem
        {
            get
            {
                return this.selectedItem;
            }
            set
            {
                this.selectedItem = value;
            }
        }

        public ISelectableList PreviousList
        {
            get { return this.previousList; }
            protected set
            {
                this.previousList = value;
            }
        }

        public virtual string BreadcrumbString
        {
            get { return previousList != null ? this.previousList.BreadcrumbString + "/" + BreadcrumbSuffix : this.BreadcrumbSuffix; }
        }

        protected string BreadcrumbSuffix { get; set; }

        public event MarketSelectedEventHandler MarketSelected;

        protected virtual void OnMarketSelected(object sender, MarketSelectedEventArgs args)
        {
            MarketSelectedEventHandler handler = MarketSelected;
            if (handler != null)
                handler(sender, args);
        }

    }
}

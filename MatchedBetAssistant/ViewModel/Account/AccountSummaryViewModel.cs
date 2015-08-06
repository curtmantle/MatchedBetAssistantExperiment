using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using MatchedBetAssistant.Model;
namespace MatchedBetAssistant.ViewModel.Account
{
    public sealed class AccountSummaryViewModel : ViewModelBase
    {
        private BetfairAccount account;
        private string name = string.Empty;

        public AccountSummaryViewModel(BetfairAccount account)
        {
            this.account = account;
        }

        public string Name
        {
            get
            {
                return this.account.Name;
            }
        }

        public double Balance
        {
            get { return this.account.Balance; }
        }
    }
}

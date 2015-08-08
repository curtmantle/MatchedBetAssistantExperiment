
using MatchedBetAssistant.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchedBetAssistant.ViewModel.Messages
{
    public class LoggedOnMessage
    {
        public LoggedOnMessage(BetfairAccount account)
        {
            this.Account = account;
        }

        public BetfairAccount Account
        {
            get;
            private set;
        }
    }
}

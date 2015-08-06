using MatchedBetAssistant.Model.Betfair.Account;
using MatchedBetAssistant.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MatchedBetAssistant.Model
{
    public class BetAssistant
    {
        private BetfairAccountClient accountClient;
        private string applicationId;
        private string sessionToken;
        private BetfairAccount account;

        public bool Login(string applicationId, string sessionToken)
        {
            this.applicationId = applicationId;
            this.sessionToken = sessionToken;

            this.accountClient = new BetfairAccountClient(applicationId, sessionToken);

            var result = this.accountClient.GetAccountDetails();
            var jresult = JsonConvert.DeserializeObject<AccountDetails>(result);

            var fundsResult = this.accountClient.GetAccountFunds();
            var jfundsResult = JsonConvert.DeserializeObject<AccountFunds>(fundsResult);


            this.account = new BetfairAccount()
            {
                Name = string.Format("{0} {1}", jresult.FirstName, jresult.LastName),
                Balance = jfundsResult.AvailableToBetBalance
            };

            return true;
        }

        public BetfairAccount Account
        {
            get
            {
                return account;
            }
        }
    }

    public class BetfairAccount
    {

        public string Name { get; set; }

        public double Balance { get; set; }
    }
}

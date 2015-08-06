using MatchedBetAssistant.Model.Betfair.Account;
using MatchedBetAssistant.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MatchedBetAssistant.Model
{
    public class BetAssistant : IDisposable
    {
        private BetfairAccountClient accountClient;
        private string applicationId;
        private string sessionToken;
        private BetfairAccount account;

        public BetAssistant()
        {
            this.applicationId = ReadApplicationKey();
        }

        public bool Login(string sessionToken)
        {
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

        public string ReadApplicationKey()
        {
            var fileName = System.Environment.CurrentDirectory + "\\app.key";
            if (!File.Exists(fileName))
            {
                Console.WriteLine(@"Bother");
            }
            var iniFile = new StreamReader(fileName);

            string key = iniFile.ReadToEnd();

            iniFile.Close();

            return key;
        }

        public string ApplicationKey
        {
            get { return this.applicationId; }
        }

        public BetfairAccount Account
        {
            get
            {
                return account;
            }
        }

        public void Dispose()
        {
            if (accountClient != null)
            {
                accountClient.Dispose();
                accountClient = null;
            }
        }
    }

    public class BetfairAccount
    {

        public string Name { get; set; }

        public double Balance { get; set; }
    }
}

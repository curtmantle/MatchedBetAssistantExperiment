using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services.Protocols;

namespace MatchedBetAssistant.Services
{
    public class BetfairAccountClient : IDisposable
    {
        private BetfairWebClient webclient;

        public BetfairAccountClient( string appKey, string sessionToken)
        {
            this.webclient = new BetfairWebClient(appKey, sessionToken);

        }

        public string GetAccountDetails()
        {
            var address = accountsAddress + GET_ACCOUNT_DETAILS + '/';

            var request = this.webclient.GetRequest(address);

            var response = this.webclient.GetResponse(request);

            var readResponse =this.webclient.ReadResponse(response);

            return readResponse;
        }

        public string GetAccountFunds()
        {
            var address = accountsAddress + GET_ACCOUNT_FUNDS + '/';

            var request = this.webclient.GetRequest(address);

            var response = this.webclient.GetResponse(request);

            var readResponse = this.webclient.ReadResponse(response);

            return readResponse;
        }

        public void Dispose()
        {
            if (webclient != null)
            {
                Console.WriteLine("Disposing of Web Client");
                webclient.Dispose();
                webclient = null;
            }
        }
        private const string bettingAddress = "https://api.betfair.com/exchange/betting/rest/v1.0/";
        private const string accountsAddress = "https://api.betfair.com/exchange/account/rest/v1.0/";

        private const string GET_ACCOUNT_DETAILS = "getAccountDetails";
        private const string GET_ACCOUNT_FUNDS = "getAccountFunds";
    }
}

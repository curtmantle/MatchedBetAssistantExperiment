using MatchedBetAssistant.Services.Betfair;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

        public AccountDetails GetAccountDetails()
        {
            var address = accountsAddress + GET_ACCOUNT_DETAILS + '/';

            return GetJsonResult<AccountDetails>(address);
        }

        public AccountFunds GetAccountFunds()
        {
            var address = accountsAddress + GET_ACCOUNT_FUNDS + '/';

            return GetJsonResult<AccountFunds>(address);
        }

        public IList<EventTypeResult> ListEventTypes(MarketFilter marketFilter)
        {
            var address = bettingAddress + LIST_EVENT_TYPES + '/';

            return GetJsonResult<IList<EventTypeResult>>(address, marketFilter);
        }

        public IList<CountryCodeResult> ListCountryCodes(MarketFilter marketFilter)
        {
            var address = bettingAddress + LIST_COUNTRIES + '/';

            return GetJsonResult<IList<CountryCodeResult>>(address, marketFilter);
        }


        private T GetJsonResult<T>(string method, MarketFilter marketFilter = null)
        {
            var request = this.webclient.GetRequest(method);

            if (marketFilter != null)
            {
                var postData = JObject.FromObject(new { filter = marketFilter });
                var bytes = Encoding.GetEncoding("UTF-8").GetBytes(postData.ToString());
                request.ContentLength = bytes.Length;

                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
            }

            var response = this.webclient.GetResponse(request);

            var readResponse = this.webclient.ReadResponse(response);

            var jresult = JsonConvert.DeserializeObject<T>(readResponse);

            return jresult;
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
        private const string LIST_EVENT_TYPES = "listEventTypes";
        private const string LIST_COUNTRIES = "listCountries";
    }
}

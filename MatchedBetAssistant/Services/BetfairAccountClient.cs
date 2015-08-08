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
    public interface IRequestParameterWriter
    {
        void WriteParameters(ref HttpWebRequest request);
    }


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

            var paramWriter = new NoParameterWriter();

            return GetJsonResult<AccountDetails>(address, paramWriter);
        }

        public AccountFunds GetAccountFunds()
        {
            var address = accountsAddress + GET_ACCOUNT_FUNDS + '/';

            var paramWriter = new NoParameterWriter();

            return GetJsonResult<AccountFunds>(address, paramWriter);
        }

        public IList<EventTypeResult> ListEventTypes(MarketFilter marketFilter)
        {
            var address = bettingAddress + LIST_EVENT_TYPES + '/';

            var paramWriter = new StandardMarketFilterParameterWriter(marketFilter);

            return GetJsonResult<IList<EventTypeResult>>(address, paramWriter);
        }

        public IList<CountryCodeResult> ListCountryCodes(MarketFilter marketFilter)
        {
            var address = bettingAddress + LIST_COUNTRIES + '/';

            var paramWriter = new StandardMarketFilterParameterWriter(marketFilter);

            return GetJsonResult<IList<CountryCodeResult>>(address, paramWriter);
        }

        public IList<EventResult> ListEvents(MarketFilter marketFilter)
        {
            var address = bettingAddress + LIST_EVENTS + '/';

            var paramWriter = new StandardMarketFilterParameterWriter(marketFilter);

            return GetJsonResult<IList<EventResult>>(address, paramWriter);
        }

        public IList<MarketCatalogue> ListMarketCatalogue(MarketFilter marketFilter)
        {
            var address = bettingAddress + LIST_MARKET_CATALOGUE + '/';

            var paramWriter = new MarketCatalogueParameterWriter(marketFilter, 100);

            return GetJsonResult<IList<MarketCatalogue>>(address, paramWriter);
        }


        public IList<CompetitionResult> ListCompetitions(MarketFilter marketFilter)
        {
            var address = bettingAddress + LIST_COMPETITIONS + '/';

            var paramWriter = new MarketCatalogueParameterWriter(marketFilter, 100);

            return GetJsonResult<IList<CompetitionResult>>(address, paramWriter);
        }

        private T GetJsonResult<T>(string method, IRequestParameterWriter parameterWriter)
        {
            var request = this.webclient.GetRequest(method);

            parameterWriter.WriteParameters(ref request);

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
        private const string LIST_EVENTS = "listEvents";
        private const string LIST_MARKET_CATALOGUE = "listMarketCatalogue";
        private const string LIST_COMPETITIONS = "listCompetitions";

        private class StandardMarketFilterParameterWriter : IRequestParameterWriter
        {
            private MarketFilter filter;

            public StandardMarketFilterParameterWriter(MarketFilter filter)
            {
                this.filter = filter;
            }
            public void WriteParameters(ref HttpWebRequest request)
            {
                if (filter != null)
                {
                    var postData = JObject.FromObject(new { filter = filter });
                    var bytes = Encoding.GetEncoding("UTF-8").GetBytes(postData.ToString());
                    request.ContentLength = bytes.Length;

                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }

        private class NoParameterWriter : IRequestParameterWriter
        {

            public void WriteParameters(ref HttpWebRequest request)
            {
                return;
            }
        }

        private class MarketCatalogueParameterWriter : IRequestParameterWriter
        {
            private MarketFilter filter;
            int maximumResults;

            public MarketCatalogueParameterWriter(MarketFilter filter, int maxResults)
            {
                this.filter = filter;
                this.maximumResults = maxResults;
            }

            public void WriteParameters(ref HttpWebRequest request)
            {
                if (filter != null)
                {
                    var postData = JObject.FromObject(new { filter = filter, maxResults = maximumResults });
                    var bytes = Encoding.GetEncoding("UTF-8").GetBytes(postData.ToString());
                    request.ContentLength = bytes.Length;

                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(bytes, 0, bytes.Length);
                    }
                }
            }
        }
    }
}

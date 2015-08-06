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
    public class BetfairAccountClient : WebClient
    {
        private NameValueCollection customHeaders;

        public BetfairAccountClient( string appKey, string sessionToken)
        {

            this.customHeaders = new NameValueCollection();

            this.customHeaders[APPKEY_HEADER] = appKey;

            this.customHeaders[SESSION_TOKEN_HEADER] = sessionToken;

        }

        public string GetAccountDetails()
        {
            var address = accountsAddress + GET_ACCOUNT_DETAILS + '/';

            var request = GetRequest(address);

            var response = GetWebResponse(request);

            var readResponse = ReadResponse(response);

            return readResponse;
        }

        public string GetAccountFunds()
        {
            var address = accountsAddress + GET_ACCOUNT_FUNDS + '/';

            var request = GetRequest(address);

            var response = GetWebResponse(request);

            var readResponse = ReadResponse(response);

            return readResponse;
        }

        private string ReadResponse(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                var jsonResponse = reader.ReadToEnd();
                string result = jsonResponse;
                return result;
            }
        }

        private HttpWebRequest GetRequest(string address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            request.Accept = "application/json";
            request.Headers.Add("X-Application", this.customHeaders[APPKEY_HEADER]);
            request.Headers.Add("X-Authentication", this.customHeaders[SESSION_TOKEN_HEADER]);

            return request;
        }


        private const string APPKEY_HEADER = "X-Application";
        private const string SESSION_TOKEN_HEADER = "X-Authentication";

        private const string bettingAddress = "https://api.betfair.com/exchange/betting/rest/v1.0/";
        private const string accountsAddress = "https://api.betfair.com/exchange/account/rest/v1.0/";

        private const string GET_ACCOUNT_DETAILS = "getAccountDetails";
        private const string GET_ACCOUNT_FUNDS = "getAccountFunds";
    }
}

using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace MatchedBetAssistant.Services
{
    public class BetfairWebClient : WebClient
    {
        private NameValueCollection customHeaders;

        public BetfairWebClient(string applicationId, string sessionToken)
        {
            this.customHeaders = new NameValueCollection();

            this.customHeaders[APPKEY_HEADER] = applicationId;
            this.customHeaders[SESSION_TOKEN_HEADER] = sessionToken;

        }

        public string ReadResponse(WebResponse response)
        {
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                var jsonResponse = reader.ReadToEnd();
                string result = jsonResponse;
                return result;
            }
        }

        public WebResponse GetResponse(HttpWebRequest request)
        {
            return GetWebResponse(request);
        }

        public HttpWebRequest GetRequest(string address)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(address);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            request.Headers.Add(HttpRequestHeader.AcceptCharset, "UTF-8");
            request.Accept = "application/json";
            request.Headers.Add(APPKEY_HEADER, this.customHeaders[APPKEY_HEADER]);
            request.Headers.Add(SESSION_TOKEN_HEADER, this.customHeaders[SESSION_TOKEN_HEADER]);
            return request;
        }


        private const string APPKEY_HEADER = "X-Application";
        private const string SESSION_TOKEN_HEADER = "X-Authentication";
    }
}

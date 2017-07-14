using System;
using System.Net;

namespace TradeStockCalc.WebUtils
{
    class WebClientWrapper : IWebClientRequest
    {
        protected WebClient WebClient { get; set; } = new WebClient();

        public void Dispose()
        {
            WebClient.Dispose();
        }

        public string MakeRequest(string request)
        {
            return WebClient.DownloadString(request);
        }
    }
}

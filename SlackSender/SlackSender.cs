using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlackSender
{
    public class SlackSender : IDisposable
    {
        private HttpHelperUtility.HttpHelper httpHelper;

        public SlackSender(bool useProxy = true, string proxyUri = "http://bluecoat.media-saturn.com:80")
        {
            httpHelper = new HttpHelperUtility.HttpHelper(useProxy, proxyUri);
        }

        public bool SendMessage(string destination, string message, string queue = "AXMsg1", string httpHandlerUri = "https://warm-savannah-34152.herokuapp.com/api/send")
        {
            var result = httpHelper.PostJson(httpHandlerUri, "{\"queue\":\"" + queue + "\", \"message\":\"" + $"{destination}::{message}" + "\"}");
            return result.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public void Dispose()
        {
            httpHelper?.Dispose();
        }
    }
}

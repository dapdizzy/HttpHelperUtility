using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;
using System.Net.Http;
using System.Net;

namespace HttpHelperUtilityTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGetUrl1()
        {
            var result = GetUri("https://fierce-scrubland-26538.herokuapp.com/muter");
            StringAssert.Contains(result, "Mute", "Result does not contaienr [Mute]");
            //using (var helper = new HttpHelperUtility.HttpHelper())
            //{
            //    var result = helper.Get("https://fierce-scrubland-26538.herokuapp.com/muter");
            //    StringAssert.Contains(result, "Mute", "Result does not contaienr [Mute]");
            //}
        }

        [TestMethod]
        public void TestGetUrl12()
        {
            AssertUriNotContainerString("https://fierce-scrubland-26538.herokuapp.com/muter", "Cute");
        }

        [TestMethod]
        public void TestPostJsonUri()
        {
            var result = PostJsonUri("https://warm-savannah-34152.herokuapp.com/api/send", "{\"queue\": \"AXMsg1\", \"message\":\"dpyatkov::Hi there!\"}");
            Assert.AreEqual<HttpStatusCode>(HttpStatusCode.OK, result.StatusCode);
            StringAssert.Contains(result.Content.ReadAsStringAsync().GetAwaiter().GetResult(), "sent");
        }

        private static void AssertUriContainerString(string uri, string containingString)
        {
            var result = GetUri(uri);
            StringAssert.Contains(result, containingString, $"Result does not container {containingString}");
        }

        private static void AssertUriNotContainerString(string uri, string containingString)
        {
            var result = GetUri(uri);
            StringAssert.DoesNotMatch(result, new Regex(containingString), $"Result still contains {containingString}");
            //StringAssert.Contains(result, containingString, $"Result does not container {containingString}");
        }

        private static string GetUri(string uri)
        {
            using (var helper = NewHelper())
            {
                return helper.Get(uri);
            }
        }

        private static HttpResponseMessage PostJsonUri(string uri, string payload)
        {
            using (var helper = NewHelper())
            {
                return helper.PostJson(uri, payload);
            }
        }

        private static HttpHelperUtility.HttpHelper NewHelper()
        {
            return new HttpHelperUtility.HttpHelper(true);
        }
    }
}

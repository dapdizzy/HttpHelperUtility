using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SlackSenderTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSendMessage()
        {
            string destination = "dpyatkov";
            string message = "Привет из внутренней сети!";
            Assert.IsTrue(SendMessage(destination, message), $"Failed to send message [{message}] to {destination}");
        }

        private static bool SendMessage(string destination, string message)
        {
            using (var sender = NewSender())
            {
                return sender.SendMessage(destination, message);
            }
        }

        private static SlackSender.SlackSender NewSender(bool useProxy = true)
        {
            return new SlackSender.SlackSender(useProxy);
        }
    }
}

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoChat.Core.Tests
{
    [TestClass]
    public class ServerTests
    {
        [TestMethod]
        public void SendMessageTest()
        {
            var logProvider = new FakeLogProvider();
            var messageObserver = new FakeMessageObserver();
            var server = new Server
            {
                LogProvider = logProvider
            };
            server.AddMessageObserver("fakeClient", messageObserver);
            
            server.SendMessage("test");

            List<string> receivedMessages = messageObserver.GetReceivedMessages();
            Assert.AreEqual(receivedMessages.Count, 1);
            Assert.AreEqual(receivedMessages[0], "test");
        }
    }
}

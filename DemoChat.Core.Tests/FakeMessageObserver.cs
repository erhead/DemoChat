using System.Collections.Generic;

namespace DemoChat.Core.Tests
{
    class FakeMessageObserver : IMessageObserver
    {
        private List<string> _receivedMessages = new List<string>();

        public List<string> GetReceivedMessages()
        {
            return new List<string>(_receivedMessages);
        }

        public void ReceiveMessage(string message)
        {
            _receivedMessages.Add(message);
        }
    }
}

using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace DemoChat.Core.Tests
{
    class FakeLogProvider : ILogProvider
    {
        private List<string> _messages = new List<string>();

        public void WriteMessage(string message)
        {
            _messages.Add(message);
        }

        public void ClearLog()
        {
            _messages.Clear();
        }

        public IEnumerable<string> GetLog()
        {
            return new List<string>(_messages);
        }
    }
}

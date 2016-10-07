using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoChat.Core.LogProviders
{
    public class SimpleLogProvider : ILogProvider
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

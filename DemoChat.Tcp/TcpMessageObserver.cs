using System.Net.Sockets;
using DemoChat.Core;

namespace DemoChat.Tcp
{
    /// <summary>
    /// Наблюдатель для сообщений чата, реализующий их отправку через TCP при получении от сервера.
    /// </summary>
    class TcpMessageObserver : IMessageObserver
    {
        public Socket Socket { get; set; }

        public void ReceiveMessage(string message)
        {
            Socket.Send(CommandHelper.GetCommandData(TcpClientCommand.ReceiveMessage, message));
        }
    }
}

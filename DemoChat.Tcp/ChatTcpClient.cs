using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace DemoChat.Tcp
{
    /// <summary>
    /// Делегат, описывающий обработчик принятия сообщения клиентом.
    /// </summary>
    /// <param name="message">Текст сообщения.</param>
    public delegate void MessageReceivedHandler(string message);

    /// <summary>
    /// Клиент чата, использующий TCP для обмена данными.
    /// </summary>
    public class ChatTcpClient
    {
        private const int ReceiveBufferSize = 258;

        private TcpClient _connection;

        private Thread _receivingThread;

        private IEnumerable<string> _history;

        private ManualResetEvent _historyReceivedEvent = new ManualResetEvent(false);

        /// <summary>
        /// Получает или устанавливает IP-адрес сервера для подключения.
        /// </summary>
        public IPAddress ServerIpAddress { get; set; }

        /// <summary>
        /// Получает или устанавливает порт сервера для подключения.
        /// </summary>
        public int ServerPort { get; set; }

        /// <summary>
        /// Получает или устанавливает обработчик принятия сообщения клиентом.
        /// </summary>
        public MessageReceivedHandler MessageReceived { get; set; }

        /// <summary>
        /// Получает или устанавливает обработчик отключения клиента от сервера.
        /// </summary>
        public Action Disconnected { get; set; }

        /// <summary>
        /// Подключиться к серверу.
        /// </summary>
        public void Connect()
        {
            if (_connection != null && _connection.Connected)
                throw new InvalidOperationException("Подключение уже выполнено.");

            if (ServerIpAddress == null)
                throw new InvalidOperationException("IP-адрес сервера не задан.");

            _connection = new TcpClient();
            _connection.Connect(ServerIpAddress, ServerPort);

            var stream = _connection.GetStream();

            _receivingThread = new Thread(() =>
            {
                List<string> history = new List<string>();
                while (true)
                {
                    try
                    {
                        byte[] buffer = new byte[ReceiveBufferSize];
                        stream.Read(buffer, 0, buffer.Length);
                        var cmd = CommandHelper.GetClientCommand(buffer);
                        switch (cmd.Item1)
                        {
                            case TcpClientCommand.ReceiveMessage:
                                MessageReceived?.Invoke(cmd.Item2);
                                break;
                            case TcpClientCommand.ReceiveHistoryString:
                                history.Add(cmd.Item2);
                                break;
                            case TcpClientCommand.EndReceiveHistory:
                                _history = new List<string>(history);
                                _historyReceivedEvent.Set();
                                history.Clear();
                                break;
                        }
                    }
                    catch
                    {
                        // В случае ошибки принятия данных от сервера отключаемся.
                        Disconnected?.Invoke();
                        break;
                    }
                }
            });
            _receivingThread.Start();
        }

        /// <summary>
        /// Отключиться от сервера.
        /// </summary>
        public void Disconnect()
        {
            if (_connection != null)
            {
                // После закрытия соединения поток принятия остановится.
                _connection.Close();
                _receivingThread.Join();
                _receivingThread = null;
                _connection = null;
            }
        }

        /// <summary>
        /// Отправить сообщение в чат.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void SendMessage(string message)
        {
            if(_connection == null || !_connection.Connected)
                throw new InvalidOperationException("Подключение не выполнено.");

            if (string.IsNullOrEmpty(message))
                return;

            var stream = _connection.GetStream();
            byte[] data = CommandHelper.GetCommandData(TcpServerCommand.SendMessage, message);
            stream.Write(data, 0, data.Length);
        }

        /// <summary>
        /// Получить всю историю сообщений чата.
        /// </summary>
        /// <returns>Коллекция строк, представляющая собой историю сообщений.</returns>
        public IEnumerable<string> GetHistory()
        {
            if (_connection == null || !_connection.Connected)
                throw new InvalidOperationException("Подключение не выполнено.");

            var stream = _connection.GetStream();
            byte[] data = CommandHelper.GetCommandData(TcpServerCommand.GetHistory, null);

            _historyReceivedEvent.Reset();
            stream.Write(data, 0, data.Length);

            _historyReceivedEvent.WaitOne();
            return _history;
        }
    }
}

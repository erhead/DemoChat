using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using DemoChat.Core;

namespace DemoChat.Tcp
{
    /// <summary>
    /// Класс, обеспечивающий работу <see cref="Server"/> через TCP.
    /// </summary>
    public class ChatTcpServer
    {
        private struct ReceiveState
        {
            public byte[] Buffer;

            public Socket Socket;

            public string ClientName;
        }

        private const int ReceiveBufferSize = 258;

        private readonly object _isListeningLockObject = new object();

        private TcpListener _listener;

        private Thread _listeningThread;

        private ConcurrentDictionary<string, Socket> _connections = new ConcurrentDictionary<string, Socket>();

        private bool DataIsEmpty(byte[] data)
        {
            byte result = 0;
            for (var i = 0; i < data.Length; i++)
            {
                result = (byte) (result | data[i]);
            }
            return result == 0;
        }

        private void ClearBuffer(byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; i++)
            {
                buffer[i] = 0;
            }
        }

        private void RemoveConnection(string clientName)
        {
            Socket socket;
            Server.RemoveMessageObserver(clientName);
            _connections.TryRemove(clientName, out socket);
            if (socket != null)
                socket.Close();
        }

        private void ReceiveCallback(IAsyncResult result)
        {
            ReceiveState state = (ReceiveState) result.AsyncState;

            if (!result.IsCompleted || DataIsEmpty(state.Buffer))
            {
                RemoveConnection(state.ClientName);
                return;
            }

            Tuple<TcpServerCommand, string> command = CommandHelper.GetServerCommand(state.Buffer);
            switch (command.Item1)
            {
                case TcpServerCommand.SendMessage:
                    Server.SendMessage(command.Item2);
                    break;
                case TcpServerCommand.GetHistory:
                    IEnumerable<string> history = Server.GetAllMessages();

                    try
                    {
                        foreach (var s in history)
                        {
                            var cmdData = CommandHelper.GetCommandData(TcpClientCommand.ReceiveHistoryString, s);
                            state.Socket.Send(cmdData);
                        }
                        state.Socket.Send(CommandHelper.GetCommandData(TcpClientCommand.EndReceiveHistory, null));
                    }
                    catch
                    {
                        RemoveConnection(state.ClientName);
                    }
                    
                    break;
            }

            try
            {
                ClearBuffer(state.Buffer);
                state.Socket.BeginReceive(state.Buffer, 0, ReceiveBufferSize, SocketFlags.None, ReceiveCallback, state);
            }
            catch
            {
                RemoveConnection(state.ClientName);
            }
        }

        /// <summary>
        /// Возвращает логическое значение, сообщающее, производится ли прослушивание TCP-порта в данный момент.
        /// </summary>
        protected bool IsListening { get; private set; }

        /// <summary>
        /// Получает или устанавливает IP-адрес, по которому будет вестись прослушивание входящих соединений.
        /// </summary>
        public IPAddress IpAddress { get; set; }

        /// <summary>
        /// Получает или устанавливает порт, по которому будет вестись прослушивание входящих соединений.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Получает или устанавливает объект, инкапсулирующий логику сервера чата.
        /// </summary>
        public Server Server { get; set; }

        /// <summary>
        /// Начать прослушивание порта.
        /// </summary>
        public void StartListening()
        {
            lock (_isListeningLockObject)
            {
                if (IsListening)
                    throw new InvalidOperationException("Прослушивание входящих соединений уже выполняется.");

                if (IpAddress == null)
                    IpAddress = IPAddress.Any;

                _listener = new TcpListener(IpAddress, Port);
                _listener.Start();

                _listeningThread = new Thread(() =>
                {
                    while (true)
                    {
                        try
                        {
                            Socket socket = _listener.AcceptSocket();
                            socket.ReceiveBufferSize = ReceiveBufferSize;

                            // При подключении клиента добавляем его на сервер и начинаем прием данных.
                            string clientName = Guid.NewGuid().ToString();
                            _connections[clientName] = socket;
                            Server.AddMessageObserver(clientName, new TcpMessageObserver {Socket = socket});

                            byte[] buffer = new byte[ReceiveBufferSize];
                            socket.BeginReceive(buffer, 0, ReceiveBufferSize, SocketFlags.None, ReceiveCallback,
                                new ReceiveState
                                {
                                    Buffer = buffer,
                                    ClientName = clientName,
                                    Socket = socket
                                });
                        }
                        catch
                        {
                            // При ошибке принятия соединения прекращаем прослушивание.
                            break;
                        }
                        
                    }
                });
                _listeningThread.Start();

                IsListening = true;
            }
        }

        /// <summary>
        /// Остановить прослушивание входящих соединений.
        /// </summary>
        public void StopListening()
        {
            lock (_isListeningLockObject)
            {
                if (!IsListening)
                    return;

                _listener.Stop();
                _listeningThread.Join();
                _listeningThread = null;

                foreach (string clientName in _connections.Keys)
                {
                    Server.RemoveMessageObserver(clientName);
                    Socket removedSocket;
                    if (_connections.TryRemove(clientName, out removedSocket))
                    {
                        removedSocket.Close();
                    }
                }

                IsListening = false;
            }
        }
    }
}

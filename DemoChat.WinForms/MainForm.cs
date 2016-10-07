using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using DemoChat.Core;
using DemoChat.Core.LogProviders;
using DemoChat.Tcp;

namespace DemoChat.WinForms
{
    public partial class MainForm : Form
    {
        private delegate void AddMessageDelegate(TextBox textBox, string message);

        private delegate void ClientDisconnectedDelegate();

        private ChatTcpServer _ownServer;

        private ChatTcpClient _client;

        private ChatState _state = ChatState.Inactive;

        private void AddMessage(TextBox textBox, string message)
        {
            textBox.AppendText(message + Environment.NewLine);
        }

        private void WindowClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_client != null)
            {
                _client.Disconnect();
            }

            if (_ownServer != null)
            {
                _ownServer.StopListening();
            }
        }

        private void ClientDisconnected()
        {
            if(!Disposing && !IsDisposed)
                State = ChatState.Inactive;
        }

        private ChatState State
        {
            get { return _state; }
            set
            {
                RefreshControlsState(value);
                _state = value;
            }
        }

        private ChatTcpClient CreateClientAndConnect(IPAddress serverIpAddress, int serverPort)
        {
            var result = new ChatTcpClient();
            result.ServerIpAddress = serverIpAddress;
            result.ServerPort = serverPort;
            result.Disconnected += () =>
            {
                BeginInvoke(new ClientDisconnectedDelegate(ClientDisconnected));
            };
            result.MessageReceived = message =>
            {
                chatTextBox.BeginInvoke(new AddMessageDelegate(AddMessage), chatTextBox, message);
            };
            result.Connect();

            return result;
        }

        private void RefreshControlsState(ChatState chatState)
        {
            switch (chatState)
            {
                case ChatState.Inactive:
                    ownServerIpTextBox.Enabled = true;
                    ownServerPortTextBox.Enabled = true;
                    createOwnServerButton.Enabled = true;
                    outServerIpTextBox.Enabled = true;
                    outServerPortTextBox.Enabled = true;
                    logFilePathTextBox.Enabled = true;
                    connectToServerButton.Enabled = true;
                    resetConnectionButton.Enabled = false;
                    newMessageTextBox.Enabled = false;
                    loadHistoryButton.Enabled = false;
                    sendButton.Enabled = false;
                    statusLabel.Text = "Не активен";
                    break;

                case ChatState.ConnectedToAnotherServer:
                    ownServerIpTextBox.Enabled = false;
                    ownServerPortTextBox.Enabled = false;
                    createOwnServerButton.Enabled = false;
                    outServerIpTextBox.Enabled = false;
                    outServerPortTextBox.Enabled = false;
                    logFilePathTextBox.Enabled = false;
                    connectToServerButton.Enabled = false;
                    resetConnectionButton.Enabled = true;
                    newMessageTextBox.Enabled = true;
                    loadHistoryButton.Enabled = true;
                    sendButton.Enabled = true;
                    statusLabel.Text = "Подключен к серверу";
                    break;

                case ChatState.OwnServerIsCreated:
                    ownServerIpTextBox.Enabled = false;
                    ownServerPortTextBox.Enabled = false;
                    createOwnServerButton.Enabled = false;
                    outServerIpTextBox.Enabled = false;
                    outServerPortTextBox.Enabled = false;
                    logFilePathTextBox.Enabled = false;
                    connectToServerButton.Enabled = false;
                    resetConnectionButton.Enabled = true;
                    newMessageTextBox.Enabled = true;
                    loadHistoryButton.Enabled = true;
                    sendButton.Enabled = true;
                    statusLabel.Text = "Запущен сервер";
                    break;
            }
        }

        private void createOwnServerButton_Click(object sender, EventArgs e)
        {
            if (State == ChatState.OwnServerIsCreated)
            {
                MessageBox.Show("Сервер уже создан.");
                return;
            }

            try
            {
                var ip = new IPAddress(ownServerIpTextBox.Text.Split('.').Select(x => byte.Parse(x)).ToArray());
                var port = int.Parse(ownServerPortTextBox.Text);

                var server = new Server
                {
                    LogProvider = new TextFileLogProvider
                    {
                        FilePath = logFilePathTextBox.Text
                    }
                };

                _ownServer = new ChatTcpServer()
                {
                    IpAddress = ip,
                    Port = port,
                    Server = server
                };
                _ownServer.StartListening();

                if (_client != null)
                {
                    _client.Disconnect();
                }
                _client = CreateClientAndConnect(ip, port);

                State = ChatState.OwnServerIsCreated;
            }
            catch (Exception ex)
            {
                if (_ownServer != null)
                {
                    _ownServer.StopListening();
                }

                _ownServer = null;
                State = ChatState.Inactive;
                MessageBox.Show(ex.Message);
            }
        }

        private void connectToServerButton_Click(object sender, EventArgs e)
        {
            if (State == ChatState.ConnectedToAnotherServer)
            {
                MessageBox.Show("Клиент уже подключен к серверу.");
                return;
            }

            try
            {
                var ip = new IPAddress(outServerIpTextBox.Text.Split('.').Select(x => byte.Parse(x)).ToArray());
                var port = int.Parse(outServerPortTextBox.Text);

                if (_client != null)
                {
                    _client.Disconnect();
                }
                _client = CreateClientAndConnect(ip, port);

                State = ChatState.ConnectedToAnotherServer;
            }
            catch (Exception ex)
            {
                State = ChatState.Inactive;
                MessageBox.Show(ex.Message);
            }
        }

        private void resetConnectionButton_Click(object sender, EventArgs e)
        {
            if (_client != null)
            {
                _client.Disconnect();
                _client = null;
            }

            if (_ownServer != null)
            {
                _ownServer.StopListening();
                _ownServer = null;
            }

            State = ChatState.Inactive;
        }

        private void sendButton_Click(object sender, EventArgs e)
        {
            _client.SendMessage(newMessageTextBox.Text);
            newMessageTextBox.Text = string.Empty;
        }

        public MainForm()
        {
            InitializeComponent();

            Closing += WindowClosing;

            RefreshControlsState(State);
        }

        private void loadHistoryButton_Click(object sender, EventArgs e)
        {
            chatTextBox.AppendText(string.Join(Environment.NewLine, _client.GetHistory()) + Environment.NewLine);
        }

        private void clearChatButton_Click(object sender, EventArgs e)
        {
            chatTextBox.Text = string.Empty;
        }
    }
}

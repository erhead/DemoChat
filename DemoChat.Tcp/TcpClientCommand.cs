namespace DemoChat.Tcp
{
    /// <summary>
    /// Возможные команды клиента.
    /// </summary>
    enum TcpClientCommand : byte
    {
        /// <summary>
        /// Принять сообщение.
        /// </summary>
        ReceiveMessage = 0,

        /// <summary>
        /// Принять строку истории.
        /// </summary>
        ReceiveHistoryString = 1,

        /// <summary>
        /// Окончание передачи истории.
        /// </summary>
        EndReceiveHistory = 2
    }
}

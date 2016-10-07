namespace DemoChat.Tcp
{
    /// <summary>
    /// Возможные команды сервера.
    /// </summary>
    enum TcpServerCommand : byte
    {
        /// <summary>
        /// Отправить сообщение.
        /// </summary>
        SendMessage = 0,

        /// <summary>
        /// Получить историю сообщений.
        /// </summary>
        GetHistory = 1
    }
}

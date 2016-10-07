namespace DemoChat.Core
{
    /// <summary>
    /// Интерфейс, описывающий наблюдателя сообщений в чате.
    /// </summary>
    public interface IMessageObserver
    {
        /// <summary>
        /// Принять сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void ReceiveMessage(string message);
    }
}

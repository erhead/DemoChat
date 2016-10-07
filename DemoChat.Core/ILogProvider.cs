using System.Collections.Generic;

namespace DemoChat.Core
{
    /// <summary>
    /// Интерфейс, описывающий провайдер ведения лога для чата.
    /// </summary>
    public interface ILogProvider
    {
        /// <summary>
        /// Записать сообщение в лог.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        void WriteMessage(string message);

        /// <summary>
        /// Очистить лог.
        /// </summary>
        void ClearLog();

        /// <summary>
        /// Получить содержимое лога.
        /// </summary>
        /// <returns>Коллекция строк лога.</returns>
        IEnumerable<string> GetLog();
    }
}

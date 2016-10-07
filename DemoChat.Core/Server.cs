using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoChat.Core
{
    /// <summary>
    /// Сервер для отправки сообщений и сохранения их в лог.
    /// </summary>
    public class Server
    {
        private ConcurrentDictionary<string, IMessageObserver> _messageObservers = new ConcurrentDictionary<string, IMessageObserver>();

        /// <summary>
        /// Объект для ведения лога.
        /// </summary>
        public ILogProvider LogProvider { get; set; }

        /// <summary>
        /// Добавить наблюдателя сообщений в чат. При каждой отправке сообщения у наблюдателей будет вызываться метод ReceiveMessage.
        /// </summary>
        /// <param name="name">Имя клиента.</param>
        /// <param name="messageObserver">Экземпляр наблюдателя.</param>
        public void AddMessageObserver(string name, IMessageObserver messageObserver)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (messageObserver == null)
                throw new ArgumentNullException(nameof(messageObserver));

            if (!_messageObservers.TryAdd(name, messageObserver))
                throw new InvalidOperationException($"Подписчик на сообщения к именем {name} уже добавлен.");
        }

        /// <summary>
        /// Возвращает все сообщения, имеющиеся в логе.
        /// </summary>
        /// <returns>Коллекция сообщений.</returns>
        public IEnumerable<string> GetAllMessages()
        {
            if (LogProvider == null)
                throw new InvalidOperationException("Свойство LogProvider не инициализировано.");

            return LogProvider.GetLog();
        }

        /// <summary>
        /// Отправить в чат сообщение.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void SendMessage(string message)
        {
            if (LogProvider == null)
                throw new InvalidOperationException("Свойство LogProvider не инициализировано.");

            LogProvider.WriteMessage(message);

            foreach (IMessageObserver observer in _messageObservers.Values)
            {
                var task = new Task(() => observer.ReceiveMessage(message));
                task.Start();
            }
        }

        /// <summary>
        /// Удалить подписчика на сообщения.
        /// </summary>
        /// <param name="name">Имя подписчика, которого нужно удалить.</param>
        /// <returns>Результат удаления: <c>true</c>, если подписчик был удален; <c>false</c>, если он не был найден.</returns>
        public bool RemoveMessageObserver(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            IMessageObserver val;
            return _messageObservers.TryRemove(name, out val);
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;

namespace DemoChat.Core.LogProviders
{
    /// <summary>
    /// Провайдер лога, пишущий данные в файл на диске и сортирующий строки в нем алфавитном порядке.
    /// </summary>
    public class TextFileLogProvider : ILogProvider
    {
        /// <summary>
        /// Задает или получает путь к файлу, в котором будет храниться лог.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Записать сообщение в лог.
        /// </summary>
        /// <param name="message">Текст сообщения.</param>
        public void WriteMessage(string message)
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new InvalidOperationException("Не задан путь к файлу лога.");

            List<string> strings = new List<string>();
            strings.AddRange(GetLog());

            

            strings.Add(message.Replace("\r", string.Empty).Replace("\n", string.Empty));
            strings.Sort();

            using (StreamWriter sw = File.CreateText(FilePath))
            {
                foreach (string s in strings)
                    sw.WriteLine(s);
            }
        }

        /// <summary>
        /// Очистить лог.
        /// </summary>
        public void ClearLog()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new InvalidOperationException("Не задан путь к файлу лога.");

            File.Delete(FilePath);
        }

        /// <summary>
        /// Получить весь лог.
        /// </summary>
        /// <returns>Коллекция строк в логе, отсортированных в алфавитном порядке.</returns>
        public IEnumerable<string> GetLog()
        {
            if (string.IsNullOrEmpty(FilePath))
                throw new InvalidOperationException("Не задан путь к файлу лога.");

            var result = new List<string>();

            if (File.Exists(FilePath))
            {
                using (StreamReader sr = File.OpenText(FilePath))
                    while (!sr.EndOfStream)
                        result.Add(sr.ReadLine());
            }

            return result;
        }
    }
}

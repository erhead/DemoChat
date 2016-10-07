using System;
using System.Text;

namespace DemoChat.Tcp
{
    /// <summary>
    /// Утилиты для работы с серверными и клиентскими командами для TCP.
    /// </summary>
    static class CommandHelper
    {
        /// <summary>
        /// Размер буфера одной команды. имеет такой размер, т.к. под код команды и значение длины данных 
        /// отведено по одному байту и, соответственно, данные могут иметь длину до 256 байт.
        /// </summary>
        public static readonly int ReceiveBufferSize = 258;

        /// <summary>
        /// Создать массив байт для команды.
        /// </summary>
        /// <param name="command">Код команды.</param>
        /// <param name="data">Данные команды.</param>
        /// <returns></returns>
        public static byte[] MergeCommandWithStringData(byte command, string data)
        {
            byte[] cmdData = Encoding.UTF8.GetBytes(data ?? string.Empty);
            var result = new byte[ReceiveBufferSize];
            result[0] = command;
            result[1] = (byte)cmdData.Length;
            for (var i = 0; i < cmdData.Length; i++)
            {
                result[i + 2] = cmdData[i];
            }

            return result;
        }

        /// <summary>
        /// Получить код команды и данные из массива байт.
        /// </summary>
        /// <param name="data">Массив байт, принятый из TCP.</param>
        /// <returns></returns>
        public static Tuple<byte, string> UnmergeCommandWithData(byte[] data)
        {
            byte command = data[0];
            string stringData = string.Empty;
            byte cmdDataLength = data[1];
            if (cmdDataLength > 0)
            {
                var cmdData = new byte[cmdDataLength];
                for (var i = 0; i < cmdDataLength; i++)
                {
                    cmdData[i] = data[i + 2];
                }

                stringData = Encoding.UTF8.GetString(cmdData);
            }

            return new Tuple<byte, string>(command, stringData);
        }

        /// <summary>
        /// Получить из массива байт серверную команду.
        /// </summary>
        /// <param name="data">Данные, пришедшие от TCP-клиента.</param>
        /// <returns>Пара значений, где первое - серверная команда, второе - данные команды.</returns>
        public static Tuple<TcpServerCommand, string> GetServerCommand(byte[] data)
        {
            Tuple<byte, string> rawCmd = UnmergeCommandWithData(data);
            try
            {
                return new Tuple<TcpServerCommand, string>((TcpServerCommand) rawCmd.Item1, rawCmd.Item2);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Неизвестная команда сервера.");
            }
        }

        /// <summary>
        /// Получить из массива байт клиентскую команду.
        /// </summary>
        /// <param name="data">Данные, пришедшие от TCP-сервеа.</param>
        /// <returns>Пара значений, где первое - клиентская команда, второе - данные команды.</returns>
        public static Tuple<TcpClientCommand, string> GetClientCommand(byte[] data)
        {
            Tuple<byte, string> rawCmd = UnmergeCommandWithData(data);
            try
            {
                return new Tuple<TcpClientCommand, string>((TcpClientCommand)rawCmd.Item1, rawCmd.Item2);
            }
            catch (InvalidCastException)
            {
                throw new ArgumentException("Неизвестная команда клиента.");
            }
        }

        /// <summary>
        /// Преобразовать серверную команду с данными в массив байт.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="data">Данные команды. Если данных нет, <c>null</c>.</param>
        /// <returns>Результирующий массив байт для отправки на сервер.</returns>
        public static byte[] GetCommandData(TcpServerCommand command, string data)
        {
            return MergeCommandWithStringData((byte) command, data);
        }

        /// <summary>
        /// Преобразовать клиентскую команду с данными в массив байт.
        /// </summary>
        /// <param name="command">Команда.</param>
        /// <param name="data">Данные команды. Если данных нет, <c>null</c>.</param>
        /// <returns>Результирующий массив байт для отправки на клиент..</returns>
        public static byte[] GetCommandData(TcpClientCommand command, string data)
        {
            return MergeCommandWithStringData((byte) command, data);
        }
    }
}

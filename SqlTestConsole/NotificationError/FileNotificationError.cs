using Microsoft.Extensions.Configuration;
using System.IO;

namespace SqlTestConsole.NotificationError
{
    public sealed class FileNotificationError : INotificationError
    {
        private readonly string _dirBase;
        private readonly string _fileName;
        private FileNotificationError(string dirBase, string fileName)
        {
            _dirBase = dirBase;
            _fileName = fileName;
        }
        public static FileNotificationError Create(IConfiguration configuration)
        {
            var fileNotificationErrorOptions = new FileNotificationErrorOptions();
            configuration.GetSection(FileNotificationErrorOptions.ConfigName).Bind(fileNotificationErrorOptions);
            return new FileNotificationError(
                fileNotificationErrorOptions.DirBase,
                fileNotificationErrorOptions.FileName);
        }
        public void Send(SqlSourceDto sqlSourceDto, int errorNum)
        {
            string path = $"{_dirBase}/{_fileName}_{errorNum}.txt";


            if (File.Exists(path))
            {
                File.Delete(path);
            }

            // Create a file to write to.
            using StreamWriter sw = File.CreateText(path);
            sw.WriteLine(sqlSourceDto.QueryName);
            sw.WriteLine(sqlSourceDto.Query);
        }
    }
}

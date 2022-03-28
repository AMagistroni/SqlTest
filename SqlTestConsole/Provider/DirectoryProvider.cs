using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace SqlTestConsole.Provider
{
    public sealed class DirectoryProvider : ISqlSourceProvider
    {
        private readonly IEnumerable<string> _dirQueryBase;
        private readonly string _disableStart;
        private DirectoryProvider(IEnumerable<string> dirQueryBase, string disableStart)
        {
            _dirQueryBase = dirQueryBase;
            _disableStart = disableStart;
        }

        public static DirectoryProvider Create(IConfiguration configuration)
        {
            var directoryProviderOptions = new DirectoryProviderOptions();
            configuration.GetSection(DirectoryProviderOptions.ConfigName).Bind(directoryProviderOptions);
            return new DirectoryProvider(
                directoryProviderOptions.Directories.Split(","),
                directoryProviderOptions.DisableFileStart);
        }

        public IEnumerable<SqlSourceDto> GetSqlQueryTest()
        {
            foreach (var curDir in _dirQueryBase)
            {
                DirectoryInfo dirInfo = new(curDir);
                foreach (var item in dirInfo.GetFiles())
                {
                    if (!item.Name.StartsWith(_disableStart))
                    {
                        yield return new SqlSourceDto
                        {
                            Query = File.ReadAllText(item.FullName),
                            QueryName = item.Name
                        };
                    }
                }
            }
        }
    }
}

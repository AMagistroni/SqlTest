using Serilog;
using SqlTestConsole.NotificationError;
using SqlTestConsole.Provider;
using System;
using System.Data.SqlClient;

namespace SqlTestConsole
{
    public class SqlTestConsoleManager
    {
        private readonly ISqlSourceProvider _sqlSourceProvider;
        private readonly INotificationError _notificationError;
        private readonly string _connectionString;
        private readonly ILogger _logger;
        public SqlTestConsoleManager(ISqlSourceProvider sqlSourceProvider, INotificationError notificationError, string connectionString, ILogger logger)
        {
            _sqlSourceProvider = sqlSourceProvider;
            _logger = logger;
            _connectionString = connectionString;
            _notificationError = notificationError;
        }

        public void TestQuery()
        {
            var sqlSourceDto = _sqlSourceProvider.GetSqlQueryTest();
            int t = 0;
            using SqlConnection conn = new(_connectionString);
            conn.Open();
            foreach (var sqlSource in sqlSourceDto)
            {
                try
                {
                    var command = new SqlCommand(sqlSource.Query, conn);
                    using var numrow = command.ExecuteReader();
                    if (numrow.Read())
                    {
                        _logger.Error($"Error query {sqlSource.QueryName}");
                        _notificationError.Send(sqlSource, t);
                        t++;
                    }
                }
                catch (Exception exc)
                {
                    _logger.Error("Error executing query", exc);
                    _notificationError.SendException(exc, sqlSource, t);
                }
            }
        }
    }
}

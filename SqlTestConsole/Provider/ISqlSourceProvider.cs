using System.Collections.Generic;

namespace SqlTestConsole.Provider
{
    public interface ISqlSourceProvider
    {
        IEnumerable<SqlSourceDto> GetSqlQueryTest();
    }
}

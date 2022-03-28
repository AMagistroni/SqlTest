using System;

namespace SqlTestConsole.NotificationError
{
    public interface INotificationError
    {
        public void Send(SqlSourceDto sqlSourceDto, int errorNum);
        public void SendException(Exception exc, SqlSourceDto sqlSourceDto, int errorNum);
    }
}

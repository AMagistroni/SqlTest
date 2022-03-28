namespace SqlTestConsole.NotificationError
{
    public interface INotificationError
    {
        public void Send(SqlSourceDto sqlSourceDto, int errorNum);
    }
}

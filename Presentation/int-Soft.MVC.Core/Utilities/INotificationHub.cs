namespace intSoft.MVC.Core.Utilities
{
    public interface INotificationHub<in T>
        where T : class
    {
        void Notify(T model);
        void SessionEnded(T model);
    }
}
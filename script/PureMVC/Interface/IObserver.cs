public interface IObserver
{
    /// <summary>
    /// 对比NotifyContext
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    bool CompareNotifyContext(object obj);
    /// <summary>
    /// 通知观察者
    /// </summary>
    /// <param name="notification"></param>
    void NotifyObserver(INotification notification);
    /// <summary>
    /// 记录是Mediator或Command
    /// </summary>
    object NotifyContext { set; }
    /// <summary>
    /// 通知方法
    /// </summary>
    string NotifyMethod { set; }
}
public interface IView
{
    /// <summary>
    /// 是否存在中介者
    /// </summary>
    /// <param name="mediatorName"></param>
    /// <returns></returns>
    bool HasMediator(string mediatorName);
    /// <summary>
    /// 注册中介者
    /// </summary>
    /// <param name="mediator"></param>
    void RegisterMediator(IMediator mediator);
    /// <summary>
    /// 移除中介者
    /// </summary>
    /// <param name="mediatorName"></param>
    /// <returns></returns>
    IMediator RemoveMediator(string mediatorName);
    /// <summary>
    /// 回复中介者
    /// </summary>
    /// <param name="mediatorName"></param>
    /// <returns></returns>
    IMediator RetrieveMediator(string mediatorName);
    /// <summary>
    /// 向观察者下发事件通知
    /// </summary>
    /// <param name="note"></param>
    void NotifyObservers(INotification note);
    /// <summary>
    /// 注册观察者
    /// </summary>
    /// <param name="notificaitoName"></param>
    /// <param name="observer"></param>
    void RegisterObserer(string notificaitoName, IObserver observer);
    /// <summary>
    /// 移除观察者
    /// </summary>
    /// <param name="notificatioName"></param>
    /// <param name="notifyContext"></param>
    void RemoveObserver(string notificatioName, object notifyContext);
}
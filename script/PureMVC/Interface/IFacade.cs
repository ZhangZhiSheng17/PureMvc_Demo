
    using System;
    using UnityEngine.PlayerLoop;
    /// <summary>
    /// 负责注册、注销、恢复、和判断是否存在Command、Proxy、Mediator
    /// </summary>
    public interface IFacade : INotifier
    {
        bool HasCommand(string notifcationName);
        bool HasMediator(string mediatorName);
        bool HasProxy(string ProxyName);
        void RegisterCommand(string notificationName, Type commandType);
        void RegisterMediator(IMediator mediator);
        void RegisterProxy(IProxy proxy);
        void RemoveCommand(string notificationName);
        IMediator RemoveMediator(string mediatorName);
        IProxy RemoveProxy(string ProxyName);
        IMediator RetieveMediator(string mediatorName);
        IProxy RetieveProxy(string ProxyName);
        /// <summary>
        /// 通知观察者
        /// </summary>
        /// <param name="note"></param>
        void NotifyObservers(INotification note);
    }

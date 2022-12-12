using System;

public class Facade : IFacade
{
    protected IController m_controller;
    protected static volatile IFacade m_instance;
    protected IModel m_model;
    protected static readonly object m_staticSyncRoot = new object();
    protected IView m_view;

    /// <summary>
    /// 单例接口
    /// </summary>
    public static IFacade Instance
    {
        get
        {
            if (m_instance == null)
            {
                lock (m_staticSyncRoot)
                {
                    if (m_instance == null)
                    {
                        m_instance = new Facade();
                    }
                }
            }

            return m_instance;
        }
    }

    /// <summary>
    /// Facade 构造函数
    /// </summary>
    protected Facade()
    {
        this.InitializeFacade();
    }

    /// <summary> 
    /// 执行对 mvc进行 赋值  用来持有 IController  IModel  IView
    /// </summary>
    protected virtual void InitializeFacade()
    {
        this.InitializeMove();
        this.InitializeController();
        this.InitializeView();
    }

    /// <summary>
    /// 进行V层 单例接口赋值
    /// </summary>
    protected virtual void InitializeView()
    {
        if (this.m_view == null)
        {
            this.m_view = View.Instanse;
        }
    }

    /// <summary>
    /// 进行C层 单例接口赋值
    /// </summary>
    protected virtual void InitializeController()
    {
        if (this.m_controller == null)
        {
            this.m_controller = Controller.Instance;
        }
    }

    /// <summary>
    /// 进行M层 单例接口赋值
    /// </summary>
    protected virtual void InitializeMove()
    {
        if (this.m_model == null)
        {
            this.m_model = Model.Instance;
        }
    }

    /// <summary>
    ///用来发送命令  获取是否已经注册
    /// C 层调用方法 并执行
    /// </summary>
    /// <param name="notifcationName"></param>
    /// <returns></returns>
    public bool HasCommand(string notifcationName)
    {
        return this.m_controller.HasCommand(notifcationName);
    }

    /// <summary>
    /// 用来发送命令  获取是否 已有该 观察者消息
    /// V 层调用方法  并执行
    /// </summary>
    /// <param name="mediatorName"></param>
    /// <returns></returns>
    public bool HasMediator(string mediatorName)
    {
        return this.m_view.HasMediator(mediatorName);
    }

    /// <summary>
    /// 用来发送命令  获取是否 已有该 状态
    /// M层调用方法   并执行
    /// </summary>
    /// <param name="ProxyName"></param>
    /// <returns></returns>
    public bool HasProxy(string ProxyName)
    {
        return this.m_model.HasProxy(ProxyName);
    }
    
    /// <summary>
    /// 注册观察者 和 观察者信息 
    /// </summary>
    /// <param name="notificationName"></param>
    /// <param name="commandType"></param>
    public void RegisterCommand(string notificationName, Type commandType)
    {
        this.m_controller.RegisterCommand(notificationName, commandType);
    }
    /// <summary>
    ///  注册观察者信息 添加 并 刷新数据
    ///  V 层显调用
    /// </summary>
    /// <param name="mediator"></param>
    public void RegisterMediator(IMediator mediator)
    {
        this.m_view.RegisterMediator(mediator);
    }
    /// <summary>
    /// 添加  数据名字   并刷新
    /// </summary>
    /// <param name="proxy"></param>
    public void RegisterProxy(IProxy proxy)
    {
        this.m_model.RegisterProxy(proxy);
    }
/// <summary>
/// 删除 观察者
/// </summary>
/// <param name="notificationName"></param>
    public void RemoveCommand(string notificationName)
    {
        this.m_controller.RemoveCommand(notificationName);
    }
    /// <summary>
    /// 删除信息
    /// </summary>
    /// <param name="mediatorName"></param>
    /// <returns></returns>
    public IMediator RemoveMediator(string mediatorName)
    {
        return this.m_view.RemoveMediator(mediatorName);
    }
    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="ProxyName"></param>
    /// <returns></returns>
    public IProxy RemoveProxy(string ProxyName)
    {
        return this.m_model.RemvoeProxy(ProxyName);
    }
    /// <summary>
    /// 获取观察者信息
    /// </summary>
    /// <param name="mediatorName"></param>
    /// <returns></returns>
    public IMediator RetieveMediator(string mediatorName)
    {
        return this.m_view.RetrieveMediator(mediatorName);
    }
   
    public IProxy RetieveProxy(string ProxyName)
    {
        return this.m_model.RetrieveProxy(ProxyName);
    }

    public void NotifyObservers(INotification note)
    {
        this.m_view.NotifyObservers(note);
    }


    public void SendNotification(string notificationName)
    {
        this.NotifyObservers(new Notification(notificationName));
    }

    public void SendNotification(string notificationName, object body)
    {
        this.NotifyObservers(new Notification(notificationName, body));
    }

    public void SendNotification(string notificationName, object body, string type)
    {
        this.NotifyObservers(new Notification(notificationName, body, type));
    }
}
using System.Collections.Generic;
using script.CLass;

public class View : IView
{
    protected static volatile IView m_instance;
    protected  IDictionary<string,IMediator> m_mediatorMap=new Dictionary<string, IMediator>();
    protected  IDictionary<string,IList<IObserver>> m_observerMap=new Dictionary<string, IList<IObserver>>();
    protected  static readonly  object m_staticSynRoot=new object();
    protected readonly  object m_syncRoot=new  object();

    protected View()
    {
        this.InitializeView();
    }
    protected virtual void InitializeView()
    {
    }
    
    public virtual bool HasMediator(string mediatorName)
    {
        lock (this.m_syncRoot)
        {
            return this.m_mediatorMap.ContainsKey(mediatorName);
        }
    }
    /// <summary>
    /// 调用 代理 执行方法 执行过的不会再执行
    /// 添加到观察者信息
    /// 通过反射执行  HandleNotification
    /// 执行 OnRegister命令
    /// </summary>
    /// <param name="mediator"></param>
    public virtual void RegisterMediator(IMediator mediator)
    {
        lock (this.m_syncRoot)
        {
            if (this.m_mediatorMap.ContainsKey(mediator.MediatorName))
            {
                return;
            }

            this.m_mediatorMap[mediator.MediatorName] = mediator;
            //添加上 通知的名字
            IList<string> list = mediator.ListNotificationInterests();
            if (list.Count>0)
            {
                IObserver observer = new Observer("handleNotification",mediator);
                for (int i = 0; i < list.Count; i++)
                {
                    this.RegisterObserer(list[i].ToString(),observer);
                }
            }
        }
        mediator.OnRegister();
    }

    public  virtual IMediator RemoveMediator(string mediatorName)
    {
        IMediator notifyContext = null;
        lock (this.m_syncRoot)
        {
            if (!this.m_mediatorMap.ContainsKey(mediatorName))
            {
                return null;
            }

            notifyContext = this.m_mediatorMap[mediatorName];
            IList<string> list = notifyContext.ListNotificationInterests();
            for (int i = 0; i < list.Count; i++)
            {
                this.RemoveObserver(list[i],notifyContext);
            }

            this.m_mediatorMap.Remove(mediatorName);
        }

        if (notifyContext!=null)
        {
            notifyContext.OnRemove();
        }

        return notifyContext;
    }

    public virtual IMediator RetrieveMediator(string mediatorName)
    {
        lock (this.m_syncRoot)
        {
            if (!this.m_mediatorMap.ContainsKey(mediatorName))
            {
                return null;
            }

            return this.m_mediatorMap[mediatorName];
        }
    }

    public virtual void NotifyObservers(INotification note)
    {
        IList<IObserver> list = null;
        lock (this.m_syncRoot)
        {
            if (this.m_observerMap.ContainsKey(note.Name))
            {
                IList<IObserver> collection = this.m_observerMap[note.Name];
                list=new List<IObserver>(collection);
            }
        }

        if (list!=null)
        {
            for (int i = 0; i < list.Count; i++)
            {
                list[i].NotifyObserver(note);
            }
        }
    }
    /// <summary>
    /// 添加观察者信息  优先判断是否 相同名字 如果存在 再判断  信息是否相同
    ///  如果名字不同  信息不同 就添加到字典中 
    /// </summary>
    /// <param name="notificaitoName"></param>
    /// <param name="observer"></param>
    public virtual void RegisterObserer(string notificaitoName, IObserver observer)
    {
        lock (this.m_syncRoot)
        {
            if (!this.m_observerMap.ContainsKey(notificaitoName))
            {
                this.m_observerMap[notificaitoName]=new List<IObserver>();
            }
            // Todd  判断元素是否相同，否则容易造成单一物体的多册注册，造成反复调用的问题
            if (!this.m_observerMap[notificaitoName].Contains(observer))
            {
                this.m_observerMap[notificaitoName].Add(observer);
            }
        }
    }

    public virtual void RemoveObserver(string notificatioName, object notifyContext)
    {
        lock (this.m_syncRoot)
        {
            if (this.m_observerMap.ContainsKey(notificatioName))
            {
                IList<IObserver> list = this.m_observerMap[notificatioName];
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].CompareNotifyContext(notifyContext))
                    {
                        list.RemoveAt(i);
                    }
                }

                if (list.Count==0)
                {
                    this.m_observerMap.Remove(notificatioName);
                }
            }
        }
    }

    public static IView Instanse
    {
        get
        {
            if (m_instance==null)
            {
                lock (m_staticSynRoot)
                {
                    if (m_instance==null)
                    {
                        m_instance=new View();
                    }
                }
            }

            return m_instance;
        }
    }
}
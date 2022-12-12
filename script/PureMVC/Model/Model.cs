using System.Collections.Generic;

public class Model : IModel
{
    protected static volatile IModel m_instance;
    protected IDictionary<string,IProxy> m_proxyMap=new Dictionary<string, IProxy>();
    protected static  readonly  object m_staicSyncRoot=new object();
    protected  readonly  object m_syncRoot=new  object();

    protected Model()
    {
        this.InitializeModel();
    }

    protected virtual void InitializeModel()
    {
        
    }
    
    
    public virtual bool HasProxy(string ProxyName)
    {
        lock (this.m_syncRoot)
        {
            return this.m_proxyMap.ContainsKey(ProxyName);
        }
    }

    public virtual void RegisterProxy(IProxy proxy)
    {
        lock (this.m_syncRoot)
        {
            this.m_proxyMap[proxy.ProxyName] = proxy;
        }
        proxy.OnRegister();
    }

    public virtual IProxy RemvoeProxy(string ProxyName)
    {
        IProxy proxy = null;
        lock (this.m_syncRoot)
        {
            if (this.m_proxyMap.ContainsKey(ProxyName))
            {
                proxy = this.RetrieveProxy(ProxyName);
                this.m_proxyMap.Remove(ProxyName);
            }
        }

        if (proxy!=null)
        {
            proxy.OnRemove();
        }

        return proxy;
    }

    public virtual IProxy RetrieveProxy(string proxyName)
    {
        lock (this.m_syncRoot)
        {
            if (!this.m_proxyMap.ContainsKey(proxyName))
            {
                return null;
            }
            return this.m_proxyMap[proxyName];
        }
    }

    public static IModel Instance
    {
        get
        {
            if (m_instance==null)
            {
                lock (m_staicSyncRoot)
                {
                    if (m_instance==null)
                    {
                        m_instance=new Model();
                    }
                }
            }

            return m_instance;
        }
    }
}
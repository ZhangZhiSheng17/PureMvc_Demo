/// <summary>
/// 通知者负责通知的发放，并持有Facade
/// </summary>
public class Notifier : INotifier
{ 
   private IFacade m_facade= Facade.Instance;

    protected IFacade Facades
    {
        get
        {
            return this.m_facade;
        }
    }
    
    public void SendNotification(string notificationName)
    {
       this.m_facade.SendNotification(notificationName);
    }

    public void SendNotification(string notificationName, object body)
    {
       this.m_facade.SendNotification(notificationName,body);
    }

    public void SendNotification(string notificationName, object body, string type)
    {
        this.m_facade.SendNotification(notificationName,body,type);
    }
    
}
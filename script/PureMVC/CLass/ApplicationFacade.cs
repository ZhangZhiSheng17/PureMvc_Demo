using UnityEngine;

public class ApplicationFacade : Facade 
{
    /// <summary>
    /// Facade单例接口
    /// </summary>
    public new static IFacade Instance
    {
        get
        {
            if (null==m_instance)
            {
                lock (m_staticSyncRoot)
                {
                    if (null==m_instance)
                    {
                        //构造Facade,此处会自动构造View、Controller和Model"
                        m_instance=new ApplicationFacade();
                    }
                }
            }

            return m_instance;
        }
    }
    /// <summary>
    /// qi启动程序
    /// </summary>
    /// <param name="mainUi"></param>
    public void StartUp(MainUI mainUi)
    {
        Debug.Log("启动程序");
        //1返送消息用来做启动
        SendNotification(OrderSystemEvent.STARTUP,mainUi);
    }
    /// <summary>
    /// 重写父类 用来给MVC 做启动  虽然是在父类执行的方法但还是属于子类
    /// </summary>
    protected override void InitializeFacade()
    {
        base.InitializeFacade();
    }
   /// <summary>
   /// 层执行 发送给  C层后用来做观察者消息注册  
   /// </summary>
    protected override void InitializeController()
    {
        base.InitializeController();
        
        RegisterCommand(OrderSystemEvent.STARTUP, typeof(StartUpCommand));
    }

    protected override void InitializeMove()
    {
        base.InitializeMove();
    }

    protected override void InitializeView()
    {
        base.InitializeView();
    }
}
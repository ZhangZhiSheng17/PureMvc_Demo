using System;
using System.Collections.Generic;
using UnityEngine;

public class ClientMediator : Mediator
{
    private ClientProxy clientProxy = null;
    public new const string NAME = "ClientMediator";

    private ClientView View
    {
        get { return (ClientView) ViewCompoent; }
    }

    public ClientMediator(ClientView view) : base(NAME, view)
    {
        view.CallWaitr += data =>
        {
            SendNotification(OrderSystemEvent.CALL_WAITER, data);
        };
        view.Order += data =>
        {
            SendNotification(OrderSystemEvent.ORDER,data);
        };
        view.Pay += () =>
        {
            SendNotification(OrderSystemEvent.PAY);
        };
    }

    public override void OnRegister()
    {
        base.OnRegister();
        clientProxy=Facades.RetieveProxy(ClientProxy.NAME) as ClientProxy;
        if (null==clientProxy)
        {
            throw  new Exception("获取"+ClientProxy.NAME+"代理失败");
        }
        IList<Action<object>> actionlist=new List<Action<object>>()
        {
            item=>SendNotification(OrderCommandEvent.GUEST_BE_AWAY,item,"Add"),
            item=> SendNotification(OrderSystemEvent.GET_PAY,item),
        };
        
        View.UpdataClient(clientProxy.Clients,actionlist);
    }

    public override IList<string> ListNotificationInterests()
    {
       IList<string> notifications=new List<string>();
       notifications.Add(OrderSystemEvent.CALL_WAITER);
       notifications.Add(OrderSystemEvent.ORDER);
       notifications.Add(OrderSystemEvent.PAY);
       notifications.Add(OrderSystemEvent.REFRESH);
       return notifications;
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case OrderSystemEvent.CALL_WAITER:
                ClientItem client = notification.Body as ClientItem;
                Debug.Log(client.id + " 号桌顾客呼叫服务员 , 索要菜单 ");
                break;
            case OrderSystemEvent.ORDER: 
                Order order1 = notification.Body as Order;
                if(null == order1)
                    throw new Exception("order1 is null ,please check it!");
                SendNotification(OrderCommandEvent.ChangeClientState, order1,"WaitFood");
                break;
            case OrderSystemEvent.PAY:
                break;
            case OrderSystemEvent.REFRESH:
                Debug.Log("刷新界面");
                ClientItem clientItem = notification.Body as ClientItem;
                if (null == clientProxy)
                    throw new Exception("获取" + ClientProxy.NAME + "代理失败");
                View.UpdateState(clientItem);
                break;
        }
    }
}
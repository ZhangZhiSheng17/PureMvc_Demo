    
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    
    public class RoomMediator : Mediator
    {
        private RoomProxy roomltem = null;
        public new const string NAME = "RoomMediator";
    
        private RoomView View
        {
            get
            {
                return (RoomView) ViewCompoent;
            }
        }
         public RoomMediator( RoomView view ) : base(NAME , view)
        {
        }
    
        public override void OnRegister()
        {
            base.OnRegister();
            roomltem = Facades.RetieveProxy(RoomProxy.NAME) as RoomProxy;
            if(null == roomltem)
                throw new Exception("获取" + RoomProxy.NAME + "代理失败");
            IList<Action<object> > actionList = new List<Action<object>>()
            {
                item =>  SendNotification(OrderSystemEvent.Invoicing, item),
            };
            View.UpdateRoom(roomltem.Roomltems,actionList);
        }
    
        public override IList<string> ListNotificationInterests()
        {
            IList<string> notifications = new List<string>();
            notifications.Add(OrderCommandEvent.Wehaveavisitor);
            notifications.Add(OrderSystemEvent.Someoneroom);
            notifications.Add(OrderSystemEvent.Invoicing);
            return notifications;
        }
    
        public override void HandleNotification(INotification notification)
        {
            Debug.Log(notification.Name);
            switch (notification.Name)
            {
                case OrderCommandEvent.Wehaveavisitor :
                    Bill order1 = notification.Body as Bill;
                    if(null == order1)
                        throw new Exception("order1 is null ,please check it!");
                    
                    SendNotification(OrderCommandEvent.Lookingfornemptyroom, order1,"Busy");
                    break;
                case  OrderSystemEvent.Someoneroom : 
                    Debug.Log("刷新界面");
                    Roomltem bill = notification.Body as Roomltem;
                    if (null == bill)
                        throw new Exception("获取" + RoomProxy.NAME + "代理失败");
                    View.UpdateState(bill);
                    break;
                case OrderSystemEvent.Invoicing : 
                    Debug.Log(" 客房结账了 ");
                    Roomltem item = notification.Body as Roomltem;
                    SendNotification(OrderCommandEvent.Roombill, item, "Pay");
                    break;
            }
        }
    }

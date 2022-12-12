
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;
    using UnityEngine;

    public class WaiterMediator : Mediator
    {
        private WaiterProxy waiterProxy = null;
        public new const string NAME = "WaiterMediator";

        public WaiterView WaiterView
        {
            get
            {
                return (WaiterView) base.ViewCompoent;
            }
        }

        private OrderProxy orderProxy = null;

        public WaiterMediator(WaiterView view) : base(NAME, view)
        {
            WaiterView.CallWaiter += () =>
            {
                SendNotification(OrderSystemEvent.CALL_WAITER);
            };
            WaiterView.Order += data =>
            {
                SendNotification(OrderSystemEvent.ORDER,data);
            };
            WaiterView.Pay += () =>
            {
                SendNotification(OrderSystemEvent.PAY);
            };
            WaiterView.CallCook += () =>
            {
                SendNotification(OrderSystemEvent.CALL_COOK);
            };
            WaiterView.ServerFood += itme =>
            {
                SendNotification(OrderCommandEvent.selectWaiter,itme,"WANSHI");
            };
             
        }

        public override void OnRegister()
        {
            base.OnRegister();
            waiterProxy=Facades.RetieveProxy(WaiterProxy.NAME) as WaiterProxy;
            orderProxy=Facades.RetieveProxy(OrderProxy.NAME) as OrderProxy;
            if (null==waiterProxy)
            {
                throw new Exception(WaiterProxy.NAME +"is null,please check it!");
            }

            if (null == orderProxy)
            {
                throw new Exception(OrderProxy.NAME+"is null,please check it!");
            }
            WaiterView.UpdataWaiter(waiterProxy.Waiters);
        }

        public override IList<string> ListNotificationInterests()
        {
           IList<string> notifications=new List<string>();
           notifications.Add(OrderSystemEvent.CALL_WAITER);
           notifications.Add(OrderSystemEvent.ORDER);
           notifications.Add(OrderSystemEvent.GET_PAY);
           notifications.Add(OrderSystemEvent.FOOD_TO_CLIENT);
           notifications.Add(OrderSystemEvent.ResfrshWarite);
           return notifications;
        }

        public override void HandleNotification(INotification notification)
        {
            switch (notification.Name)
            {
                case OrderSystemEvent.CALL_WAITER : ClientItem client=notification.Body as ClientItem;
                    SendNotification(OrderCommandEvent.GET_ORDER,client,"Get");
                    break;
                case OrderSystemEvent.ORDER : 
                    SendNotification(OrderSystemEvent.CALL_COOK,notification.Body);
                    break;
                case OrderSystemEvent.GET_PAY:
                    Debug.Log("服务员拿到顾客的付款");
                    ClientItem item=notification.Body as ClientItem;
                    SendNotification(OrderCommandEvent.GUEST_BE_AWAY,item,"Remove");
                    break;
                case OrderSystemEvent.FOOD_TO_CLIENT : 
                    Debug.Log("服务员上菜");
                    WaiterItem waiterItem=notification.Body as WaiterItem;
                    SendNotification(OrderCommandEvent.ChangeClientState,waiterItem.order,"Eating");
                    SendNotification(OrderSystemEvent.PAY,waiterItem);
                    break;
                case OrderSystemEvent.ResfrshWarite :
                    waiterProxy=Facades.RetieveProxy(WaiterProxy.NAME) as WaiterProxy;
                    WaiterView.Move(waiterProxy.Waiters);
                    break;
            }
        }

        private WaiterItem GetIdleWaiter()
        {
            foreach (WaiterItem waiter in waiterProxy.Waiters)
            {
                if (waiter.state.Equals((int)E_WaiterState.Idle))
                {
                    return waiter;
                }
            }
            Debug.LogWarning("暂无空闲服务员请稍等");
            return null;
        }
    }

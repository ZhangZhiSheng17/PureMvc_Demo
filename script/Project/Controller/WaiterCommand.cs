
    using UnityEngine;

    public class WaiterCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            WaiterProxy waiterProxy=Facades.RetieveProxy(WaiterProxy.NAME) as WaiterProxy;
            if (notification.Type=="SERVING")
            {
                Debug.Log("寻找服务员上菜");
                waiterProxy.ChangeWaiter(notification.Body as Order);
            }
            else if(notification.Type=="WANSHI")
            {
                Debug.Log("服务员没事干");
                waiterProxy.RemoveWiaiter(notification.Body as WaiterItem );
            }
        }
    }

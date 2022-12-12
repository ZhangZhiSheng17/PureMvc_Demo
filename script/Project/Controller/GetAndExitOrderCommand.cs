
    using UnityEngine;

    public class GetAndExitOrderCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            OrderProxy orderProxy=Facade.Instance.RetieveProxy("OrderProxy") as OrderProxy;
            MenuProxy menuProxy=Facade.Instance.RetieveProxy("MenuProxy") as MenuProxy;
            Debug.Log(notification.Type);
            if (notification.Type=="Get")
            {
                Order order=new Order(notification.Body as ClientItem,null,menuProxy.Menus);
                orderProxy.AddOrder(order);
                SendNotification(OrderSystemEvent.UPMENU,order);
            }
            else if(notification.Type=="Exit")
            {
                Order order = new Order(notification.Body as ClientItem,null, menuProxy.Menus);
                orderProxy.RemoveOrder(order); 
            }
        }
    }

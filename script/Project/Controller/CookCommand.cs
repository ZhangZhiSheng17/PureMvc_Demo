
    public class CookCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            CookProxy cookProxy=Facades.RetieveProxy(CookProxy.NAME) as CookProxy;
            Order order= notification.Body as Order;
            cookProxy.CookCooking(order);
        }
    }

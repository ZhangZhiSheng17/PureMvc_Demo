
    public class ClientChangeStateCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
           Order order=notification.Body as Order;
           ClientProxy clientProxy=Facades.RetieveProxy(ClientProxy.NAME)as ClientProxy;
           switch (notification.Type)
           {
               case "WaitFood":
                   clientProxy.ChangeClientState(order.client,ClientState.WaitFood);
                   break;
               case "Eating" : 
                   clientProxy.ChangeClientState(order.client,ClientState.Eating);
                   break;
           }
           
        }
    }



    public class RoomRoomCommand : SimpleCommand
    {
        public override void Execute(INotification notification)
        {
            Roomltem order = notification.Body as Roomltem;
            RoomProxy roomProxy = Facades.RetieveProxy(RoomProxy.NAME) as RoomProxy;
            switch (notification.Type)
            {
                case "Pay":
                    roomProxy.DeleteRoom(order);
                    break;
            }
        }
    }

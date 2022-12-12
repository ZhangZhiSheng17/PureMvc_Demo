public class RoomCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Bill order=notification.Body as Bill;
        RoomProxy roomProxy=Facades.RetieveProxy(RoomProxy.NAME)as RoomProxy;
        switch (notification.Type)
        {
            case "Busy":
                roomProxy.ChangeRoomState(order.roomltem);
                break;
        }
        
       
      
    }
}
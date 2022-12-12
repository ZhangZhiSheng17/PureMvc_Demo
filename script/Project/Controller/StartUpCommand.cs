
    using System;
    using UnityEngine;

    internal class StartUpCommand : SimpleCommand
    {
        public override void Execute(INotification parameter)
        {
            //菜单代理
           MenuProxy menuProxy=new MenuProxy();
           Facades.RegisterProxy(menuProxy);
           //客户端代理
           ClientProxy clientProxy=new ClientProxy();
           Facades.RegisterProxy(clientProxy);
           //服务员代理
           WaiterProxy waiterProxy=new WaiterProxy();
           Facades.RegisterProxy(waiterProxy);
           ///厨师代理
           CookProxy cookProxy=new CookProxy();
           Facades.RegisterProxy(cookProxy);
           //房间代理
           RoomProxy roomProxy=new RoomProxy();
           Facades.RegisterProxy(roomProxy);
           
           //订单代理
           OrderProxy orderProxy=new OrderProxy();
           Facades.RegisterProxy(orderProxy);
           
           
           
           
           
           
           
           //通知事件
           MainUI mainUi=parameter.Body as MainUI;

           if (null== mainUi)
           {
               throw  new  Exception("程序启动失败...");
           }
           Facades.RegisterMediator(new MenuMediator(mainUi.MenuView));
           Facades.RegisterMediator(new  ClientMediator(mainUi.ClientView));
           Facades.RegisterMediator(new WaiterMediator(mainUi.WaiterView));
           Facades.RegisterMediator(new CookMediator(mainUi.CookView));
           Facades.RegisterMediator(new RoomMediator(mainUi.RoomView));
     
           
           Facades.RegisterCommand(OrderCommandEvent.GUEST_BE_AWAY,typeof(GuestBeAwayCommand));
           Facades.RegisterCommand(OrderCommandEvent.GET_ORDER,typeof(GetAndExitOrderCommand));
           Facades.RegisterCommand(OrderCommandEvent.CooKCooking, typeof(CookCommand));
           Facades.RegisterCommand(OrderCommandEvent.selectWaiter, typeof(WaiterCommand));
           Facades.RegisterCommand(OrderCommandEvent.ChangeClientState, typeof(ClientChangeStateCommand));
           Facades.RegisterCommand(OrderCommandEvent.Lookingfornemptyroom, typeof(RoomCommand));
           Facades.RegisterCommand(OrderCommandEvent.Roombill, typeof(RoomRoomCommand));



        }
    }

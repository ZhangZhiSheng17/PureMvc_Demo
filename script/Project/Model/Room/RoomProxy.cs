    
    using System.Collections.Generic;
    using UnityEngine;
    
    public class RoomProxy : Proxy
    {
        public new const string NAME = "RoomProxy";
    
        public IList<Roomltem> Roomltems
        {
            get
            {
                return (IList<Roomltem>) base.Data;
            }
        }
    
        public RoomProxy() : base(NAME,new List<Roomltem>())
        {
            AddRoom(new Roomltem(1,0,0));
            AddRoom(new Roomltem(2,0,0));
            AddRoom(new Roomltem(3,0,0));
            AddRoom(new Roomltem(4,0,0));
        }
    
        public void AddRoom(Roomltem item)
        {
        if (Roomltems.Count < 4) 
            {
                Roomltems.Add(item);
            }
            UpdateRoom(item);
        }
    
        public void DeleteRoom(Roomltem item)
        {
            Debug.Log(item);
            for (int i = 0; i < Roomltems.Count; i++)
            {
                if (Roomltems[i].id==item.id)
                {
                    Roomltems[i].roomState = RoomState.Idle;
                    SendNotification(OrderSystemEvent.Someoneroom,Roomltems[i]);
                    return;
                }
            }
        }
        public void ChangeRoomState(Roomltem item)
        {
            SendNotification(OrderSystemEvent.Someoneroom,item);
        }
    
        private void UpdateRoom(Roomltem item)
        {
            Debug.Log(item.roomState);
            for (int i = 0; i < Roomltems.Count; i++)
            {
                if (Roomltems[i].id==item.id)
                {
                    Roomltems[i] = item;
                    Roomltems[i].roomState = item.roomState;
                    Roomltems[i].number = item.number;
                    SendNotification(OrderSystemEvent.Someoneroom,Roomltems[i]);
                    return;
                }
            }
        }
    }

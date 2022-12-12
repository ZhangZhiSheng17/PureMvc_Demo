    
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    
    public class RoomView : MonoBehaviour
    {
        public UnityAction<Roomltem> CallRoom = null;
        private ObjectPool<RoomItemView> objectPool = null;
        private List<RoomItemView> rooms=new List<RoomItemView>();
        private Transform parent = null;
        public UnityAction<Order> Order = null;
        public UnityAction Pay = null;
    
        public void Awake()
        {
        
            parent = this.transform.Find("Content");
            var prefab = Resources.Load<GameObject>("UI/RoomItem");
            objectPool = new ObjectPool<RoomItemView>(prefab , "RoomPool");
        }
    
        public void UpdateRoom(IList<Roomltem> rooms,IList<Action<object>> objs)
        {
            for (int i = 0; i < this.rooms.Count; i++)
            {
                objectPool.Push(this.rooms[i]);
            }
            this.rooms.AddRange(objectPool.Pop(rooms.Count));
            
            for ( int i = 0 ; i < this.rooms.Count ; i++ )
            {
                var room = this.rooms[i];
                room.transform.SetParent(parent);
                room.InitClient(rooms[i]);
                room.actionList = objs;
            }
        }
        public void UpdateState( Roomltem room )
        {
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms[i].roomeitem.id.Equals(room.id))
                {
                    rooms[i].InitClient(room);
                }
            }
        }
    
        public void RefrshClient(IList<Roomltem> Reclients)
        {
            for (int i = 0; i < Reclients.Count; i++)
            {
                UpdateState(Reclients[i]);
            }
        }
    }

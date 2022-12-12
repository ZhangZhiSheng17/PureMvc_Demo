
public enum RoomState
{
    Idle,
    Busy,
    Pay
}
    public class Roomltem
    {
        public int id { get;set;}
        public int number { get; set; }
        public RoomState roomState { get; set; }

        public Roomltem(int id,int number,RoomState roomState)
        {
            this.id = id;
            this.number = number;
            this.roomState = roomState;
        }

        public override string ToString()
        {
            return id + "号房间" + "\n" + number + "个人" + "\n" + returnState(roomState);
        }

        public string returnState(RoomState roomState)
        {
            if (roomState.Equals(RoomState.Idle))
            {
                return "空房间";
            }

            if (roomState.Equals(RoomState.Busy))
            {
                return "已经有人";
            }
            return "人走了";
        }
        
    }

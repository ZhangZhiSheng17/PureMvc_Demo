    
    using System.Collections.Generic;
    
    public class Bill
    {
        public int id { get; set; }
      
        public Roomltem roomltem { get; set; }

        public Bill(Roomltem roomltems)
        {
           
             this.roomltem=roomltems;
        }
    
        public override string ToString()
        {
            return roomltem.id + "客房" + roomltem.number + "位顾客入住";
        }
    }

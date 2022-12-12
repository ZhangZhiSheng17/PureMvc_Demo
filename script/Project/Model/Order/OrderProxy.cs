using System.Collections.Generic;

public class OrderProxy : Proxy
{
     public new const string NAME = "OrderProxy";

     public IList<Order> Orders
     {
          get
          {
               return (IList<Order>) base.Data;
          }
     }

     public void AddOrder(Order order)
     {
          order.id = Orders.Count + 1;
          Orders.Add(order);
     }

     public void RemoveOrder(Order order)
     {
          Orders.Remove(order);
     }
     /// <summary>
     /// 订单 来自顾客
     /// </summary>
     public OrderProxy() : base(NAME, new List<Order>())
     {
          
     }
     
     
}
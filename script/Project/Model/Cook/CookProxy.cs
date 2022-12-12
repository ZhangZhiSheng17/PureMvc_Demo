
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 厨师
/// </summary>
public class CookProxy : Proxy
{
    public new const string NAME = "CookProxy";

    public IList<CookItem> Cooks
    {
        get
        {
            return (IList<CookItem>)base.Data;
        }
    }

    public void AddCook(CookItem item)
    {
        Cooks.Add(item);
    }

    public void RemoveCook(CookItem item)
    {
        Cooks.Remove(item);
    }

    public void CookCooking(Order order)
    {
        for (int i = 0; i < Cooks.Count; i++)
        {
            if (Cooks[i].state == 0)
            {
                Cooks[i].state++;
                Cooks[i].cooking = order.names;
                Cooks[i].cookOrder = order;
                UnityEngine.Debug.Log(order.names);
                SendNotification(OrderSystemEvent.ResfrshCook);
                return;
            }
        }
    }

    public CookProxy() : base(NAME, new List<CookItem>())
    {
        AddCook(new CookItem(1, "强尼", 0));
        AddCook(new CookItem(2, "托尼", 0));
        AddCook(new CookItem(3, "鲍比", 0));
        AddCook(new CookItem(4, "缇米", 0));
    }
}

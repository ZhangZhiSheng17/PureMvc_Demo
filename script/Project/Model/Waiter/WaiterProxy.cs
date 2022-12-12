using System.Collections.Generic;

/// <summary>
/// 服务员
/// </summary>
public class WaiterProxy : Proxy
{
    public new const string NAME = "WaiterProxy";

    public IList<WaiterItem> Waiters
    {
        get { return (IList<WaiterItem>) base.Data; }
    }

    public void AddWaiter(WaiterItem item)
    {
        Waiters.Add(item);
    }

    public void RemoveWiaiter(WaiterItem item)
    {
        for (int i = 0; i < Waiters.Count; i++)
        {
            if (item.id == Waiters[i].id)
            {
                Waiters[i].state = 0;
                SendNotification(OrderSystemEvent.ResfrshWarite);
                return;
            }
        }
    }

    public WaiterProxy() : base(NAME, new List<WaiterItem>())
    {
        AddWaiter(new WaiterItem(1, "小丽", 0));
        AddWaiter(new WaiterItem(2, "小红", 0));
        AddWaiter(new WaiterItem(3, "小花", 0));
    }

    public void ChangeWaiter(Order order)
    {
        WaiterItem item = GetIdleWaiter();
        if (item != null)
        {
            item.state = 1;
            item.order = order;

            SendNotification(OrderSystemEvent.ResfrshWarite);
            SendNotification(OrderSystemEvent.FOOD_TO_CLIENT, item); //发送炒好的菜单信息
        }
    }

    public WaiterItem GetIdleWaiter()
    {
        foreach (WaiterItem waiter in Waiters)
            if (waiter.state.Equals((int) E_WaiterState.Idle))
            {
                return waiter;
            }

        UnityEngine.Debug.LogWarning("暂无空闲服务员请稍等..");
        return null;
    }
}
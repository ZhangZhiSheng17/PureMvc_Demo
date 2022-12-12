using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 客户端
/// </summary>
public class ClientProxy : Proxy
{
    public new const string NAME = "ClientProxy";
    public Roomltem roomltem = null;

    public IList<ClientItem> Clients
    {
        get { return (IList<ClientItem>) base.Data; }
    }

    public void AddClient(ClientItem item)
    {
        if (Clients.Count < 5)
        {
            Clients.Add(item);
        }

        UpdataClient(item);
    }

    public void DeleteClient(ClientItem item)
    {
        Debug.Log(item);
        for (int i = 0; i < Clients.Count; i++)
        {
            if (Clients[i].id == item.id)
            {
                Clients[i].state = ClientState.Pay;
                SendNotification(OrderSystemEvent.REFRESH, Clients[i]);
                int Raom = 1;
                if (Raom == 1 && Clients[i].id != 5)
                {
                    roomltem = new Roomltem(Clients[i].id, Clients[i].population, RoomState.Busy);
                    SendNotification(OrderCommandEvent.Wehaveavisitor, new Bill(roomltem));
                }

                return;
            }
        }

        Debug.Log("客人走了刷新一下界面");
    }

    public void UpdataClient(ClientItem item)
    {
        for (int i = 0; i < Clients.Count; i++)
        {
            if (Clients[i].id == item.id)
            {
                Clients[i] = item;
                Clients[i].state = item.state;
                Clients[i].population = item.population;
                SendNotification(OrderSystemEvent.REFRESH, Clients[i]);
                return;
            }
        }
    }

    public void ChangeClientState(ClientItem item, ClientState state)
    {
        item.state = state;
        SendNotification(OrderSystemEvent.REFRESH, item);
    }

    public ClientProxy() : base(NAME, new List<ClientItem>())
    {
        AddClient(new ClientItem(1, 2, 0));
        AddClient(new ClientItem(2, 1, 0));
        AddClient(new ClientItem(3, 4, 0));
        AddClient(new ClientItem(4, 5, 0));
        AddClient(new ClientItem(5, 12, 0));
    }
}
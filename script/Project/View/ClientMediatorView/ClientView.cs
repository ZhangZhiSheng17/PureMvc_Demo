
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;

    public class ClientView : MonoBehaviour
    {
        public UnityAction<ClientItem> CallWaitr = null;
        public UnityAction<Order> Order = null;
        public UnityAction Pay = null;
        private ObjectPool<ClientItemView> objectPool = null;
        private  List<ClientItemView> clients=new List<ClientItemView>();
        private Transform parent = null;

        private void Awake()
        {
            parent = this.transform.Find("Content");
            var prefab = Resources.Load<GameObject>("UI/ClientItem");
            objectPool=new ObjectPool<ClientItemView>(prefab,"ClientPool");
        }

        public void UpdataClient(IList<ClientItem> clients,IList<Action<object>> objs)
        {
            for (int i = 0; i < this.clients.Count; i++)
            {
                objectPool.Push(this.clients[i]);
            }
            this.clients.AddRange(objectPool.Pop(clients.Count));
            for (int i = 0; i < this.clients.Count; i++)
            {
                var client = this.clients[i];
                client.transform.SetParent(parent);
                client.InitClient(clients[i]);
                client.actionList = objs;
                client.GetComponent<Button>().onClick.RemoveAllListeners();
                client.GetComponent<Button>().onClick.AddListener(() =>
                {
                    CallWaitr(client.client);
                });
            }
        }

        public void UpdataState(ClientItem client)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].client==null)
                {
                    continue;
                }

                if (clients[i].client.id.Equals(client.id))
                {
                    clients[i].InitClient(client);
                    return;
                }
            }
        }


        public void UpdateState(ClientItem clientItem)
        {
            for (int i = 0; i < clients.Count; i++)
            {
                if (clients[i].client.id.Equals(clientItem.id))
                {
                    clients[i].InitClient(clientItem);
                    return;
                }
            }
        }
    }

public class CopyOfClientView : MonoBehaviour
{
    public UnityAction<ClientItem> CallWaitr = null;
    public UnityAction<Order> Order = null;
    public UnityAction Pay = null;
    private ObjectPool<ClientItemView> objectPool = null;
    private List<ClientItemView> clients = new List<ClientItemView>();
    private Transform parent = null;

    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("UI/ClientItem");
        objectPool = new ObjectPool<ClientItemView>(prefab, "ClientPool");
    }

    public void UpdataClient(IList<ClientItem> clients)
    {
        for (int i = 0; i < this.clients.Count; i++)
        {
            objectPool.Push(this.clients[i]);
        }
        this.clients.AddRange(objectPool.Pop(clients.Count));
        for (int i = 0; i < this.clients.Count; i++)
        {
            var client = this.clients[i];
            client.transform.SetParent(parent);
            client.InitClient(clients[i]);
            client.GetComponent<Button>().onClick.RemoveAllListeners();
            client.GetComponent<Button>().onClick.AddListener(() =>
            {
                CallWaitr(client.client);
            });
        }
    }

    public void UpdataState(ClientItem client)
    {
        for (int i = 0; i < clients.Count; i++)
        {
            if (clients[i].client == null)
            {
                continue;
            }

            if (clients[i].client.id.Equals(client.id))
            {
                clients[i].InitClient(client);
                return;
            }
        }
    }


}

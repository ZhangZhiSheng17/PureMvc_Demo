using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class MenuView : MonoBehaviour
{
    public UnityAction<Order> Submit = null;

    public UnityAction Cancel = null;

    private ObjectPool<MenuItemView> objectPool = null;
    private  List<MenuItemView> menus=new List<MenuItemView>();
    private Transform parent = null;
    private Order indexOrder = null;

    private void Awake()
    {
        parent = this.transform.Find("Content");
        var prefab = Resources.Load<GameObject>("UI/MenuItem");
        objectPool=new ObjectPool<MenuItemView>(prefab,"MenuPool");
        transform.Find("SubmitButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Submit(indexOrder);
        });
        transform.Find("CancelButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            CancelMenu();
        });
    }

    public void CancelMenu()
    {
        this.transform.localPosition=new Vector3(0,-800,0);
    }

    public void UpdataMenu(IList<MenuItem> menus)
    {
        for (int i = 0; i < this.menus.Count; i++)
        {
            objectPool.Push(this.menus[i]);
        }
        this.menus.AddRange(objectPool.Pop(menus.Count));
        for (int i = 0; i < this.menus.Count; i++)
        {
            this.menus[i].transform.SetParent(parent);
            var item = this.menus[i];
            item.InitData(menus[i]);
        }
    }

    public void UpMeun(Order order)
    {
        ResetMenu();
        indexOrder = order;
        this.transform.localPosition=new Vector3(0,0,0);
    }
    
    private void ResetMenu()
    {
        foreach (MenuItemView menu in menus)
        {
            menu.toggle.isOn = false;
        }
    }

    public void SubmitMenun(Order order)
    {
        order.menus = GetSelected();
        CancelMenu();
    }

    private IList<MenuItem> GetSelected()
    {
        IList<MenuItem> result=new List<MenuItem>();
        for (int i = 0; i < menus.Count; i++)
        {
            if (menus[i].Menu.iselected)
            {
                result.Add(menus[i].Menu);
            }
        }

        return result;
    }

}

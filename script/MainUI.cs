using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public MenuView MenuView = null;

    public ClientView ClientView = null;

    public WaiterView WaiterView = null;

    public CookView CookView = null;

    public RoomView RoomView = null;
    // Start is called before the first frame update
    void Start()
    {
        //做启动处理
        ApplicationFacade facade=new ApplicationFacade();
        facade.StartUp(this);   
        Debug.Log("启动成功");
    }

    // Update is called once per frame
    void Update()
    {
    }
}

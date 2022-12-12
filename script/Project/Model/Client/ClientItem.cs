
/// <summary>
/// 客户端枚举
/// </summary>
public enum E_ClientState
{
    WaitMenu=0,
    WaitFood=1,
    Pay=2
}
/// <summary>
/// 客户端数据类型
/// </summary>
public class ClientItem
{
    public int id { get; set; }
    public int population{ get; set; }
    public ClientState state{ get; set; }

    public ClientItem(int id, int population, ClientState state)
    {
        this.id = id;
        this.population = population;
        this.state = state;
    }

    public override string ToString()
    {
        return id + "桌号" + "\n" + population + "个人" + "\n" + retunrState(state);
    }

    private string retunrState( ClientState state)
    {
        if (state.Equals(ClientState.WaitMenu))
        {
            return "等待带单";
        }
        else if(state.Equals(ClientState.WaitFood))
        {
            return "等待上菜";
        }
        else if (state.Equals(ClientState.Eating))
        {
            return "就餐中";
        }
        return "就餐完毕";

    }
}

public enum E_WaiterState
{
    Idle,
    Busy
}
/// <summary>
/// 服务员数据类型
/// </summary>
public class WaiterItem
{
    public int id { get; set; }
    public string name { get; set; }
    public int state { get; set; }
    public  Order order { get; set; }

    public WaiterItem(int id,string name,int state)
    {
        this.id = id;
        this.name = name;
        this.state = state;
    }

    public override string ToString()
    {
        return id + "服务员\n" + name + "\n" + returnLtState();
    }

    private string returnLtState()
    {
        if (state.Equals(0))
        {
            return " 休息中";
        }

        return "忙碌中";
    }
}
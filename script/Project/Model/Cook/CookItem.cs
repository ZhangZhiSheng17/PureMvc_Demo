/// <summary>
/// 厨师数据类型
/// </summary>
public class CookItem 
{
    public int id { get; set; }
    public string name { get; set; }
    public int state { get; set; }
    public string cooking { get; set; }
    public Order cookOrder { get; set; }

    public CookItem(int id,string name,int state,string cooking="",Order cookOrder=null)
    {
        this.id = id;
        this.name = name;
        this.state = state;
        this.cooking = cooking;
        this.cookOrder = cookOrder;
    }

    public override string ToString()
    {
        return id + "\n" + name + "\n" + returnItstate();
    }

    private string returnItstate()
    {
        if (state.Equals(0))
        {
            return "休息中";
        }

        return "忙碌中";
    }
}
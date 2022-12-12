/// <summary>
/// 菜单数据类型
/// </summary>
public class MenuItem
{
    public int id { get; set; }
    public string name { get; set; }
    public float price { get; set; }
    /// <summary>
    /// 是否有货 true 有货  false无货
    /// </summary>
    public bool instock { get; set; }
    public bool iselected { get; set; }

    public MenuItem(int id,string name,float price,bool instock)
    {
        this.id = id;
        this.name = name;
        this.price = price;
        this.instock = instock;
        iselected = false;
    }

    public override string ToString()
    {
        return id + ":" + name + ":" + price + ":" + instock;
    }
}
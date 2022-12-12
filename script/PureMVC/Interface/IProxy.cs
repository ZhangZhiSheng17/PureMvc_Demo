/// <summary>
/// 处理M层
/// </summary>
public interface IProxy
{
    /// <summary>
    /// 注册
    /// </summary>
    void OnRegister();
    /// <summary>
    /// 注销
    /// </summary>
    void OnRemove();
    /// <summary>
    /// 原始数据
    /// </summary>
    object Data { get; set; }
    /// <summary>
    /// 代理名称
    /// </summary>
    string ProxyName { get; }
}
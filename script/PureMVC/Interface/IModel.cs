/// <summary>
/// 处理V层
/// </summary>
public interface IModel
{
    /// <summary>
    /// 是否存在代理
    /// </summary>
    /// <param name="ProxyName"></param>
    /// <returns></returns>
    bool HasProxy(string ProxyName);
    /// <summary>
    /// 注册代理
    /// </summary>
    /// <param name="proxy"></param>
    void RegisterProxy(IProxy proxy);
    /// <summary>
    /// 修除代理
    /// </summary>
    /// <param name="ProxyName"></param>
    /// <returns></returns>
    IProxy RemvoeProxy(string ProxyName);
    /// <summary>
    ///回复代理
    /// </summary>
    /// <param name="proxyName"></param>
    /// <returns></returns>
    IProxy RetrieveProxy(string proxyName);
}
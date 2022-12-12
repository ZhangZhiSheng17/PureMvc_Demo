public interface INotification
{
    /// <summary>
    /// 重写通知用于调试输出
    /// </summary>
    /// <returns></returns>
    string ToString();

    /// <summary>
    /// 通知事件
    /// </summary>
    object Body { get; set; }

    /// <summary>
    /// 通知名称
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 通知类型
    /// </summary>
    string Type { get; set; }
}
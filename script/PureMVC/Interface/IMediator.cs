using System.Collections.Generic;
using UnityEngine.PlayerLoop;

public interface IMediator
{
    /// <summary>
    /// 处理通知
    /// </summary>
    /// <param name="notification"></param>
    void HandleNotification(INotification notification);

    /// <summary>
    /// 通知的名字
    /// </summary>
    /// <returns></returns>
    IList<string> ListNotificationInterests();

    /// <summary>
    /// 注册
    /// </summary>
    void OnRegister();
    /// <summary>
    /// 移除
    /// </summary>
    void OnRemove();
    /// <summary>
    /// 名称
    /// </summary>
    string MediatorName { get; }
    /// <summary>
    /// 视图组件 这里是村的object类型
    /// </summary>
    object ViewCompoent { get; set; }
}
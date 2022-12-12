using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 接口类 发送消息
/// </summary>
public interface  INotifier
{
   /// <summary>
   /// 接口方法   根据要发的消息的不同用不同的方法
   /// </summary>
   /// <param name="notificationName"></param>
   void SendNotification(string notificationName);
   /// <summary>
   /// 接口方法   根据要发的消息的不同用不同的方法
   /// </summary>
   void SendNotification(string notificationName,object body);
   /// <summary>
   /// 接口方法   根据要发的消息的不同用不同的方法
   /// </summary>
   void SendNotification(string notificationName,object body,string type);
}

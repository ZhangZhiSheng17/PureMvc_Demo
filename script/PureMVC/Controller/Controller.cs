
    using System;
    using System.Collections.Generic;
    using System.Windows.Input;
    using script.CLass;

    public class Controller : IController
    {
        protected  IDictionary<string,Type> m_commandMap=new Dictionary<string, Type>();
        protected static volatile IController m_instance;
        protected static readonly object m_staticSyncRoot= new  object();
        protected readonly  object m_syncRoot = new  object();
        protected IView m_view;
        
        protected Controller()
        {
            this.InitializeController();
        }

        protected virtual void InitializeController()
        {
            this.m_view = View.Instanse;
        }

        public static IController Instance
        {
            get
            {
                if (m_instance==null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance==null)
                        {
                            m_instance=new Controller();
                        }
                    }
                }

                return m_instance;
            }
        }
        
        
        public void ExecuteCommand(INotification notification)
        {
            Type type = null;
            lock (this.m_syncRoot)
            {
                if (!this.m_commandMap.ContainsKey(notification.Name))
                {
                 return;   
                }

                type = this.m_commandMap[notification.Name];
            }
            object obj2 = Activator.CreateInstance(type);
            if (obj2 is ICommand)
            {
                ((ICommand) obj2).Execute(notification);
            }
        }

        public bool HasCommand(string notificationName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_commandMap.ContainsKey(notificationName);
            }
        }
        /// <summary>
        /// 当调用方法会优先判断是否存在 如果没有就 调用V C层创建一个观察者信息 Observer 
        /// </summary>
        /// <param name="notificationName"></param>
        /// <param name="commandType"></param>
        public void RegisterCommand(string notificationName, Type commandType)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_commandMap.ContainsKey(notificationName))
                {
                    this.m_view.RegisterObserer(notificationName,new Observer("executeCommand",this));
                }

                this.m_commandMap[notificationName] = commandType;
            }
        }

        public void RemoveCommand(string notificationName)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_commandMap.ContainsKey(notificationName))
                {
                    this.m_view.RemoveObserver(notificationName,this);
                    this.m_commandMap.Remove(notificationName);
                }
            }
        }
    }

using System;
using System.Reflection;

namespace script.CLass
{
    public class Observer : IObserver
    {
        private object m_notifyContext;
        private string m_notifyMethod;
        protected  readonly  object m_syncRoot=new  object();

        public Observer(string notifyMethod, object notifyContext)
        {
            this.m_notifyMethod = notifyMethod;
            this.m_notifyContext = notifyContext;
        }
        
        public bool CompareNotifyContext(object obj)
        {
            lock (this.m_syncRoot)
            {
                return this.NotifyContext.Equals(obj);
            }
        }

        public void NotifyObserver(INotification notification)
        {
            object notifyConText;
            lock (this.m_syncRoot)
            {
                notifyConText = this.NotifyContext;
            }
            Type type = notifyConText.GetType();
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            MethodInfo methodInfo = type.GetMethod(this.NotifyMethod,bindingFlags);
            methodInfo.Invoke(notifyConText, new object[] {notification});
        }

        public object NotifyContext {
            private get
            {
                return this.m_notifyContext;
            }
            set
            {
                this.m_notifyContext = value;
            }
        }
        public string NotifyMethod {
           private get
           {
               return this.m_notifyMethod;
           }
            set
            {
                this.m_notifyMethod = value;
            }
        }
    }
}
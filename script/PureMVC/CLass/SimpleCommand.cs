using System;
using System.Windows.Input;

public class SimpleCommand : Notifier,ICommand,INotifier
{
    public virtual void Execute(INotification parameter)
    {
        
    }

  
}
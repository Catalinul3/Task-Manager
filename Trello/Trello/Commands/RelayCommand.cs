using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Trello.Commands
{ class RelayCommands : ICommand
{
    private Action commandTask;
    private Predicate<object> canExecuteTask;

    public RelayCommands(Action workToDo)
        : this(workToDo, DefaultCanExecute)
    {
        commandTask = workToDo;
    }

    public RelayCommands(Action workToDo, Predicate<object> canExecute)
    {
        commandTask = workToDo;
        canExecuteTask = canExecute;
    }

    private static bool DefaultCanExecute(object parameter)
    {
        return true;
    }

    public bool CanExecute(object parameter)
    {
        return canExecuteTask != null && canExecuteTask(parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add
        {
            CommandManager.RequerySuggested += value;
        }

        remove
        {
            CommandManager.RequerySuggested -= value;
        }
    }
    public void Execute(object parameter)
    {
        commandTask();
    }
}
}

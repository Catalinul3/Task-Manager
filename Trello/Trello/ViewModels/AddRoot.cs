using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trello.Views;

namespace Trello.ViewModels
{
    public class AddRootCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            AddRoot addRootWindow=new AddRoot();
            addRootWindow.ShowDialog();
            
        }
    }
}

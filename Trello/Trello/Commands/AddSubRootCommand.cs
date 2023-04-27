using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Trello.Views;

namespace Trello.Commands
{
    public class AddSubRootCommand:ICommand
    {
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public void Execute(object parameter)
        {
            AddSubRoots addSubRootWindow = new AddSubRoots();
            addSubRootWindow.ShowDialog();

        }
    }
}

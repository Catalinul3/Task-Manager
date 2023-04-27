using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Trello.TDL;

namespace Trello.Commands
{
    public class SelectedItem : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {ListViewItem list=parameter as ListViewItem;
            if (list != null)
            {
                list.Background = Brushes.Green;
            }
        }
    }
}

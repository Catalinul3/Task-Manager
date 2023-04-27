using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Trello.Enums;
using Trello.Models;
using Trello.ViewModels;

namespace Trello.Views
{
    /// <summary>
    /// Interaction logic for EditTask.xaml
    /// </summary>
    public partial class EditTask : Window
    {
        public EditTask(string name, EnumPriority priority, EnumCategory category, DateTime data, string description,bool done)
        {
            InitializeComponent();
            DataContext = new EditTaskView(name, priority,category,data,description,done);
        }
    }
}

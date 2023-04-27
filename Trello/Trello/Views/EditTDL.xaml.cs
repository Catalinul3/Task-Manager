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
using Trello.ViewModels;

namespace Trello.Views
{
    /// <summary>
    /// Interaction logic for EditTDL.xaml
    /// </summary>
    public partial class EditTDL : Window
    {
        public EditTDL(string selectedItem,string imgURL)
        {
            InitializeComponent();
            DataContext = new EditTDLView(selectedItem,imgURL);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Trello.Models;
using Trello.Task;

namespace Trello.TDL
{
    public class TDLView:INotifyPropertyChanged
    {
        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value; OnPropertyChanged("name");
                }
            }
        }
        private string _imageURL;
        public string imageURL
        {
            get { return _imageURL; }
            set { _imageURL = value; OnPropertyChanged("imageURL"); }
        }
        public int size { get; set; }
        private ObservableCollection<TasksView> _tasks;
        public ObservableCollection<TasksView> tasks
        {
            get { return _tasks; }
            set
            {
                if (_tasks != value)
                {
                    _tasks = value;
                    OnPropertyChanged("tasks");
                }
            }
        }
        public ObservableCollection<TDLView> tDLs { get; set; }
        
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged("IsSelected");
                }
            }
        }
        public TDLView()
        {
            tDLs = new ObservableCollection<TDLView>();
            

        }
        public TDLView(string name,ObservableCollection<TasksView> tasks)
        {
            this.name = name;
            this.tasks = tasks;
           
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}

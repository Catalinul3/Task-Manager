using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;
using Trello.Commands;
using Trello.Enums;
using Trello.Models;

namespace Trello.ViewModels
{
    public  class EditTaskView:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _taskName { get; set; }
        private string _rootName { get; set; }
        private string _description { get; set; }
        private ObservableCollection<EnumPriority> _priorities { get; set; }
        private string _newTask;
        public string newTask
        {
            get { return _newTask; }
            set { _newTask = value; OnPropertyChanged("name"); }
        }
        private string _newDescriptiom;
        public string newDescriptiom
        {
            get { return _newDescriptiom; }
            set { _newDescriptiom = value;OnPropertyChanged("Description"); }
        }
        private EnumPriority _newPriority;
        public EnumPriority newPriority
        {
            get { return _newPriority; }
            set { _newPriority = value;OnPropertyChanged("Priority"); }
        }
        private EnumCategory _newCategory;
        public EnumCategory newCategory
        {
            get { return _newCategory; }
            set { _newCategory = value;OnPropertyChanged("category"); }
        }
        private DateTime _time;
        public DateTime Time
        {
            get
            {
                return _time;
            }
            set
            {
                _time = value;
                OnPropertyChanged("time");
            }
        }


        private EnumPriority Prority { get; set; }
        public EnumPriority priority
        {
            get { return Prority; }
            set
            {
                Prority = value;
                OnPropertyChanged("Priority");
            }
        }
        private EnumCategory Category { get; set; }
        public EnumCategory category
        {
            get
            {
                return Category;
            }
            set
            {
                Category = value;
                OnPropertyChanged("Category");
            }
        }
        private ObservableCollection<EnumCategory> _status { get; set; }
        private DateTime _deadline { get; set; }
        public string taskName
        {
            get { return _taskName; }
            set
            {
                _taskName = value;
                OnPropertyChanged("taskName");
            }
        }
        public string rootName
        {
            get { return _rootName; }
            set
            {
                _rootName = value;
                OnPropertyChanged("rootName");
            }
        }
        public string description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public ObservableCollection<EnumCategory> categories
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
                OnPropertyChanged("categories");
            }
        }
        public ObservableCollection<EnumPriority> priorities
        {
            get
            {
                return _priorities;
            }
            set
            {
                _priorities = value;
                OnPropertyChanged("priorities");
            }

        }
        public DateTime deadline
        {
            get
            {
                return _deadline;
            }
            set
            {
                _deadline = value;
                OnPropertyChanged("deadline");
            }
        }

        private ICommand _enter;
        private bool canExecuteCommand = true;
        public bool CanExecuteCommand
        {
            get
            {
                return canExecuteCommand;
            }
            set
            {
                if (canExecuteCommand == value)
                {
                    return;
                }
                canExecuteCommand = value;
            }
        }
        public bool done { get; set; }

        public ICommand enter
        {
            get
            {
                if (_enter == null)
                {
                    _enter = new RelayCommands(editTaskCommand, param => CanExecuteCommand);
                }
                return _enter;
            }
        }
        public void editTaskCommand()
        {
            editTask(taskName, priority, category, deadline, description,done);
            MessageBox.Show("Edit task succesfull");

        }
        public void editTask(string selectedName,EnumPriority selectedPriority,EnumCategory selectedCategory,DateTime date,string descriptionEdit,bool done ) {
            ObservableCollection<string> list = new ObservableCollection<string>();
            string newTaskName = taskName;
            EnumPriority newTPriority=priority;
            EnumCategory newTCategory = category;
            DateTime newTDate = deadline;
            string newTDescription = description;
            selectedName = newTask;
            
           
            using(StreamReader sr=new StreamReader("Task.txt"))
            {
                string linie;
                while((linie=sr.ReadLine())!=null)
                {
                    list.Add(linie);
                }
            }
            int remind = 0;
            string data = oldDeadLine.ToString();
           for(int i=0;i<list.Count;i++)
            {
                if (list[i].Contains(newTask) && list[i].Contains(priorityToString(newPriority)) && list[i].Contains(categoryToString(newCategory)) && list[i].Contains(data.Substring(0, data.Length - 11)) && list[i].Contains(newDescriptiom))
                {
                    remind = i; break;
                }
            }
            string deadl = newTDate.ToString();
            list[remind] = newTaskName + ' ' + priorityToString(newTPriority) + ' ' + categoryToString(newTCategory) + ' ' + deadl.Substring(0, deadl.Length - 11) +"false "+ ',' + newTDescription;
            File.WriteAllLines("Task.txt", list);
        }
        public EditTaskView()
        {
            priorities = new ObservableCollection<EnumPriority>();
            categories = new ObservableCollection<EnumCategory>();
            foreach (EnumPriority items in Enum.GetValues(typeof(EnumPriority)))
            {
                priorities.Add(items);
            }
            foreach (EnumCategory item in Enum.GetValues(typeof(EnumCategory)))
            {
                categories.Add(item);
            }
        }
        private DateTime _oldDeadline;
        public DateTime oldDeadLine
        {
            get { return _oldDeadline; }
            set { _oldDeadline = value; }

        }
        public bool stringToBoolean(string boolean)
        {
            if (boolean.Equals("true"))
            {
                return true;
            }
            if (boolean.Equals("false"))
            {
                return false;
            }
            return false;
        }
        public string boolToString(bool boolean)
        {
            if (boolean == true)
            {
                return "true";
            }
            if (boolean == false)
            {
                return "false";
            }
            return "false";
        }
        public EditTaskView(string name, EnumPriority selectedPriority, EnumCategory selectedCategory, DateTime date, string descriptionEdit, bool isDone)
        {taskName= name;
            newTask = taskName;
            priority = selectedPriority;
            newPriority = priority;
            category = selectedCategory;
            newCategory= category;
            deadline= date;
            oldDeadLine = deadline;
            description = descriptionEdit;
            newDescriptiom= descriptionEdit;
            done = isDone;
            priorities = new ObservableCollection<EnumPriority>();
            categories = new ObservableCollection<EnumCategory>();
            foreach (EnumPriority items in Enum.GetValues(typeof(EnumPriority)))
            {
                priorities.Add(items);
            }
            foreach (EnumCategory item in Enum.GetValues(typeof(EnumCategory)))
            {
                categories.Add(item);
            }
        }
        public string priorityToString(EnumPriority priority)
        {
            if (priority == EnumPriority.LOW)
            {
                return "Low";
            }
            if (priority == EnumPriority.HIGH)
            {
                return "High";
            }
            if (priority == EnumPriority.MEDIUM)
            {
                return "Medium";
            }
            if (priority == EnumPriority.NONE)
            {
                return "None";
            }
            return "None";
        }
        public string categoryToString(EnumCategory category)
        {
            if (category == EnumCategory.MAJOR_TASK)
            {
                return "MajorTask";
            }
            if (category == EnumCategory.MINOR_TASK)
            {
                return "MinorTask";
            }
            if (category == EnumCategory.NOT_IMPORTANT)
            {
                return "NotImportant";
            }
            return "None";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using Trello.Commands;
using Trello.Enums;
using Trello.Models;
using Trello.Task;

namespace Trello.ViewModels
{
    public class AddTaskView:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _taskName{get;set; }
        private string _rootName { get; set; }
        private string _description { get; set; }
        private ObservableCollection<EnumPriority> _priorities { get; set; }
        private EnumPriority Prority { get; set; }
        public EnumPriority priority
        {
            get { return Prority; }
            set { Prority = value;
                           OnPropertyChanged("Priority"); }
        }
        private EnumCategory Category { get; set; }
        public EnumCategory category
        {
            get
            {
                return Category;
            }
            set { Category = value;
                OnPropertyChanged("Category");
            }
        }
        private ObservableCollection<EnumCategory> _status { get; set; }
        private DateTime _deadline { get; set; }
        public string taskName
        {
            get { return _taskName; }
            set { _taskName = value;
                OnPropertyChanged("taskName");
            }
        }
        public string rootName
        {
            get { return _rootName; }
            set { _rootName = value;
                           OnPropertyChanged("rootName");
                       }
        }
        public string description
        {
            get { return _description; }
            set
            {
                _description= value;
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
            { _status = value;
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
       
        public ICommand enter
        {
            get
            {
                if (_enter == null)
                {
                    _enter = new RelayCommands(writeTask, param => CanExecuteCommand);
                }
                return _enter;
            }
        }
        public void writeTask()
        {
            ObservableCollection<string> list = new ObservableCollection<string>();
            bool exist = false;
            int index = 0;
            using (StreamReader sr = new StreamReader("Task.txt", true))
            {
                string line;


                while ((line = sr.ReadLine()) != null)
                {
                    bool tdl = false;
                    string[] root = line.Split(':');
                    foreach (string item in root)
                    {
                        if (item == rootName)
                        {
                            exist = true;
                            break;
                        }


                    }


                }
            }
            if (!exist)
            {
                using (StreamWriter sw = new StreamWriter("Task.txt", true))
                {
                    sw.WriteLine(rootName + ':');
                }
                string[] fileLines = File.ReadAllLines("Task.txt");
                string deadl = deadline.ToString();
                string task = "";
                task = taskName + ' ' + priorityToString(priority) + ' ' + categoryToString(category) + ' ' + deadl.Substring(0, deadl.Length - 11) + "false "+ ',' + description;
                bool deja = false;
                bool primul = false;
                bool ultimul = false;
                foreach (string content in fileLines)
                {
                    list.Add(content);
                    if (fileLines[0].Contains(rootName)&&!primul)
                    {

                        list.Add(task);
                        primul = true;
                    }
                   

                    string delimitare = fileLines[index];
                    if (rootName.Equals(delimitare) && !deja)
                    {

                        list.Add(task);

                        deja = true;

                    }

                    index++;



                }
                if (fileLines[fileLines.Count() - 1].Contains(rootName) && !ultimul)
                {
                    list.Add(task);
                    ultimul = true;
                }
            }
            else
            {
                string[] fileLines = File.ReadAllLines("Task.txt");
                rootName += ':';
                if (!fileLines.Contains(rootName))

                {
                    File.WriteAllText("Task.txt", rootName);
                }
                string deadl = deadline.ToString();
                string task = "";
                task = taskName + ' ' + priorityToString(priority) + ' ' + categoryToString(category) + ' ' + deadl.Substring(0,deadl.Length-11) +"false "+ ',' + description;
                bool deja=false;
                bool primul = false;
                foreach (string content in fileLines)
                {list.Add(content);
                    //if (fileLines[0].Contains(rootName)&&!primul)
                    //{
                      
                    //    list.Add(task);
                    //    primul = true;
                    //}

                    string delimitare = fileLines[index];
                    if (rootName.Equals(delimitare) && !deja)
                    {
                        
                        list.Add(task);
                        
                        deja = true;

                    }
                   
                        index++;
                    
                  
                    
                }
            }
            File.WriteAllLines("Task.txt", list);
            OnPropertyChanged("tasks");
        }
                
            
        

        public AddTaskView(string addTask)
        {
            priorities=new ObservableCollection<EnumPriority>();
            categories=new ObservableCollection<EnumCategory>();
            rootName = addTask;
          foreach(EnumPriority items in Enum.GetValues(typeof(EnumPriority)))
            {
                priorities.Add(items);
            }
          foreach(EnumCategory item in Enum.GetValues(typeof(EnumCategory)))
            {
                categories.Add(item);
            }
        }
        public AddTaskView()
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

        public string priorityToString(EnumPriority priority)
        {
            if(priority==EnumPriority.LOW)
            {
                return "Low";
            }
            if(priority== EnumPriority.HIGH)
            {
                return "High";
            }
            if(priority==EnumPriority.MEDIUM)
            {
                return "Medium";
            }
            if(priority==EnumPriority.NONE) {
                return "None";
            }
            return "None";
        }
        public string categoryToString(EnumCategory category)
        {
            if(category==EnumCategory.MAJOR_TASK)
            {
                return "MajorTask";
            }
            if(category== EnumCategory.MINOR_TASK)
            {
                return "MinorTask";
            }
            if(category==EnumCategory.NOT_IMPORTANT) {
                return "NotImportant";
            }
            return "None";
        }
    }
}

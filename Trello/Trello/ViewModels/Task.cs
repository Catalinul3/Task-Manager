using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trello.Enums;
using Trello.Models;

namespace Trello.Task
{
    public class TasksView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _name;
        public string name { get { return _name; } set { _name = value; OnPropertyChanged("name"); } }
        public string rootName { get; set; }
        private string _description;
        public string description { get { return _description; } set { _description = value; OnPropertyChanged("description"); } }
        private EnumCategory _status;
        public EnumCategory status { get { return _status; } set { _status = value; OnPropertyChanged("status"); } }
        private EnumPriority _priority;
        public EnumPriority priority { get { return _priority; } set { _priority = value; OnPropertyChanged("priority"); } }
        private DateTime _deadline;
        public DateTime deadline { get { return _deadline; } set { _deadline = value; OnPropertyChanged("deadline"); } }
        private bool _isDone;
        public bool isDone
        {
            get { return _isDone; }
            set
            {
                _isDone = value;
                OnPropertyChanged("isDone");
            }
        }

        private ObservableCollection<TasksView> _tasks;
        public ObservableCollection<TasksView> tasks
        {
            get
            {
                return _tasks;
            }
            set
            {
                _tasks = value;
                OnPropertyChanged("tasks");
            }
        }
        public TasksView()
        {
            tasks = new ObservableCollection<TasksView>();


        }

        public string readRoot()
        {

            tasks = new ObservableCollection<TasksView>();
            using (StreamReader sr = new StreamReader("Task.txt"))
            {
                string linie;
                while ((linie = sr.ReadLine()) != null)
                {
                    string[] s = linie.Split(' ');

                    foreach (string task in s)
                    {
                        if (task.Contains(':'))
                        {
                            rootName = task;
                        }

                    }
                }
            }
            return rootName;
        }
        public ObservableCollection<TasksView> readFile(string rootName)
        {
            tasks = new ObservableCollection<TasksView>();

            using (StreamReader sr = new StreamReader("Task.txt"))
            {
                string linie;
                string rootfile = "";
                while ((linie = sr.ReadLine()) != null)
                {
                    string descriere = "";
                    string[] s = linie.Split(' ');

                    foreach (string task in s)
                    {
                        if (task.Contains(":"))
                        {
                            rootfile = task.Substring(0, task.Length - 1);
                        }


                        if (!linie.Contains(':') && rootfile.Equals(rootName))
                        {
                            int i = linie.IndexOf(',');
                            tasks.Add(new TasksView
                            {
                                name = s[0],
                                status = toCategory(s[2]),
                                priority = toPriority(s[1]),
                                deadline = DateTime.Parse(s[3]),
                                isDone = bool.Parse(s[4]),
                                description = i >= 0 ? linie.Substring(i + 1) : ""


                            });
                            descriere = string.Empty;
                            break;


                        }


                    }
                }


            }


            return tasks;
        }
        public ObservableCollection<TasksView> sortByDeadline(ObservableCollection<TasksView> sort)
        {
            sort = new ObservableCollection<TasksView>(tasks.OrderBy(x => x.deadline));
            return sort;
        }
        public ObservableCollection<TasksView> sortByPriorities(ObservableCollection<TasksView> sort)
        {
            sort = new ObservableCollection<TasksView>(tasks.OrderByDescending(x => x.priority));
            return sort;
        }
        public ObservableCollection<TasksView> filterByDoneView(ObservableCollection<TasksView> filter)
        {
            ObservableCollection<TasksView> filterbyDone = new ObservableCollection<TasksView>();

            foreach (TasksView task in filter)
            {
                if (task.isDone.Equals(stringToBoolean("true")))
                {
                    filterbyDone.Add(task);
                }
            }
            return filterbyDone;
        }
        public ObservableCollection<TasksView> filterByUnDoneView(ObservableCollection<TasksView> filter)
        {
            ObservableCollection<TasksView> filterbyUnDone = new ObservableCollection<TasksView>();

            foreach (TasksView task in filter)
            {
                if (task.isDone.Equals(stringToBoolean("false")))
                {
                    filterbyUnDone.Add(task);
                }
            }
            return filterbyUnDone;
        }
        public ObservableCollection<TasksView> filterByUnDoneAndLate(ObservableCollection<TasksView> filter)
        {
            ObservableCollection<TasksView> filterbyUnDone = new ObservableCollection<TasksView>();
            DateTime dateTime = DateTime.Now;

            foreach (TasksView task in filter)
            {
                if (task.isDone.Equals(stringToBoolean("false")) && task.deadline < dateTime)
                {
                    filterbyUnDone.Add(task);
                }
            }
            return filterbyUnDone;
        }
        public ObservableCollection<TasksView> filterByUnDoneButNearlyDeadline(ObservableCollection<TasksView> filter)
        {
            ObservableCollection<TasksView> filterbyUnDone = new ObservableCollection<TasksView>();
            DateTime dateTime = DateTime.Now;

            foreach (TasksView task in filter)
            {
                if (task.isDone.Equals(stringToBoolean("false")) && task.deadline > dateTime)
                {
                    TimeSpan time = task.deadline - dateTime;
                    if (time.TotalDays < 5)
                    {
                        filterbyUnDone.Add(task);
                    }
                }
            }
            return filterbyUnDone;
        }
        public ObservableCollection<TasksView> filterByUnDoneButFutureDeadline(ObservableCollection<TasksView> filter)
        {
            ObservableCollection<TasksView> filterbyUnDone = new ObservableCollection<TasksView>();
            DateTime dateTime = DateTime.Now;

            foreach (TasksView task in filter)
            {
                if (task.isDone.Equals(stringToBoolean("false")) && task.deadline > dateTime)
                {
                    TimeSpan time = task.deadline - dateTime;
                    if (time.TotalDays > 31)
                    {
                        filterbyUnDone.Add(task);
                    }
                }
            }
            return filterbyUnDone;
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

        public EnumCategory toCategory(string category)
        {
            if (category.Equals("MajorTask"))
            {
                return EnumCategory.MAJOR_TASK;
            }
            if (category.Equals("MinorTask"))
            {
                return EnumCategory.MINOR_TASK;
            }
            if (category.Equals("NotImportant"))
            {
                return EnumCategory.NOT_IMPORTANT;
            }
            return EnumCategory.NONE;
        }
        public EnumStatus toStatus(string status)
        {
            if (status.Equals("Created"))
            {
                return EnumStatus.CREATED;
            }
            if (status.Equals("InProgress"))
            {
                return EnumStatus.IN_PROGRESS;
            }
            if (status.Equals("Done"))
            {
                return EnumStatus.DONE;
            }
            else
                return EnumStatus.NONE;
        }
        public EnumPriority toPriority(string priority)
        {
            if (priority.Equals("Low"))
            {
                return EnumPriority.LOW;
            }
            if (priority.Equals("Medium"))
            {
                return EnumPriority.MEDIUM;
            }
            if (priority.Equals("High"))
            {
                return EnumPriority.HIGH;
            }
            else
                return EnumPriority.NONE;
        }

    }
}

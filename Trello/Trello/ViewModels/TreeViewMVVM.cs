using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Security.Policy;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml.Linq;
using Trello.Commands;
using Trello.Enums;
using Trello.Models;
using Trello.Task;
using Trello.ViewModels;
using Trello.Views;
namespace Trello.TDL
{
    public class TreeViewMVVM : INotifyPropertyChanged
    {
        public ObservableCollection<TDLView> itemsCollection { get; set; }
        private ObservableCollection<TasksView> tasks { get; set; }
        public ObservableCollection<TasksView> Tasks
        {
            get { return tasks; }
            set
            {
                tasks = value;
                OnPropertyChanged("tasks");
            }
        }
        private TDLView selectedTDL { get; set; }
        public int size { get; set; }
        public string rootName { get; set; }
        public string firstBlock { get; set; }
        public string description { get; set; }
        public string selected { get; set; }
        public ICommand addRoot { get; set; }
        public ICommand addsubRoot { get; set; }
        private ICommand _addTask { get; set; }
        private ICommand _delete { get; set; }
        private ICommand _edit { get; set; }
        private ICommand _moveUP { get; set; }
        private ICommand _moveDOWN { get; set; }
        private ICommand _editTask { get; set; }
        private ICommand _deleteTask { get; set; }
        private ICommand _setDone { get; set; }
        private ICommand _moveUPTask { get; set; }
        private ICommand _moveDOWNTask { get; set; }
        private ICommand _sortByDeadline;
        private ICommand _sortByPriority;
        private ICommand _filterByDone;
        private ICommand _filterByNotDone;
        private ICommand _filterByNotDoneAndLate;
        private ICommand _filterByNotDoneButNearlyDeadline;
        private ICommand _filterByNotDoneButFutureDeadline;
        private int _sizeTaskDone;
        private int _sizeTaskUnDone;
        private int _sizeDeadlineTodayTask;
        private int _sizeDeadLineTomorrowTask;
        private int _sizeOverueTask;
        public int sizeOverdueTask
        {
            get { return _sizeOverueTask; }
            set
            {
                _sizeOverueTask = value;
                OnPropertyChanged("sizeOverdueTask");
            }
        }
        private TasksView _selectedTask { get; set; }
        public TasksView selectedTask
        {
            get
            {
                return _selectedTask;
            }
            set
            {
                _selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }
        public TDLView SelectedTDL
        {
            get { return selectedTDL; }
            set
            {
                if (selectedTDL != value)
                {
                    if (selectedTDL != null)
                    {
                        selectedTDL.IsSelected = false;
                    }
                    selectedTDL = value;
                    if (selectedTDL != null)
                    {
                        selectedTDL.IsSelected = true;
                    }
                    OnPropertyChanged("SelectedTDL");
                }
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TreeViewMVVM()
        {
            itemsCollection = new ObservableCollection<TDLView>();
            ObservableCollection<TDLView> allTDL = new ObservableCollection<TDLView>();
            int rootSize = readTitle().Count;
            List<String> roots = readTitle();
            tasks = new ObservableCollection<TasksView>();
            ObservableCollection<TasksView> tasksViews = new ObservableCollection<TasksView>();
            TasksView view = new TasksView();
            ObservableCollection<string> images = new ObservableCollection<string>();
            images = readImage();
            rootName = view.readRoot();
            addRoot = new AddRootCommand();
            addsubRoot = new AddSubRootCommand();
            sizeDoneTask();
            sizeDoneToday();
            sizeTaskDoneTillTomorrow();
            sizeUnDoneTask();
            sizeOverdueTaskView();
            for (int i = 0; i < rootSize; i++)
            {
                int subSize = readSub(readTitle()[i]).Count;
                ObservableCollection<TDLView> items = new ObservableCollection<TDLView>();
                for (int j = 0; j < subSize; j++)
                {
                    items.Add(new TDLView
                    {
                        name = readSub(readTitle()[i])[j],
                        imageURL = readSubRootImage(readSub(readTitle()[i])[j], j),
                        tDLs = new ObservableCollection<TDLView>(),
                        tasks = view.readFile(readSub(readTitle()[i])[j]),
                        size = view.readFile(readSub(readTitle()[i])[j]).Count
                    });
                    allTDL.Add(new TDLView
                    {
                        name = readSub(readTitle()[i])[j],
                        imageURL = images[10],
                        tDLs = new ObservableCollection<TDLView>()
                    });
                }
                itemsCollection.Add(new TDLView
                {
                    name = readTitle()[i],
                    imageURL = readRootImage(readTitle()[i]),
                    tDLs = items,
                    tasks = view.readFile(readTitle()[i]),
                    size = view.readFile(readTitle()[i]).Count
                });
                allTDL.Add(new TDLView
                {
                    name = readTitle()[i],
                    imageURL = "Resources/folder_document.png",
                    tDLs = items
                });
            }
        }
        public ObservableCollection<string> readImage()
        {
            ObservableCollection<string> imgs = new ObservableCollection<string>();
            string based = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(based, "resources");
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                imgs.Add(file);
            }
            return imgs;
        }
        public string readRootImage(string root)
        {
            string img = "";
            string r = "";
            using (StreamReader sr = new StreamReader("ToDoList.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split(',');
                    string[] word = line.Split('=');
                    foreach (string w in word)
                    {
                        r = w;
                        break;
                    }
                    foreach (string w in words)
                    {
                        string aux = r;
                        if (r.Equals(root))
                        {
                            aux += '=';
                            if (!w.Contains('=') && !w.Contains('('))
                            {
                                img = w;
                                break;
                            }
                        }
                    }

                }
            }
            return img;
        }
        public string readSubRootImage(string subRoot, int index)
        {
            string img = "";
            string sR = "";
            string r = "";
            using (StreamReader sr = new StreamReader("ToDoList.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    ObservableCollection<string> subRoots = new ObservableCollection<string>();
                    string[] words = line.Split(',');
                    string[] word = line.Split('|');
                    string[] st;
                    if (line.Contains('('))
                    {
                        string[] wd = line.Split('(');
                        for (int i = 0; i < wd.Length; i++)
                        {
                            st = wd[i].Split(',');
                            if (!st[0].Contains('='))
                            {
                                subRoots.Add(st[0]);
                            }
                        }
                    }
                    foreach (string w in words)
                    {
                        sR = w;
                        r = sR.Substring(1);
                        if (r.Equals(subRoot))
                        {
                            break;
                        }
                    }
                    string ajutor = "";
                    foreach (string w in words)
                    {
                        string aux = r;
                        if (r.Equals(subRoot))
                        {
                            if (!w.Contains('=') && !w.Contains('(') & w.Contains('|') && (subRoots[index].Equals(ajutor)))
                            {
                                string s = w.Substring(1);
                                img = s;
                                break;
                            }
                        }
                        ajutor = w.Substring(1);
                    }
                }
            }
            return img;
        }
        public List<String> readTitle()
        {
            List<String> titles = new List<string>();
            using (StreamReader sr = new StreamReader("ToDoList.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split('=');
                    foreach (string word in words)
                    {
                        if (!word.Contains(" ") && (!word.Equals("")))
                        {
                            titles.Add(word);
                        }
                    }
                }
            }
            return titles;
        }
        public List<String> readSub(string roots)
        {
            List<String> title = new List<string>();
            string root = "";
            using (StreamReader sr = new StreamReader("ToDoList.txt"))
            {
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    string[] words = line.Split('=');
                    string[] words1 = line.Split(',');
                    foreach (string w in words)
                    {
                        root = w;
                        break;
                    }
                    foreach (string word in words1)
                    {
                        string aux = root;
                        if (aux.Equals(roots))
                        {
                            aux += "=";
                            if (!word.Contains(aux) && !word.Contains(':'))
                            {
                                title.Add(word.Substring(1));
                            }
                        }
                    }
                }
            }
            return title;
        }
        protected bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }
        private object selectedItem;
        public object SelectedItem { get => selectedItem; set => SetProperty(ref selectedItem, value); }
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
        public ICommand edit
        {
            get
            {
                if (_edit == null)
                {
                    _edit = new RelayCommands(editTDL, param => CanExecuteCommand);
                }
                return _edit;
            }
        }
        public void editTDL()
        {
            if (SelectedItem != null)
            {
                TDLView t = (TDLView)SelectedItem;
                if (t.name != null)
                {
                    EditTDL edit = new EditTDL(t.name, t.imageURL);
                    edit.ShowDialog();
                }
            }
        }
        public ICommand delete
        {
            get
            {
                if (_delete == null)
                {
                    _delete = new RelayCommands(deleteTDL, param => CanExecuteCommand);
                }
                return _delete;
            }
        }
        public void deleteTDL()
        {
            if (SelectedItem != null)
            {
                bool isRoot = false;
                bool isSubRoot = false;
                TDLView t = (TDLView)SelectedItem;
                if (t.name != null)
                {
                    string[] fileContent = File.ReadAllLines("ToDoList.txt");
                    string fileText = File.ReadAllText("ToDoList.txt");
                    List<string> list = new List<string>(fileContent);
                    for (int i = 0; i < list.Count; i++)
                    {
                        string root = t.name;
                        root += '=';
                        if (list[i].Contains(root))
                        {
                            isRoot = true;
                            list.RemoveAt(i);
                            break;
                        }
                        else
                        {
                            string subRoot = "";
                            subRoot += ',';
                            subRoot += '(';
                            subRoot += t.name;
                            subRoot += ',';
                            subRoot += '|';
                            string subRootImg = t.imageURL;
                            string subRootdelete = subRoot + subRootImg;
                            if (list[i].Contains(subRootdelete))
                            {
                                isSubRoot = true;
                                fileText = fileText.Replace(subRootdelete, "");
                                break;
                            }

                        }
                    }
                    if (isRoot)
                    {
                        File.WriteAllLines("ToDoList.txt", list.ToArray());
                        
                    }
                    else if (isSubRoot)
                    {
                        File.WriteAllText("ToDoList.txt", fileText);
                    }

                }
            }
        }
        public ICommand moveUP
        {
            get
            {
                if (_moveUP == null)
                {
                    _moveUP = new RelayCommands(moveUPTDL, param => CanExecuteCommand);
                }
                return _moveUP;
            }
        }
        public void moveUPTDL()
        {
            if (SelectedItem != null)
            {
                TDLView t = (TDLView)SelectedItem;
                if (t.name != null)
                {
                    string[] fileContent = File.ReadAllLines("ToDoList.txt");
                    string fileText = File.ReadAllText("ToDoList.txt");
                    List<string> list = new List<string>(fileContent);
                    for (int i = 0; i < list.Count; i++)
                    {
                        string root = t.name;
                        root += '=';
                        if (list[i].Contains(root))
                        {
                            if (i != 0)
                            {
                                string aux = list[i];
                                list[i] = list[i - 1];
                                list[i - 1] = aux;
                                break;
                            }
                        }
                    }
                    File.WriteAllLines("ToDoList.txt", list.ToArray());
                }
            }
        }
        public ICommand moveDown
        {
            get
            {
                if (_moveDOWN == null)
                {
                    _moveDOWN = new RelayCommands(moveDownTDL, param => CanExecuteCommand);
                }
                return _moveDOWN;
            }
        }
        public ICommand addTask
        {
            get
            {
                if (_addTask == null)
                {
                    _addTask = new RelayCommands(addTaskTDL, param => CanExecuteCommand);

                }
                return _addTask;
            }
        }
        public void addTaskTDL()
        {
            if (SelectedItem != null)
            {
                TDLView t = (TDLView)SelectedItem;
                if (t.name != null)
                {
                    AddTask add = new AddTask(t.name);
                    add.ShowDialog();
                }
            }
        }
        public ICommand editTask
        {
            get

            {
                if (_editTask == null)
                {
                    _editTask = new RelayCommands(editTaskTDL, param => CanExecuteCommand);
                }
                return _editTask;
            }

        }
        public void editTaskTDL()
        {
            if (selectedTask.name != null)
            {
                EditTask editTask = new EditTask(selectedTask.name, selectedTask.priority, selectedTask.status, selectedTask.deadline, selectedTask.description,selectedTask.isDone);
                editTask.Show();
            }

        }
        public void moveDownTDL()
        {
            if (SelectedItem != null)
            {
                TDLView t = (TDLView)SelectedItem;
                if (t.name != null)
                {
                    bool isRoot = false;
                    bool isSubRoot = false;
                    string switchSubRoot;
                    string[] fileContent = File.ReadAllLines("ToDoList.txt");
                    string fileText = File.ReadAllText("ToDoList.txt");
                    List<string> list = new List<string>(fileContent);
                    for (int i = 0; i < list.Count; i++)
                    {
                        string root = t.name;
                        root += '=';
                        if (list[i].Contains(root))
                        {
                            if (i != list.Count - 1)
                            {
                                isRoot = true;
                                string aux = list[i];
                                list[i] = list[i + 1];
                                list[i + 1] = aux;
                                break;
                            }
                        }


                    }
                    if (isRoot)
                    {
                        File.WriteAllLines("ToDoList.txt", list.ToArray());
                    }

                }
            }

        }
        public ICommand deleteTask
        {
            get
            {
                if (_deleteTask == null)
                {
                    _deleteTask = new RelayCommands(deleteTaskTDL, param => CanExecuteCommand);
                }
                return _deleteTask;
            }
        }
        public void deleteTaskTDL()
        {
            if (selectedTask.name != null)
            {
                string[] fileContent = File.ReadAllLines("Task.txt");
                string fileText = File.ReadAllText("Task.txt");
                List<string> list = new List<string>(fileContent);
                string data = selectedTask.deadline.ToString();
                string task = selectedTask.name + ' ' + priorityToString(selectedTask.priority) + ' ' + categoryToString(selectedTask.status) + ' ' + data.Substring(0, data.Length - 11) + ',' + selectedTask.description;
                for (int i = 0; i < list.Count; i++)
                {


                    if (list[i].Equals(task))
                    {
                        list.RemoveAt(i);
                        break;
                    }
                }
                File.WriteAllLines("Task.txt", list);
                OnPropertyChanged("tasks");
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
        public ICommand setDone
        {
            get
            {
                if (_setDone == null)
                {
                    _setDone = new RelayCommands(setDoneTask, param => CanExecuteCommand);
                }
                return _setDone;
            }
        }
        public void setDoneTask()
        {
            if (selectedTask != null)
            {
                string[] fileContent = File.ReadAllLines("Task.txt");
                string data = selectedTask.deadline.ToString();
                string task = selectedTask.name + ' ' + priorityToString(selectedTask.priority) + ' ' + categoryToString(selectedTask.status) + ' ' + data.Substring(0, data.Length - 11) + convertBoolToString(selectedTask.isDone) + ' ' + ',' + selectedTask.description;
                selectedTask.isDone = true;

                int index = 0;
                for (int i = 0; i < fileContent.Length; i++)
                {
                    if (fileContent[i].Equals(task))
                    {
                        task = selectedTask.name + ' ' + priorityToString(selectedTask.priority) + ' ' + categoryToString(selectedTask.status) + ' ' + data.Substring(0, data.Length - 11) + convertBoolToString(selectedTask.isDone) + ' ' + ',' + selectedTask.description;
                        index = i; break;
                    }
                }
                fileContent[index] = task;
                File.WriteAllLines("Task.txt", fileContent);
                OnPropertyChanged("isDone");

            }
        }
        public string convertBoolToString(bool value)
        {
            if (value == true)
            {
                return "true";
            }
            else
                return "false";
        }
        public ICommand moveUPTask
        {
            get
            {
                if (_moveUPTask == null)
                {
                    _moveUPTask = new RelayCommands(moveUpTask, param => CanExecuteCommand);
                }
                return _moveUPTask;
            }
        }
        public void moveUpTask()
        {
            if (selectedTask != null)
            {
                string[] fileContent = File.ReadAllLines("Task.txt");
                string data = selectedTask.deadline.ToString();
                string task = selectedTask.name + ' ' + priorityToString(selectedTask.priority) + ' ' + categoryToString(selectedTask.status) + ' ' + data.Substring(0, data.Length - 11) + convertBoolToString(selectedTask.isDone) + ' ' + ',' + selectedTask.description;
                for (int i = 0; i < fileContent.Length; i++)
                {


                    if (!fileContent[i].Contains(':'))
                    {
                        if (i != 0)
                        {
                            if (!fileContent[i - 1].Contains(':') && fileContent[i].Equals(task))
                            {
                                string aux = fileContent[i];
                                fileContent[i] = fileContent[i - 1];
                                fileContent[i - 1] = aux;

                                break;
                            }
                        }
                    }
                }
                File.WriteAllLines("Task.txt", fileContent);
                OnPropertyChanged("tasks");

            }

        }
        public ICommand moveDownTask
        {
            get
            {
                if (_moveDOWNTask == null)
                {
                    _moveDOWNTask = new RelayCommands(moveDownTaskView, param => CanExecuteCommand);
                }
                return _moveDOWNTask;
            }
        }
        public void moveDownTaskView()
        {
            if (selectedTask != null)
            {
                string[] fileContent = File.ReadAllLines("Task.txt");
                string data = selectedTask.deadline.ToString();
                string task = selectedTask.name + ' ' + priorityToString(selectedTask.priority) + ' ' + categoryToString(selectedTask.status) + ' ' + data.Substring(0, data.Length - 11) + convertBoolToString(selectedTask.isDone) + ' ' + ',' + selectedTask.description;
                for (int i = 0; i < fileContent.Length; i++)
                {


                    if (!fileContent[i].Contains(':'))
                    {
                        if (i != 0)
                        {
                            if (!fileContent[i + 1].Contains(':') && fileContent[i].Equals(task))
                            {
                                string aux = fileContent[i];
                                fileContent[i] = fileContent[i + 1];
                                fileContent[i + 1] = aux;

                                break;
                            }
                        }
                    }
                }
                File.WriteAllLines("Task.txt", fileContent);
                OnPropertyChanged("tasks");

            }
        }
        public ICommand sortByDeadline
        {
            get
            {
                if (_sortByDeadline == null)
                {
                    _sortByDeadline = new RelayCommands(sortByDeadLineView, param => CanExecuteCommand);
                }
                return _sortByDeadline;
            }
        }
        public void sortByDeadLineView()
        {
            TasksView tasksView = new TasksView();
            TDLView view = (TDLView)SelectedItem;
            ObservableCollection<TasksView> f = tasksView.readFile(view.name);
            ObservableCollection<TasksView> t = tasksView.sortByDeadline(f);

            if (view != null)
            {
                view.tasks = t;
                OnPropertyChanged(view.tasks.ToString());
            }

        }
        public ICommand sortByPriority
        {
            get
            {
                if (_sortByPriority == null)
                {
                    _sortByPriority = new RelayCommands(sortByPriorityView, param => CanExecuteCommand);
                }
                return _sortByPriority;
            }
        }
        public void sortByPriorityView()
        {
            TasksView tasksView = new TasksView();
            TDLView view = (TDLView)SelectedItem;
            ObservableCollection<TasksView> f = tasksView.readFile(view.name);
            ObservableCollection<TasksView> t = tasksView.sortByPriorities(f);
            if (view != null)
            {
                view.tasks = t;
                OnPropertyChanged(view.tasks.ToString());
            }

        }
        public ICommand filterByDone
        {
            get
            {
                if (_filterByDone == null)
                {
                    _filterByDone = new RelayCommands(filterByDoneView, param => CanExecuteCommand);
                }
                return _filterByDone;
            }

        }
        public void filterByDoneView()
        {
            TasksView tasksView = new TasksView();
            TDLView view = (TDLView)SelectedItem;
            ObservableCollection<TasksView> f = tasksView.readFile(view.name);
            ObservableCollection<TasksView> t = tasksView.filterByDoneView(f);
            if (view != null)
            {
                view.tasks = t;
                OnPropertyChanged(view.tasks.ToString());
            }
        }
        public ICommand filterByUndone
        {
            get
            {
                if (_filterByNotDone == null)
                {
                    _filterByNotDone = new RelayCommands(filterByUnDone, param => CanExecuteCommand);
                }
                return _filterByNotDone;
            }
        }
        public void filterByUnDone()
        {
            TasksView tasksView = new TasksView();
            TDLView view = (TDLView)SelectedItem;
            ObservableCollection<TasksView> f = tasksView.readFile(view.name);
            ObservableCollection<TasksView> t = tasksView.filterByUnDoneView(f);
            if (view != null)
            {
                view.tasks = t;
                OnPropertyChanged(view.tasks.ToString());
            }
        }
        public ICommand filterByUnDoneAndLate
        {
            get
            {
                if (_filterByNotDoneAndLate == null)
                {
                    _filterByNotDoneAndLate = new RelayCommands(filterByUnDoneAndLateView, param => CanExecuteCommand);
                }
                return _filterByNotDoneAndLate;
            }
        }
        public void filterByUnDoneAndLateView()
        {
            TasksView tasksView = new TasksView();
            TDLView view = (TDLView)SelectedItem;
            ObservableCollection<TasksView> f = tasksView.readFile(view.name);
            ObservableCollection<TasksView> t = tasksView.filterByUnDoneAndLate(f);
            if (view != null)
            {
                view.tasks = t;
                OnPropertyChanged(view.tasks.ToString());
            }
        }
        public ICommand filterByUnDoneButNearlyDeadline
        {
            get
            {
                if (_filterByNotDoneButNearlyDeadline == null)
                {
                    _filterByNotDoneButNearlyDeadline = new RelayCommands(filterByUnDoneButNearlyDeadlineView, param => CanExecuteCommand);
                }
                return _filterByNotDoneButNearlyDeadline;
            }
        }
        public void filterByUnDoneButNearlyDeadlineView()
        {
            TasksView tasksView = new TasksView();
            TDLView view = (TDLView)SelectedItem;
            ObservableCollection<TasksView> f = tasksView.readFile(view.name);
            ObservableCollection<TasksView> t = tasksView.filterByUnDoneButNearlyDeadline(f);
            if (view != null)
            {
                view.tasks = t;
                OnPropertyChanged(view.tasks.ToString());
            }
        }
        public ICommand filterByUnDoneButFutureDeadline
        {
            get
            {
                if (_filterByNotDoneButFutureDeadline == null)
                {
                    _filterByNotDoneButFutureDeadline = new RelayCommands(filterByUnDoneButFutureDeadLineView, param => CanExecuteCommand);
                }
                return _filterByNotDoneButFutureDeadline;
            }
        }
        public void filterByUnDoneButFutureDeadLineView()
        {
            TasksView tasksView = new TasksView();
            TDLView view = (TDLView)SelectedItem;
            ObservableCollection<TasksView> f = tasksView.readFile(view.name);
            ObservableCollection<TasksView> t = tasksView.filterByUnDoneButFutureDeadline(f);
            if (view != null)
            {
                view.tasks = t;
                OnPropertyChanged(view.tasks.ToString());
            }
        }
        public void sizeDoneTask()
        {
            int done = 0;
            string[] file = File.ReadAllLines("Task.txt");
            foreach (string task in file)
            {
                if (!task.Contains(':'))
                {
                    string[] words = task.Split(' ');
                    if (words[4].Equals("true"))
                    {
                        done++;
                    }
                }
            }
            sizeDone= done;       
        }
        public int sizeDone
        {
            get
            {
                return _sizeTaskDone;
            }
            set { _sizeTaskDone = value;OnPropertyChanged("sizeDone"); }
        }
        public void sizeUnDoneTask()
        {
            int unDone = 0;
            string[] file = File.ReadAllLines("Task.txt");
            foreach (string task in file)
            {
                if (!task.Contains(':'))
                {
                    string[] words = task.Split(' ');
                    if (words[4].Equals("false"))
                    {
                        unDone++;
                    }
                }
            }
            sizeUnDone = unDone;
        }
        public int sizeUnDone
        {
            get
            {
                return _sizeTaskUnDone;
            }
            set { _sizeTaskUnDone = value;OnPropertyChanged("sizeUnDone"); }
        }
        public void sizeDoneToday()
        {
            int todayTasks = 0;
            string[] file = File.ReadAllLines("Task.txt");
            foreach (string task in file)
            {
                if (!task.Contains(':'))
                {
                    string[] words = task.Split(' ');
                    if (words[4].Equals("false"))
                    {
                        DateTime date = DateTime.Parse(words[3]);
                        if(date==DateTime.Today)
                        {
                            todayTasks++;
                        }

                    }
                }
            }
            todayTask = todayTasks;
            
        }
        public int todayTask
        {
            get
            {
                return _sizeDeadlineTodayTask;
            }
            set { _sizeDeadlineTodayTask = value;
                OnPropertyChanged("todayTask");
            }
        }
        public void sizeTaskDoneTillTomorrow()
        {
            int tomorrowTasks = 0;
            string[] file = File.ReadAllLines("Task.txt");
            foreach (string task in file)
            {
                if (!task.Contains(':'))
                {
                    string[] words = task.Split(' ');
                    if (words[4].Equals("false"))
                    {
                        DateTime date = DateTime.Parse(words[3]);
                        if (date == DateTime.Today.AddDays(1))
                        {
                            tomorrowTasks++;
                        }

                    }
                }
            }
            tomorrowTask= tomorrowTasks;
        }
        public int tomorrowTask
        {
            get
            {
                return _sizeDeadLineTomorrowTask;
            }
            set
            {
                _sizeDeadLineTomorrowTask= value;
                OnPropertyChanged("tomorrowTask");
            }
        }
        public void sizeOverdueTaskView()
        {
            int overdueTasks = 0;
            string[] file = File.ReadAllLines("Task.txt");
            foreach (string task in file)
            {
                if (!task.Contains(':'))
                {
                    string[] words = task.Split(' ');
                    if (words[4].Equals("false"))
                    {
                        DateTime date = DateTime.Parse(words[3]);
                        if (date <DateTime.Today)
                        {
                            overdueTasks++;
                        }

                    }
                }
            }
            sizeOverdueTask = overdueTasks;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Trello.Commands;
using System.IO;

namespace Trello.ViewModels
{
    public class EditTDLView:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _name;
        private int imageIndex = 0;
        private string _selectedItem;
        public string selectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged("selected"); }
        }
        private string _imgURL;
        public string imgURL
        {
            get { return _imgURL; }
            set { _imgURL = value; OnPropertyChanged("imgURL"); }
        }
        public ObservableCollection<string> imageURL { get; set; }
        
        public string newName
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name == value)
                {
                    return;
                }
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        private string _image;
        public string image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image == value)
                {
                    return;
                }
                _image= value;
                OnPropertyChanged("image");
            }
        }
        public EditTDLView()
        {
           

        }
        public EditTDLView(string selectedItem,string imgURL)
        {
            this._selectedItem=selectedItem;
            this._imgURL=imgURL;
            imageURL = new ObservableCollection<string>();
            string based = AppDomain.CurrentDomain.BaseDirectory;
            string path = Path.Combine(based, "resources");
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                imageURL.Add(file);
            }
            image = imageURL[imageIndex];
        }
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
        private ICommand _enter;
        public ICommand enter
        {
            get
            {
                if (_enter == null)
                {
                    _enter = new RelayCommands(enterCommand, param => CanExecuteCommand);
                }
                return _enter;
            }
        }
        public void edtiTDL(string name,string URL)
        {

            name = selectedItem;
            URL=imgURL;
            bool findRoot = false;
            bool findSubRoot =false;

            string fileContent=File.ReadAllText("ToDoList.txt");
             if(fileContent.Contains(name))
            {
                fileContent = fileContent.Replace(name, newName);
                File.WriteAllText("ToDoList.txt",fileContent);
            }
             if(fileContent.Contains(imgURL))
            {
                fileContent  =fileContent.Replace(URL,image);
                File.WriteAllText("ToDoList.txt",fileContent);
            }
            
        }
        public void enterCommand()
        {
            edtiTDL(selectedItem,imgURL);
                MessageBox.Show("Edit Successfully");
        }
        private ICommand _next;
        public ICommand next
        {
            get
            {
                if (_next == null)
                {
                    _next = new RelayCommands(nextImage, param => CanExecuteCommand);
                }
                return _next;
            }
        }
        private ICommand _previous;
        public ICommand previous
        {
            get
            {
                if (_previous == null)
                {
                    _previous = new RelayCommands(previousImage, param => CanExecuteCommand);
                }
                return _previous;
            }
        }
        public void nextImage()
        {
            imageIndex++;

            ObservableCollection<string> images = imageURL;
            if (imageIndex >= images.Count)
            {
                imageIndex = 0;
            }
            image = images[imageIndex];
            OnPropertyChanged("image");
        }
        public void previousImage()
        {
            imageIndex--;

            ObservableCollection<string> images = imageURL;
            if (imageIndex < 0)
            {
                imageIndex = images.Count - 1;
            }
            image = images[imageIndex];
            OnPropertyChanged("image");
        }
        private ICommand _cancel;
        public ICommand cancel
        {
            get
            {
                if (_cancel == null)
                {
                    _cancel = new RelayCommands(cancelCommand, param => CanExecuteCommand);
                }
                return _cancel;
            }
        }
        public void cancelCommand()
        {
            newName = "";
            imageIndex = 0;
            image = imageURL[imageIndex];
            OnPropertyChanged("image");
            OnPropertyChanged("Name");
        }
    }
}


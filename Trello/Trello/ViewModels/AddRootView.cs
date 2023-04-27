using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static System.Net.WebRequestMethods;
using Trello.Commands;


namespace Trello.ViewModels
{
    public class AddRootView:INotifyPropertyChanged
    {
        public int imageIndex = 0;
        private string _rootName { get; set; }
        public string rootName
        {
            get { return _rootName; }
            set
            {
                _rootName = value;
                
            }
        }   
        public ObservableCollection<string> imageURL { get; set; }
        public string image { get; set; }
        public AddRootView()
        {
            imageURL = new ObservableCollection<string>();
            string based=AppDomain.CurrentDomain.BaseDirectory;
            string path=Path.Combine(based,"resources");
            string[] files = Directory.GetFiles(path);
            foreach (string file in files)
            {
             imageURL.Add(file);              
            }
            image = imageURL[imageIndex];
            

        }
        public void writeRoot()
        { rootName += '=';
          
            using(StreamWriter sw=new StreamWriter("ToDoList.txt",true))
            {
                sw.WriteLine(rootName+","+image);
           
            }

        }
        public void nextImage()
        {
            imageIndex++;
           
            ObservableCollection<string> images = imageURL;
            if(imageIndex>=images.Count)
            {
                imageIndex= 0;
            }
            image= images[imageIndex];
            OnPropertyChanged("image");
        }
        private ICommand _enter;

        public event PropertyChangedEventHandler PropertyChanged;
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
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public ICommand enter
        {
            get
            {
                if(_enter==null)
                {
                    _enter = new RelayCommands(writeRoot, param=>CanExecuteCommand);
                }
                return _enter;
            }
        }
        private ICommand _next;
        public ICommand next
        {
            get
            {
                if(_next==null)
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
                if(_previous==null)
                {
                    _previous = new RelayCommands(previousImage, param => CanExecuteCommand);
                }
                return _previous;
            }
        }
        public void previousImage()
        {
            imageIndex--;

            ObservableCollection<string> images = imageURL;
            if (imageIndex<0)
            {
                imageIndex = images.Count-1;
            }
            image = images[imageIndex];
            OnPropertyChanged("image");
        }
        private ICommand _cancel;
        public ICommand cancel
        {
            get
            {
                if(_cancel==null)
                {
                    _cancel = new RelayCommands(cancelCommand, param => CanExecuteCommand);
                }
                return _cancel;
            }
        }
        public void cancelCommand()
        {
            rootName = "";
            imageIndex = 0;
            image = imageURL[imageIndex];
            OnPropertyChanged("image");
            OnPropertyChanged("rootName");
        }
    }
}

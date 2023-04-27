using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trello.Task;

namespace Trello.ViewModels
{
    public class BindableSelectedListItem
    {
        public BindableSelectedListItem(string description)
        {
            selectedTask.description = description;
        }

        public TasksView selectedTask { get; set; }
    }
}

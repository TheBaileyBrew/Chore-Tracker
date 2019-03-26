using ChoreTracker.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ChoreTracker.ViewModels
{
    class TaskViewModel : BaseViewModel
    {
        public ObservableCollection<Tasks> Tasks { get; set; }

        public TaskViewModel()
        {
            this.Tasks = new ObservableCollection<Tasks>();
        }
    }
}

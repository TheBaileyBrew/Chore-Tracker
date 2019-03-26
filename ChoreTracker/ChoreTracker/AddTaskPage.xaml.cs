using ChoreTracker.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreTracker
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddTaskPage : ContentPage
	{
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        public AddTaskPage ()
		{
			InitializeComponent ();
		}

        private async void addTask_ToDatabase(object sender, EventArgs e)
        {
            //TODO: get the details from the ENTRY fields and use as values for new TASKS object
            var newTaskTitle = taskTitle.Text;
            var newTaskShort = taskShortDesc.Text;
            var newTaskLong = taskLongDesc.Text;
            var newTaskDollar = Convert.ToDouble(taskDollarValue.Text);
            var newTaskTime = Convert.ToDouble(taskTimeValue.Text);
  
            await firebaseHelper.AddTask(newTaskTitle, newTaskShort, newTaskLong, newTaskTime, newTaskDollar);

            await DisplayAlert("Success", "Task added successfully", "OK");

           
            await Navigation.PushAsync(new MainPage());

        }
    }
}
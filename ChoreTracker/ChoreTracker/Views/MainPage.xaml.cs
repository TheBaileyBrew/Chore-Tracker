using ChoreTracker.Helpers;
using ChoreTracker.Models;
using ChoreTracker.ViewModels;
using ChoreTracker.Views;
using Rg.Plugins.Popup.Services;
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
    public partial class MainPage : ContentPage
    {
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        TaskViewModel taskViewModel;
        

        public MainPage()
        {
            InitializeComponent();
            Device.BeginInvokeOnMainThread(() => { Title = "Chore Tracker"; });
            taskViewModel = new TaskViewModel();
            BindingContext = taskViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allTasks = await firebaseHelper.GetAllTasks();
            listTasks.ItemsSource = allTasks;
            updateUIGridFields();
        }

        private async void updateUIGridFields()
        {
            var brycenDetails = await firebaseHelper.GetChild("Brycen");
            var travisDetails = await firebaseHelper.GetChild("Travis");
            B_Switch.Text = Convert.ToString(brycenDetails.ChildEarnedTime);
            B_Allow.Text = "$ " + Convert.ToString(brycenDetails.ChildEarnedDollar);
            T_Switch.Text = Convert.ToString(travisDetails.ChildEarnedTime);
            T_Allow.Text = "$ " + Convert.ToString(travisDetails.ChildEarnedDollar); 
        }

        private async void addTask_Clicked(object sender, EventArgs e)
        {
            //TODO: launch new activity for ADD NEW ACTIVITY
            await Navigation.PushAsync(new AddTaskPage());
        }

        private async void clearTimes_ClickEvent(object sender, EventArgs e)
        {
            await firebaseHelper.ResetChild("Brycen", 0.00, 0.00);
            await firebaseHelper.ResetChild("Travis", 0.00, 0.00);
            updateUIGridFields();
        }

        private async void redeemEarnings_ClickEvent(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RedeemEarningsPage());
        }

        private async void selectedTask_ClickEvent(object sender, ItemTappedEventArgs e)
        {
            var mylistView = (ListView)sender;
            var mySelectedItem = (Tasks)mylistView.SelectedItem;
            var taskSelectedTitle = mySelectedItem.TaskTitle;
            var taskSelectedLongDescription = mySelectedItem.TaskLongDescription;
            var answer = await DisplayAlert(taskSelectedTitle, taskSelectedLongDescription, "Earn", "Cancel");

            if (answer == true)
            {
                //Success
                //TODO: Update Grid Layout
                
                var childSelected = await DisplayAlert("Which child completed the task?", " ", "Brycen", "Travis");
                if (childSelected == true)
                {
                    //This is Brycen
                    await firebaseHelper.UpdateChild("Brycen", mySelectedItem.TaskWorthDollarValue, mySelectedItem.TaskWorthSwitchTime);
                } else
                {
                    //This is Travis
                    await firebaseHelper.UpdateChild("Travis", mySelectedItem.TaskWorthDollarValue, mySelectedItem.TaskWorthSwitchTime);
                }
                
                
            } else
            {
                //Close
            }
            updateUIGridFields();
            mylistView.SelectedItem = null;

        }
    }
}

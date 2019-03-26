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

        ChildPopupPage childPopupPage;

        public MainPage()
        {
            InitializeComponent();
            taskViewModel = new TaskViewModel();
            BindingContext = taskViewModel;
            childPopupPage = new ChildPopupPage();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var allTasks = await firebaseHelper.GetAllTasks();
            listTasks.ItemsSource = allTasks;

            checkForChildObject();
            
        }

        private async void checkForChildObject()
        {
            var childObject = await firebaseHelper.GetChild("Brycen");

            if (childObject != null)
            {
                child_name.Text = (childObject.ChildName + " - ");
                earned_minutes.Text = (Convert.ToString(childObject.ChildEarnedTime) + " minutes");
                earned_dollars.Text = ("$" + Convert.ToString(childObject.ChildEarnedDollar));

            }
            else
            {
                earned_minutes.Text = " ";
                earned_dollars.Text = " ";
            }
        }

        private async void addTask_Clicked(object sender, EventArgs e)
        {
            //TODO: launch new activity for ADD NEW ACTIVITY
            await Navigation.PushAsync(new AddTaskPage());

        }

        private async void addChild_ClickEvent(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PushAsync(childPopupPage);
        }


        private async void selectedTask_ClickEvent(object sender, ItemTappedEventArgs e)
        {
            var mylistView = (ListView)sender;
            var mySelectedItem = (Tasks)mylistView.SelectedItem;

            var taskSelectedTitle = mySelectedItem.TaskTitle;
            var taskSelectedShortDescription = mySelectedItem.TaskLongDescription;

            var answer = await DisplayAlert(taskSelectedTitle, taskSelectedShortDescription, "Earn", "Cancel");

            if (answer == true)
            {
                //Success
                await firebaseHelper.UpdateChild("Brycen", mySelectedItem.TaskWorthDollarValue, mySelectedItem.TaskWorthSwitchTime);
                var childObject = await firebaseHelper.GetChild("Brycen");
                earned_minutes.Text = (Convert.ToString(childObject.ChildEarnedTime) + " minutes");
                earned_dollars.Text = ("$" + Convert.ToString(childObject.ChildEarnedDollar));
            } else
            {
                //Close
            }
            mylistView.SelectedItem = null;

        }
    }
}

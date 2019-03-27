using ChoreTracker.Helpers;
using ChoreTracker.Models;
using ChoreTracker.Views.PopupViews;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RedeemEarningsPage : ContentPage
	{
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Child childDetails;

        public RedeemEarningsPage ()
		{
			InitializeComponent ();
            Device.BeginInvokeOnMainThread(() => { Title = "Redeem Earnings"; });


        }

        private void launchPicker_ClickEvent(object sender, EventArgs e)
        {
            launchChildPicker();
        }

        private async void launchChildPicker()
        {
            var childSelected = await DisplayAlert("Select A Child", "Who is spending their earnings?", "Brycen", "Travis");
            if (childSelected == true)
            {
                //This is Brycen
                childDetails = await firebaseHelper.GetChild("Brycen");
            }
            else
            {
                //This is Travis
                childDetails = await firebaseHelper.GetChild("Travis");
            }

            Child_Name.Text = childDetails.ChildName;
            Earned_Time.Text = Convert.ToString(childDetails.ChildEarnedTime) + "MINUTES";
            Earned_Allow.Text = "$ " + Convert.ToString(childDetails.ChildEarnedDollar);

            var earnedTimeTapGesture = new TapGestureRecognizer();
            earnedTimeTapGesture.Tapped += async (s, e) =>
            {
                TimeUsed time = new TimeUsed(childDetails);
                await PopupNavigation.Instance.PushAsync(time);
            };
            Earned_Time.GestureRecognizers.Add(earnedTimeTapGesture);

            var earnedDollarTapGesture = new TapGestureRecognizer();
            earnedDollarTapGesture.Tapped += async (s, e) =>
            {
                EarningsUsed earnings = new EarningsUsed(childDetails);
                await PopupNavigation.Instance.PushAsync(earnings);
            };
            Earned_Allow.GestureRecognizers.Add(earnedDollarTapGesture);
        }
	}
}
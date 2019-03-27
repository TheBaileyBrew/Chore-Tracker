using ChoreTracker.Helpers;
using ChoreTracker.Models;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreTracker.Views.PopupViews
{
	public partial class EarningsUsed : PopupPage
	{
        FirebaseHelper firebaseHelper = new FirebaseHelper();
        private Child incomingChild;

        public EarningsUsed (Child childDetails)
		{
			InitializeComponent ();
            incomingChild = childDetails;
		}

        private async void OnClose(object sender, EventArgs e)
        {
            //Get the value in the Popup Entry
            var dollarUsed = Convert.ToDouble(dollar_used.Text);
            //Get the details of the 
            await firebaseHelper.SubtractChild(incomingChild.ChildName, dollarUsed, (dollarUsed * 20));
            await PopupNavigation.Instance.PopAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(1);
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(0.25);
        }
    }
}
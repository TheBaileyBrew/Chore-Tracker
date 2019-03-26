using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChoreTracker.Helpers;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChoreTracker.Views
{
	public partial class ChildPopupPage : PopupPage
	{
        FirebaseHelper firebaseHelper = new FirebaseHelper();

        public ChildPopupPage ()
		{
			InitializeComponent ();
		}

        private async void OnClose(object sender, EventArgs e)
        {
            var childName = childEntryInput.Text;
            await firebaseHelper.AddChild(childName);

            await PopupNavigation.Instance.PopAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return Content.FadeTo(1.0);
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return Content.FadeTo(0.25);
        }
    }
}
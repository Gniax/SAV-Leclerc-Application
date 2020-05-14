using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;

namespace Leclerc
{
    public partial class ContactPage : PopupPage
    {
        public bool heightContainerBool = false;
        public double heightContainer = 0;
        public ContactPage()
        {
            InitializeComponent();
            if (Accueil.buttonValeur == "depannage")
            {
                Call.Source = "call2.png";
                Email.Source = "email2.png";
            }
            else
            {
                Call.Source = "call1.png";
                Email.Source = "email.png";
            }
        }

        void callOpen(object sender, System.EventArgs args)
        {
            try
            {
                if (Accueil.buttonValeur == "depannage")
                    Device.OpenUri(new Uri(String.Format("tel:{0}", "+33546058038")));
                else Device.OpenUri(new Uri(String.Format("tel:{0}", "+33546058028")));
            }
            catch (ArgumentNullException anEx)
            {
                Console.WriteLine(anEx);
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex);
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Other error has occurred.
            }
        }

        void emailOpen(object sender, System.EventArgs args)
        {
            try
            {
                if (Accueil.buttonValeur == "depannage")
                    Device.OpenUri(new Uri(String.Format("mailto:{0}", "SAV.LECLERC@WANADOO.FR")));
                else Device.OpenUri(new Uri(String.Format("mailto:{0}", "COMPTOIRSAVLECLERCROYAN@GMAIL.COM")));
            }
            catch (ArgumentNullException anEx)
            {
                Console.WriteLine(anEx);
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex);
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Other error has occurred.
            }
        }

        void planOpen(object sender, System.EventArgs args)
        {
            try
            {
                if (Device.RuntimePlatform == Device.iOS)
                {
                    var request = string.Format("http://maps.apple.com/?daddr=" + "45.6307796" + "," + "-0.9972002");
                    Device.OpenUri(new Uri(request));
                }

                if (Device.RuntimePlatform == Device.Android)
                {

                    var request = string.Format("http://maps.google.com/?daddr=" + "45.6307796" + "," + "-0.9972002");
                    Device.OpenUri(new Uri(request));
                }
            }
            catch (ArgumentNullException anEx)
            {
                Console.WriteLine(anEx);
                // Number was null or white space
            }
            catch (FeatureNotSupportedException ex)
            {
                Console.WriteLine(ex);
                // Phone Dialer is not supported on this device.
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Other error has occurred.
            }
        }

        protected override void OnAppearingAnimationBegin()
        {
            if (heightContainerBool == true)
            {
                heightContainerBool = false;
                FrameContainer.HeightRequest = heightContainer;
            }
            base.OnAppearingAnimationBegin();

            FrameContainer.MinimumHeightRequest = 350;
            FrameContainer.MinimumWidthRequest = 350;
            
            if (!IsAnimationEnabled)
            {
                CloseImage.Rotation = 0;
                CloseImage.Scale = 1;
                CloseImage.Opacity = 1;

                contactus.Scale = 1;
                contactus.Opacity = 1;

                Call.TranslationX = Email.TranslationX = Plan.TranslationX = 0;
                Call.Opacity = Email.Opacity = Plan.Opacity = 1;

                return;
            }

            

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            contactus.Scale = 0.3;
            contactus.Opacity = 0;

            Call.TranslationX = Email.TranslationX = Plan.TranslationX = -10;
            Call.Opacity = Email.Opacity = Plan.Opacity = 0;
        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var translateLength = 400u;

            await Task.WhenAll(
                Call.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                Call.FadeTo(1),
                (new Func<Task>(async () =>
                {
                    await Task.Delay(200);
                    await Task.WhenAll(
                        Email.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                        Email.FadeTo(1),
                        Plan.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                        Plan.FadeTo(1));

        }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                contactus.ScaleTo(1),
                contactus.FadeTo(1));
        }

        protected override async Task OnDisappearingAnimationBeginAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var taskSource = new TaskCompletionSource<bool>();

            var currentHeight = FrameContainer.Height;
            heightContainer = FrameContainer.Height;
            heightContainerBool = true;

            await Task.WhenAll(
                Call.FadeTo(0),
                Email.FadeTo(0),
                Plan.FadeTo(0),
                contactus.FadeTo(0));

            FrameContainer.Animate("HideAnimation", d =>
            {
                FrameContainer.HeightRequest = d;
            },
            start: currentHeight,
            end: 170,
            finished: async (d, b) =>
            {
                await Task.Delay(300);
                taskSource.TrySetResult(true);

            });

            await taskSource.Task;
        }

        
        private void OnCloseButtonTapped(object sender, EventArgs e)
        {
            CloseAllPopup();
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();
            return false;
        }


        public static async void CloseAllPopup()
        {
            App.nbrPopup--;
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
    
}
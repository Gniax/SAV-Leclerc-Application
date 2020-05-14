using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;

namespace Leclerc
{
    public partial class APropos : PopupPage
    {
        public bool heightContainerBool = false;
        public double heightContainer = 0;
        public APropos()
        {
            InitializeComponent();
           
        }

        protected override void OnAppearingAnimationBegin()
        {
            if (heightContainerBool == true)
            {
                heightContainerBool = false;
                FrameContainer.HeightRequest = heightContainer;
            }
            base.OnAppearingAnimationBegin();

            FrameContainer.MinimumHeightRequest = 250;
            FrameContainer.MinimumWidthRequest = 250;
            
            if (!IsAnimationEnabled)
            {
                CloseImage.Rotation = 0;
                CloseImage.Scale = 1;
                CloseImage.Opacity = 1;

                test.Scale = 1;
                test.Opacity = 1;

                return;
            }

            

            CloseImage.Rotation = 30;
            CloseImage.Scale = 0.3;
            CloseImage.Opacity = 0;

            test.Scale = 0.3;
            test.Opacity = 0;

        }

        protected override async Task OnAppearingAnimationEndAsync()
        {
            if (!IsAnimationEnabled)
                return;

            var translateLength = 400u;

            await Task.WhenAll(
                test.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                test.FadeTo(1),
                (new Func<Task>(async () =>
                {
                    await Task.Delay(200);
                    await Task.WhenAll(
                        test.TranslateTo(0, 0, easing: Easing.SpringOut, length: translateLength),
                        test.FadeTo(1));

        }))());

            await Task.WhenAll(
                CloseImage.FadeTo(1),
                CloseImage.ScaleTo(1, easing: Easing.SpringOut),
                CloseImage.RotateTo(0),
                test.ScaleTo(1),
                test.FadeTo(1));
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
                test.FadeTo(0));

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
            await PopupNavigation.Instance.PopAllAsync();
        }
    }
    
}
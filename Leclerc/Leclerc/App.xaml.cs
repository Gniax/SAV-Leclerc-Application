using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Leclerc
{
    public partial class App : Application
    {
        public static int nbrPopup = 0;
        public App()
        {
            InitializeComponent();

            MainPage = new Accueil();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

        }

        void finLogo()
        {
            if (Accueil.buttonValeur != "accueil")
            {

                MainPage = new Accueil(true);

                Accueil.buttonValeur = "accueil";



            }
        }

        void onLogoTap(object sender, System.EventArgs args)
        {

            try
            {
                Image savLogo = (Image)sender;

                // Dirty hack you probably shouldn't use
                var width = Application.Current.MainPage.Width;

                var storyboard = new Animation();

                if (Accueil.buttonValeur != "accueil")
                {
                    var rotation = new Animation(callback: d => savLogo.Rotation = d,
                                                  start: 0,
                                                  end: 380,
                                                  easing: Easing.SpringOut);

                    var rotation2 = new Animation(callback: d => savLogo.Rotation = d,
                                                  start: 380,
                                                  end: 360,
                                                  easing: Easing.SpringOut);

                    storyboard.Add(0, 0.85, rotation);
                    storyboard.Add(0.85, 1, rotation2);


                    storyboard.Commit(savLogo, "Loop", 16, length: 600, Easing.Linear, (v, c) => finLogo());
                }
                else
                {

                    var rotation = new Animation(callback: d => savLogo.Rotation = d,
                                                  start: 0,
                                                  end: 360,
                                                  easing: Easing.SpringOut);


                    var exitRight = new Animation(callback: d => savLogo.TranslationX = d,
                                                   start: 0,
                                                   end: width,
                                                   easing: Easing.SpringIn);

                    var enterLeft = new Animation(callback: d => savLogo.TranslationX = d,
                                                   start: -width,
                                                   end: 0,
                                                   easing: Easing.BounceOut);

                    storyboard.Add(0, 1, rotation);
                    storyboard.Add(0, 0.5, exitRight);
                    storyboard.Add(0.5, 1, enterLeft);

                    storyboard.Commit(savLogo, "Loop", 16, length: 1400, Easing.Linear);
                }

            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }

        void onFacebookTap(object sender, System.EventArgs args)
        {
            try
            {
                Device.OpenUri(new Uri("fb://facewebmodal/f?href=https://www.facebook.com/ELeclercRoyan"));
            }

            catch (Exception ex)
            {
                try
                {
                    Device.OpenUri(new Uri("https://fr-fr.facebook.com/ELeclercRoyan/"));
                }
                catch (Exception ex2)
                {

                    Console.WriteLine(ex2);
                }
                Console.WriteLine(ex);
            }
        }

        void onWebsiteTap(object sender, System.EventArgs args)
        {
            try
            {
                Device.OpenUri(new Uri("http://www.e-leclerc.com/royan"));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}

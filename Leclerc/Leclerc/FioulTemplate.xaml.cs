using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Leclerc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FioulTemplate : ContentPage
    {
        private ContactPage _contactPage;
        public FioulTemplate()
        {
            InitializeComponent();
            _contactPage = new ContactPage();
            loadingImages();
            loadingText();
        }
        void loadingImages()
        {
           
        }

        void debutAnimation()
        {
           
        }
        async void loadingText()
        {
            try
            {
                fioulImage.Source = new UriImageSource
                {
                    CachingEnabled = false,
                    Uri = new Uri("https://drive.google.com/uc?id=1AGBPRb6rm0W4meBQI0WtXD7rFFYUaDD7")
                };

                string textPieces = await getStringAsync("https://drive.google.com/uc?id=1Oh36Ak1shkalAeiCbR-fKYRvy6eg8Vfz");
                var array = textPieces.Split('\n');
                string titre = array[0];

                titreFioul.Text = titre;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                titreFioul.Text = "Erreur réseau. Vérifiez votre connexion Internet.";
            }
        }


        async Task<string> getStringAsync(string url)
        {
            try
            {
                var Client = new HttpClient();
                var response = await Client.GetStringAsync(url);
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return "Erreur réseau. Vérifiez votre connexion Internet.";
            }
        }
        private async void OnOpenPupup(object sender, EventArgs e)
        {
            App.nbrPopup++;
            await PopupNavigation.Instance.PushAsync(_contactPage);
        }
        void finLogo()
        {
            if (Accueil.buttonValeur != "accueil")
            {

                App.Current.MainPage = new Accueil(true);

                Accueil.buttonValeur = "accueil";



            }
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                if (App.nbrPopup > 0)
                {
                    ContactPage.CloseAllPopup();
                    return true;
                }
                else
                {

                    // Do something if there are not any pages in the `PopupStack`
                    var var2 = Accueil.buttonValeur;
                    if (var2 != "accueil")
                    {
                        App.Current.MainPage = new Accueil();
                        Accueil.buttonValeur = "accueil";
                        return true;

                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return true;
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


            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}
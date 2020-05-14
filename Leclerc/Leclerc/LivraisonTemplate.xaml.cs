using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Leclerc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LivraisonTemplate : ContentPage
    {
        private ContactPage _contactPage;
        public LivraisonTemplate()
        {
            InitializeComponent();
            _contactPage = new ContactPage();
            loadingImages();
            loadingText();
        }
        private async void OnOpenPupup(object sender, EventArgs e)
        {
            App.nbrPopup++;
            await PopupNavigation.Instance.PushAsync(_contactPage);
        }
        void loadingImages()
        {
            pancarte.IsVisible = true;
            fondrose.IsVisible = true;
            scotch.IsVisible = true;
            pancarte.Scale = 2;
            fondrose.Scale = 2;
            scotch.Scale = 2;


            var width = Application.Current.MainPage.Width;

            double test = 1 + 4 * (width / 10000);


            var storyboard = new Animation();

            var descale = new Animation(callback: d => pancarte.Scale = d,
                                            start: 2,
                                            end: test,
                                            easing: Easing.SpringOut);

            var descale1 = new Animation(callback: d => fondrose.Scale = d,
                                          start: 2,
                                          end: test,
                                          easing: Easing.SpringOut);

            var descale2 = new Animation(callback: d => scotch.Scale = d,
                                              start: 2,
                                              end: test,
                                              easing: Easing.SpringOut);

            storyboard.Add(0, 1, descale);
            storyboard.Add(0, 1, descale1);
            storyboard.Add(0, 1, descale2);


                storyboard.Commit(pancarte, "apparition", 16, length: 600, Easing.Linear, (v, c) => debutAnimation());


            }

        void debutAnimation()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = mainDisplayInfo.Width;
            var height = mainDisplayInfo.Height;

            var storyboard = new Animation();


            var rotation = new Animation(callback: d => scotch.Rotation = d,
                                          start: 0,
                                          end: -90,
                                          easing: Easing.SpringOut);

            var rotation4 = new Animation(callback: d => scotch.RotationX = d,
                              start: 0,
                              end: 40);

            var rotation5 = new Animation(callback: d => scotch.RotationX = d,
                                          start: 40,
                                          end: -40);

            var tombe = new Animation(callback: d => pancarte.TranslationY = d,
                                          start: 0,
                                          end: height * 0.8,
                                          easing: Easing.CubicIn);

            var tombe2 = new Animation(callback: d => scotch.TranslationY = d,
                                          start: 0,
                                          end: height * 0.8,
                                          easing: Easing.CubicIn);

            storyboard.Add(0, 0.5, rotation);
            storyboard.Add(0.6, 1, rotation4);
            storyboard.Add(0.6, 1, rotation5);
            storyboard.Add(0, 0.8, tombe);
            storyboard.Add(0.6, 1, tombe2);


            storyboard.Commit(scotch, "scotchTombe", 16, length: 4000, Easing.Linear);
        }
        async void loadingText()
        {
            try
            {
                string textPieces = await getStringAsync("https://drive.google.com/uc?id=10ujelaiqQH6PwIOLSVE-R66CtT0jfMcl");
                var array = textPieces.Split('\n');
                string titre = array[0];
                string description = array[2] + '\n' + array[3] + '\n' + array[4];
                string description2 = array[6] + array[7];
                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                var width = mainDisplayInfo.Width;
                descriptionLivraison.WidthRequest = fondrose.Width / 1.25;
                descriptionLivraison.HeightRequest = fondrose.Height / 2;

                var height2 = DeviceDisplay.MainDisplayInfo.Height;
                var width2 = DeviceDisplay.MainDisplayInfo.Width;

                descriptionLivraison.FontSize = Accueil.freeValueSize / 1.5;
                descriptionLivraison2.FontSize = Accueil.freeValueSize / 1.5;

                titreLivraison.FontSize = Accueil.freeValueSize;
                //descriptionAntenne.FontSize = Accueil.freeValueSize - (Accueil.freeValueSize / 5);

                titreLivraison.Text = titre;
                descriptionLivraison.Text = description;
                descriptionLivraison2.Text = description2;
                //descriptionAntenne.Text = description;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                titreLivraison.Text = "Erreur réseau. Vérifiez votre connexion Internet.";
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
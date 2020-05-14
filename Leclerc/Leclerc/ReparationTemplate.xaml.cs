using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using FFImageLoading.Forms;
using Rg.Plugins.Popup.Services;

namespace Leclerc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReparationTemplate : ContentPage
    {
        private ContactPage _contactPage;
        public ReparationTemplate()
        {

            InitializeComponent();
            _contactPage = new ContactPage();
            FFImageLoading.Svg.Forms.SvgCachedImage.Init();
            loadingImages();
            loadingText();
        }

        private async void OnOpenPupup(object sender, EventArgs e)
        {
            App.nbrPopup++;
            await PopupNavigation.Instance.PushAsync(_contactPage);
        }

        public static bool bikeAnim = true;
        void bikeAnimation()
        {
            if(bikeAnim == true)
            {
                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                var width = App.Current.MainPage.Width;

                var storyboard = new Animation();

                var roule = new Animation(callback: d => bikeGif.TranslationX = d,
                                              start: -width,
                                              end: width,
                                              Easing.CubicIn);

                var roule2 = new Animation(callback: d => bikeGif.TranslationX = d,
                                              start: width,
                                              end: -width,
                                              Easing.CubicIn);

                var rotate = new Animation(callback: d => bikeGif.RotationY = d,
                                              start: 0,
                                              end: 180);

                var rotate1 = new Animation(callback: d => bikeGif.RotationY = d,
                                              start: 180,
                                              end: 0);

                storyboard.Add(0, 0.5, roule);
                storyboard.Add(0.5, 1, roule2);
                storyboard.Add(0.49, 0.5, rotate);
                storyboard.Add(0.99, 1, rotate1);


                storyboard.Commit(bikeGif, "bike", 16, length: 7000, null, (v, c) => bikeAnimation());
            }
        }
        void loadingImages()
        {
            var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
            var width = App.Current.MainPage.Width;

            bikeGif.TranslationX = -width;
            bikeGif.IsVisible = true;

            var storyboard = new Animation();

            var roule = new Animation(callback: d => bikeGif.TranslationX = d,
                                          start: -width,
                                          end: width,
                                              Easing.CubicIn);

            var roule2 = new Animation(callback: d => bikeGif.TranslationX = d,
                                          start: width,
                                          end: -width,
                                              Easing.CubicIn);

            var rotate = new Animation(callback: d => bikeGif.RotationY = d,
                                          start: 0,
                                          end: 180);

            var rotate1 = new Animation(callback: d => bikeGif.RotationY = d,
                                          start: 180,
                                          end: 0);

            storyboard.Add(0, 0.5, roule);
            storyboard.Add(0.5, 1, roule2);
            storyboard.Add(0.49, 0.5, rotate);
            storyboard.Add(0.99, 1, rotate1);


            storyboard.Commit(bikeGif, "bike", 16, length: 7000, null, (v, c) => bikeAnimation());



        }

        async void loadingText()
        {
            try
            {
                string textRepar = await getStringAsync("https://drive.google.com/uc?id=1RZ4AK4PufZ58O7Tgu64jkSec3jelV390");
                var array = textRepar.Split('\n');
                string titre = array[0];
                string description = array[1] + '\n' + array[2];
                string tarif1T = array[4];
                string tarif2T = array[5];
                string tarif3T = array[6];
                string tarif4T = array[7];
                string tarif5T = array[8];
                string tarif6T = array[9];
                string tarif7T = array[10];
                string tarif8T = array[11];
                string tarif9T = array[12];

                var tarif10 = tarif1T.Split(':');
                string tarifTS10 = tarif10[0] + ":";
                string tarifTS11 = '\n' + tarif10[1];

                var tarif20 = tarif2T.Split(':');
                string tarifTS20 = tarif20[0] + ":";
                string tarifTS21 = '\n' + tarif20[1];

                var tarif30 = tarif3T.Split(':');
                string tarifTS30 = tarif30[0] + ":";
                string tarifTS31 = '\n' + tarif30[1];

                var tarif40 = tarif1T.Split(':');
                string tarifTS40 = tarif40[0] + ":";
                string tarifTS41 = '\n' + tarif40[1];

                var tarif50 = tarif5T.Split(':');
                string tarifTS50 = tarif50[0] + ":";
                string tarifTS51 = '\n' + tarif50[1];

                var tarif60 = tarif6T.Split(':');
                string tarifTS60 = tarif60[0] + ":";
                string tarifTS61 = '\n' + tarif60[1];

                var tarif70 = tarif7T.Split(':');
                string tarifTS70 = tarif70[0] + ":";
                string tarifTS71 = '\n' + tarif70[1];

                var tarif80 = tarif8T.Split(':');
                string tarifTS80 = tarif80[0] + ":";
                string tarifTS81 = '\n' + tarif80[1];

                var tarif90 = tarif9T.Split(':');
                string tarifTS90 = tarif90[0] + ":";
                string tarifTS91 = '\n' + tarif90[1];

                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;
                var width = mainDisplayInfo.Width;

                    titreA.FontSize = Accueil.freeValueSize;
                    informationsR.FontSize = Accueil.freeValueSize-2;
                    tarif1.FontSize = Accueil.freeValueSize-3;
                    tarif2.FontSize = Accueil.freeValueSize-3;
                    tarif3.FontSize = Accueil.freeValueSize-3;
                    tarif4.FontSize = Accueil.freeValueSize-3;
                    tarif5.FontSize = Accueil.freeValueSize-3;
                    tarif6.FontSize = Accueil.freeValueSize-3;
                    tarif7.FontSize = Accueil.freeValueSize-3;
                    tarif8.FontSize = Accueil.freeValueSize-3;
                    tarif9.FontSize = Accueil.freeValueSize-3;

                titreA.Text = titre;
                informationsR.Text = description;

                tarifS10.Text = tarifTS10;
                tarifS11.Text = tarifTS11;

                tarifS20.Text = tarifTS20;
                tarifS21.Text = tarifTS21;

                tarifS30.Text = tarifTS30;
                tarifS31.Text = tarifTS31;

                tarifS40.Text = tarifTS40;
                tarifS41.Text = tarifTS41;

                tarifS50.Text = tarifTS50;
                tarifS51.Text = tarifTS51;

                tarifS60.Text = tarifTS60;
                tarifS61.Text = tarifTS61;

                tarifS70.Text = tarifTS70;
                tarifS71.Text = tarifTS71;

                tarifS80.Text = tarifTS80;
                tarifS81.Text = tarifTS81;

                tarifS90.Text = tarifTS90;
                tarifS91.Text = tarifTS91;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                titreA.Text = "Erreur réseau. Vérifiez votre connexion Internet.";
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
                    bikeAnim = false;
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
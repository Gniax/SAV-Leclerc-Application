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
    public partial class DepannageTemplate : ContentPage
    {
        private ContactPage _contactPage;
        public DepannageTemplate()
        {
            InitializeComponent();
            _contactPage = new ContactPage();
            loadingImages();
            loadingText();
        }
        void loadingImages()
        {
            backGif.IsVisible = true;

            Tarifs.MinimumHeightRequest = accueilB.MinimumHeightRequest = Tarifs.MinimumWidthRequest = accueilB.MinimumWidthRequest = backGif2.Width / 3;
        }

        async void switchTarif1()
        {
            uint timeout = 800;
            accueilB.RotationY = -270;
            await Tarifs.RotateYTo(-90, timeout, Easing.SpringIn);
            Tarifs.IsVisible = false;
            accueilB.IsVisible = true;
            await accueilB.RotateYTo(-360, timeout, Easing.SpringOut);
            accueilB.RotationY = 0;
        }

        async void switchTarif2()
        {
            uint timeout = 800;
            Tarifs.RotationY = -270;
            await accueilB.RotateYTo(-90, timeout, Easing.SpringIn);
            accueilB.IsVisible = false;
            Tarifs.IsVisible = true;
            await Tarifs.RotateYTo(-360, timeout, Easing.SpringOut);
            Tarifs.RotationY = 0;
        }
        async void switchDesc1()
        {
            uint timeout = 800;
            tarifsDesc1.RotationY = -270;
            await description1.RotateYTo(-90, timeout, Easing.SpringIn);
            description1.IsVisible = false;
            tarifsDesc1.IsVisible = true;
            await tarifsDesc1.RotateYTo(-360, timeout, Easing.SpringOut);
            tarifsDesc1.RotationY = 0;
        }
        async void switchDesc2()
        {
            uint timeout = 800;
            tarifsDesc2.RotationY = -270;
            await description2.RotateYTo(-90, timeout, Easing.SpringIn);
            description2.IsVisible = false;
            tarifsDesc2.IsVisible = true;
            await tarifsDesc2.RotateYTo(-360, timeout, Easing.SpringOut);
            tarifsDesc2.RotationY = 0;
        }

        async void switchDesc11()
        {
            uint timeout = 800;
            description1.RotationY = -270;
            await tarifsDesc1.RotateYTo(-90, timeout, Easing.SpringIn);
            tarifsDesc1.IsVisible = false;
            description1.IsVisible = true;
            await description1.RotateYTo(-360, timeout, Easing.SpringOut);
            description1.RotationY = 0;
        }
        async void switchDesc22()
        {
            uint timeout = 800;
            description2.RotationY = -270;
            await tarifsDesc2.RotateYTo(-90, timeout, Easing.SpringIn);
            tarifsDesc2.IsVisible = false;
            description2.IsVisible = true;
            await description2.RotateYTo(-360, timeout, Easing.SpringOut);
            description2.RotationY = 0;
        }
        async void switchTarif(object sender, EventArgs e)
        {
            if (backGif.IsVisible == true)
            {
                switchTarif1();
                switchDesc1();
                switchDesc2();
                uint timeout = 800;
                backGif2.RotationY = -270;
                await backGif.RotateYTo(-90, timeout, Easing.SpringIn);
                backGif.IsVisible = false;
                backGif2.IsVisible = true;
                await backGif2.RotateYTo(-360, timeout, Easing.SpringOut);
                backGif2.RotationY = 0;

            }
            else if (backGif2.IsVisible == true)
            {
                switchTarif2();
                switchDesc11();
                switchDesc22();
                uint timeout = 800;
                backGif.RotationY = -270;
                await backGif2.RotateYTo(-90, timeout, Easing.SpringIn);
                backGif2.IsVisible = false;
                backGif.IsVisible = true;
                await backGif.RotateYTo(-360, timeout, Easing.SpringOut);
                backGif.RotationY = 0;

                
            }
        }
        async void loadingText()
        {
            try
            {
                string textPieces = await getStringAsync("https://drive.google.com/uc?id=1h_KGXDmPDNQGlOhMIcNN6KgFN3ZNHutb");
                var array = textPieces.Split('\n');
                string titre = array[0];
                string description = array[1] + '\n' + array[2];
                string descriptionM = array[4] + '\n' + array[5] + '\n' + array[6] + '\n' + array[7];

                string descriptionDesc1 = array[10] + '\n' + array[11] + '\n' + array[12] + '\n' + array[13] + '\n' + array[14];
                string descriptionDesc2 = array[15] + '\n' + array[16] + '\n' + array[17] + '\n' + array[18];

                titreDepannage.Text = titre;
                description1.Text = description;
                description2.Text = descriptionM;

                tarifsDesc1.Text = descriptionDesc1;
                tarifsDesc2.Text = descriptionDesc2;

                description1.WidthRequest = backGif.Width / 1.25;
                description1.HeightRequest = backGif.Height / 2;
                description2.WidthRequest = backGif.Width / 1.25;
                description2.HeightRequest = backGif.Height / 2;


                description1.FontSize = Accueil.freeValueSize / 1.2;
                description2.FontSize = Accueil.freeValueSize / 1.2;

                tarifsDesc1.WidthRequest = backGif.Width / 1.2;
                tarifsDesc1.HeightRequest = backGif.Height / 2;
                tarifsDesc2.WidthRequest = backGif.Width / 1.2;
                tarifsDesc2.HeightRequest = backGif.Height / 2;

                tarifsDesc1.FontSize = Accueil.freeValueSize / 1.4;
                tarifsDesc2.FontSize = Accueil.freeValueSize / 1.4;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                titreDepannage.Text = "Erreur réseau. Vérifiez votre connexion Internet.";
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
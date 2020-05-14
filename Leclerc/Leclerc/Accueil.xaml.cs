using Rg.Plugins.Popup.Services;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Leclerc
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Accueil : ContentPage
    {
        double width;
        double height;
        private APropos _apropos;
        public Accueil(bool valeur = false)
        {
            InitializeComponent();

            _apropos = new APropos();
            
        }

        private async void onSettingsTap(object sender, System.EventArgs args)
        {
            nbrpopupA++;
            await PopupNavigation.Instance.PushAsync(_apropos);
        }

        void finLogo()
        {
            if (Accueil.buttonValeur != "accueil")
            {

                App.Current.MainPage = new Accueil(true);

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
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (this.width != width || this.height != height)
            {
                this.width = width;
                this.height = height;
            }

            if (this.width != -1 || this.height != -1)
            {
                Resizing();
                
            }
        }
        public static int freeValueSize = 0;
        public void Resizing()
        {
            int reduction = 20;

            freeValueSize = (int)(pieces.Width + pieces.Height) / reduction - 1;
            leclercR.FontSize = freeValueSize / 1.5;
            pieces.FontSize = (int)(pieces.Width + pieces.Height) / reduction - 1;
            depannage.FontSize = (int)(depannage.Width + depannage.Height) / reduction - 1;
            reparation.FontSize = (int)(reparation.Width + reparation.Height) / reduction - 1;
            antenne.FontSize = (int)(antenne.Width + antenne.Height) / reduction - 1;
            livraison.FontSize = (int)(livraison.Width + livraison.Height) / reduction - 1;
            fioul.FontSize = (int)(fioul.Width + fioul.Height) / reduction - 1;

        }

        public static bool buttonAnimation = false;
        public static string buttonValeur = "accueil";


        public void launchPage(bool var, string var2)
        {
            buttonAnimation = var;
            buttonValeur = var2;
            if (var2 == "pieces")
            {
                App.Current.MainPage = new PiecesTemplate();
                this.ControlTemplate = (ControlTemplate)App.Current.Resources["PiecesTemplate"];
            }
            else if (var2 == "depannage")
            {
                this.ControlTemplate = (ControlTemplate)App.Current.Resources["DepannageTemplate"];
                App.Current.MainPage = new DepannageTemplate();
            }
            else if (var2 == "reparation")
            {
                ReparationTemplate.bikeAnim = true;
                this.ControlTemplate = (ControlTemplate)App.Current.Resources["ReparationTemplate"];
                App.Current.MainPage = new ReparationTemplate();
            }
            else if (var2 == "antenne")
            {
                this.ControlTemplate = (ControlTemplate)App.Current.Resources["AntenneTemplate"];
                App.Current.MainPage = new AntenneTemplate();
            }
            else if (var2 == "livraison")
            {
                this.ControlTemplate = (ControlTemplate)App.Current.Resources["LivraisonTemplate"];
                App.Current.MainPage = new LivraisonTemplate();
            }
            else if (var2 == "fioul")
            {
                this.ControlTemplate = (ControlTemplate)App.Current.Resources["FioulTemplate"];
                App.Current.MainPage = new FioulTemplate();
            }

        }
        public static int nbrpopupA = 0;
        protected override bool OnBackButtonPressed()
        {
            try
            {
                if (App.nbrPopup > 0)
                {
                    ContactPage.CloseAllPopup();
                    
                    return true;
                }
                else if (Accueil.nbrpopupA > 0)
                {
                    nbrpopupA--;
                    APropos.CloseAllPopup();
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

        async void Bouton_Click(object sender, EventArgs e)
        {
            try
            {
                var mainDisplayInfo = DeviceDisplay.MainDisplayInfo;

                Button bouton = (Button)sender;
                Grid testgrid = zoneBas;

                if (buttonAnimation == false)
                {
                    buttonAnimation = true;

                    pieces.IsEnabled = true;
                    depannage.IsEnabled = true;
                    reparation.IsEnabled = true;
                    antenne.IsEnabled = true;
                    livraison.IsEnabled = true;
                    fioul.IsEnabled = true;
                    await Task.WhenAll(

                      pieces.ColorTo(pieces.BackgroundColor, bouton.BackgroundColor, c => pieces.BackgroundColor = c, 400),
                      depannage.ColorTo(depannage.BackgroundColor, bouton.BackgroundColor, c => depannage.BackgroundColor = c, 400),
                      reparation.ColorTo(reparation.BackgroundColor, bouton.BackgroundColor, c => reparation.BackgroundColor = c, 400),
                      antenne.ColorTo(antenne.BackgroundColor, bouton.BackgroundColor, c => antenne.BackgroundColor = c, 400),
                      livraison.ColorTo(livraison.BackgroundColor, bouton.BackgroundColor, c => livraison.BackgroundColor = c, 400),
                      fioul.ColorTo(fioul.BackgroundColor, bouton.BackgroundColor, c => fioul.BackgroundColor = c, 400));

                    var width = Application.Current.MainPage.Width;
                    var height = Application.Current.MainPage.Height;
                    var text = bouton.TextColor;
                    var A = text.Luminosity;

                    var storyboard = new Animation();

                    var scaleRed = new Animation(callback: d => accueilButtons.Scale = d,
                                                   start: 1,
                                                   end: 0);


                    storyboard.Add(0, 1, scaleRed);

                    var opacityR = new Animation(callback: d => accueilButtons.Opacity = d,
                                                   start: 1,
                                                   end: 0);

                    storyboard.Add(0.5, 1, opacityR);


                    storyboard.Commit(bouton, "AnimationOpeningPage", 16, length: 400, null, (v, c) => launchPage(false, bouton.ClassId));
                    
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

        }
    }
}

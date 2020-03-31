using Antize_TFT.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for ChampionOnClasseOrigine.xaml
    /// </summary>
    public partial class ChampionOnClasseOrigine : UserControl
    {
        public ChampionOnClasseOrigine(Champion _Champ, bool _AlreadyTake)
        {
            this.InitializeComponent();

            if (_Champ != null)
            {
                if (TacheFond.GetUserSettingsRef.OnlyOneType)
                {
                    if (_AlreadyTake)
                        this.Disabler.Visibility = Visibility.Visible;
                    else
                        this.Disabler.Visibility = Visibility.Hidden;
                }
                else
                    this.Disabler.Visibility = Visibility.Hidden;

                this.Champion_Icon.Source = _Champ.GetIcon;
                this.Name_Champion.Text = _Champ.GetName;

                this.Classe_1.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champ.GetClasse_One}.png", UriKind.Relative));
                this.Classe_2.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champ.GetClasse_Two}.png", UriKind.Relative));
                this.Classe_3.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champ.GetClasse_Three}.png", UriKind.Relative));

                //Met la couleur par le tier
                this.Name_Champion.Foreground = TacheFond.GetColorTier(_Champ.GetTier);
            }
            else
            {
                this.Champion_Icon.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                this.Name_Champion.Text = "";

                this.Classe_1.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Classes/Invisible.png", UriKind.Relative));
                this.Classe_2.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Classes/Invisible.png", UriKind.Relative));
                this.Classe_3.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Classes/Invisible.png", UriKind.Relative));

                this.Name_Champion.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8C9095"));
            }
        }
    }
}

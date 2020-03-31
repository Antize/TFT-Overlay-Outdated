using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Antize_TFT.Class;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for DetailChampion.xaml
    /// </summary>
    public partial class DetailChampion : Window
    {
        public DetailChampion()
        {
            this.InitializeComponent();
        }

        public void UpdateDetailChampion(Champion _Champion)
        {
            if (_Champion != null)
            {
                //Attribue les informations
                this.Champion_Icon.Source = _Champion.GetIcon;
                this.Champion_Name.Text = _Champion.GetName;
                this.ChampionHealth.Text = _Champion.GetHealth;
                this.ChampionDamage.Text = _Champion.GetDamage;

                this.Tier.Text = _Champion.GetTier.ToString();

                this.Classe_One.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champion.GetClasse_One}.png", UriKind.Relative));
                this.Classe_Two.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champion.GetClasse_Two}.png", UriKind.Relative));

                this.Classe_One_Name.Text = _Champion.GetClasse_One.ToString();
                this.Classe_Two_Name.Text = _Champion.GetClasse_Two.ToString();


                if (_Champion.GetClasse_Three.ToString().Contains("None"))
                {
                    this.Classe_Three.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Classes/Inivisible.png", UriKind.Relative));
                    this.Classe_Three_Name.Text = "";
                }
                else
                {
                    this.Classe_Three.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champion.GetClasse_Three}.png", UriKind.Relative));
                    this.Classe_Three_Name.Text = _Champion.GetClasse_Three.ToString();
                }

                //Rafraichi les items
                int Local_index = 1;

                foreach (Items Item in _Champion.MyItem)
                {
                    BitmapImage Local_Icon_Stuff = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                    BitmapImage Local_Icon_StuffNeed1 = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                    BitmapImage Local_Icon_StuffNeed2 = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));

                    if (Item != null)
                    {
                        Local_Icon_Stuff = Item.GetIcon;

                        if (Item.GetNeeded_Type1 != Enums.Enum_Item.None)
                        {
                            Local_Icon_StuffNeed1 = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{Item.GetNeeded_Type1}.png", UriKind.Relative));
                            Local_Icon_StuffNeed2 = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{Item.GetNeeded_Type2}.png", UriKind.Relative));
                        }
                    }

                    switch (Local_index)
                    {
                        case 1:
                            this.Stuff1.Source = Local_Icon_Stuff;
                            this.Stuff1_Need1.Source = Local_Icon_StuffNeed1;
                            this.Stuff1_Need2.Source = Local_Icon_StuffNeed2;
                            break;
                        case 2:
                            this.Stuff2.Source = Local_Icon_Stuff;
                            this.Stuff2_Need1.Source = Local_Icon_StuffNeed1;
                            this.Stuff2_Need2.Source = Local_Icon_StuffNeed2;
                            break;
                        case 3:
                            this.Stuff3.Source = Local_Icon_Stuff;
                            this.Stuff3_Need1.Source = Local_Icon_StuffNeed1;
                            this.Stuff3_Need2.Source = Local_Icon_StuffNeed2;
                            break;
                        default:
                            break;
                    }

                    Local_index++;
                }

                //Met la couleur par le tier
                this.Champion_Name.Foreground = TacheFond.GetColorTier(_Champion.GetTier);
                this.Tier.Foreground = TacheFond.GetColorTier(_Champion.GetTier);
            }
        }
    }
}
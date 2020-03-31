using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Antize_TFT.Class;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for SlotChampion.xaml
    /// </summary>
    public partial class SlotChampion : UserControl
    {
        private readonly bool IsUserSlot;

        private OrigineClasse OrigineClasseOne;
        private OrigineClasse OrigineClasseTwo;
        private OrigineClasse OrigineClasseThree;

        private bool SelfEnabled;

        public SlotChampion(int _Index, bool _IsUserSlot)
        {
            this.InitializeComponent();

            this.Index = _Index;
            this.IsUserSlot = _IsUserSlot;

            this.DisabledOrEnable(false);
        }

        public int Index
        {
            get; set;
        }

        public Champion ChampionOnSlot
        {
            get; set;
        }

        /*****************************/
        /*** Fonctions ***************/
        /*****************************/
        public void AddOrRemoveChampion(Champion _Champion, bool _ByUser)
        {
            this.ChampionOnSlot = _Champion;

            this.OrigineClasseOne = null;
            this.OrigineClasseTwo = null;
            this.OrigineClasseThree = null;

            int Local_Number = int.Parse(TacheFond.GetMainRef.GetSetActualNumber);

            if (_Champion != null)
            {
                if (_ByUser)
                {
                    Local_Number++;
                    TacheFond.GetMainRef.GetSetActualNumber = Local_Number.ToString();
                }

                this.Champion_Icon.Source = _Champion.GetIcon;
                this.Name_Champion.Text = _Champion.GetName;

                this.Classe_1.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champion.GetClasse_One}.png", UriKind.Relative));
                this.Classe_2.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champion.GetClasse_Two}.png", UriKind.Relative));
                this.Classe_3.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{_Champion.GetClasse_Three}.png", UriKind.Relative));

                //Recupere les Origines et Classes
                bool Local_MakeOne = true;
                bool Local_MakeTwo = true;
                bool Local_MakeThree = true;

                if (_Champion.GetClasse_One == Enums.Enum_Classe.None)
                    Local_MakeOne = false;

                if (_Champion.GetClasse_Two == Enums.Enum_Classe.None)
                    Local_MakeTwo = false;

                if (_Champion.GetClasse_Three == Enums.Enum_Classe.None)
                    Local_MakeThree = false;

                if (Local_MakeOne == true || Local_MakeTwo == true || Local_MakeThree == true)
                {
                    foreach (OrigineClasse item in TacheFond.ListOrigineClasse)
                    {
                        if (Local_MakeOne == true && _Champion.GetClasse_One == item.GetClasseType)
                        {
                            this.OrigineClasseOne = item;
                            Local_MakeOne = false;
                        }
                        else if (Local_MakeTwo == true && _Champion.GetClasse_Two == item.GetClasseType)
                        {
                            this.OrigineClasseTwo = item;
                            Local_MakeTwo = false;
                        }
                        else if (Local_MakeThree == true && _Champion.GetClasse_Three == item.GetClasseType)
                        {
                            this.OrigineClasseThree = item;
                            Local_MakeThree = false;
                        }
                    }
                }

                //Met la couleur par le tier
                this.Name_Champion.Foreground = TacheFond.GetColorTier(_Champion.GetTier);
            }
            else
            {
                if (_ByUser)
                {
                    Local_Number--;
                    TacheFond.GetMainRef.GetSetActualNumber = Local_Number.ToString();
                }

                this.Champion_Icon.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                this.Name_Champion.Text = "";

                this.Classe_1.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Classes/Invisible.png", UriKind.Relative));
                this.Classe_2.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Classes/Invisible.png", UriKind.Relative));
                this.Classe_3.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Classes/Invisible.png", UriKind.Relative));

                this.Name_Champion.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8C9095"));
            }
        }

        /*****************************/
        /*** Bouton ******************/
        /*****************************/
        private void Champion_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bool Local_Refresh = false;

            if (this.ChampionOnSlot != null)
            {
                if (e.ChangedButton == MouseButton.Left)
                {
                    if (this.IsUserSlot)
                    {
                        if (TacheFond.GetMainRef.GetPlacementIsOpen)
                        {
                            TacheFond.GetMainRef.GetPlacementRef.SetAttente(this.ChampionOnSlot);

                            return;
                        }

                        this.AddOrRemoveChampion(null, true);

                        TacheFond.RefreshUserProfile();
                        Local_Refresh = true;

                        TacheFond.GetMainRef.GetDetailChampionRef.Hide();
                    }
                    else
                    {
                        if (this.SelfEnabled)
                        {
                            if (TacheFond.GetMainRef.AddChampionToSlot(this.ChampionOnSlot))
                            {
                                TacheFond.RefreshUserProfile();
                                Local_Refresh = true;
                            }
                        }
                    }

                    if (Local_Refresh)
                    {
                        TacheFond.GetMainRef.GetListOfChampionRef.RefreshList();
                    }
                }
                else if (e.ChangedButton == MouseButton.Right)
                {
                    if (TacheFond.GetMainRef.GetPlacementIsOpen)
                    {
                        TacheFond.GetMainRef.ShowHidePlacement(true);
                    }

                    if (this.ChampionOnSlot == TacheFond.GetMainRef.GetGestionItemRef.GetSelectedChampion)
                    {
                        if (TacheFond.GetMainRef.GetGestionItemIsOpen == true)
                            TacheFond.GetMainRef.ForceShowHideGestionItem(false);
                        else
                            TacheFond.GetMainRef.ForceShowHideGestionItem(true);
                    }
                    else
                    {
                        TacheFond.GetMainRef.GetGestionItemRef.RefreshChampion(this.ChampionOnSlot);

                        if (TacheFond.GetMainRef.GetGestionItemIsOpen == false)
                            TacheFond.GetMainRef.ForceShowHideGestionItem(true);
                        else if (TacheFond.GetMainRef.GetListOfChampionVisible == false)
                            TacheFond.GetMainRef.ShowHideListOfChampion(true, false);
                    }
                }
            }
        }

        private void Champion_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.ChampionOnSlot != null)
            {
                TacheFond.GetMainRef.GetDetailChampionRef.UpdateDetailChampion(this.ChampionOnSlot);
                TacheFond.GetMainRef.GetDetailChampionRef.Show();
            }
        }

        private void Champion_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.ChampionOnSlot != null)
                TacheFond.GetMainRef.GetDetailChampionRef.Hide();
        }

        private void Classe_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.ChampionOnSlot != null)
            {
                try
                {
                    int Local = int.Parse(Regex.Replace(((e.Source as FrameworkElement).Name), "[^0-9]+", string.Empty));

                    switch (Local)
                    {
                        case 1:
                            if (this.OrigineClasseOne != null)
                                TacheFond.GetMainRef.GetDetailOrigineClasseRef.UpdateDetailOrigineClasse(this.OrigineClasseOne);
                            break;
                        case 2:
                            if (this.OrigineClasseTwo != null)
                                TacheFond.GetMainRef.GetDetailOrigineClasseRef.UpdateDetailOrigineClasse(this.OrigineClasseTwo);
                            break;
                        case 3:
                            if (this.OrigineClasseThree != null)
                                TacheFond.GetMainRef.GetDetailOrigineClasseRef.UpdateDetailOrigineClasse(this.OrigineClasseThree);
                            break;
                        default:
                            break;
                    }

                    TacheFond.GetMainRef.GetDetailOrigineClasseRef.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, "Error : " + ex.Message);
                }
            }
        }

        private void Classe_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.ChampionOnSlot != null)
                TacheFond.GetMainRef.GetDetailOrigineClasseRef.Hide();
        }

        public void DisabledOrEnable(bool _Disabled)
        {
            if (_Disabled)
                this.Disabler.Visibility = Visibility.Visible;
            else
                this.Disabler.Visibility = Visibility.Hidden;

            this.SelfEnabled = !_Disabled;
        }
    }
}
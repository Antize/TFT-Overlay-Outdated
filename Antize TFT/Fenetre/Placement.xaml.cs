using System;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Antize_TFT.Class;
using Antize_TFT.Enums;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for Placement.xaml
    /// </summary>
    public partial class Placement : Window
    {
        private Champion ChampionEnAttente;

        private readonly ArrayList MySlotPlacement;

        public Placement()
        {
            this.InitializeComponent();

            this.EnAttente.Text = TacheFond.GetLocalizationRef.Placement_Placement;
            this.Reset.Text = TacheFond.GetLocalizationRef.Placement_Reset;

            this.MySlotPlacement = new ArrayList();

            this.Panel_1.Children.Clear();
            this.Panel_2.Children.Clear();
            this.Panel_3.Children.Clear();

            for (int Index = 1; Index <= 7; Index++)
            {
                SlotPlacement TempSlot = new SlotPlacement()
                {
                    Width = 35,
                    Height = 35
                };

                this.Panel_1.Children.Add(TempSlot);
                this.MySlotPlacement.Add(TempSlot);
            }

            for (int Index = 1; Index <= 7; Index++)
            {
                SlotPlacement TempSlot = new SlotPlacement()
                {
                    Width = 35,
                    Height = 35
                };

                this.Panel_2.Children.Add(TempSlot);
                this.MySlotPlacement.Add(TempSlot);
            }

            for (int Index = 1; Index <= 7; Index++)
            {
                SlotPlacement TempSlot = new SlotPlacement()
                {
                    Width = 35,
                    Height = 35
                };

                this.Panel_3.Children.Add(TempSlot);
                this.MySlotPlacement.Add(TempSlot);
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            TacheFond.GetMainRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            TacheFond.GetMainRef.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
        }

        //Drag window
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ChangedButton == MouseButton.Left)
                    this.DragMove();
            }
            catch
            {

            }
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            TacheFond.RealignerFenetre(Enum_State.MovedFromPlacement, false);
        }

        public Champion GetChampionEnAttente
        {
            get { return this.ChampionEnAttente; }
        }

        public void SetAttente(Champion _Champion)
        {
            if (_Champion != null)
            {
                this.ChampionEnAttente = _Champion;
                this.Attente.Source = this.ChampionEnAttente.GetIcon;
            }
            else
            {
                this.ChampionEnAttente = null;
                this.Attente.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/empty.png", UriKind.Relative));
            }
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ResetPlacement(true);
        }

        public ArrayList GetPlacement()
        {
            ArrayList Local_TempPlacement = new ArrayList
            {
                -1000
            };

            foreach (SlotPlacement Slot in this.MySlotPlacement)
            {
                if (Slot.GetChampionOnSlot != null)
                {
                    Local_TempPlacement.Add(Slot.GetChampionOnSlot.GetID);
                }
                else
                    Local_TempPlacement.Add(-999);
            }

            return Local_TempPlacement;
        }

        private void ResetPlacement(bool _ByUser)
        {
            foreach (SlotPlacement Slot in this.MySlotPlacement)
            {
                Slot.SetChampionOnPlacement(null, false);
            }

            if (_ByUser)
                TacheFond.RefreshUserProfile();
        }

        public void RefreshPlacement(ArrayList _Load)
        {
            this.ResetPlacement(false);
            this.SetAttente(null);

            try
            {
                for (int IDProfile = _Load.Count - 21, Index = 0; IDProfile < _Load.Count; IDProfile++, Index++)
                {
                    if ((int)_Load[IDProfile] != -999)
                    {
                        ((SlotPlacement)this.MySlotPlacement[Index]).SetChampionOnPlacement((Champion)TacheFond.ListChampions[(int)_Load[IDProfile]], false);
                    }
                }
            }
            catch
            {

            }
        }
    }
}
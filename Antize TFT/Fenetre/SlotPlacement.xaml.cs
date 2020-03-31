using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Antize_TFT.Class;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for SlotPlacement.xaml
    /// </summary>
    public partial class SlotPlacement : UserControl
    {
        private Champion ChampionOnSlot;

        public SlotPlacement()
        {
            this.InitializeComponent();
        }

        public Champion GetChampionOnSlot
        {
            get { return this.ChampionOnSlot; }
        }

        private void Icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (TacheFond.GetMainRef.GetPlacementRef.GetChampionEnAttente != null)
                {
                    this.SetChampionOnPlacement(TacheFond.GetMainRef.GetPlacementRef.GetChampionEnAttente, true);
                }
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                this.SetChampionOnPlacement(null, true);
            }
        }

        public void SetChampionOnPlacement(Champion _Champion, bool _ByUser)
        {
            if (_Champion != null)
            {
                this.ChampionOnSlot = _Champion;
                this.ChampionIcon.Source = this.ChampionOnSlot.GetIcon;
                this.ToolTip = this.ChampionOnSlot.GetName;
                ToolTipService.SetIsEnabled(this, true);
            }
            else
            {
                this.ChampionOnSlot = null;
                this.ChampionIcon.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/empty.png", UriKind.Relative));
                this.ToolTip = "";
                ToolTipService.SetIsEnabled(this, false);
            }

            if (_ByUser)
                TacheFond.RefreshUserProfile();
        }
    }
}

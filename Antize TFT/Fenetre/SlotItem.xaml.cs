using System.Windows.Controls;
using System.Windows.Input;
using Antize_TFT.Class;
using Antize_TFT.Enums;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for SlotItem.xaml
    /// </summary>
    public partial class SlotItem : UserControl
    {
        private readonly GestionItem GestionItemRef;

        private readonly Items ItemOnSlot;

        public SlotItem(GestionItem _GestionItemRef, Items _ItemOnSlot)
        {
            this.InitializeComponent();

            this.GestionItemRef = _GestionItemRef;
            this.ItemOnSlot = _ItemOnSlot;

            this.ItemIcon.Source = _ItemOnSlot.GetIcon;
        }

        public Items GetItemOnSlot
        {
            get { return this.ItemOnSlot; }
        }

        private void ItemIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            this.GestionItemRef.HoveredItem(this.ItemOnSlot);
        }

        private void ItemIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GestionItemRef.HoveredItem(null);
        }

        private void ItemIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                if (this.GetItemOnSlot != null)
                {
                    this.GestionItemRef.AddItemToChampion(this.ItemOnSlot);
                }
            }
            else if (e.ChangedButton == MouseButton.Right)
            {
                if (this.GetItemOnSlot.GetNeeded_Type1 == Enum_Item.None)
                {
                    TacheFond.LastItemClicked = this.GetItemOnSlot.GetItemType;
                    TacheFond.GetMainRef.GetListOfChampionRef.SwitchList(TacheFond.LastItemClicked);
                }
            }
        }
    }
}
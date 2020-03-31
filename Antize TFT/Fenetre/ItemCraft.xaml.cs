using Antize_TFT.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for ItemCraft.xaml
    /// </summary>
    public partial class ItemCraft : UserControl
    {
        public ItemCraft(Items _Items, Enums.Enum_Item _Base)
        {
            this.InitializeComponent();

            this.Item.Source = _Items.GetIcon;

            if (_Items.GetNeeded_Type1 == _Base)
            {
                this.Item_Need1.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{_Items.GetNeeded_Type1}.png", UriKind.Relative));
                this.Item_Need2.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{_Items.GetNeeded_Type2}.png", UriKind.Relative));
            }
            else
            {
                this.Item_Need2.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{_Items.GetNeeded_Type1}.png", UriKind.Relative));
                this.Item_Need1.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{_Items.GetNeeded_Type2}.png", UriKind.Relative));
            }

            this.Description.Text = _Items.GetDescription;
        }
    }
}
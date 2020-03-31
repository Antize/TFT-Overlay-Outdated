using Antize_TFT.Enums;
using System;
using System.Windows.Media.Imaging;

namespace Antize_TFT.Class
{
    public class Items
    {
        private readonly Enum_Item ItemType;
        private readonly BitmapImage ItemIcon;

        private readonly string Name;

        private readonly string Description;

        private readonly Enum_Item Needed_Type1;
        private readonly Enum_Item Needed_Type2;

        public Items()
        {

        }

        public Items(string _Name, Enum_Item _ItemType, Enum_Item _Needed_Type1, Enum_Item _Needed_Type2, string _Description)
        {
            this.Name = _Name;
            this.Description = _Description;

            this.ItemType = _ItemType;
            this.ItemIcon = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{this.ItemType.ToString()}.png", UriKind.Relative));

            this.Needed_Type1 = _Needed_Type1;
            this.Needed_Type2 = _Needed_Type2;
        }

        public int GetID
        {
            get; set;
        }

        public BitmapImage GetIcon
        {
            get { return this.ItemIcon; }
        }

        public string GetName
        {
            get { return this.Name; }
        }

        public string GetDescription
        {
            get { return this.Description; }
        }

        public Enum_Item GetItemType
        {
            get { return this.ItemType; }
        }

        public Enum_Item GetNeeded_Type1
        {
            get { return this.Needed_Type1; }
        }

        public Enum_Item GetNeeded_Type2
        {
            get { return this.Needed_Type2; }
        }
    }
}
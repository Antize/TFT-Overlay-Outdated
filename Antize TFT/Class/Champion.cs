using System;
using System.Windows.Media.Imaging;
using Antize_TFT.Enums;

namespace Antize_TFT.Class
{
    public class Champion
    {
        private readonly BitmapImage ChampionIcon;

        private readonly string Name;
        private readonly string Health;
        private readonly string Damage;

        private readonly Enum_Classe Classe_TypeOne;
        private readonly Enum_Classe Classe_TypeTwo;
        private readonly Enum_Classe Classe_TypeThree;

        private readonly int Tier;

        public Champion()
        {

        }

        public Champion(int _Tier, string _Name, string _Health, string _Damage, Enum_Classe _Classe_TypeOne, Enum_Classe _Classe_TypeTwo, Enum_Classe _Classe_TypeThree, string _IconPath)
        {
            this.ChampionIcon = new BitmapImage(new Uri(_IconPath, UriKind.RelativeOrAbsolute));

            this.Name = _Name;
            this.Health = _Health;
            this.Damage = _Damage;

            this.Classe_TypeOne = _Classe_TypeOne;
            this.Classe_TypeTwo = _Classe_TypeTwo;
            this.Classe_TypeThree = _Classe_TypeThree;

            this.Tier = _Tier;

            this.MyItem = new Items[3] { null, null, null };

            this.GetDebugPath = _IconPath;
        }

        public Items[] MyItem
        {
            get; set;
        }

        public int GetID
        {
            get; set;
        }

        public BitmapImage GetIcon
        {
            get { return this.ChampionIcon; }
        }

        public string GetName
        {
            get { return this.Name; }
        }

        public string GetHealth
        {
            get { return this.Health; }
        }

        public string GetDamage
        {
            get { return this.Damage; }
        }

        public Enum_Classe GetClasse_One
        {
            get { return this.Classe_TypeOne; }
        }

        public Enum_Classe GetClasse_Two
        {
            get { return this.Classe_TypeTwo; }
        }

        public Enum_Classe GetClasse_Three
        {
            get { return this.Classe_TypeThree; }
        }

        public int GetTier
        {
            get { return this.Tier; }
        }

        public string GetDebugPath
        {
            get; set;
        }
    }
}
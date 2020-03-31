using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;
using Antize_TFT.Enums;

namespace Antize_TFT.Class
{
    public class OrigineClasse
    {
        private readonly BitmapImage Icon;

        private readonly string Name;
        private readonly string Description;

        private readonly Enum_Classe ClasseType;
        private readonly bool IsClasse;

        private readonly Dictionary<int, string> Bonus;

        public OrigineClasse(Enum_Classe _ClasseType, bool _IsClasse, string _Description, Dictionary<int, string> _Bonus)
        {
            this.ClasseType = _ClasseType;
            this.Name = this.ClasseType.ToString();
            this.Icon = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{this.ClasseType}.png", UriKind.Relative));

            this.Description = _Description;
            this.Bonus = _Bonus;

            this.IsClasse = _IsClasse;
        }

        public int GetID
        {
            get; set;
        }

        public BitmapImage GetIcon
        {
            get { return this.Icon; }
        }

        public string GetName
        {
            get { return this.Name; }
        }

        public string GetDescription
        {
            get { return this.Description; }
        }

        public Enum_Classe GetClasseType
        {
            get { return this.ClasseType; }
        }

        public Dictionary<int, string> GetBonus
        {
            get { return this.Bonus; }
        }

        public bool GetIsClasse
        {
            get { return this.IsClasse; }
        }
    }
}

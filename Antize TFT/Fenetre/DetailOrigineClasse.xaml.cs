using Antize_TFT.Class;
using System.Linq;
using System.Windows;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for DetailOrigineClasse.xaml
    /// </summary>
    public partial class DetailOrigineClasse : Window
    {
        private int NumberChamp;

        public DetailOrigineClasse()
        {
            this.InitializeComponent();

            this.Reset();
        }

        private void Reset()
        {
            this.Classe_Name.Text = "";
            this.Classe_Icon.Source = null;
            this.Classe_Description.Text = "";

            this.Need_1.Text = "";
            this.Effect_1.Text = "";

            this.Need_2.Text = "";
            this.Effect_2.Text = "";

            this.Need_3.Text = "";
            this.Effect_3.Text = "";
        }

        private void RefreshSize()
        {
            int Local_Multi = (int)System.Math.Ceiling((double)this.NumberChamp / (double)2);

            this.Width = 330 + (Local_Multi * 72) + 72;

            TacheFond.RealignerOnRefreshSize();
        }

        public void UpdateDetailOrigineClasse(OrigineClasse _Classe)
        {
            this.Reset();

            if (_Classe != null)
            {
                if (_Classe.GetIsClasse)
                    this.Classe_Type.Text = "Classe";
                else
                    this.Classe_Type.Text = "Origin";

                if (_Classe.GetDescription.Replace(" ", "").Length <= 0)
                {
                    this.Main.RowDefinitions[0].Height = new GridLength(45);
                    this.Classe_Description.Text = "";
                }
                else
                {
                    this.Main.RowDefinitions[0].Height = new GridLength(70);
                    this.Classe_Description.Text = _Classe.GetDescription;
                }                    

                //Attribue les informations
                this.Classe_Name.Text = _Classe.GetName;
                this.Classe_Icon.Source = _Classe.GetIcon;               

                if (_Classe.GetBonus.ElementAt(1).Key < 0)
                {
                    this.Need_1.Text = _Classe.GetBonus.ElementAt(0).Key.ToString();
                    this.Effect_1.Text = _Classe.GetBonus.ElementAt(0).Value;
                }
                else if (_Classe.GetBonus.ElementAt(2).Key < 0)
                {
                    this.Need_1.Text = _Classe.GetBonus.ElementAt(0).Key.ToString();
                    this.Effect_1.Text = _Classe.GetBonus.ElementAt(0).Value;

                    this.Need_2.Text = _Classe.GetBonus.ElementAt(1).Key.ToString();
                    this.Effect_2.Text = _Classe.GetBonus.ElementAt(1).Value;
                }
                else
                {
                    this.Need_1.Text = _Classe.GetBonus.ElementAt(0).Key.ToString();
                    this.Effect_1.Text = _Classe.GetBonus.ElementAt(0).Value;

                    this.Need_2.Text = _Classe.GetBonus.ElementAt(1).Key.ToString();
                    this.Effect_2.Text = _Classe.GetBonus.ElementAt(1).Value;

                    this.Need_3.Text = _Classe.GetBonus.ElementAt(2).Key.ToString();
                    this.Effect_3.Text = _Classe.GetBonus.ElementAt(2).Value;
                }

                this.WrapPanel_Champions.Children.Clear();

                this.NumberChamp = 0;

                foreach (Champion Champ in TacheFond.ListChampions)
                {
                    bool Local_AlreadyTake = false;

                    if (_Classe.GetClasseType == Champ.GetClasse_One || _Classe.GetClasseType == Champ.GetClasse_Two || _Classe.GetClasseType == Champ.GetClasse_Three)
                    {
                        foreach (SlotChampion SlotChamp in TacheFond.GetMainRef.SlotChampion)
                        {
                            if (SlotChamp.ChampionOnSlot != null)
                            {
                                if (SlotChamp.ChampionOnSlot == Champ)
                                {
                                    Local_AlreadyTake = true;
                                    break;
                                }
                            }
                        }

                        this.WrapPanel_Champions.Children.Add(new ChampionOnClasseOrigine(Champ, Local_AlreadyTake)
                        {
                            Height = 70,
                            Width = 72
                        });

                        this.NumberChamp++;
                    }
                }

                this.RefreshSize();
            }
        }
    }
}

using Antize_TFT.Class;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for MyCheckBox.xaml
    /// </summary>
    public partial class MyCheckBox : UserControl
    {
        private readonly ListOfChampion ListOfChampionRef;
        private readonly Enums.Enum_Classe EnumType;

        private readonly OrigineClasse SelfOriginClass;

        private readonly int[] BonusNeed;

        private readonly int NmbrPalier;

        private int ActualOnType;

        private bool ImChecked;

        public MyCheckBox(Enums.Enum_Classe _EnumType, ListOfChampion _ListOfChampion)
        {
            this.InitializeComponent();

            this.ActualOnType = 0;

            this.EnumType = _EnumType;
            this.ListOfChampionRef = _ListOfChampion;

            this.ChangeMeCheck(true);

            if (this.EnumType == Enums.Enum_Classe.None)
            {
                this.Bonus.Visibility = Visibility.Collapsed;
                this.Icon.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/All.png", UriKind.Relative));
            }
            else
            {
                this.BonusNeed = new int[3] { -1, -1, -1 };

                this.Icon.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Classes/{this.EnumType}.png", UriKind.Relative));

                foreach (OrigineClasse Classe in TacheFond.ListOrigineClasse)
                {
                    if (Classe.GetClasseType == this.EnumType)
                    {
                        this.SelfOriginClass = Classe;

                        int Local_Index = 0;

                        foreach (int item1 in Classe.GetBonus.Keys)
                        {
                            this.BonusNeed[Local_Index] = item1;

                            Local_Index++;
                        }

                        if (this.BonusNeed[2] >= 0)
                        {
                            this.Max.Text = this.BonusNeed[2].ToString();
                            this.NmbrPalier = 2;
                        }
                        else if (this.BonusNeed[1] >= 0)
                        {
                            this.Max.Text = this.BonusNeed[1].ToString();
                            this.NmbrPalier = 1;
                        }
                        else
                        {
                            this.Max.Text = this.BonusNeed[0].ToString();
                            this.NmbrPalier = 0;
                        }

                        break;
                    }
                }

                this.ImageForMouse.ToolTip = TacheFond.GetLocalizationRef.MyCheckBox_IconToolTip + this.EnumType;
            }
        }

        public void RefreshNumberOnType(bool _Reset)
        {
            if (this.EnumType != Enums.Enum_Classe.None)
            {
                SolidColorBrush Local_TempColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#515151"));

                if (_Reset)
                {
                    this.ActualOnType = 0;
                }
                else
                {
                    this.ActualOnType++;
                }

                if (this.ActualOnType != 0 && this.ActualOnType < this.BonusNeed[0])
                {
                    Local_TempColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#ffffff"));
                }
                else if (this.ActualOnType > 0)
                {
                    switch (this.NmbrPalier)
                    {
                        case 0:
                            if (this.ActualOnType >= this.BonusNeed[0])
                                Local_TempColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD800"));
                            break;
                        case 1:
                            if (this.ActualOnType >= this.BonusNeed[1])
                                Local_TempColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD800"));
                            else if (this.ActualOnType >= this.BonusNeed[0])
                                Local_TempColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6A00"));
                            break;
                        case 2:
                            if (this.ActualOnType >= this.BonusNeed[2])
                                Local_TempColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFD800"));
                            else if (this.ActualOnType >= this.BonusNeed[1])
                                Local_TempColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#B6FF00"));
                            else if (this.ActualOnType >= this.BonusNeed[0])
                                Local_TempColor = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF6A00"));
                            break;
                        default:
                            break;
                    }
                }

                this.Actual.Foreground = Local_TempColor;
                this.Sep.Foreground = Local_TempColor;
                this.Max.Foreground = Local_TempColor;

                this.Actual.Text = this.ActualOnType.ToString();
            }
        }

        public void ChangeMeCheck(bool _ImChecked)
        {
            this.ImChecked = _ImChecked;

            if (_ImChecked)
                BorderStatut.BorderThickness = new Thickness(1);
            else
                BorderStatut.BorderThickness = new Thickness(0);
        }

        public Enums.Enum_Classe GetEnumType
        {
            get { return this.EnumType; }
        }

        public bool GetImChecked
        {
            get { return this.ImChecked; }
        }

        private void Icon_MouseEnter(object sender, MouseEventArgs e)
        {
            if (this.EnumType != Enums.Enum_Classe.None)
            {
                TacheFond.GetMainRef.GetDetailOrigineClasseRef.UpdateDetailOrigineClasse(this.SelfOriginClass);
                TacheFond.GetMainRef.GetDetailOrigineClasseRef.Show();
            }
        }

        private void Icon_MouseLeave(object sender, MouseEventArgs e)
        {
            if (this.EnumType != Enums.Enum_Classe.None)
            {
                TacheFond.GetMainRef.GetDetailOrigineClasseRef.Hide();
            }
        }

        private void Icon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.EnumType == Enums.Enum_Classe.None)
            {
                this.ChangeMeCheck(true);
                this.ListOfChampionRef.CheckedAll(true, this.EnumType);
            }
            else
            {
                if (e.ChangedButton == MouseButton.Right)
                {
                    this.ChangeMeCheck(true);
                    this.ListOfChampionRef.CheckedAll(false, this.EnumType);
                }
                else if (e.ChangedButton == MouseButton.Left)
                {
                    this.ChangeMeCheck(!this.ImChecked);
                    this.ListOfChampionRef.CheckedModified();
                }
            }
        }
    }
}
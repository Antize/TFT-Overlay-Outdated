using System;
using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Antize_TFT.Class;
using Antize_TFT.Enums;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for ListOfChampion.xaml
    /// </summary>
    public partial class ListOfChampion : Window
    {
        private readonly ArrayList ListSlotChampion;
        private readonly ArrayList ListMyCheckBox;

        private bool IsOnListChampion;

        private int FilterLastLenght;
        private bool SecureFilter;
        private ArrayList ChampionOnCurrentFilter;

        public ListOfChampion()
        {
            this.InitializeComponent();

            this.ListSlotChampion = new ArrayList();
            this.ListMyCheckBox = new ArrayList();

            this.IsOnListChampion = true;

            this.FilterLastLenght = 0;
            this.SecureFilter = false;
            this.ChampionOnCurrentFilter = new ArrayList();

            this.Intialize();
        }

        public void Intialize()
        {
            foreach (Champion Champ in TacheFond.ListChampions)
            {
                SlotChampion Local_SlotChampion = new SlotChampion(0, false)
                {
                    Height = 80,
                    Width = 86
                };

                Local_SlotChampion.AddOrRemoveChampion(Champ, false);

                switch (Champ.GetTier)
                {
                    case 1:
                        this.WrapPanel_ChampionsTier1.Children.Add(Local_SlotChampion);
                        break;
                    case 2:
                        this.WrapPanel_ChampionsTier2.Children.Add(Local_SlotChampion);
                        break;
                    case 3:
                        this.WrapPanel_ChampionsTier3.Children.Add(Local_SlotChampion);
                        break;
                    case 4:
                        this.WrapPanel_ChampionsTier4.Children.Add(Local_SlotChampion);
                        break;
                    case 5:
                        this.WrapPanel_ChampionsTier5.Children.Add(Local_SlotChampion);
                        break;
                    case 7:
                        this.WrapPanel_ChampionsTier7.Children.Add(Local_SlotChampion);
                        break;
                    default:
                        break;
                }

                this.ListSlotChampion.Add(Local_SlotChampion);
                this.ChampionOnCurrentFilter.Add(Local_SlotChampion);

                this.Actual_Listed.Text = $"{this.ChampionOnCurrentFilter.Count.ToString()}/{this.ListSlotChampion.Count.ToString()}";
            }

            MyCheckBox Local_MyCheckBox = new MyCheckBox(Enum_Classe.None, this)
            {
                Height = 45,
                Width = 80
            };

            this.WrapPanel_Check.Children.Add(Local_MyCheckBox);
            this.ListMyCheckBox.Add(Local_MyCheckBox);

            foreach (Enum_Classe MyEnum in (Enum_Classe[])Enum.GetValues(typeof(Enum_Classe)))
            {
                if (MyEnum != Enum_Classe.None)
                {
                    Local_MyCheckBox = new MyCheckBox(MyEnum, this)
                    {
                        Height = 45,
                        Width = 80
                    };

                    this.WrapPanel_Check.Children.Add(Local_MyCheckBox);
                    this.ListMyCheckBox.Add(Local_MyCheckBox);
                }
            }
        }

        public bool GetIsOnListChampion
        {
            get { return this.IsOnListChampion; }
        }

        public void RefreshList()
        {
            ArrayList Local_ChampionMake = new ArrayList();

            //Active tous les slots
            foreach (SlotChampion SlotChamp in this.ListSlotChampion)
            {
                SlotChamp.DisabledOrEnable(false);
            }

            //Reset le nombre de classes/origins
            foreach (MyCheckBox MyCheck in this.ListMyCheckBox)
            {
                MyCheck.RefreshNumberOnType(true);
            }

            foreach (SlotChampion SlotChamp in TacheFond.GetMainRef.SlotChampion)
            {
                if (SlotChamp.ChampionOnSlot != null)
                {
                    if (TacheFond.GetUserSettingsRef.OnlyOneType)
                    {
                        foreach (SlotChampion SlotThis in this.ListSlotChampion)
                        {
                            if (SlotThis.ChampionOnSlot == SlotChamp.ChampionOnSlot)
                            {
                                SlotThis.DisabledOrEnable(true);
                                break;
                            }
                        }
                    }

                    if (Local_ChampionMake.Contains(SlotChamp.ChampionOnSlot) == false)
                    {
                        foreach (MyCheckBox MyCheck in this.ListMyCheckBox)
                        {
                            if (SlotChamp.ChampionOnSlot.GetClasse_One == MyCheck.GetEnumType || SlotChamp.ChampionOnSlot.GetClasse_Two == MyCheck.GetEnumType || SlotChamp.ChampionOnSlot.GetClasse_Three == MyCheck.GetEnumType)
                            {
                                MyCheck.RefreshNumberOnType(false);
                            }
                        }

                        Local_ChampionMake.Add(SlotChamp.ChampionOnSlot);
                    }
                }
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            TacheFond.GetMainRef.GetGestionItemRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
            TacheFond.GetMainRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            TacheFond.GetMainRef.GetGestionItemRef.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
            TacheFond.GetMainRef.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;

            TacheFond.GetMainRef.GetDetailChampionRef.Hide();
            TacheFond.GetMainRef.GetDetailOrigineClasseRef.Hide();
        }

        //stackoverflow.com/questions/3600874/window-actualtop-actualleft
        public double GetWindowLeft()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                FieldInfo leftField = typeof(Window).GetField("_actualLeft", BindingFlags.NonPublic | BindingFlags.Instance);
                return (double)leftField.GetValue(this);
            }
            else
                return this.Left;
        }

        public double GetWindowTop()
        {
            if (this.WindowState == WindowState.Maximized)
            {
                FieldInfo topField = typeof(Window).GetField("_actualTop", BindingFlags.NonPublic | BindingFlags.Instance);
                return (double)topField.GetValue(this);
            }
            else
                return this.Top;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TacheFond.RealignerFenetre(Enum_State.SizedFromListChamp, false);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            TacheFond.RealignerFenetre(Enum_State.MovedFromListChamp, false);
        }

        public void CheckedAll(bool _CheckAll, Enum_Classe _OwnerClasse)
        {
            foreach (MyCheckBox CheckBox in this.ListMyCheckBox)
            {
                if (CheckBox.GetEnumType != _OwnerClasse)
                {
                    CheckBox.ChangeMeCheck(_CheckAll);
                }
            }

            if (_CheckAll)
            {
                this.ChampionOnCurrentFilter = new ArrayList();

                foreach (SlotChampion SlotChamp in this.ListSlotChampion)
                {
                    SlotChamp.Visibility = Visibility.Visible;
                    this.ChampionOnCurrentFilter.Add(SlotChamp);
                }

                this.Actual_Listed.Text = $"{this.ChampionOnCurrentFilter.Count.ToString()}/{this.ListSlotChampion.Count.ToString()}";
                this.ApplyFilter(false);
            }
            else
            {
                this.CheckedModified();
            }
        }

        public void CheckedModified()
        {
            this.ChampionOnCurrentFilter = new ArrayList();

            bool Local_All = true;

            foreach (SlotChampion item in this.ListSlotChampion)
            {
                item.Visibility = Visibility.Collapsed;
            }

            foreach (MyCheckBox CheckBox in this.ListMyCheckBox)
            {
                if (CheckBox.GetEnumType != Enum_Classe.None)
                {
                    if (CheckBox.GetImChecked == true)
                    {
                        foreach (SlotChampion SlotChamp in this.ListSlotChampion)
                        {
                            if (SlotChamp.ChampionOnSlot != null && this.ChampionOnCurrentFilter.Contains(SlotChamp) == false)
                            {
                                if (SlotChamp.ChampionOnSlot.GetClasse_One == CheckBox.GetEnumType || SlotChamp.ChampionOnSlot.GetClasse_Two == CheckBox.GetEnumType || SlotChamp.ChampionOnSlot.GetClasse_Three == CheckBox.GetEnumType)
                                {
                                    SlotChamp.Visibility = Visibility.Visible;
                                    this.ChampionOnCurrentFilter.Add(SlotChamp);
                                }
                            }
                        }
                    }

                    if (Local_All == true)
                    {
                        if (CheckBox.GetImChecked == false)
                        {
                            Local_All = false;
                        }
                    }
                }
            }

            ((MyCheckBox)this.ListMyCheckBox[0]).ChangeMeCheck(Local_All);

            this.Actual_Listed.Text = $"{this.ChampionOnCurrentFilter.Count.ToString()}/{ this.ListSlotChampion.Count.ToString()}";
            this.ApplyFilter(false);
        }

        public void SwitchList(Enum_Item _ItemsType)
        {
            if (_ItemsType != Enum_Item.None)
            {
                this.WrapPanel_ItemCraft.Children.Clear();

                foreach (Items Item in TacheFond.ListItems)
                {
                    if (Item.GetNeeded_Type1 != Enum_Item.None)
                    {
                        if (_ItemsType == Item.GetNeeded_Type1 || _ItemsType == Item.GetNeeded_Type2)
                        {
                            this.WrapPanel_ItemCraft.Children.Add(new ItemCraft(Item, _ItemsType) { Height = 60 });
                        }
                    }
                }

                TacheFond.GetMainRef.Image_Item.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Armor.png", UriKind.Relative));

                this.IsOnListChampion = false;

                this.ListItemCraft.Visibility = Visibility.Visible;
                this.ListChampion.Visibility = Visibility.Hidden;
            }
            else
            {
                this.IsOnListChampion = true;

                this.ListItemCraft.Visibility = Visibility.Hidden;
                this.ListChampion.Visibility = Visibility.Visible;
            }
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

        private void TextBox_Filter_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (this.SecureFilter == true)
            {
                this.SecureFilter = false;
            }
            else
            {
                if (this.TextBox_Filter.Text.Replace(" ", string.Empty).Length > 0)
                {
                    this.FilterLastLenght = this.TextBox_Filter.Text.Length;
                }

                this.ApplyFilter(true);
            }
        }

        private void ApplyFilter(bool _ByUser)
        {
            if (this.TextBox_Filter.Text.Replace(" ", string.Empty).Length <= 0)
            {
                if (_ByUser)
                {
                    this.Actual_Listed.Text = $"{this.ChampionOnCurrentFilter.Count.ToString()}/{this.ListSlotChampion.Count.ToString()}";

                    if (this.TextBox_Filter.Text.Length > 0)
                    {
                        bool Local_Return = this.FilterLastLenght <= 0;

                        this.SecureFilter = true;
                        this.FilterLastLenght = 0;
                        this.TextBox_Filter.Text = "";

                        if (Local_Return == true)
                        {
                            return;
                        }
                    }

                    foreach (SlotChampion SlotChamp in this.ListSlotChampion)
                    {
                        if (this.ChampionOnCurrentFilter.Contains(SlotChamp))
                        {
                            SlotChamp.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            else
            {
                foreach (SlotChampion item in this.ListSlotChampion)
                {
                    item.Visibility = Visibility.Collapsed;
                }

                string TextInFilter = this.TextBox_Filter.Text.ToUpper();

                try
                {
                    int Local_Count = 0;

                    foreach (SlotChampion ChampInFilter in this.ChampionOnCurrentFilter)
                    {
                        if (ChampInFilter.ChampionOnSlot.GetName.ToUpper().Contains(TextInFilter))
                        {
                            if (ChampInFilter.Visibility == Visibility.Collapsed)
                            {
                                ChampInFilter.Visibility = Visibility.Visible;
                                Local_Count++;
                            }
                        }
                        else if (ChampInFilter.ChampionOnSlot.GetClasse_One.ToString().ToUpper().Contains(TextInFilter) || ChampInFilter.ChampionOnSlot.GetClasse_Two.ToString().ToUpper().Contains(TextInFilter) || ChampInFilter.ChampionOnSlot.GetClasse_Three.ToString().ToUpper().Contains(TextInFilter))
                        {
                            if (ChampInFilter.Visibility == Visibility.Collapsed)
                            {
                                ChampInFilter.Visibility = Visibility.Visible;
                                Local_Count++;
                            }
                        }
                    }

                    this.Actual_Listed.Text = $"{Local_Count}/{this.ListSlotChampion.Count.ToString()}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"Error filter slot : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);

                    try
                    {
                        foreach (SlotChampion item in this.ListSlotChampion)
                        {
                            item.Visibility = Visibility.Visible;
                        }

                        this.ChampionOnCurrentFilter = new ArrayList();
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void Image_ShowHideFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.ButtonText_ShowHideFilter.Text.Contains("+"))
            {
                this.ButtonText_ShowHideFilter.Text = "-";
                this.WrapPanel_Check.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.ButtonText_ShowHideFilter.Text = "+";
                this.WrapPanel_Check.Visibility = Visibility.Visible;
            }
        }
    }
}
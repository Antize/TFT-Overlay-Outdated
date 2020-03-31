using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Antize_TFT.Class;
using Antize_TFT.Enums;
using System.Text.RegularExpressions;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for GestionItem.xaml
    /// </summary>
    public partial class GestionItem : Window
    {
        private Champion SelectedChampion;

        private int FilterLastLenght;
        private bool SecureFilter;

        public GestionItem()
        {
            this.InitializeComponent();

            this.ListSlotItems = new ArrayList();

            this.FilterLastLenght = 0;
            this.SecureFilter = false;
        }

        public ArrayList ListSlotItems
        {
            get; set;
        }

        public Champion GetSelectedChampion
        {
            get { return this.SelectedChampion; }
        }

        public void Intialize()
        {
            foreach (Items Item in TacheFond.ListItems)
            {
                SlotItem Local_SlotItem = new SlotItem(this, Item)
                {
                    Height = 60,
                    Width = 60
                };

                //Ajoute soit dans les items de base ou les objets à combiner
                if (Item.GetNeeded_Type1 == Enum_Item.None)
                {
                    Local_SlotItem.ToolTip = TacheFond.GetLocalizationRef.SlotItem_IconToolTip;
                    this.WrapPanel_Base.Children.Add(Local_SlotItem);
                }
                else
                    this.WrapPanel_Combined.Children.Add(Local_SlotItem);

                this.ListSlotItems.Add(Local_SlotItem);
            }

            this.RefreshChampion(null);
            this.HoveredItem(null);
        }

        public void RefreshChampion(Champion _Champion)
        {
            this.SelectedChampion = _Champion;

            if (this.SelectedChampion != null)
            {
                this.Champion_Icon.Source = this.SelectedChampion.GetIcon;
                this.Champion_Name.Text = this.SelectedChampion.GetName;

                //Rafraichi les items
                int Local_index = 1;

                foreach (Items Item in this.SelectedChampion.MyItem)
                {
                    BitmapImage Local_Icon = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));

                    if (Item != null)
                    {
                        Local_Icon = Item.GetIcon;
                    }

                    switch (Local_index)
                    {
                        case 1:
                            this.Stuff1.Source = Local_Icon;
                            break;
                        case 2:
                            this.Stuff2.Source = Local_Icon;
                            break;
                        case 3:
                            this.Stuff3.Source = Local_Icon;
                            break;
                        default:
                            break;
                    }

                    Local_index++;
                }

                //Met la couleur par le tier
                this.Champion_Name.Foreground = TacheFond.GetColorTier(this.SelectedChampion.GetTier);
            }
            else
            {
                this.Champion_Icon.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                this.Champion_Name.Text = "";

                this.Stuff1.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                this.Stuff2.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                this.Stuff3.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));

                this.Champion_Name.Foreground = TacheFond.GetColorTier(0);
            }
        }

        public void HoveredItem(Items _Item)
        {
            if (_Item != null)
            {
                this.Item_Name.Text = _Item.GetName;
                this.Item_Icon.Source = _Item.GetIcon;
                this.Item_Description.Text = _Item.GetDescription;

                if (_Item.GetNeeded_Type1 != Enum_Item.None)
                    this.Need1.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{_Item.GetNeeded_Type1}.png", UriKind.Relative));
                else
                    this.Need1.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));

                if (_Item.GetNeeded_Type2 != Enum_Item.None)
                    this.Need2.Source = new BitmapImage(new Uri($"/Antize TFT;component/Ressources/Items/{_Item.GetNeeded_Type2}.png", UriKind.Relative));
                else
                    this.Need2.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
            }
            else
            {
                this.Item_Name.Text = "";
                this.Item_Icon.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                this.Item_Description.Text = "";

                this.Need1.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
                this.Need2.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Empty.png", UriKind.Relative));
            }
        }

        private void Stuff_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Right)
            {
                if (this.SelectedChampion != null)
                {
                    int Local_Index = int.Parse(Regex.Replace(((e.Source as FrameworkElement).Name), "[^0-9]+", string.Empty));

                    if (this.SelectedChampion.MyItem[Local_Index - 1] != null)
                    {
                        this.SelectedChampion.MyItem[Local_Index - 1] = null;
                        this.RefreshChampion(this.SelectedChampion);
                    }
                }
            }
        }

        public void AddItemToChampion(Items _Item)
        {
            if (this.SelectedChampion != null)
            {
                int Local_Index = 0;

                foreach (Items ChampItems in this.SelectedChampion.MyItem)
                {
                    if (ChampItems == null)
                    {
                        this.SelectedChampion.MyItem[Local_Index] = _Item;

                        this.RefreshChampion(this.SelectedChampion);
                        break;
                    }

                    Local_Index++;
                }
            }
        }

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            TacheFond.GetMainRef.GetListOfChampionRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
            TacheFond.GetMainRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            TacheFond.GetMainRef.GetListOfChampionRef.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
            TacheFond.GetMainRef.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;

            TacheFond.GetMainRef.GetDetailChampionRef.Hide();
            TacheFond.GetMainRef.GetDetailOrigineClasseRef.Hide();
        }

        private void Stuff_MouseEnter(object sender, MouseEventArgs e)
        {
            string Local_Path = ((Image)sender).Source.ToString();

            foreach (Items item in TacheFond.ListItems)
            {
                if (Local_Path.Contains(item.GetIcon.ToString()))
                {
                    this.HoveredItem(item);
                    break;
                }
            }
        }

        private void Stuff_MouseLeave(object sender, MouseEventArgs e)
        {
            this.HoveredItem(null);
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
            TacheFond.RealignerFenetre(Enum_State.MovedFromGestionItem, false);
        }

        private void TextBox_Filter_TextChanged(object sender, TextChangedEventArgs e)
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

                try
                {
                    this.ApplyFilter();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"Error ApplyFilter : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ApplyFilter()
        {
            try
            {
                if (this.TextBox_Filter.Text.Replace(" ", string.Empty).Length <= 0)
                {
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

                    foreach (SlotItem SlotItem in this.ListSlotItems)
                    {
                        SlotItem.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    string TextInFilter = this.TextBox_Filter.Text.ToUpper();

                    foreach (SlotItem SlotItem in this.ListSlotItems)
                    {
                        if (SlotItem.GetItemOnSlot.GetName.ToUpper().Contains(TextInFilter))
                        {
                            if (SlotItem.Visibility == Visibility.Collapsed)
                            {
                                SlotItem.Visibility = Visibility.Visible;
                            }
                        }
                        else if (SlotItem.GetItemOnSlot.GetNeeded_Type1.ToString().ToUpper().Contains(TextInFilter) || SlotItem.GetItemOnSlot.GetNeeded_Type2.ToString().ToUpper().Contains(TextInFilter))
                        {
                            if (SlotItem.Visibility == Visibility.Collapsed)
                            {
                                SlotItem.Visibility = Visibility.Visible;
                            }
                        }
                        else
                            SlotItem.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Current.MainWindow, $"Error filter slot : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);

                foreach (SlotItem SlotItem in this.ListSlotItems)
                {
                    SlotItem.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
using System;
using System.Collections;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Antize_TFT.Class;
using Antize_TFT.Fenetre;
using Antize_TFT.Enums;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace Antize_TFT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly DispatcherTimer MyTimer;

        private readonly DetailOrigineClasse DetailOrigineClasseRef;
        private readonly DetailChampion DetailChampionRef;
        private readonly ListOfChampion ListOfChampionRef;
        private readonly GestionItem GestionItemRef;
        private readonly Placement PlacementRef;
        private readonly Note NoteRef;

        private bool ListOfChampionVisible;

        public SlotChampion[] SlotChampion;

        private readonly System.Windows.Forms.NotifyIcon sysIcon;
        private readonly System.Windows.Forms.MenuItem ItemShowHide;
        private readonly System.Windows.Forms.MenuItem ItemOnlyOneType;

        private readonly System.Windows.Forms.MenuItem ItemProfile;
        private readonly System.Windows.Forms.MenuItem ItemProfile1;
        private readonly System.Windows.Forms.MenuItem ItemProfile2;
        private readonly System.Windows.Forms.MenuItem ItemProfile3;
        private readonly System.Windows.Forms.MenuItem ItemProfile4;
        private readonly System.Windows.Forms.MenuItem ItemProfile5;

        private readonly System.Windows.Forms.MenuItem ItemProfileSub;
        private readonly System.Windows.Forms.MenuItem ItemProfileSub0;
        private readonly System.Windows.Forms.MenuItem ItemProfileSub1;
        private readonly System.Windows.Forms.MenuItem ItemProfileSub2;
        private readonly System.Windows.Forms.MenuItem ItemProfileSub3;
        private readonly System.Windows.Forms.MenuItem ItemProfileSub4;

        private readonly System.Windows.Forms.MenuItem ItemEdit;
        private readonly System.Windows.Forms.MenuItem ItemCheckForUpdate;
        private Enum_State UpdateState;

        private readonly System.Windows.Forms.MenuItem ItemOptions;

        private readonly System.Windows.Forms.MenuItem ItemPosition;
        private readonly System.Windows.Forms.MenuItem ItemShowErrors;
        private readonly System.Windows.Forms.MenuItem ItemAutoUpdate;

        private readonly System.Windows.Forms.MenuItem ItemSave;
        private readonly System.Windows.Forms.MenuItem ItemLoad;

        private readonly System.Windows.Forms.MenuItem ItemSortChampion;

        private readonly System.Windows.Forms.MenuItem ItemResetProfile;

        private readonly System.Windows.Forms.MenuItem ItemPlacement; 

        private bool RenameOnProfile;

        //Ver 1.49
        private readonly System.Windows.Forms.MenuItem ItemAutoShowHide;

        private bool Startup;
        private Process CurrentProcess;
        private bool CanHide;

        //Ver 1.52
        private readonly System.Windows.Forms.MenuItem ItemNote;

        //Ver 1.53
        private KeyboardHelper _listener;

        private bool IsBaseKeyDown;

        public MainWindow()
        {
            this.InitializeComponent();

            if (CheckOnlyOnce.MakeCheckOnlyOnce() == true)
            {
                Application.Current.Shutdown();
                Environment.Exit(0);

                return;
            }

            //Create slot for champion
            this.SlotChampion = new SlotChampion[10];

            for (int Local_Index = 0; Local_Index <= 9; Local_Index++)
            {
                SlotChampion Local_SlotChampion = new SlotChampion(Local_Index, true)
                {
                    Height = 80,
                    Width = 86
                };

                this.SlotChampion[Local_Index] = Local_SlotChampion;
                this.StackPanel_Champions.Children.Add(Local_SlotChampion);
            }

            TacheFond.IntializeSettings(this);

            TacheFond.LoadRessourcesFile();

            this.DetailOrigineClasseRef = new DetailOrigineClasse();
            this.DetailChampionRef = new DetailChampion();

            this.GestionItemRef = new GestionItem();
            this.GestionItemRef.Intialize();

            this.ListOfChampionVisible = true;
            this.ListOfChampionRef = new ListOfChampion();
            this.PlacementRef = new Placement() { Height = 150, Width = 350 };

            //Create SysIcon
            this.sysIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon(Application.GetResourceStream(new Uri("/Antize TFT;component/Ressources/league-of-legends.ico", UriKind.Relative)).Stream)
            };

            //Add mouse event to SysIcon and BalloonTip
            this.sysIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.SysIcon_DoubleClicked);
            this.sysIcon.BalloonTipClicked += new EventHandler(this.BalloonTip_Clicked);

            //Create and set menu for SysIcon
            System.Windows.Forms.ContextMenu NotifiContextMenu = new System.Windows.Forms.ContextMenu();

            this.ItemOnlyOneType = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemOnlyOneTypeOn, new EventHandler(this.NotifyIcon_OnlyOneType)) { Checked = true };
            this.ItemEdit = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemEditOff, new EventHandler(this.NotifyIcon_Items));
            this.ItemPlacement = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemPlacementOff, new EventHandler(this.NotifyIcon_Placement));
            this.ItemNote = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.NoteOff, new EventHandler(this.NotifyIcon_Note));

            this.ItemSave = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemSave, new EventHandler(this.NotifyIcon_SaveAll));
            this.ItemLoad = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemLoad, new EventHandler(this.NotifyIcon_LoadAll));

            this.ItemSortChampion = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemSortChampion, new EventHandler(this.NotifyIcon_SortList));

            this.ItemResetProfile = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemResetProfile, new EventHandler(this.NotifyIcon_Reset));

            this.ItemProfile1 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_Profile} : 1", new EventHandler(this.NotifyIcon_Profile)) { Tag = "Profile_1" };
            this.ItemProfile2 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_Profile} : 2", new EventHandler(this.NotifyIcon_Profile)) { Tag = "Profile_2" };
            this.ItemProfile3 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_Profile} : 3", new EventHandler(this.NotifyIcon_Profile)) { Tag = "Profile_3" };
            this.ItemProfile4 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_Profile} : 4", new EventHandler(this.NotifyIcon_Profile)) { Tag = "Profile_4" };
            this.ItemProfile5 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_Profile} : 5", new EventHandler(this.NotifyIcon_Profile)) { Tag = "Profile_5" };

            this.ItemProfile = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_Profile);
            this.ItemProfile.MenuItems.Add(this.ItemProfile1);
            this.ItemProfile.MenuItems.Add(this.ItemProfile2);
            this.ItemProfile.MenuItems.Add(this.ItemProfile3);
            this.ItemProfile.MenuItems.Add(this.ItemProfile4);
            this.ItemProfile.MenuItems.Add(this.ItemProfile5);

            this.ItemProfileSub0 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_SubProfile} : Base", new EventHandler(this.NotifyIcon_SubProfile)) { Tag = "Profile_0" };
            this.ItemProfileSub1 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_SubProfile} : 1", new EventHandler(this.NotifyIcon_SubProfile)) { Tag = "Profile_1" };
            this.ItemProfileSub2 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_SubProfile} : 2", new EventHandler(this.NotifyIcon_SubProfile)) { Tag = "Profile_2" };
            this.ItemProfileSub3 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_SubProfile} : 3", new EventHandler(this.NotifyIcon_SubProfile)) { Tag = "Profile_3" };
            this.ItemProfileSub4 = new System.Windows.Forms.MenuItem($"{TacheFond.GetLocalizationRef.Main_SubProfile} : 4", new EventHandler(this.NotifyIcon_SubProfile)) { Tag = "Profile_4" };

            this.ItemProfileSub = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_SubProfile);
            this.ItemProfileSub.MenuItems.Add(this.ItemProfileSub0);
            this.ItemProfileSub.MenuItems.Add(this.ItemProfileSub1);
            this.ItemProfileSub.MenuItems.Add(this.ItemProfileSub2);
            this.ItemProfileSub.MenuItems.Add(this.ItemProfileSub3);
            this.ItemProfileSub.MenuItems.Add(this.ItemProfileSub4);

            this.ItemPosition = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemPositionRight, new EventHandler(this.NotifyIcon_ItemPosition));
            this.ItemAutoUpdate = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemAutoUpdateOn, new EventHandler(this.NotifyIcon_AutoUpdate)) { Checked = true };
            this.ItemShowErrors = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemShowErrorsOff, new EventHandler(this.NotifyIcon_ShowError));
            this.ItemAutoShowHide = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.AutoShowHideOn, new EventHandler(this.NotifyIcon_AutoShowHide));

            this.ItemOptions = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemOption);
            this.ItemOptions.MenuItems.Add(this.ItemPosition);
            this.ItemOptions.MenuItems.Add(this.ItemAutoShowHide);
            this.ItemOptions.MenuItems.Add(this.ItemAutoUpdate);
            this.ItemOptions.MenuItems.Add(this.ItemShowErrors);      

            this.ItemCheckForUpdate = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemNoUpdatesFound, new EventHandler(this.NotifyIcon_CheckForUpdate));
            this.ItemShowHide = new System.Windows.Forms.MenuItem(TacheFond.GetLocalizationRef.Main_ItemShowHideHide, new EventHandler(this.NotifyIcon_ShowHide));

            NotifiContextMenu.MenuItems.Add(this.ItemOnlyOneType);
            NotifiContextMenu.MenuItems.Add(this.ItemEdit);
            NotifiContextMenu.MenuItems.Add(this.ItemPlacement);
            NotifiContextMenu.MenuItems.Add(this.ItemNote);
            NotifiContextMenu.MenuItems.Add("-");
            NotifiContextMenu.MenuItems.Add(this.ItemSave);
            NotifiContextMenu.MenuItems.Add(this.ItemLoad);
            NotifiContextMenu.MenuItems.Add("-");
            NotifiContextMenu.MenuItems.Add(this.ItemSortChampion);
            NotifiContextMenu.MenuItems.Add(this.ItemProfile);
            NotifiContextMenu.MenuItems.Add(this.ItemProfileSub);
            NotifiContextMenu.MenuItems.Add(this.ItemResetProfile);
            NotifiContextMenu.MenuItems.Add("-");
            NotifiContextMenu.MenuItems.Add(this.ItemCheckForUpdate);
            NotifiContextMenu.MenuItems.Add("Discord", new EventHandler(this.NotifyIcon_Discord));
            NotifiContextMenu.MenuItems.Add("-");
            NotifiContextMenu.MenuItems.Add(this.ItemOptions);
            NotifiContextMenu.MenuItems.Add(this.ItemShowHide);
            NotifiContextMenu.MenuItems.Add("Exit", new EventHandler(this.NotifyIcon_Exit));

            this.sysIcon.ContextMenu = NotifiContextMenu;
            this.sysIcon.Text = "Double click - Show/Hide";

            this.TextProfile.Text = TacheFond.GetLocalizationRef.Main_Profile + " : ";
            this.TextProfile.ToolTip = TacheFond.GetLocalizationRef.Main_SelectedProfileToolTip;
            this.SelectedProfile.ToolTip = TacheFond.GetLocalizationRef.Main_SelectedProfileToolTip;
            this.SubProfile.ToolTip = TacheFond.GetLocalizationRef.Main_SelectedProfileToolTip;
            this.Plus.ToolTip = TacheFond.GetLocalizationRef.Main_PlusToolTip;
            this.Moins.ToolTip = TacheFond.GetLocalizationRef.Main_MoinsToolTip;

            //Create var for timer
            this.CanHide = false;
            this.Startup = true;

            //Create timer for load
            this.MyTimer = new DispatcherTimer();
            this.MyTimer.Tick += new EventHandler(this.MyTimer_Tick);
            this.MyTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
            this.MyTimer.Start();

            //Ver 1.52
            this.NoteRef = new Note();

            //Ver 1.53
            this._listener = new KeyboardHelper();
            this._listener.OnKeyPressed += this._listener_OnKeyPressed;
            this._listener.OnKeyUnPressed += this._listener_OnKeyUnpressed;
            this._listener.HookKeyboard();

            this.IsBaseKeyDown = false;
        }
               
        private void MyTimer_Tick(object sender, EventArgs e)
        {
            if (this.Startup)
            {
                //Load user settings
                TacheFond.LoadUserSettings(false);
                TacheFond.LoadUserItems();

                //Add stats | Cherck for updates
                this.sysIcon.Visible = true;
                TacheFond.ChangeVer(new MyData().AddStats());
                TacheFond.CheckForUpdate(true);

                this.Refresh_AutoUpdate(true);
                this.Refresh_ShowErrors(true);
                this.Refresh_ItemPosition(true);
                this.Refresh_AutoShowHide(true);

                this.DetailChampionRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
                this.DetailOrigineClasseRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
                this.GestionItemRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
                this.ListOfChampionRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
                this.PlacementRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
                this.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;

                //Reset all window
                this.ShowHideListOfChampion(false, false);

                //Regle le timer pour les verifications
                this.MyTimer.Interval = new TimeSpan(0, 0, 0, 1, 0);
                this.Startup = false;              
            }
            else
            {
                if (TacheFond.GetUserSettingsRef.AutoShowHide == false)
                    return;

                try
                {
                    Process[] processes = Process.GetProcessesByName(TacheFond.GetUserSettingsRef.AppName);

                    if (processes.Length > 0)
                    {
                        if (this.CurrentProcess != null)
                        {
                            if (this.CurrentProcess.HasExited)
                            {
                                this.CurrentProcess = processes[0];
                                this.MyShow();
                            }
                            else if (this.CurrentProcess.Id != processes[0].Id)
                            {
                                this.CurrentProcess = processes[0];
                                this.MyShow();
                            }
                        }
                        else
                        {
                            this.CurrentProcess = processes[0];
                            this.MyShow();
                        }
                    }
                    else if(this.CanHide)
                    {
                        this.MyHide();
                    }
                }
                catch (Exception ex)
                {
                    if (TacheFond.GetUserSettingsRef.ShowErrors == true)
                        MessageBox.Show(Application.Current.MainWindow, "Error get processes : " + ex.Message);
                }
            }           
        }

        void _listener_OnKeyPressed(object sender, KeyPressedArgs e)
        {
            if (this.IsBaseKeyDown)
            {
                if (e.KeyPressed.ToString().ToUpper() == TacheFond.GetUserSettingsRef.ShowHideKey.ToUpper())
                {
                    this.ShowHide();
                }
                else if (this.Visibility == Visibility.Visible)
                {
                    if (e.KeyPressed.ToString().ToUpper() == TacheFond.GetUserSettingsRef.ChampionKey.ToUpper())
                    {
                        this.ShowHideListOfChampion(false, false);
                    }
                    else if (e.KeyPressed.ToString().ToUpper() == TacheFond.GetUserSettingsRef.ItemKey.ToUpper())
                    {
                        if (this.ItemEdit.Checked)
                        {
                            this.ShowHideListOfChampion(false, true);
                        }                           
                        else
                            this.ForceShowHideGestionItem(true);
                    }
                    else if (e.KeyPressed.ToString().ToUpper() == TacheFond.GetUserSettingsRef.ItemCombineKey.ToUpper())
                    {
                        if (this.ItemEdit.Checked)
                        {
                            this.ShowHideListOfChampion(false, true);
                        }
                        else
                        {
                            this.ForceShowHideGestionItem(true);
                            TacheFond.GetMainRef.GetListOfChampionRef.SwitchList(TacheFond.LastItemClicked);
                        }                    
                    }
                }                     
            }
            else
            {
                this.IsBaseKeyDown = e.KeyPressed.ToString().ToUpper().Contains(TacheFond.GetUserSettingsRef.BaseKey.ToUpper());
            }
        }

        void _listener_OnKeyUnpressed(object sender, KeyPressedArgs e)
        {
            if (this.IsBaseKeyDown)
            {
                if (e.KeyPressed.ToString().ToUpper().Contains(TacheFond.GetUserSettingsRef.BaseKey.ToUpper()))
                {
                    this.IsBaseKeyDown = false;
                }              
            }
        }

        public void UpdateName()
        {
            this.ItemProfile1.Text = $"{TacheFond.GetLocalizationRef.Main_Profile} : {TacheFond.GetUserSettingsRef.NameProfile1}";
            this.ItemProfile2.Text = $"{TacheFond.GetLocalizationRef.Main_Profile} : {TacheFond.GetUserSettingsRef.NameProfile2}";
            this.ItemProfile3.Text = $"{TacheFond.GetLocalizationRef.Main_Profile} : {TacheFond.GetUserSettingsRef.NameProfile3}";
            this.ItemProfile4.Text = $"{TacheFond.GetLocalizationRef.Main_Profile} : {TacheFond.GetUserSettingsRef.NameProfile4}";
            this.ItemProfile5.Text = $"{TacheFond.GetLocalizationRef.Main_Profile} : {TacheFond.GetUserSettingsRef.NameProfile5}";
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            TacheFond.RealignerFenetre(Enum_State.SizedFromMain, false);
        }

        private void Window_LocationChanged(object sender, EventArgs e)
        {
            TacheFond.RealignerFenetre(Enum_State.MovedFromMain, false);
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

        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            this.GestionItemRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
            this.ListOfChampionRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
            this.PlacementRef.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.GestionItemRef.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
            this.ListOfChampionRef.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
            this.PlacementRef.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityOut;

            this.DetailChampionRef.Hide();
            this.DetailOrigineClasseRef.Hide();
        }

        public string GetSetActualNumber
        {
            set { this.ActualNumber.Text = value; }
            get { return this.ActualNumber.Text; }
        }

        public DetailOrigineClasse GetDetailOrigineClasseRef
        {
            get { return this.DetailOrigineClasseRef; }
        }

        public DetailChampion GetDetailChampionRef
        {
            get { return this.DetailChampionRef; }
        }

        public ListOfChampion GetListOfChampionRef
        {
            get { return this.ListOfChampionRef; }
        }

        public GestionItem GetGestionItemRef
        {
            get { return this.GestionItemRef; }
        }

        public Placement GetPlacementRef
        {
            get { return this.PlacementRef; }
        }

        public bool GetPlacementIsOpen
        {
            get { return this.ItemPlacement.Checked; }
        }

        public bool GetGestionItemIsOpen
        {
            get { return this.ItemEdit.Checked; }
        }

        public bool GetListOfChampionVisible
        {
            get { return this.ListOfChampionVisible; }
        }

        private void ImageShowList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.ShowHideListOfChampion(false, false);
            }
        }

        public void ShowHideListOfChampion(bool _ForceOpen, bool _ForceClose)
        {    
            if (this.ItemPlacement.Checked == true)
            {
                this.ShowHidePlacement(true);
            }

            if (_ForceOpen)
            {
                if (this.ListOfChampionVisible == false)
                {
                    this.ListOfChampionVisible = true;
                    this.ListOfChampionRef.Show();

                    this.Image_ShowList.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/FlecheDownOn.png", UriKind.Relative));
                }
            }
            else
            {
                if (this.ListOfChampionRef.GetIsOnListChampion == false)
                {
                    this.GetListOfChampionRef.SwitchList(Enum_Item.None);
                }

                if (this.ListOfChampionVisible || _ForceClose)
                {
                    this.ListOfChampionVisible = false;
                    this.ListOfChampionRef.Hide();

                    this.Image_ShowList.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/FlecheDown.png", UriKind.Relative));

                    this.ItemEdit.Text = TacheFond.GetLocalizationRef.Main_ItemEditOff;

                    this.ItemEdit.Checked = false;
                    this.GestionItemRef.Hide();
                    this.Image_Item.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Armor.png", UriKind.Relative));
                }
                else
                {
                    this.ListOfChampionVisible = true;
                    this.ListOfChampionRef.Show();

                    this.Image_ShowList.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/FlecheDownOn.png", UriKind.Relative));
                }
            }
        }

        private void ResetAll()
        {
            //Reset le profile selectionner
            switch (TacheFond.GetUserSettingsRef.SelectedProfile)
            {
                case 1:
                    switch (TacheFond.GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            TacheFond.GetUserSettingsRef.Profile1.Clear();
                            break;
                        case 1:
                            TacheFond.GetUserSettingsRef.Profile1Sub1.Clear();
                            break;
                        case 2:
                            TacheFond.GetUserSettingsRef.Profile1Sub2.Clear();
                            break;
                        case 3:
                            TacheFond.GetUserSettingsRef.Profile1Sub3.Clear();
                            break;
                        case 4:
                            TacheFond.GetUserSettingsRef.Profile1Sub4.Clear();
                            break;
                    }
                    break;

                case 2:
                    switch (TacheFond.GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            TacheFond.GetUserSettingsRef.Profile2.Clear();
                            break;
                        case 1:
                            TacheFond.GetUserSettingsRef.Profile2Sub1.Clear();
                            break;
                        case 2:
                            TacheFond.GetUserSettingsRef.Profile2Sub2.Clear();
                            break;
                        case 3:
                            TacheFond.GetUserSettingsRef.Profile2Sub3.Clear();
                            break;
                        case 4:
                            TacheFond.GetUserSettingsRef.Profile2Sub4.Clear();
                            break;
                    }
                    break;

                case 3:
                    switch (TacheFond.GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            TacheFond.GetUserSettingsRef.Profile3.Clear();
                            break;
                        case 1:
                            TacheFond.GetUserSettingsRef.Profile3Sub1.Clear();
                            break;
                        case 2:
                            TacheFond.GetUserSettingsRef.Profile3Sub2.Clear();
                            break;
                        case 3:
                            TacheFond.GetUserSettingsRef.Profile3Sub3.Clear();
                            break;
                        case 4:
                            TacheFond.GetUserSettingsRef.Profile3Sub4.Clear();
                            break;
                    }
                    break;


                case 4:
                    switch (TacheFond.GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            TacheFond.GetUserSettingsRef.Profile4.Clear();
                            break;
                        case 1:
                            TacheFond.GetUserSettingsRef.Profile4Sub1.Clear();
                            break;
                        case 2:
                            TacheFond.GetUserSettingsRef.Profile4Sub2.Clear();
                            break;
                        case 3:
                            TacheFond.GetUserSettingsRef.Profile4Sub3.Clear();
                            break;
                        case 4:
                            TacheFond.GetUserSettingsRef.Profile4Sub4.Clear();
                            break;
                    }
                    break;

                case 5:
                    switch (TacheFond.GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            TacheFond.GetUserSettingsRef.Profile5.Clear();
                            break;
                        case 1:
                            TacheFond.GetUserSettingsRef.Profile5Sub1.Clear();
                            break;
                        case 2:
                            TacheFond.GetUserSettingsRef.Profile5Sub2.Clear();
                            break;
                        case 3:
                            TacheFond.GetUserSettingsRef.Profile5Sub3.Clear();
                            break;
                        case 4:
                            TacheFond.GetUserSettingsRef.Profile5Sub4.Clear();
                            break;
                    }
                    break;
            }

            this.GetSetActualNumber = "0";

            foreach (SlotChampion SlotChamp in this.SlotChampion)
            {
                SlotChamp.AddOrRemoveChampion(null, false);
            }

            this.Refresh_Slots();
        }

        public void Refresh_Slots()
        {
            ArrayList Local_TempLoad = this.RefreshSelectedProfile(false, false);
            this.PlacementRef.RefreshPlacement(Local_TempLoad);

            int Local_Lenght = 10 - Local_TempLoad.Count;

            for (int i = 1; i <= Local_Lenght; i++)
            {
                Local_TempLoad.Add(-1);
            }

            this.GetSetActualNumber = "0";

            bool Local_OnPlacement = false;

            for (int Index = 0; Index <= 9; Index++)
            {
                if ((int)Local_TempLoad[Index] == -1000)
                {
                    Local_OnPlacement = true;
                }

                try
                {
                    if (Local_OnPlacement)
                    {
                        this.SlotChampion[Index].AddOrRemoveChampion(null, false);
                    }
                    else
                    {
                        if ((int)Local_TempLoad[Index] >= 0)
                            this.SlotChampion[Index].AddOrRemoveChampion((Champion)TacheFond.ListChampions[(int)Local_TempLoad[Index]], true);
                        else
                            this.SlotChampion[Index].AddOrRemoveChampion(null, false);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, "Error refresh slot : " + ex.Message);
                }
            }

            this.ListOfChampionRef.RefreshList();

            this.NoteRef.SaveLoadNote(false);
        }

        public bool AddChampionToSlot(Champion _Champion)
        {
            bool Local_Added = false;

            if (_Champion != null)
            {
                foreach (SlotChampion item in this.SlotChampion)
                {
                    if (item.ChampionOnSlot == null)
                    {
                        item.AddOrRemoveChampion(_Champion, true);
                        Local_Added = true;

                        break;
                    }
                }

                return Local_Added;
            }
            else
                return false;
        }

        /**********************/
        /* sysIcon Clicked ****/
        /**********************/
        private void SysIcon_DoubleClicked(Object sender, System.Windows.Forms.MouseEventArgs e)
        {
            ShowHide();
        }

        private void NotifyIcon_Profile(object sender, EventArgs e)
        {
            TacheFond.GetUserSettingsRef.SelectedProfile = int.Parse(Regex.Replace(((System.Windows.Forms.MenuItem)sender).Tag.ToString(), "[^0-9]+", string.Empty));
            TacheFond.GetUserSettingsRef.SelectedSubProfile = 0;
            this.Refresh_Slots();
        }

        private void NotifyIcon_SubProfile(object sender, EventArgs e)
        {
            TacheFond.GetUserSettingsRef.SelectedSubProfile = int.Parse(Regex.Replace(((System.Windows.Forms.MenuItem)sender).Tag.ToString(), "[^0-9]+", string.Empty));
            this.Refresh_Slots();
        }

        private void NotifyIcon_Items(object sender, EventArgs e)
        {
            this.ShowHideGestionItem();
        }

        private void Image_Item_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.ShowHideGestionItem();
            }
        }

        public void ForceShowHideGestionItem(bool _Show)
        {
            if (_Show)
            {
                this.ItemEdit.Checked = true;
                this.ItemEdit.Text = TacheFond.GetLocalizationRef.Main_ItemEditOn;

                this.GestionItemRef.Show();
                this.Image_Item.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/ArmorOn.png", UriKind.Relative));

                if (this.GetListOfChampionVisible == false)
                    this.ShowHideListOfChampion(true, false);
            }
            else
            {
                this.ItemEdit.Checked = false;

                this.ItemEdit.Text = TacheFond.GetLocalizationRef.Main_ItemEditOff;

                this.GestionItemRef.Hide();
                this.Image_Item.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Armor.png", UriKind.Relative));
            }
        }

        public void ShowHideGestionItem()
        {
            if (this.Visibility != Visibility.Visible)
            {
                this.ShowHide();             
            }

            if (this.ItemPlacement.Checked == true)
            {
                this.ShowHidePlacement(true);
            }

            if (this.GetListOfChampionRef.GetIsOnListChampion == false && this.ItemEdit.Checked)
            {
                this.Image_Item.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/ArmorOn.png", UriKind.Relative));
                this.GetListOfChampionRef.SwitchList(Enum_Item.None);

                return;
            }

            this.ItemEdit.Checked = !this.ItemEdit.Checked;

            if (this.ItemEdit.Checked)
            {
                this.ItemEdit.Text = TacheFond.GetLocalizationRef.Main_ItemEditOn;

                this.GestionItemRef.Show();
                this.Image_Item.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/ArmorOn.png", UriKind.Relative));

                this.ListOfChampionVisible = true;
                this.ListOfChampionRef.Show();
                this.Image_ShowList.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/FlecheDownOn.png", UriKind.Relative));
            }
            else
            {
                this.ItemEdit.Text = TacheFond.GetLocalizationRef.Main_ItemEditOff;

                this.GestionItemRef.Hide();
                this.Image_Item.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/Armor.png", UriKind.Relative));
            }
        }

        private void NotifyIcon_SaveAll(object sender, EventArgs e)
        {
            TacheFond.SaveUserSettings();
            TacheFond.SaveUserItems();
        }

        private void NotifyIcon_LoadAll(object sender, EventArgs e)
        {
            TacheFond.LoadUserSettings(true);
            TacheFond.LoadUserItems();

            if (this.GetGestionItemIsOpen && this.GestionItemRef.GetSelectedChampion != null)
            {
                this.GestionItemRef.RefreshChampion(this.GestionItemRef.GetSelectedChampion);
            }
        }

        private void NotifyIcon_Discord(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://docs.google.com/document/d/e/2PACX-1vQY9OOP0KbpV-8JxT0feztw8xIX0B89Bm7Z4JXH50YmpzrDnTlvpeCmVQR9kTN80R6q2r-EonuDAWMy/pub");
        }

        private void NotifyIcon_Reset(object sender, EventArgs e)
        {
            this.ResetAll();
        }

        private void NotifyIcon_OnlyOneType(object sender, EventArgs e)
        {
            this.Refresh_OnlyOneType(false);
        }

        public void Refresh_OnlyOneType(bool _Load)
        {
            if (_Load == false)
                TacheFond.GetUserSettingsRef.OnlyOneType = !TacheFond.GetUserSettingsRef.OnlyOneType;

            //Atribue le bool actuel
            this.ItemOnlyOneType.Checked = TacheFond.GetUserSettingsRef.OnlyOneType;

            if (TacheFond.GetUserSettingsRef.OnlyOneType)
                this.ItemOnlyOneType.Text = TacheFond.GetLocalizationRef.Main_ItemOnlyOneTypeOn;
            else
                this.ItemOnlyOneType.Text = TacheFond.GetLocalizationRef.Main_ItemOnlyOneTypeOff;

            this.ListOfChampionRef.RefreshList();
        }

        private void NotifyIcon_CheckForUpdate(object sender, EventArgs e)
        {
            if (this.UpdateState == Enum_State.UpdateAvailable)
            {
                System.Diagnostics.Process.Start(TacheFond.GetUserSettingsRef.UpdateLink);

                return;
            }

            TacheFond.CheckForUpdate(false);
        }

        private void NotifyIcon_ItemPosition(object sender, EventArgs e)
        {
            this.Refresh_ItemPosition(false);
        }

        private void Refresh_ItemPosition(bool _Load)
        {
            if (_Load == false)
            {
                TacheFond.GetUserSettingsRef.PositionLeft = !TacheFond.GetUserSettingsRef.PositionLeft;

                //Only save options
                TacheFond.GetOptionsUserSettingsRef.PositionLeft = TacheFond.GetUserSettingsRef.PositionLeft;
                TacheFond.SaveOptionSettings();
            }

            TacheFond.RealignerFenetre(Enum_State.All, _Load);

            if (TacheFond.GetUserSettingsRef.PositionLeft)
                this.ItemPosition.Text = TacheFond.GetLocalizationRef.Main_ItemPositionLeft;
            else
                this.ItemPosition.Text = TacheFond.GetLocalizationRef.Main_ItemPositionRight;
        }

        private void NotifyIcon_AutoUpdate(object sender, EventArgs e)
        {
            this.Refresh_AutoUpdate(false);
        }

        private void Refresh_AutoUpdate(bool _Load)
        {
            if (_Load == false)
            {
                TacheFond.GetUserSettingsRef.EnableCheckForUpdate = !TacheFond.GetUserSettingsRef.EnableCheckForUpdate;

                //Only save options
                TacheFond.GetOptionsUserSettingsRef.EnableCheckForUpdate = TacheFond.GetUserSettingsRef.EnableCheckForUpdate;
                TacheFond.SaveOptionSettings();
            }

            //Atribue le bool actuel
            this.ItemAutoUpdate.Checked = TacheFond.GetUserSettingsRef.EnableCheckForUpdate;

            if (TacheFond.GetUserSettingsRef.EnableCheckForUpdate)
                this.ItemAutoUpdate.Text = TacheFond.GetLocalizationRef.Main_ItemAutoUpdateOn;
            else
                this.ItemAutoUpdate.Text = TacheFond.GetLocalizationRef.Main_ItemAutoUpdateOff;
        }

        private void NotifyIcon_ShowError(object sender, EventArgs e)
        {
            this.Refresh_ShowErrors(false);
        }

        private void Refresh_ShowErrors(bool _Load)
        {
            if (_Load == false)
            {
                TacheFond.GetUserSettingsRef.ShowErrors = !TacheFond.GetUserSettingsRef.ShowErrors;

                //Only save options
                TacheFond.GetOptionsUserSettingsRef.ShowErrors = TacheFond.GetUserSettingsRef.ShowErrors;
                TacheFond.SaveOptionSettings();
            }

            this.ItemShowErrors.Checked = TacheFond.GetUserSettingsRef.ShowErrors;

            if (TacheFond.GetUserSettingsRef.ShowErrors)
                this.ItemShowErrors.Text = TacheFond.GetLocalizationRef.Main_ItemShowErrorsOn;
            else
                this.ItemShowErrors.Text = TacheFond.GetLocalizationRef.Main_ItemShowErrorsOff;
        }

        private void NotifyIcon_ShowHide(object sender, EventArgs e)
        {
            this.ShowHide();
        }

        private void ShowHide()
        {
            if (this.Visibility == Visibility.Visible)
            {
                this.ShowInTaskbar = false;
                this.Visibility = Visibility.Hidden;

                this.ShowHideListOfChampion(false, true);

                this.ItemShowHide.Text = TacheFond.GetLocalizationRef.Main_ItemShowHideShow;
            }
            else
            {
                this.ShowInTaskbar = true;
                this.Visibility = Visibility.Visible;

                this.ItemShowHide.Text = TacheFond.GetLocalizationRef.Main_ItemShowHideHide;
            }
        }

        private void MyShow()
        {
            if (this.Visibility == Visibility.Hidden)
            {
                this.ShowInTaskbar = true;
                this.Visibility = Visibility.Visible;

                this.ItemShowHide.Text = TacheFond.GetLocalizationRef.Main_ItemShowHideHide;                
            }

            this.CanHide = true;
        }

        private void MyHide()
        {
            if (this.Visibility == Visibility.Visible)
            {
                this.ShowInTaskbar = false;
                this.Visibility = Visibility.Hidden;

                this.ShowHideListOfChampion(false, true);

                this.ItemShowHide.Text = TacheFond.GetLocalizationRef.Main_ItemShowHideShow;                
            }

            this.CanHide = false;
        }

        private void NotifyIcon_AutoShowHide(object sender, EventArgs e)
        {
            this.Refresh_AutoShowHide(false);

            this.CanHide = false;
        }

        private void Refresh_AutoShowHide(bool _Load)
        {
            if (_Load == false)
            {
                TacheFond.GetUserSettingsRef.AutoShowHide = !TacheFond.GetUserSettingsRef.AutoShowHide;

                //Only save options
                TacheFond.GetOptionsUserSettingsRef.AutoShowHide = TacheFond.GetUserSettingsRef.AutoShowHide;
                TacheFond.SaveOptionSettings();
            }

            this.ItemAutoShowHide.Checked = TacheFond.GetUserSettingsRef.AutoShowHide;

            if (TacheFond.GetUserSettingsRef.AutoShowHide)
                this.ItemAutoShowHide.Text = TacheFond.GetLocalizationRef.AutoShowHideOn;
            else
                this.ItemAutoShowHide.Text = TacheFond.GetLocalizationRef.AutoShowHideOff;
        }

        private void NotifyIcon_Exit(object sender, EventArgs e)
        {
            this.sysIcon.Visible = false;

            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.sysIcon.Visible = false;

            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            this.sysIcon.Visible = false;

            Application.Current.Shutdown();
            Environment.Exit(0);
        }

        private void NotifyIcon_SortList(object sender, EventArgs e)
        {
            this.SortChampionList();
        }

        private void SortChampionList()
        {
            ArrayList Local_ArangedList = new ArrayList();

            for (int Local_Tier = 1; Local_Tier <= 5; Local_Tier++)
            {
                foreach (SlotChampion SlotChamp in this.SlotChampion)
                {
                    if (SlotChamp.ChampionOnSlot != null)
                    {
                        if (SlotChamp.ChampionOnSlot.GetTier == Local_Tier)
                        {
                            Local_ArangedList.Add(SlotChamp.ChampionOnSlot);
                            SlotChamp.AddOrRemoveChampion(null, false);
                        }
                    }
                }
            }

            int Local_Index = 0;

            foreach (Champion Champ in Local_ArangedList)
            {
                this.SlotChampion[Local_Index].AddOrRemoveChampion(Champ, false);
                Local_Index++;
            }

            TacheFond.RefreshUserProfile();
        }

        public void StateCheckForUpdate(Enum_State _UpdateState)
        {
            this.UpdateState = _UpdateState;

            switch (_UpdateState)
            {
                case Enum_State.UpdateSearch:
                    this.ItemCheckForUpdate.Text = TacheFond.GetLocalizationRef.Main_ItemCheckingForUpdates;
                    break;
                case Enum_State.UpdateAvailable:
                    this.ItemCheckForUpdate.Text = TacheFond.GetLocalizationRef.Main_ItemUpdatesAvailable;
                    this.ItemCheckForUpdate.Checked = true;
                    this.sysIcon.ShowBalloonTip(500, "Antize TFT Overlay", TacheFond.GetLocalizationRef.Main_ItemUpdatesAvailable, System.Windows.Forms.ToolTipIcon.Info);
                    break;
                case Enum_State.UpdateUnAvailable:
                    this.ItemCheckForUpdate.Text = TacheFond.GetLocalizationRef.Main_ItemNoUpdatesFound;
                    break;
            }
        }

        private void BalloonTip_Clicked(object sender, System.EventArgs e)
        {
            if (this.UpdateState == Enum_State.UpdateAvailable)
                System.Diagnostics.Process.Start(TacheFond.GetUserSettingsRef.UpdateLink);
        }

        private void Plus_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                int Local_SubProfile = TacheFond.GetUserSettingsRef.SelectedSubProfile + 1;

                if (Local_SubProfile <= 4)
                {
                    TacheFond.GetUserSettingsRef.SelectedSubProfile = Local_SubProfile;
                    this.Refresh_Slots();
                }
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                int Local_Profile = TacheFond.GetUserSettingsRef.SelectedProfile + 1;

                if (Local_Profile <= 5)
                {
                    TacheFond.GetUserSettingsRef.SelectedSubProfile = 0;
                    TacheFond.GetUserSettingsRef.SelectedProfile = Local_Profile;
                    this.Refresh_Slots();
                }
            }

            this.MyTextBox.Visibility = Visibility.Hidden;
        }

        private void Moins_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                int Local_SubProfile = TacheFond.GetUserSettingsRef.SelectedSubProfile - 1;

                if (Local_SubProfile >= 0)
                {
                    TacheFond.GetUserSettingsRef.SelectedSubProfile = Local_SubProfile;
                    this.Refresh_Slots();
                }
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                int Local_Profile = TacheFond.GetUserSettingsRef.SelectedProfile - 1;

                if (Local_Profile >= 1)
                {
                    TacheFond.GetUserSettingsRef.SelectedSubProfile = 0;
                    TacheFond.GetUserSettingsRef.SelectedProfile = Local_Profile;
                    this.Refresh_Slots();
                }
            }

            this.MyTextBox.Visibility = Visibility.Hidden;
        }

        private void SubProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.SortChampionList();
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                if (this.MyTextBox.Visibility == Visibility.Visible && this.RenameOnProfile == false)
                {
                    this.MyTextBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    if (TacheFond.GetUserSettingsRef.SelectedSubProfile <= 0)
                        return;

                    this.RenameOnProfile = false;
                    this.MyTextBox.Text = this.SubProfile.Text;
                    this.MyTextBox.Visibility = Visibility.Visible;
                }
            }
        }

        private void SelectedProfile_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.SortChampionList();
            }
            if (e.ChangedButton == MouseButton.Right)
            {
                if (this.MyTextBox.Visibility == Visibility.Visible && this.RenameOnProfile == true)
                {
                    this.MyTextBox.Visibility = Visibility.Hidden;
                }
                else
                {
                    this.RenameOnProfile = true;
                    this.MyTextBox.Text = this.SelectedProfile.Text;
                    this.MyTextBox.Visibility = Visibility.Visible;
                }
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.MyTextBox.Visibility = Visibility.Hidden;
                return;
            }
            else if (e.Key == Key.Enter)
            {
                this.MyTextBox.Text = this.MyTextBox.Text.Replace("  ", string.Empty);

                if (this.MyTextBox.Text.Length <= 0 || this.MyTextBox.Text.Length > 15)
                {
                    return;
                }

                if (this.RenameOnProfile)
                {
                    this.SelectedProfile.Text = this.MyTextBox.Text;
                    this.RefreshSelectedProfile(true, true);
                }
                else
                {
                    this.SubProfile.Text = this.MyTextBox.Text;
                    this.RefreshSelectedProfile(false, true);
                }

                this.MyTextBox.Visibility = Visibility.Hidden;
            }
        }

        private ArrayList RefreshSelectedProfile(bool OnProfile, bool _Rename)
        {
            int Local_SelectedProfile = TacheFond.GetUserSettingsRef.SelectedProfile;
            int Local_SelectedSubProfile = TacheFond.GetUserSettingsRef.SelectedSubProfile;

            this.ItemProfile1.Checked = false;
            this.ItemProfile2.Checked = false;
            this.ItemProfile3.Checked = false;
            this.ItemProfile4.Checked = false;
            this.ItemProfile5.Checked = false;

            this.ItemProfileSub0.Checked = false;
            this.ItemProfileSub1.Checked = false;
            this.ItemProfileSub2.Checked = false;
            this.ItemProfileSub3.Checked = false;
            this.ItemProfileSub4.Checked = false;

            switch (Local_SelectedProfile)
            {
                case 1:
                    if (_Rename)
                    {
                        if (OnProfile)
                        {
                            TacheFond.GetUserSettingsRef.NameProfile1 = this.MyTextBox.Text;
                            this.ItemProfile1.Text = TacheFond.GetLocalizationRef.Main_Profile + " : " + TacheFond.GetUserSettingsRef.NameProfile1;
                        }
                        else
                        {
                            switch (Local_SelectedSubProfile)
                            {
                                case 1:
                                    TacheFond.GetUserSettingsRef.NameProfile1Sub1 = this.MyTextBox.Text;
                                    this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile1Sub1;
                                    break;
                                case 2:
                                    TacheFond.GetUserSettingsRef.NameProfile1Sub2 = this.MyTextBox.Text;
                                    this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile1Sub2;
                                    break;
                                case 3:
                                    TacheFond.GetUserSettingsRef.NameProfile1Sub3 = this.MyTextBox.Text;
                                    this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile1Sub3;
                                    break;
                                case 4:
                                    TacheFond.GetUserSettingsRef.NameProfile1Sub4 = this.MyTextBox.Text;
                                    this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile1Sub4;
                                    break;
                            }
                            break;
                        }
                    }
                    else
                    {
                        this.ItemProfile1.Checked = true;
                        this.SelectedProfile.Text = TacheFond.GetUserSettingsRef.NameProfile1;

                        this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile1Sub1;
                        this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile1Sub2;
                        this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile1Sub3;
                        this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile1Sub4;

                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.ItemProfileSub0.Checked = true;
                                this.SubProfile.Text = "Base";

                                return TacheFond.GetUserSettingsRef.Profile1;
                            case 1:
                                this.ItemProfileSub1.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile1Sub1;

                                return TacheFond.GetUserSettingsRef.Profile1Sub1;
                            case 2:
                                this.ItemProfileSub2.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile1Sub2;

                                return TacheFond.GetUserSettingsRef.Profile1Sub2;
                            case 3:
                                this.ItemProfileSub3.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile1Sub3;

                                return TacheFond.GetUserSettingsRef.Profile1Sub3;
                            case 4:
                                this.ItemProfileSub4.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile1Sub4;

                                return TacheFond.GetUserSettingsRef.Profile1Sub4;
                        }
                        break;
                    }
                    break;

                case 2:
                    if (_Rename)
                    {
                        if (OnProfile)
                        {
                            TacheFond.GetUserSettingsRef.NameProfile2 = this.MyTextBox.Text;
                            this.ItemProfile2.Text = TacheFond.GetLocalizationRef.Main_Profile + " : " + TacheFond.GetUserSettingsRef.NameProfile2;
                        }
                        else
                        {
                            switch (Local_SelectedSubProfile)
                            {
                                case 1:
                                    TacheFond.GetUserSettingsRef.NameProfile2Sub1 = this.MyTextBox.Text;
                                    this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile2Sub1;
                                    break;
                                case 2:
                                    TacheFond.GetUserSettingsRef.NameProfile2Sub2 = this.MyTextBox.Text;
                                    this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile2Sub2;
                                    break;
                                case 3:
                                    TacheFond.GetUserSettingsRef.NameProfile2Sub3 = this.MyTextBox.Text;
                                    this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile2Sub3;
                                    break;
                                case 4:
                                    TacheFond.GetUserSettingsRef.NameProfile2Sub4 = this.MyTextBox.Text;
                                    this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile2Sub4;
                                    break;
                            }
                            break;
                        }
                    }
                    else
                    {
                        this.ItemProfile2.Checked = true;
                        this.SelectedProfile.Text = TacheFond.GetUserSettingsRef.NameProfile2;

                        this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile2Sub1;
                        this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile2Sub2;
                        this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile2Sub3;
                        this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile2Sub4;

                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.ItemProfileSub0.Checked = true;
                                this.SubProfile.Text = "Base";

                                return TacheFond.GetUserSettingsRef.Profile2;
                            case 1:
                                this.ItemProfileSub1.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile2Sub1;

                                return TacheFond.GetUserSettingsRef.Profile2Sub1;
                            case 2:
                                this.ItemProfileSub2.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile2Sub2;

                                return TacheFond.GetUserSettingsRef.Profile2Sub2;
                            case 3:
                                this.ItemProfileSub3.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile2Sub3;

                                return TacheFond.GetUserSettingsRef.Profile2Sub3;
                            case 4:
                                this.ItemProfileSub4.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile2Sub4;

                                return TacheFond.GetUserSettingsRef.Profile2Sub4;
                        }
                        break;
                    }
                    break;

                case 3:
                    if (_Rename)
                    {
                        if (OnProfile)
                        {
                            TacheFond.GetUserSettingsRef.NameProfile3 = this.MyTextBox.Text;
                            this.ItemProfile3.Text = TacheFond.GetLocalizationRef.Main_Profile + " : " + TacheFond.GetUserSettingsRef.NameProfile3;
                        }
                        else
                        {
                            switch (Local_SelectedSubProfile)
                            {
                                case 1:
                                    TacheFond.GetUserSettingsRef.NameProfile3Sub1 = this.MyTextBox.Text;
                                    this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile3Sub1;
                                    break;
                                case 2:
                                    TacheFond.GetUserSettingsRef.NameProfile3Sub2 = this.MyTextBox.Text;
                                    this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile3Sub2;
                                    break;
                                case 3:
                                    TacheFond.GetUserSettingsRef.NameProfile3Sub3 = this.MyTextBox.Text;
                                    this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile3Sub3;
                                    break;
                                case 4:
                                    TacheFond.GetUserSettingsRef.NameProfile3Sub4 = this.MyTextBox.Text;
                                    this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile3Sub4;
                                    break;
                            }
                            break;
                        }
                    }
                    else
                    {
                        this.ItemProfile3.Checked = true;
                        this.SelectedProfile.Text = TacheFond.GetUserSettingsRef.NameProfile3;

                        this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile3Sub1;
                        this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile3Sub2;
                        this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile3Sub3;
                        this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile3Sub4;

                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.ItemProfileSub0.Checked = true;
                                this.SubProfile.Text = "Base";

                                return TacheFond.GetUserSettingsRef.Profile3;
                            case 1:
                                this.ItemProfileSub1.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile3Sub1;

                                return TacheFond.GetUserSettingsRef.Profile3Sub1;
                            case 2:
                                this.ItemProfileSub2.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile3Sub2;

                                return TacheFond.GetUserSettingsRef.Profile3Sub2;
                            case 3:
                                this.ItemProfileSub3.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile3Sub3;

                                return TacheFond.GetUserSettingsRef.Profile3Sub3;
                            case 4:
                                this.ItemProfileSub4.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile3Sub4;

                                return TacheFond.GetUserSettingsRef.Profile3Sub4;
                        }
                        break;
                    }
                    break;

                case 4:
                    if (_Rename)
                    {
                        if (OnProfile)
                        {
                            TacheFond.GetUserSettingsRef.NameProfile4 = this.MyTextBox.Text;
                            this.ItemProfile4.Text = TacheFond.GetLocalizationRef.Main_Profile + " : " + TacheFond.GetUserSettingsRef.NameProfile4;
                        }
                        else
                        {
                            switch (Local_SelectedSubProfile)
                            {
                                case 1:
                                    TacheFond.GetUserSettingsRef.NameProfile4Sub1 = this.MyTextBox.Text;
                                    this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile4Sub1;
                                    break;
                                case 2:
                                    TacheFond.GetUserSettingsRef.NameProfile4Sub2 = this.MyTextBox.Text;
                                    this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile4Sub2;
                                    break;
                                case 3:
                                    TacheFond.GetUserSettingsRef.NameProfile4Sub3 = this.MyTextBox.Text;
                                    this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile4Sub3;
                                    break;
                                case 4:
                                    TacheFond.GetUserSettingsRef.NameProfile4Sub4 = this.MyTextBox.Text;
                                    this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile4Sub4;
                                    break;
                            }
                            break;
                        }
                    }
                    else
                    {
                        this.ItemProfile4.Checked = true;
                        this.SelectedProfile.Text = TacheFond.GetUserSettingsRef.NameProfile4;

                        this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile4Sub1;
                        this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile4Sub2;
                        this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile4Sub3;
                        this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile4Sub4;

                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.ItemProfileSub0.Checked = true;
                                this.SubProfile.Text = "Base";

                                return TacheFond.GetUserSettingsRef.Profile4;
                            case 1:
                                this.ItemProfileSub1.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile4Sub1;

                                return TacheFond.GetUserSettingsRef.Profile4Sub1;
                            case 2:
                                this.ItemProfileSub2.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile4Sub2;

                                return TacheFond.GetUserSettingsRef.Profile4Sub2;
                            case 3:
                                this.ItemProfileSub3.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile4Sub3;

                                return TacheFond.GetUserSettingsRef.Profile4Sub3;
                            case 4:
                                this.ItemProfileSub4.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile4Sub4;

                                return TacheFond.GetUserSettingsRef.Profile4Sub4;
                        }
                        break;
                    }
                    break;

                case 5:
                    if (_Rename)
                    {
                        if (OnProfile)
                        {
                            TacheFond.GetUserSettingsRef.NameProfile5 = this.MyTextBox.Text;
                            this.ItemProfile5.Text = TacheFond.GetLocalizationRef.Main_Profile + " : " + TacheFond.GetUserSettingsRef.NameProfile5;
                        }
                        else
                        {
                            switch (Local_SelectedSubProfile)
                            {
                                case 1:
                                    TacheFond.GetUserSettingsRef.NameProfile5Sub1 = this.MyTextBox.Text;
                                    this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile5Sub1;
                                    break;
                                case 2:
                                    TacheFond.GetUserSettingsRef.NameProfile5Sub2 = this.MyTextBox.Text;
                                    this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile5Sub2;
                                    break;
                                case 3:
                                    TacheFond.GetUserSettingsRef.NameProfile5Sub3 = this.MyTextBox.Text;
                                    this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile5Sub3;
                                    break;
                                case 4:
                                    TacheFond.GetUserSettingsRef.NameProfile5Sub4 = this.MyTextBox.Text;
                                    this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile5Sub4;
                                    break;
                            }
                            break;
                        }
                    }
                    else
                    {
                        this.ItemProfile5.Checked = true;
                        this.SelectedProfile.Text = TacheFond.GetUserSettingsRef.NameProfile5;

                        this.ItemProfileSub1.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile5Sub1;
                        this.ItemProfileSub2.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile5Sub2;
                        this.ItemProfileSub3.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile5Sub3;
                        this.ItemProfileSub4.Text = TacheFond.GetLocalizationRef.Main_SubProfile + " : " + TacheFond.GetUserSettingsRef.NameProfile5Sub4;

                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.ItemProfileSub0.Checked = true;
                                this.SubProfile.Text = "Base";

                                return TacheFond.GetUserSettingsRef.Profile5;
                            case 1:
                                this.ItemProfileSub1.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile5Sub1;

                                return TacheFond.GetUserSettingsRef.Profile5Sub1;
                            case 2:
                                this.ItemProfileSub2.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile5Sub2;

                                return TacheFond.GetUserSettingsRef.Profile5Sub2;
                            case 3:
                                this.ItemProfileSub3.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile5Sub3;

                                return TacheFond.GetUserSettingsRef.Profile5Sub3;
                            case 4:
                                this.ItemProfileSub4.Checked = true;
                                this.SubProfile.Text = TacheFond.GetUserSettingsRef.NameProfile5Sub4;

                                return TacheFond.GetUserSettingsRef.Profile5Sub4;
                        }
                        break;
                    }
                    break;
            }

            return null;
        }

        private void NotifyIcon_Placement(object sender, EventArgs e)
        {
            this.ShowHidePlacement(false);
        }

        private void NotifyIcon_Note(object sender, EventArgs e)
        {
            if (this.ItemNote.Checked == true)
            {
                this.ItemNote.Checked = false;
                this.ItemNote.Text = TacheFond.GetLocalizationRef.NoteOff;

                this.NoteRef.Hide();
            }
            else
            {
                this.ItemNote.Checked = true;
                this.ItemNote.Text = TacheFond.GetLocalizationRef.NoteOn;

                this.NoteRef.SaveLoadNote(false);
                this.NoteRef.Show();               
            }
        }

        private void Image_Placement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.ShowHidePlacement(false);
            }
        }

        public void ShowHidePlacement(bool _ForceHide)
        {
            if (this.ItemPlacement.Checked == true || _ForceHide)
            {
                this.ItemPlacement.Checked = false;
                this.ItemPlacement.Text = TacheFond.GetLocalizationRef.Main_ItemPlacementOff;

                this.PlacementRef.Hide();
                this.Image_Placement.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/PositionOff.png", UriKind.Relative));
            }
            else
            {
                this.ShowHideListOfChampion(false, true);

                this.ItemPlacement.Checked = true;
                this.ItemPlacement.Text = TacheFond.GetLocalizationRef.Main_ItemPlacementOn;

                this.PlacementRef.Show();
                this.Image_Placement.Source = new BitmapImage(new Uri("/Antize TFT;component/Ressources/PositionOn.png", UriKind.Relative));
            }
        }
    }
}
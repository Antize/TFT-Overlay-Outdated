using System;
using System.Collections;
using System.Xml.Serialization;

namespace Antize_TFT.Class
{
    [XmlRoot("UserSettings")]
    public class UserSettings : ICloneable
    {
        public object Clone()
        {
            return this.MemberwiseClone();
        }

        public UserSettings()
        {
            this.OnlyOneType = true;

            this.SelectedProfile = 1;
            this.SelectedSubProfile = 0;

            this.Profile1 = new ArrayList();
            this.Profile2 = new ArrayList();
            this.Profile3 = new ArrayList();
            this.Profile4 = new ArrayList();
            this.Profile5 = new ArrayList();

            //Ver.0.6
            this.UpdateLink = "https://github.com/Antize/TFT-Overlay";
            this.UpdateFindStart = "(Ver.";
            this.UpdateFindEnd = ")";

            this.EnableCheckForUpdate = true;
            this.ShowErrors = false;

            //Ver.0.7
            this.PositionLeft = true;

            //Ver.1.0 |Sub 4 Ver.1.2
            this.Profile1Sub1 = new ArrayList();
            this.Profile1Sub2 = new ArrayList();
            this.Profile1Sub3 = new ArrayList();
            this.Profile1Sub4 = new ArrayList();

            this.Profile2Sub1 = new ArrayList();
            this.Profile2Sub2 = new ArrayList();
            this.Profile2Sub3 = new ArrayList();
            this.Profile2Sub4 = new ArrayList();

            this.Profile3Sub1 = new ArrayList();
            this.Profile3Sub2 = new ArrayList();
            this.Profile3Sub3 = new ArrayList();
            this.Profile3Sub4 = new ArrayList();

            this.Profile4Sub1 = new ArrayList();
            this.Profile4Sub2 = new ArrayList();
            this.Profile4Sub3 = new ArrayList();
            this.Profile4Sub4 = new ArrayList();

            this.Profile5Sub1 = new ArrayList();
            this.Profile5Sub2 = new ArrayList();
            this.Profile5Sub3 = new ArrayList();
            this.Profile5Sub4 = new ArrayList();

            //Ver.1.2
            this.NameProfile1 = "1";
            this.NameProfile2 = "2";
            this.NameProfile3 = "3";
            this.NameProfile4 = "4";
            this.NameProfile5 = "5";

            this.NameProfile1Sub1 = "1";
            this.NameProfile1Sub2 = "2";
            this.NameProfile1Sub3 = "3";
            this.NameProfile1Sub4 = "4";

            this.NameProfile2Sub1 = "1";
            this.NameProfile2Sub2 = "2";
            this.NameProfile2Sub3 = "3";
            this.NameProfile2Sub4 = "4";

            this.NameProfile3Sub1 = "1";
            this.NameProfile3Sub2 = "2";
            this.NameProfile3Sub3 = "3";
            this.NameProfile3Sub4 = "4";

            this.NameProfile4Sub1 = "1";
            this.NameProfile4Sub2 = "2";
            this.NameProfile4Sub3 = "3";
            this.NameProfile4Sub4 = "4";

            this.NameProfile5Sub1 = "1";
            this.NameProfile5Sub2 = "2";
            this.NameProfile5Sub3 = "3";
            this.NameProfile5Sub4 = "4";

            //Ver 1.47
            this.OpacityOut = 0.5;
            this.OpacityIn = 0.9;

            //Ver 1.49
            this.AppName = "League of Legends";
            this.AutoShowHide = true;

            //Ver 1.53
            this.BaseKey = "LeftCtrl";

            this.ShowHideKey = "w";
            this.ChampionKey = "x";
            this.ItemKey = "c";
            this.ItemCombineKey = "v";
        }

        public void MakeDefaultVariables()
        {
            this.UpdateLink = "https://github.com/Antize/TFT-Overlay";
            this.UpdateFindStart = "(Ver.";
            this.UpdateFindEnd = ")";

            this.EnableCheckForUpdate = true;
            this.ShowErrors = false;

            this.BaseKey = "LeftCtrl";

            this.ShowHideKey = "w";
            this.ChampionKey = "x";
            this.ItemKey = "c";
            this.ItemCombineKey = "v";
        }

        //
        public string UpdateLink
        {
            get; set;
        }

        public string UpdateFindStart
        {
            get; set;
        }

        public string UpdateFindEnd
        {
            get; set;
        }

        public bool EnableCheckForUpdate
        {
            get; set;
        }

        public bool ShowErrors
        {
            get; set;
        }

        //1.53
        public string BaseKey
        {
            get; set;
        }

        public string ShowHideKey
        {
            get; set;
        }

        public string ChampionKey
        {
            get; set;
        }

        public string ItemKey
        {
            get; set;
        }

        public string ItemCombineKey
        {
            get; set;
        }

        public bool PositionLeft
        {
            get; set;
        }

        public double OpacityOut
        {
            get; set;
        }

        public double OpacityIn
        {
            get; set;
        }

        public string AppName
        {
            get; set;
        }

        public bool AutoShowHide
        {
            get; set;
        }

        public string NameProfile1
        {
            get; set;
        }

        public string NameProfile1Sub1
        {
            get; set;
        }

        public string NameProfile1Sub2
        {
            get; set;
        }

        public string NameProfile1Sub3
        {
            get; set;
        }

        public string NameProfile1Sub4
        {
            get; set;
        }

        public string NameProfile2
        {
            get; set;
        }

        public string NameProfile2Sub1
        {
            get; set;
        }

        public string NameProfile2Sub2
        {
            get; set;
        }

        public string NameProfile2Sub3
        {
            get; set;
        }

        public string NameProfile2Sub4
        {
            get; set;
        }

        public string NameProfile3
        {
            get; set;
        }

        public string NameProfile3Sub1
        {
            get; set;
        }

        public string NameProfile3Sub2
        {
            get; set;
        }

        public string NameProfile3Sub3
        {
            get; set;
        }

        public string NameProfile3Sub4
        {
            get; set;
        }

        public string NameProfile4
        {
            get; set;
        }

        public string NameProfile4Sub1
        {
            get; set;
        }

        public string NameProfile4Sub2
        {
            get; set;
        }

        public string NameProfile4Sub3
        {
            get; set;
        }

        public string NameProfile4Sub4
        {
            get; set;
        }

        public string NameProfile5
        {
            get; set;
        }

        public string NameProfile5Sub1
        {
            get; set;
        }

        public string NameProfile5Sub2
        {
            get; set;
        }

        public string NameProfile5Sub3
        {
            get; set;
        }

        public string NameProfile5Sub4
        {
            get; set;
        }

        public bool OnlyOneType
        {
            get; set;
        }

        public int SelectedProfile
        {
            get; set;
        }

        public int SelectedSubProfile
        {
            get; set;
        }

        public ArrayList Profile1
        {
            get; set;
        }

        public ArrayList Profile1Sub1
        {
            get; set;
        }

        public ArrayList Profile1Sub2
        {
            get; set;
        }

        public ArrayList Profile1Sub3
        {
            get; set;
        }

        public ArrayList Profile1Sub4
        {
            get; set;
        }

        public ArrayList Profile2
        {
            get; set;
        }

        public ArrayList Profile2Sub1
        {
            get; set;
        }

        public ArrayList Profile2Sub2
        {
            get; set;
        }

        public ArrayList Profile2Sub3
        {
            get; set;
        }

        public ArrayList Profile2Sub4
        {
            get; set;
        }

        public ArrayList Profile3
        {
            get; set;
        }

        public ArrayList Profile3Sub1
        {
            get; set;
        }

        public ArrayList Profile3Sub2
        {
            get; set;
        }

        public ArrayList Profile3Sub3
        {
            get; set;
        }

        public ArrayList Profile3Sub4
        {
            get; set;
        }

        public ArrayList Profile4
        {
            get; set;
        }

        public ArrayList Profile4Sub1
        {
            get; set;
        }

        public ArrayList Profile4Sub2
        {
            get; set;
        }

        public ArrayList Profile4Sub3
        {
            get; set;
        }

        public ArrayList Profile4Sub4
        {
            get; set;
        }

        public ArrayList Profile5
        {
            get; set;
        }

        public ArrayList Profile5Sub1
        {
            get; set;
        }

        public ArrayList Profile5Sub2
        {
            get; set;
        }

        public ArrayList Profile5Sub3
        {
            get; set;
        }

        public ArrayList Profile5Sub4
        {
            get; set;
        }

        public string NoteProfile1
        {
            get; set;
        }

        public string NoteProfile1Sub1
        {
            get; set;
        }

        public string NoteProfile1Sub2
        {
            get; set;
        }

        public string NoteProfile1Sub3
        {
            get; set;
        }

        public string NoteProfile1Sub4
        {
            get; set;
        }

        public string NoteProfile2
        {
            get; set;
        }

        public string NoteProfile2Sub1
        {
            get; set;
        }

        public string NoteProfile2Sub2
        {
            get; set;
        }

        public string NoteProfile2Sub3
        {
            get; set;
        }

        public string NoteProfile2Sub4
        {
            get; set;
        }

        public string NoteProfile3
        {
            get; set;
        }

        public string NoteProfile3Sub1
        {
            get; set;
        }

        public string NoteProfile3Sub2
        {
            get; set;
        }

        public string NoteProfile3Sub3
        {
            get; set;
        }

        public string NoteProfile3Sub4
        {
            get; set;
        }

        public string NoteProfile4
        {
            get; set;
        }

        public string NoteProfile4Sub1
        {
            get; set;
        }

        public string NoteProfile4Sub2
        {
            get; set;
        }

        public string NoteProfile4Sub3
        {
            get; set;
        }

        public string NoteProfile4Sub4
        {
            get; set;
        }

        public string NoteProfile5
        {
            get; set;
        }

        public string NoteProfile5Sub1
        {
            get; set;
        }

        public string NoteProfile5Sub2
        {
            get; set;
        }

        public string NoteProfile5Sub3
        {
            get; set;
        }

        public string NoteProfile5Sub4
        {
            get; set;
        }
    }
}
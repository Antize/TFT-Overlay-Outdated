using System;
using System.Collections.Generic;
using System.Collections;
using Antize_TFT.Enums;
using System.IO;
using System.Xml.Serialization;
using Antize_TFT.Fenetre;
using System.Windows.Media;
using System.Net;
using System.Xml.Linq;
using System.Linq;
using System.Windows;

namespace Antize_TFT.Class
{
    static class TacheFond
    {
        private static readonly string PathActualFolder;

        private static MyLanguage LocalizationRef;

        private static MainWindow MainRef;
        private static UserSettings UserSettingsRef;
        private static UserSettings OptionsUserSettingsRef;
        private static UserItems UserItemsRef;

        private static bool InStateTraitement;

        private static double MyVer;
        private static bool InCheckForUpdate;

        public static Enum_Item LastItemClicked;

        static TacheFond()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
            MyVer = -1;

            PathActualFolder = AppDomain.CurrentDomain.BaseDirectory;

            UserSettingsRef = new UserSettings();
            OptionsUserSettingsRef = new UserSettings();

            UserItemsRef = new UserItems();

            ListChampions = new ArrayList();
            ListOrigineClasse = new ArrayList();
            ListItems = new ArrayList();

            LocalizationRef = new MyLanguage();

            InStateTraitement = false;
        }

        public static MyLanguage GetLocalizationRef
        {
            get { return LocalizationRef; }
        }

        public static MainWindow GetMainRef
        {
            get { return MainRef; }
        }

        public static ArrayList ListChampions
        {
            get; set;
        }

        public static ArrayList ListOrigineClasse
        {
            get; set;
        }

        public static ArrayList ListItems
        {
            get; set;
        }

        public static UserSettings GetUserSettingsRef
        {
            get { return UserSettingsRef; }
        }

        public static UserSettings GetOptionsUserSettingsRef
        {
            get { return OptionsUserSettingsRef; }
        }

        private static Enum_Classe GetEnumClasseOrigin(string _FDP)
        {
            foreach (Enum_Classe MyEnum in (Enum_Classe[])Enum.GetValues(typeof(Enum_Classe)))
            {
                if (_FDP.Contains(MyEnum.ToString()))
                {
                    return MyEnum;
                }
            }

            return Enum_Classe.None;
        }

        private static Enum_Item GetEnumItem(string _FDP)
        {
            foreach (Enum_Item MyEnum in (Enum_Item[])Enum.GetValues(typeof(Enum_Item)))
            {
                if (_FDP.Contains(MyEnum.ToString()))
                {
                    return MyEnum;
                }
            }

            return Enum_Item.None;
        }

        public static void IntializeSettings(MainWindow _MainRef)
        {
            MainRef = _MainRef;

            bool Local_FailledFile = false;

            bool Local_Failled = false;

            Dictionary<string, string> Options = new Dictionary<string, string>();
            string[] ResultatBrut;

            if (File.Exists(PathActualFolder + "/Ressources.xml"))
            {
                //Essaye de récupérer les paramètres dans le fichier
                try
                {
                    foreach (XElement item in XElement.Load(PathActualFolder + "/Ressources.xml").Elements())
                    {
                        Options.Add(item.Attribute("key").Value, item.Attribute("value").Value);
                    }

                    if (Options.Count <= 0)
                    {
                        Local_FailledFile = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"Ressources - Get file : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);

                    Local_FailledFile = true;
                }
            }
            else
            {
                Local_FailledFile = true;
            }

            Local_Failled = false;

            //Fait les champion - soit par fichier ou soit par logiciel
            if (Local_FailledFile == false)
            {
                try
                {
                    ResultatBrut = Options["Champions"].Split(new[] { "||" }, StringSplitOptions.None);

                    if (ResultatBrut.Length <= 0)
                    {
                        Local_Failled = true;
                    }
                    else
                    {
                        foreach (string item in ResultatBrut)
                        {
                            string[] Resultat = item.Split('|');

                            string Local_Link = Resultat[7];

                            if (bool.Parse(Resultat[8]))
                            {
                                Local_Link = PathActualFolder + Resultat[9];
                            }

                            ListChampions.Add(new Champion(int.Parse(Resultat[0]), Resultat[1], Resultat[2], Resultat[3], GetEnumClasseOrigin(Resultat[4]), GetEnumClasseOrigin(Resultat[5]), GetEnumClasseOrigin(Resultat[6]), Local_Link));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"Ressources - Make champion : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);

                    Local_Failled = true;
                }
            }

            if (Local_FailledFile == true || Local_Failled == true)
            {
                //Tier 1
                ListChampions.Add(new Champion(1, "Diana", "550 / 990 / 1782", "35 / 63 / 126", Enum_Classe.Inferno, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Diana.png"));
                ListChampions.Add(new Champion(1, "Ivern", "600 / 1080 / 1944", "30 / 54 / 108", Enum_Classe.Woodland, Enum_Classe.Druid, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Ivern.png"));
                ListChampions.Add(new Champion(1, "KogMaw", "500 / 900 / 1620", "18 / 32 / 63", Enum_Classe.Poison, Enum_Classe.Predator, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/KogMaw.png"));
                ListChampions.Add(new Champion(1, "Maokai", "650 / 1170 / 2106", "28 / 50 / 99", Enum_Classe.Woodland, Enum_Classe.Druid, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Maokai.png"));
                ListChampions.Add(new Champion(1, "Nasus", "650 / 1170 / 2106", "28 / 50 / 99", Enum_Classe.Light, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Nasus.png"));
                ListChampions.Add(new Champion(1, "Ornn", "650 / 1170 / 2106", "28 / 50 / 99", Enum_Classe.Electric, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Ornn.png"));
                ListChampions.Add(new Champion(1, "Renekton", "600 / 1080 / 1944", "36 / 65 / 130", Enum_Classe.Desert, Enum_Classe.Berserker, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Renekton.png"));
                ListChampions.Add(new Champion(1, "Taliyah", "500 / 900 / 1620", "26 / 47 / 94", Enum_Classe.Mountain, Enum_Classe.Mage, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Taliyah.png"));
                ListChampions.Add(new Champion(1, "Vladimir", "550 / 990 / 1782", "26 / 47 / 94", Enum_Classe.Ocean, Enum_Classe.Mage, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Vladimir.png"));
                ListChampions.Add(new Champion(1, "Zyra", "500 / 900 / 1620", "16 / 29 / 59", Enum_Classe.Inferno, Enum_Classe.Summoner, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Zyra.png"));
                ListChampions.Add(new Champion(1, "Vayne", "550 / 990 / 1782", "30 / 54 / 108", Enum_Classe.Light, Enum_Classe.Ranger, Enum_Classe.None, " /Antize TFT;component/Ressources/Champions/Vayne.png"));
                ListChampions.Add(new Champion(1, "Warwick", "650 / 1170 / 2106", "30 / 54 / 108", Enum_Classe.Glacial, Enum_Classe.Predator, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Warwick.png"));
                
                //Tier 2
                ListChampions.Add(new Champion(2, "Jax", "700 / 1260 / 2268", "44 / 79 / 158", Enum_Classe.Light, Enum_Classe.Berserker, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Jax.png"));
                ListChampions.Add(new Champion(2, "LeBlanc", "550 / 990 / 1782", "39 / 69 / 139", Enum_Classe.Woodland, Enum_Classe.Assassin, Enum_Classe.Mage, "/Antize TFT;component/Ressources/Champions/LeBlanc.png"));
                ListChampions.Add(new Champion(2, "Braum", "750 / 1350 / 2430", "24 / 43 / 86", Enum_Classe.Glacial, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Braum.png"));
                ListChampions.Add(new Champion(2, "Malzahar", "550 / 990 / 1782", "26 / 47 / 94", Enum_Classe.Shadow, Enum_Classe.Summoner, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Malzahar.png"));
                ListChampions.Add(new Champion(2, "Neeko", "500 / 900 / 1620", "31 / 57 / 113", Enum_Classe.Woodland, Enum_Classe.Druid, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Neeko.png"));
                ListChampions.Add(new Champion(2, "Skarner", "650 / 1170 / 2106", "39 / 70 / 140", Enum_Classe.Crystal, Enum_Classe.Predator, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Skarner.png"));
                ListChampions.Add(new Champion(2, "Syndra", "500 / 900 / 1620", "28 / 50 / 101", Enum_Classe.Ocean, Enum_Classe.Mage, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Syndra.png"));              
                ListChampions.Add(new Champion(2, "RekSai", "650 / 1170 / 2106", "42 / 76 / 151", Enum_Classe.Steel, Enum_Classe.Predator, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/RekSai.png"));
                ListChampions.Add(new Champion(2, "Thresh", "700 / 1260 / 2268", "30 / 54 / 109", Enum_Classe.Ocean, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Thresh.png"));
                ListChampions.Add(new Champion(2, "Volibear", "850 / 1530 / 2754", "42 / 76 / 151", Enum_Classe.Electric, Enum_Classe.Glacial, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Volibear.png"));
                ListChampions.Add(new Champion(2, "Varus", "550 / 990 / 1782", "35 / 63 / 126", Enum_Classe.Inferno, Enum_Classe.Ranger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Varus.png"));
                ListChampions.Add(new Champion(2, "Yasuo", "600 / 1080 / 1944", "42 / 76 / 151", Enum_Classe.Cloud, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Yasuo.png"));

                //Tier 3
                ListChampions.Add(new Champion(3, "Aatrox", "800 / 1440 / 2592", "42 / 76 / 152", Enum_Classe.Light, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Aatrox.png"));
                ListChampions.Add(new Champion(3, "Azir", "600 / 1080 / 1944", "44 / 79 / 158", Enum_Classe.Desert, Enum_Classe.Summoner, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Azir.png"));
                ListChampions.Add(new Champion(3, "Dr Mundo", "750 / 1350 / 2430", "36 / 65 / 130", Enum_Classe.Poison, Enum_Classe.Berserker, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/DrMundo.png"));
                ListChampions.Add(new Champion(3, "Ezreal", "650 / 1080 / 1944", "41 / 74 / 149", Enum_Classe.Glacial, Enum_Classe.Ranger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Ezreal.png"));
                ListChampions.Add(new Champion(3, "Kindred", "650 / 1170 / 2106", "55 / 99 / 198", Enum_Classe.Inferno, Enum_Classe.Shadow, Enum_Classe.Ranger, "/Antize TFT;component/Ressources/Champions/Kindred.png"));
                ListChampions.Add(new Champion(3, "Nautilus", "850 / 1530 / 2754", "33 / 59 / 119", Enum_Classe.Ocean, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Nautilus.png"));
                ListChampions.Add(new Champion(3, "Nocturne", "650 / 1170 / 2106", "39 / 69 / 139", Enum_Classe.Steel, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Nocturne.png"));
                ListChampions.Add(new Champion(3, "Qiyana", "650 / 1170 / 2106", "46 / 82 / 164", Enum_Classe.Variable, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Qiyana.png"));
                ListChampions.Add(new Champion(3, "Sion", "850 / 1530 / 2754", "42 / 76 / 152", Enum_Classe.Shadow, Enum_Classe.Berserker, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Sion.png"));
                ListChampions.Add(new Champion(3, "Sivir", "600 / 1080 / 1944", "39 / 69 / 139", Enum_Classe.Desert, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Sivir.png"));
                ListChampions.Add(new Champion(3, "Veigar", "600 / 1080 / 1944", "30 / 54 / 108", Enum_Classe.Shadow, Enum_Classe.Mage, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Veigar.png"));
                ListChampions.Add(new Champion(3, "Soraka", "600 / 1080 / 1944", "28 / 50 / 101", Enum_Classe.Light, Enum_Classe.Mystic, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Soraka.png"));

                //Tier 4
                ListChampions.Add(new Champion(4, "Annie", "700 / 1260 / 2268", "31 / 57 / 113", Enum_Classe.Inferno, Enum_Classe.Summoner, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Annie.png"));
                ListChampions.Add(new Champion(4, "Ashe", "550 / 990 / 1782", "48 / 86 / 173", Enum_Classe.Crystal, Enum_Classe.Ranger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Ashe.png"));
                ListChampions.Add(new Champion(4, "Brand", "700 / 1260 / 2268", "39 / 69 / 139", Enum_Classe.Inferno, Enum_Classe.Mage, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Brand.png"));
                ListChampions.Add(new Champion(4, "Janna", "600 / 1080 / 1944", "31 / 57 / 113", Enum_Classe.Cloud, Enum_Classe.Mystic, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Janna.png"));
                ListChampions.Add(new Champion(4, "Khazix", "750 / 1350 / 2430", "60 / 108 / 216", Enum_Classe.Desert, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Khazix.png"));
                ListChampions.Add(new Champion(4, "Malphite", "850 / 1530 / 2754", "33 / 59 / 119", Enum_Classe.Mountain, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Malphite.png"));
                ListChampions.Add(new Champion(4, "Olaf", "750 / 1350 / 2430", "56 / 101 / 181", Enum_Classe.Glacial, Enum_Classe.Berserker, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Olaf.png"));
                ListChampions.Add(new Champion(4, "Twitch", "650 / 1170 / 2106", "49 / 88 / 176", Enum_Classe.Poison, Enum_Classe.Ranger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Twitch.png"));
                ListChampions.Add(new Champion(4, "Yorick", "800 / 1440 / 2592", "46 / 82 / 164", Enum_Classe.Light, Enum_Classe.Summoner, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Yorick.png"));

                //Tier 5
                ListChampions.Add(new Champion(5, "Master Yi", "850 / 1530 / 2754", "70 / 126 / 252", Enum_Classe.Shadow, Enum_Classe.Blademaster, Enum_Classe.Mystic, "/Antize TFT;component/Ressources/Champions/MasterYi.png"));
                ListChampions.Add(new Champion(5, "Nami", "750 / 1350 / 2430", "38 / 68 / 135", Enum_Classe.Ocean, Enum_Classe.Mystic, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Nami.png"));
                ListChampions.Add(new Champion(5, "Singed", "950 / 1710 / 3078", "0 / 0 / 0", Enum_Classe.Poison, Enum_Classe.Alchemist, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Singed.png"));
                ListChampions.Add(new Champion(5, "Taric", "900 / 1620 / 2916", "39 / 70 / 140", Enum_Classe.Crystal, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Taric.png"));
                ListChampions.Add(new Champion(5, "Zed", "850 / 1530 / 2754", "70 / 126 / 252", Enum_Classe.Electric, Enum_Classe.Assassin, Enum_Classe.Summoner, "/Antize TFT;component/Ressources/Champions/Zed.png"));

                //Tier 7
                ListChampions.Add(new Champion(7, "Lux", "850 / 1530 / 2754", "55 / 99 / 199", Enum_Classe.None, Enum_Classe.Avatar, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Lux.png"));

                //9.24
                ListChampions.Add(new Champion(2, "Senna", "450 / 810 / 1458", "31 / 57 / 113", Enum_Classe.Shadow, Enum_Classe.Soulbound, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Senna.png"));
                ListChampions.Add(new Champion(2, "Lucian", "550 / 990 / 1782", "52 / 94 / 187", Enum_Classe.Light, Enum_Classe.Soulbound, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Lucian.png"));
                ListChampions.Add(new Champion(2, "Amumu", "900 / 1620 / 2916", "41 / 74 / 149", Enum_Classe.Inferno, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Amumu.png"));

                //10.1
                ListChampions.Add(new Champion(1, "Leona", "650 / 1170 / 2106", "28 / 50 / 99", Enum_Classe.Lunar, Enum_Classe.Warden, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Leona.png"));
                ListChampions.Add(new Champion(3, "Karma", "600 / 1080 / 1944", "35 / 63 / 126", Enum_Classe.Lunar, Enum_Classe.Mystic, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Karma.png"));
              
                /*//Tier 1
                ListChampions.Add(new Champion(1, "Darius", "600 / 1080 / 2160", "25 / 45 / 90", Enum_Classe.Imperial, Enum_Classe.Knight, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Darius.png"));
                ListChampions.Add(new Champion(1, "Elise", "500 / 900 / 1800", "27 / 49 / 97", Enum_Classe.Demon, Enum_Classe.Shapeshifter, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Elise.png"));
                ListChampions.Add(new Champion(1, "Fiora", "450 / 810 / 1620", "40 / 72 / 144", Enum_Classe.Noble, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Fiora.png"));
                ListChampions.Add(new Champion(1, "Garen", "600 / 1080 / 2160", "30 / 54 / 108", Enum_Classe.Noble, Enum_Classe.Knight, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Garen.png"));
                ListChampions.Add(new Champion(1, "Graves", "450 / 810 / 1620", "30 / 54 / 109", Enum_Classe.Pirate, Enum_Classe.Gunslinger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Graves.png"));
                ListChampions.Add(new Champion(1, "Kassadin", "550 / 990 / 1980", "22 / 40 / 79", Enum_Classe.Void, Enum_Classe.Sorcerer, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Kassadin.png"));
                ListChampions.Add(new Champion(1, "Khazix", "500 / 900 / 1800", "33 / 59 / 119", Enum_Classe.Void, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Khazix.png"));
                ListChampions.Add(new Champion(1, "Mordekaiser", "550 / 990 / 1980", "25 / 45 / 90", Enum_Classe.Phantom, Enum_Classe.Knight, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Mordekaiser.png"));
                ListChampions.Add(new Champion(1, "Nidalee", "500 / 900 / 1800", "33 / 59 / 117", Enum_Classe.Wild, Enum_Classe.Shapeshifter, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Nidalee.png"));
                ListChampions.Add(new Champion(1, "Tristana", "500 / 900 / 1800", "33 / 59 / 117", Enum_Classe.Yordle, Enum_Classe.Gunslinger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Tristana.png"));
                ListChampions.Add(new Champion(1, "Vayne", "550 / 990 / 1980", "28 / 50 / 101", Enum_Classe.Noble, Enum_Classe.Ranger, Enum_Classe.None, " /Antize TFT;component/Ressources/Champions/Vayne.png"));
                ListChampions.Add(new Champion(1, "Warwick", "650 / 1170 / 2340", "30 / 54 / 108", Enum_Classe.Wild, Enum_Classe.Brawler, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Warwick.png"));

                //Tier 2
                ListChampions.Add(new Champion(2, "Ahri", "450 / 810 / 1620", "28 / 50 / 99", Enum_Classe.Wild, Enum_Classe.Sorcerer, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Ahri.png"));
                ListChampions.Add(new Champion(2, "Blitzcrank", "650 / 1170 / 2340", "25 / 45 / 90", Enum_Classe.Robot, Enum_Classe.Brawler, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Blitzcrank.png"));
                ListChampions.Add(new Champion(2, "Braum", "650 / 1170 / 2340", "24 / 43 / 86", Enum_Classe.Glacial, Enum_Classe.Guardian, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Braum.png"));
                ListChampions.Add(new Champion(2, "Lissandra", "500 / 900 / 1800", "24 / 43 / 86", Enum_Classe.Glacial, Enum_Classe.Elementalist, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Lissandra.png"));
                
                ListChampions.Add(new Champion(2, "Lulu", "500 / 900 / 1800", "30 / 54 / 108", Enum_Classe.Yordle, Enum_Classe.Sorcerer, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Lulu.png"));
                ListChampions.Add(new Champion(2, "Pyke", "600 / 1080 / 2160", "36 / 65 / 130", Enum_Classe.Pirate, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Pyke.png"));
                ListChampions.Add(new Champion(2, "RekSai", "650 / 1170 / 2340", "30 / 54 / 108", Enum_Classe.Void, Enum_Classe.Brawler, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/RekSai.png"));
                ListChampions.Add(new Champion(2, "Shen", "700 / 1260 / 2520", "46 / 82 / 164", Enum_Classe.Ninja, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Shen.png"));
                ListChampions.Add(new Champion(2, "Twisted Fate", "500 / 900 / 1800", "30 / 54 / 108", Enum_Classe.Pirate, Enum_Classe.Sorcerer, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/TwistedFate.png"));
                ListChampions.Add(new Champion(2, "Varus", "500 / 900 / 1800", "42 / 76 / 151", Enum_Classe.Demon, Enum_Classe.Ranger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Varus.png"));
                ListChampions.Add(new Champion(2, "Zed", "550 / 990 / 1980", "46 / 82 / 164", Enum_Classe.Ninja, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Zed.png"));

                //Tier 3
                ListChampions.Add(new Champion(3, "Aatrox", "700 / 1260 / 2520", "42 / 76 / 152", Enum_Classe.Demon, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Aatrox.png"));
                ListChampions.Add(new Champion(3, "Ashe", "550 / 990 / 1980", "46 / 82 / 164", Enum_Classe.Glacial, Enum_Classe.Ranger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Ashe.png"));
                ListChampions.Add(new Champion(3, "Evelynn", "550 / 990 / 1980", "42 / 76 / 151", Enum_Classe.Demon, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Evelynn.png"));
                ListChampions.Add(new Champion(3, "Gangplank", "700 / 1260 / 2520", "42 / 76 / 152", Enum_Classe.Pirate, Enum_Classe.Blademaster, Enum_Classe.Gunslinger, "/Antize TFT;component/Ressources/Champions/Gangplank.png"));
                ListChampions.Add(new Champion(3, "Katarina", "450 / 810 / 1620", "45 / 81 / 162", Enum_Classe.Imperial, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Katarina.png"));
                ListChampions.Add(new Champion(3, "Kennen", "550 / 990 / 1980", "42 / 76 / 152", Enum_Classe.Ninja, Enum_Classe.Yordle, Enum_Classe.Elementalist, "/Antize TFT;component/Ressources/Champions/Kennen.png"));
                ListChampions.Add(new Champion(3, "Morgana", "650 / 1170 / 2340", "30 / 54 / 108", Enum_Classe.Demon, Enum_Classe.Sorcerer, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Morgana.png"));
                ListChampions.Add(new Champion(3, "Poppy", "700 / 1260 / 2520", "25 / 45 / 90", Enum_Classe.Yordle, Enum_Classe.Knight, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Poppy.png"));
                ListChampions.Add(new Champion(3, "Rengar", "550 / 990 / 1980", "42 / 76 / 151", Enum_Classe.Wild, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Rengar.png"));
                ListChampions.Add(new Champion(3, "Shyvana", "650 / 1170 / 2340", "35 / 63 / 126", Enum_Classe.Dragon, Enum_Classe.Shapeshifter, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Shyvana.png"));
                ListChampions.Add(new Champion(3, "Veigar", "500 / 900 / 1800", "25 / 45 / 89", Enum_Classe.Yordle, Enum_Classe.Sorcerer, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Veigar.png"));
                ListChampions.Add(new Champion(3, "Volibear", "750 / 1350 / 2700", "46 / 82 / 164", Enum_Classe.Glacial, Enum_Classe.Brawler, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Volibear.png"));

                //Tier 4
                ListChampions.Add(new Champion(4, "Akali", "650 / 1170 / 2340", "64 / 115 / 230", Enum_Classe.Ninja, Enum_Classe.Assassin, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Akali.png"));
                ListChampions.Add(new Champion(4, "Aurelion Sol", "650 / 1170 / 2340", "24 / 43 / 86", Enum_Classe.Dragon, Enum_Classe.Sorcerer, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/AurelionSol.png"));
                ListChampions.Add(new Champion(4, "Brand", "700 / 1260 / 2520", "36 / 65 / 130", Enum_Classe.Demon, Enum_Classe.Elementalist, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Brand.png"));
                ListChampions.Add(new Champion(4, "Chogath", "1000 / 1800 / 3600", "42 / 76 / 151", Enum_Classe.Void, Enum_Classe.Brawler, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Chogath.png"));
                ListChampions.Add(new Champion(4, "Draven", "650 / 1170 / 2340", "49 / 88 / 176", Enum_Classe.Imperial, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Draven.png"));
                ListChampions.Add(new Champion(4, "Gnar", "700 / 1260 / 2520", "35 / 63 / 126", Enum_Classe.Wild, Enum_Classe.Yordle, Enum_Classe.Shapeshifter, "/Antize TFT;component/Ressources/Champions/Gnar.png"));
                ListChampions.Add(new Champion(4, "Kindred", "600 / 1080 / 2160", "42 / 76 / 151", Enum_Classe.Phantom, Enum_Classe.Ranger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Kindred.png"));
                ListChampions.Add(new Champion(4, "Leona", "800 / 1440 / 2880", "25 / 45 / 89", Enum_Classe.Noble, Enum_Classe.Guardian, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Leona.png"));
                ListChampions.Add(new Champion(4, "Sejuani", "800 / 1440 / 2880", "25 / 45 / 89", Enum_Classe.Glacial, Enum_Classe.Knight, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Sejuani.png"));

                //Tier 5
                ListChampions.Add(new Champion(5, "Anivia", "750 / 1350 / 2700", "32 / 58 / 115", Enum_Classe.Glacial, Enum_Classe.Elementalist, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Anivia.png"));
                ListChampions.Add(new Champion(5, "Karthus", "850 / 1530 / 3060", "42 / 76 / 152", Enum_Classe.Phantom, Enum_Classe.Sorcerer, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Karthus.png"));
                ListChampions.Add(new Champion(5, "Kayle", "800 / 1440 / 2880", "60 / 108 / 216", Enum_Classe.Noble, Enum_Classe.Knight, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Kayle.png"));
                ListChampions.Add(new Champion(5, "Miss Fortune", "750 / 1350 / 2700", "71 / 128 / 257", Enum_Classe.Pirate, Enum_Classe.Gunslinger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/MissFortune.png"));
                ListChampions.Add(new Champion(5, "Swain", "850 / 1530 / 3060", "42 / 76 / 152", Enum_Classe.Demon, Enum_Classe.Imperial, Enum_Classe.Shapeshifter, "/Antize TFT;component/Ressources/Champions/Swain.png"));
                ListChampions.Add(new Champion(5, "Yasuo", " 750 / 1350 / 2700", "75 / 135 / 270", Enum_Classe.Exile, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Yasuo.png"));

                //New
                ListChampions.Add(new Champion(1, "Camille", "550 / 990 / 1980", "33 / 59 / 119", Enum_Classe.Hextech, Enum_Classe.Blademaster, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Camille.png"));
                ListChampions.Add(new Champion(2, "Jayce", "600 / 1080 / 2160", "39 / 69 / 139", Enum_Classe.Hextech, Enum_Classe.Shapeshifter, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Jayce.png"));
                ListChampions.Add(new Champion(3, "Vi", "700 / 1260 / 2520", "36 / 64 / 129", Enum_Classe.Hextech, Enum_Classe.Brawler, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Vi.png"));
                ListChampions.Add(new Champion(4, "Jinx", "550 / 990 / 1980", "53 / 95 / 189", Enum_Classe.Hextech, Enum_Classe.Gunslinger, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Jinx.png"));

                //New 9.17
                ListChampions.Add(new Champion(5, "Pantheon", "850 / 1530 / 3060", "56 / 101 / 203", Enum_Classe.Dragon, Enum_Classe.Guardian, Enum_Classe.None, "/Antize TFT;component/Ressources/Champions/Pantheon.png"));

                //New 9.18
                ListChampions.Add(new Champion(5, "Kaisa", "700 / 1260 / 2520", "69 / 124 / 248", Enum_Classe.Void, Enum_Classe.Assassin, Enum_Classe.Ranger, "/Antize TFT;component/Ressources/Champions/Kaisa.png"));
                */
            }

            //Make Champion ID
            int Local_ID = 0;

            foreach (Champion item in ListChampions)
            {
                item.GetID = Local_ID;
                Local_ID++;
            }

            Local_Failled = false;

            //Fait les classe & origin - soit par fichier ou soit par logiciel
            if (Local_FailledFile == false)
            {
                try
                {
                    ResultatBrut = Options["ClasseOrigin"].Split(new[] { "||" }, StringSplitOptions.None);

                    if (ResultatBrut.Length <= 0)
                    {
                        Local_Failled = true;
                    }
                    else
                    {
                        foreach (string item in ResultatBrut)
                        {
                            string[] Resultat = item.Split('|');

                            ListOrigineClasse.Add(new OrigineClasse(GetEnumClasseOrigin(Resultat[0]), bool.Parse(Resultat[1]), Resultat[2], new Dictionary<int, string>() { { int.Parse(Resultat[3]), Resultat[4] }, { int.Parse(Resultat[5]), Resultat[6] }, { int.Parse(Resultat[7]), Resultat[8] } }));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"Ressources - Make classe & origin : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);

                    Local_Failled = true;
                }
            }

            if (Local_FailledFile == true || Local_Failled == true)
            {
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Alchemist, true, "", new Dictionary<int, string>() { { 1, "Innate: Alchemists ignore collision and never stop moving." }, { -1, "" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Assassin, true, "At the start of combat, Assassins leap to the farthest enemy. Assassins gain bonus Critical Strike Damage and Chance.", new Dictionary<int, string>() { { 3, "+65% Critical Strike Damage & 10% Critical Strike Chance" }, { 6, "+225% Critical Strike Damage & 20% Critical Strike Chance" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Avatar, true, "", new Dictionary<int, string>() { { 1, "An Avatar's Origin Element is counted twice for Trait bonuses." }, { -1, "" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Berserker, true, "At the start of combat, Berserkers leap to the nearest enemy. Berserkers have a 40% chance to hit all units in a cone in front of them with their attacks.", new Dictionary<int, string>() { { 3, "40% Chance" }, { 6, "100% Chance & +25 Attack Damage" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Blademaster, true, "Blademaster Basic Attacks have a 35% chance to trigger additional attacks. These additional attacks deal damage like Basic Attacks and trigger on-hit effects.", new Dictionary<int, string>() { { 2, "1 Extra Attacks" }, { 4, "2 Extra Attacks" }, { 6, "3 Extra Attacks" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Druid, true, "", new Dictionary<int, string>() { { 2, "Druids regenerate 40 health each second." }, { -1, "" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Mage, true, "Mages have a chance on cast to instead Doublecast.", new Dictionary<int, string>() { { 3, "50% Chance" }, { 6, "100% Chance & 20 AP to all Mages" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Mystic, true, "All allies gain increased Magic Resistance.", new Dictionary<int, string>() { { 2, "40 Magic Resistance" }, { 4, "120 Magic Resistance" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Predator, true, "", new Dictionary<int, string>() { { 3, "Predators instantly kill enemies they damage who are below 25% health." }, { -1, "" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Ranger, true, "Every 3 seconds, Rangers have a chance to double their Attack Speed for 3 seconds.", new Dictionary<int, string>() { { 2, "35% Chance to Double Attack Speed" }, { 4, "80% Chance to Double Attack Speed" }, { 6, "100% Chance to gain x2.5 Attack Speed" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Summoner, true, "Summoned units have increased health and duration.", new Dictionary<int, string>() { { 3, "+30% increase" }, { 6, "+120% increase" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Warden, true, "Wardens gain increased total Armor.", new Dictionary<int, string>() { { 2, "+150% Armor" }, { 4, "+300% Armor" }, { 6, "+999% Armor" } }));
                               

                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Cloud, false, "All allies gain dodge chance.", new Dictionary<int, string>() { { 2, "+20% Dodge Chance" }, { 3, "+25% Dodge Chance" }, { 4, "+35% Dodge Chance" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Crystal, false, "Crystal champions have a maximum amount of damage they can take from a single hit.", new Dictionary<int, string>() { { 2, "100 Max Damage" }, { 4, "60 Max Damage" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Desert, false, "Reduces each enemy's armor.", new Dictionary<int, string>() { { 2, "40% Armor Reduction" }, { 4, "100% Armor Reduction" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Electric, false, "Electric champions shock nearby enemies whenever they deal or receive a critical strike.", new Dictionary<int, string>() { { 2, "80 Damage" }, { 3, "250 Damage" }, { 4, "500 Damage" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Glacial, false, "Basic Attacks from Glacials have a chance to stun their target for 1.5 seconds.", new Dictionary<int, string>() { { 2, "20% Chance to Stun" }, { 4, "35% Chance to Stun" }, { 6, "50% Chance to Stun" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Inferno, false, "Inferno spell damage burns the ground beneath the target, dealing a percent of that spell's pre-mitigation damage as magic damage over 4 seconds.", new Dictionary<int, string>() { { 3, "+70% Damage & 1 Hex" }, { 6, "+150% Damage & 3 Hexes" }, { 9, "+250% Damage & 5 Hexes" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Light, false, "When a Light champion dies, all other Light champions gain Attack Speed and are healed for 25% of the champion's Maximum Health.", new Dictionary<int, string>() { { 3, "+15% Attack Speed" }, { 6, "+35% Attack Speed" }, { 9, "+55% Attack Speed" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Mountain, false, "", new Dictionary<int, string>() { { 2, "At the start of combat, a random ally gains a 1500 Stoneshield." }, { -1, "" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Ocean, false, "All allies restore mana every 4 seconds.", new Dictionary<int, string>() { { 2, "+10 Mana" }, { 4, "+30 Mana" }, { 6, "+60 Mana" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Poison, false, "", new Dictionary<int, string>() { { 3, "Poison champions apply Neurotoxin when they deal damage, increasing the target's mana cost by 50%." }, { -1, "" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Shadow, false, "Shadow units deal increased damage for 6 seconds at combat start, refreshed on takedown.", new Dictionary<int, string>() { { 3, "+65% Increased Damage, Self Takedown" }, { 6, "+175% Increased Damage, Any Shadow Takedown" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Steel, false, "Steel champions gain damage immunity for a few seconds when they are reduced below 50% health.", new Dictionary<int, string>() { { 2, "2 Seconds of Immunity" }, { 3, "3 Seconds of Immunity." }, { 4, "4 Seconds of Immunity" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Variable, false, "", new Dictionary<int, string>() { { 1, "Origin is determined by the Element of the current match. It can be Cloud, Inferno, Ocean, or Mountain." }, { -1, "" }, { -2, "" } }));
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Woodland, false, "", new Dictionary<int, string>() { { 3, "At the start of combat, a random Woodland champion makes a copy of themselves." }, { 6, "At the start of combat, all Woodland champions make a copy of themselves." }, { -2, "" } }));

                //9.24
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Soulbound, true, "", new Dictionary<int, string>() { { 2, "The first Soulbound unit to die will instead enter the Spirit Realm, becoming untargetable and continuing to fight as long as another Soulbound unit is alive." }, { -1, "" }, { -2, "" } }));

                //10.10
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Lunar, false, "", new Dictionary<int, string>() { { 2, "Every 7 seconds, your team gains 15% Critical Strike Chance, 15% Critical Strike Damage, and 10% Spell Power (Stacks up to 4 times)." }, { -1, "" }, { -2, "" } }));


                /* ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Assassin, true, "Innate: At the start of combat, Assassins leap to the farthest enemy. Assassins gain bonus Critical Strike Damage and Chance.", new Dictionary<int, string>() { { 3, "+75% Critical Strike Damage & 5% Critical Strike Chance" }, { 6, "+150% Critical Strike Damage & 20% Critical Strike Chance" }, { 9, "+225% Critical Strike Damage & 30% Critical Strike Chance" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Blademaster, true, "Blademasters have a 35% chance to strike additional times each attack.", new Dictionary<int, string>() { { 3, "One Extra Attack" }, { 6, "Two Extra Attack" }, { 9, "Four Extra Attack" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Brawler, true, "Brawlers receive bonus maximum health.", new Dictionary<int, string>() { { 2, "250 Bonus Health" }, { 4, "500 Bonus Health" }, { 6, "900 Bonus Health" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Elementalist, true, "Elementalists gain double mana from attacks.", new Dictionary<int, string>() { { 3, "At the start of combat, summon a Golem with 2200 HP" }, { -1, "" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Guardian, true, "", new Dictionary<int, string>() { { 2, "At the start of combat, Guardians grant +40 Armor to adjacent allies (this bonus can stack any number of times)." }, { -1, "" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Gunslinger, true, "Gunslinger Basic Attacks have a 50% chance to fire additional bullets at other enemies within range. Deal damage like Basic Attacks and apply on-hit effects.", new Dictionary<int, string>() { { 2, "Attack 1 extra enemy" }, { 4, "Attack 2 extra enemies" }, { 6, "Attack 4 extra enemies" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Knight, true, "All allies block a flat amount of damage from all sources.", new Dictionary<int, string>() { { 2, "15 Damage Ignored" }, { 4, "30 Damage Ignored" }, { 6, "60 Damage Ignored" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Shapeshifter, true, "Shapeshifters gain bonus Maximum Health when they transform.", new Dictionary<int, string>() { { 3, "60% Bonus Maximum Health" }, { 6, "100% Bonus Maximum Health" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Sorcerer, true, "Sorcerers gain double mana from attacking and allies have increased ability power.", new Dictionary<int, string>() { { 3, "+40% ability power" }, { 6, "+120% ability power" }, { 9, "+200% ability power" } }));

                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Demon, false, "Demon basic attacks have a 40% chance to burn 20 mana from their target and return some mana to the attacker.", new Dictionary<int, string>() { { 2, "15 mana returned" }, { 4, "30 mana returned" }, { 6, "45 mana returned" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Dragon, false, "", new Dictionary<int, string>() { { 2, "Dragons gain 75% resistance to Magic Damage" }, { -1, "" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Exile, false, "", new Dictionary<int, string>() { { 1, "If an Exile has no adjacent allies at the start of combat, they gain a shield equal to 100% of their maximum health" }, { -1, "" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Glacial, false, "Basic Attacks from Glacials have a chance to stun their target for 1.5 seconds.", new Dictionary<int, string>() { { 2, "20% Chance to Stun" }, { 4, "33% Chance to Stun" }, { 6, "50% Chance to Stun" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Imperial, false, "Imperials deal Double Damage.", new Dictionary<int, string>() { { 2, "One Random Imperial" }, { 4, "All Imperials" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Noble, false, "At the start of combat, select allies gain +50 Armor, +50 Magic Resistance, and heal 30 on-hit.", new Dictionary<int, string>() { { 3, "One Random Ally" }, { 6, "All Allies" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Ninja, false, "Ninjas gain bonus Attack Damage and Spell Power. This trait is only active when you have exactly 1 or 4 unique Ninjas.", new Dictionary<int, string>() { { 1, "Ninja gains +50 Attack Damage & +50% Spell Power" }, { 4, "All Ninjas gain +80 Attack Damage & +80% Spell Power" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Pirate, false, "", new Dictionary<int, string>() { { 3, "Earn up to 4 additional gold after combat against another player" }, { -1, "" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Phantom, false, "", new Dictionary<int, string>() { { 2, "Curse a random enemy at the start of combat, setting their HP to 100" }, { -1, "" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Robot, false, "", new Dictionary<int, string>() { { 1, "Robots start combat at full mana" }, { -1, "" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Void, false, "Basic Attacks and Spells from select Void champions deal true damage.", new Dictionary<int, string>() { { 2, "1 Random Void champion" }, { 4, "All Void champions" }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Wild, false, "Select allies' Basic Attacks are empowered to grant +10% Bonus Attack Speed (stacks up to 5 times).", new Dictionary<int, string>() { { 2, "Wild Allies Only" }, { 4, "All Allies. Basic attacks cannot be dodged." }, { -2, "" } }));
                 ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Yordle, false, "Yordles gain a chance to dodge enemy Basic Attacks.", new Dictionary<int, string>() { { 3, "35% Chance to Miss" }, { 6, "60% Chance to Miss" }, { 9, "90% Chance to Dodge" } }));
                  
                //New
                ListOrigineClasse.Add(new OrigineClasse(Enum_Classe.Hextech, false, "Throw a bomb at an enemy unit with an item, and disables all items in a hex radius for 5 seconds.", new Dictionary<int, string>() { { 2, "1 hex radius" }, { 4, "2 hex radius" }, { -2, "" } }));
                */
            }

            //Make ID
            Local_ID = 0;

            foreach (OrigineClasse item in ListOrigineClasse)
            {
                item.GetID = Local_ID;
                Local_ID++;
            }

            Local_Failled = false;

            //Fait les items - soit par fichier ou soit par logiciel
            if (Local_FailledFile == false)
            {
                try
                {
                    ResultatBrut = Options["Items"].Split(new[] { "||" }, StringSplitOptions.None);

                    if (ResultatBrut.Length <= 0)
                    {
                        Local_Failled = true;
                    }
                    else
                    {
                        foreach (string item in ResultatBrut)
                        {
                            string[] Resultat = item.Split('|');

                            ListItems.Add(new Items(Resultat[0], GetEnumItem(Resultat[1]), GetEnumItem(Resultat[2]), GetEnumItem(Resultat[3]), Resultat[4]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"Ressources - Make items : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);

                    Local_Failled = true;
                }
            }

            if (Local_FailledFile == true || Local_Failled == true)
            {
                ListItems.Add(new Items("B.F. Sword", Enum_Item.BFSword, Enum_Item.None, Enum_Item.None, "+15 Attack Damage."));
                ListItems.Add(new Items("Chain Vest", Enum_Item.ChainVest, Enum_Item.None, Enum_Item.None, "+20 Armor."));
                ListItems.Add(new Items("Giant's Belt", Enum_Item.GiantsBelt, Enum_Item.None, Enum_Item.None, "+200 Health."));
                ListItems.Add(new Items("Needlessly Large Rod", Enum_Item.NeedlesslyLargeRod, Enum_Item.None, Enum_Item.None, "+20% Spell Damage."));
                ListItems.Add(new Items("Negatron Cloak", Enum_Item.NegatronCloak, Enum_Item.None, Enum_Item.None, "+20 Magic Resist."));
                ListItems.Add(new Items("Recurve Bow", Enum_Item.RecurveBow, Enum_Item.None, Enum_Item.None, "+20% Attack Speed."));
                ListItems.Add(new Items("Spatula", Enum_Item.Spatula, Enum_Item.None, Enum_Item.None, "It must do something..."));
                ListItems.Add(new Items("Tear of the Goddess", Enum_Item.TearoftheGoddess, Enum_Item.None, Enum_Item.None, "+20 Mana."));
                ListItems.Add(new Items("Sparring Gloves", Enum_Item.SparringGloves, Enum_Item.None, Enum_Item.None, "+10% Critical Strike Chance. +10% Dodge Chance."));                

                ListItems.Add(new Items("Berserker Axe", Enum_Item.BerserkerAxe, Enum_Item.Spatula, Enum_Item.SparringGloves, "Wearer is also a Berserker."));
                ListItems.Add(new Items("Blade of the Ruined King", Enum_Item.BladeoftheRuinedKing, Enum_Item.Spatula, Enum_Item.RecurveBow, "Wearer is also a Blademaster."));
                ListItems.Add(new Items("Bloodthirster", Enum_Item.Bloodthirster, Enum_Item.BFSword, Enum_Item.NegatronCloak, "Basic Attacks heal the wearer for 50% of the damage dealt."));
                ListItems.Add(new Items("Deathblade", Enum_Item.Deathblade, Enum_Item.BFSword, Enum_Item.BFSword, "Whenever the wearer kills or participates in killing an enemy, gain +15 Attack Damage for the remainder of combat. This effect can stack any number of times."));
                ListItems.Add(new Items("Dragon's Claw", Enum_Item.DragonsClaw, Enum_Item.NegatronCloak, Enum_Item.NegatronCloak, "Wearer gains 50% resistance to magic damage."));
                ListItems.Add(new Items("Force of Nature", Enum_Item.ForceofNature, Enum_Item.Spatula, Enum_Item.Spatula, "Gain +1 team size."));
                ListItems.Add(new Items("Frozen Heart", Enum_Item.FrozenHeart, Enum_Item.ChainVest, Enum_Item.TearoftheGoddess, "Nearby enemies' attack speed is slowed by 40%. (Stacking increases the radius of this effect, not the amount of the slow)"));
                ListItems.Add(new Items("Frozen Mallet", Enum_Item.FrozenMallet, Enum_Item.GiantsBelt, Enum_Item.Spatula, "Wearer is also a Glacial."));
                ListItems.Add(new Items("Giant Slayer", Enum_Item.GiantSlayer, Enum_Item.BFSword, Enum_Item.RecurveBow, "The wearer's basic attacks deal an additional 9% of the target's Maximum Health as true damage."));
                ListItems.Add(new Items("Guardian Angel", Enum_Item.GuardianAngel, Enum_Item.BFSword, Enum_Item.ChainVest, "When the wearer dies, they cleanse negative effects and revive with up to 500 Health after a 2 second delay. This effect can trigger once per combat."));
                ListItems.Add(new Items("Guinsoo's Rageblade", Enum_Item.GuinsoosRageblade, Enum_Item.NeedlesslyLargeRod, Enum_Item.RecurveBow, "Attacks grant 5% Attack Speed. Stacks infinitely."));
                ListItems.Add(new Items("Hand of Justice", Enum_Item.HandofJustice, Enum_Item.TearoftheGoddess, Enum_Item.SparringGloves, "At the beginning of each planning phase, the wearer gains one of the following: Basic Attacks and Spells deal +50% Damage or Basic Attacks heal 50 Health on-hit."));
                ListItems.Add(new Items("Hextech Gunblade", Enum_Item.HextechGunblade, Enum_Item.BFSword, Enum_Item.NeedlesslyLargeRod, "Basic Attacks and spells heal the wearer for 33% of the damage dealt."));
                ListItems.Add(new Items("Hush", Enum_Item.Hush, Enum_Item.NegatronCloak, Enum_Item.TearoftheGoddess, "Basic Attacks have a 20% chance to silence the target on-hit, prevent the enemy from gaining mana for 4 seconds."));
                ListItems.Add(new Items("Iceborne Gauntlet", Enum_Item.IceborneGauntlet, Enum_Item.ChainVest, Enum_Item.SparringGloves, "After casting a spell, the wearer’s next basic attack freezes the target for 2.5 seconds."));
                ListItems.Add(new Items("Inferno Cinder", Enum_Item.InfernoCinder, Enum_Item.Spatula, Enum_Item.NeedlesslyLargeRod, "Wearer is also an Inferno."));
                ListItems.Add(new Items("Infinity Edge", Enum_Item.InfinityEdge, Enum_Item.BFSword, Enum_Item.SparringGloves, "The wearer gains +125% Critical Strike Damage."));
                ListItems.Add(new Items("Ionic Spark", Enum_Item.IonicSpark, Enum_Item.NeedlesslyLargeRod, Enum_Item.NegatronCloak, "Enemies within 3 hexes that cast a spell are zapped, taking magic damage equal to 200% of their max Mana."));
                ListItems.Add(new Items("Jeweled Gauntlet", Enum_Item.JeweledGauntlet, Enum_Item.NeedlesslyLargeRod, Enum_Item.SparringGloves, "The wearer's spells can critically strike."));
                ListItems.Add(new Items("Locket of the Iron Solari", Enum_Item.LocketoftheIronSolari, Enum_Item.ChainVest, Enum_Item.NeedlesslyLargeRod, "Shields allies within two hexes in the same row for 250/275/350 damage for 8 seconds (scales with wearer’s Star Level)."));
                ListItems.Add(new Items("Luden's Echo", Enum_Item.LudensEcho, Enum_Item.TearoftheGoddess, Enum_Item.NeedlesslyLargeRod, "Deals 125/175/250 magic damage (scales with wearer’s Star Level)"));
                ListItems.Add(new Items("Mage's Cap", Enum_Item.MagesCap, Enum_Item.Spatula, Enum_Item.TearoftheGoddess, "Wearer is also a Mage."));
                ListItems.Add(new Items("Morellonomicon", Enum_Item.Morellonomicon, Enum_Item.GiantsBelt, Enum_Item.NeedlesslyLargeRod, "Spells burn for 18% of max HP over 10 seconds and reduce healing effects by 80%."));
                //ListItems.Add(new Items("Phantom Dancer", Enum_Item.PhantomDancer, Enum_Item.ChainVest, Enum_Item.RecurveBow, "Wearer dodges all Critical Strikes."));
                ListItems.Add(new Items("Quicksilver", Enum_Item.Quicksilver, Enum_Item.NegatronCloak, Enum_Item.SparringGloves, "[UNIQUE] The wearer is immune to Crowd Control."));
                ListItems.Add(new Items("Rabadon's Deathcap", Enum_Item.RabadonsDeathcap, Enum_Item.NeedlesslyLargeRod, Enum_Item.NeedlesslyLargeRod, "Wearer gains +50% Spell Power Amplification. (All sources of Spell Power are 50% more effective)"));
                ListItems.Add(new Items("Rapid Firecannon", Enum_Item.RapidFirecannon, Enum_Item.RecurveBow, Enum_Item.RecurveBow, "Wearer gains +100% Attack Range."));
                ListItems.Add(new Items("Red Buff", Enum_Item.RedBuff, Enum_Item.ChainVest, Enum_Item.GiantsBelt, "Attacks burn for 18% of max HP over 10 seconds and reduce healing effects by 80%."));
                ListItems.Add(new Items("Redemption", Enum_Item.Redemption, Enum_Item.GiantsBelt, Enum_Item.TearoftheGoddess, "When the wearer falls below 30% Health, nearby allies are healed for 1200 Health after a 2.5 second delay. This effect can trigger once per combat."));
                //ListItems.Add(new Items("Repeating Crossbow", Enum_Item.RepeatingCrossbow, Enum_Item.RecurveBow, Enum_Item.SparringGloves, "When the wearer dies, Repeating Crossbow is re-equipped to a new ally and grants additional +30% Attack Speed and +20% Critical Strike Chance (Stacks infinitely)."));
                ListItems.Add(new Items("Runaan's Hurricane", Enum_Item.RunaansHurricane, Enum_Item.NegatronCloak, Enum_Item.RecurveBow, "Basic Attacks fire an additional missile at another nearby enemy, dealing 60% of the wearer's Attack damage and applying on-hit effects."));
                ListItems.Add(new Items("Seraph's Embrace", Enum_Item.SeraphsEmbrace, Enum_Item.TearoftheGoddess, Enum_Item.TearoftheGoddess, "Regain 20 mana each time a spell is cast."));
                ListItems.Add(new Items("Spear of Shojin", Enum_Item.SpearofShojin, Enum_Item.BFSword, Enum_Item.TearoftheGoddess, "After casting their spell, the wearer's Basic Attacks restore 18% of their Maximum Mana"));
                ListItems.Add(new Items("Statikk Shiv", Enum_Item.StatikkShiv, Enum_Item.RecurveBow, Enum_Item.TearoftheGoddess, "Deals 80 magic damage to 3/4/5 enemies (scales with wearer’s Star Level)"));
                ListItems.Add(new Items("Sword Breaker", Enum_Item.SwordBreaker, Enum_Item.NegatronCloak, Enum_Item.ChainVest, "Wearer's Basic Attacks have a 33% chance to disarm the target for 3 seconds, preventing that enemy from Basic Attacking."));
                ListItems.Add(new Items("Talisman of Light", Enum_Item.TalismanofLight, Enum_Item.NegatronCloak, Enum_Item.Spatula, "Wearer is also a Light."));
                ListItems.Add(new Items("Thief's Gloves", Enum_Item.ThiefsGloves, Enum_Item.SparringGloves, Enum_Item.SparringGloves, "At the beginning of each planning phase, the wearer equips 2 temporary items. Temporary items increase in power based on your player level."));
               // ListItems.Add(new Items("Thornmail", Enum_Item.Thornmail, Enum_Item.ChainVest, Enum_Item.ChainVest, "When the wearer is hit by a Basic Attack, they reflect 100% of the mitigated damage as magic damage."));
                ListItems.Add(new Items("Titanic Hydra", Enum_Item.TitanicHydra, Enum_Item.RecurveBow, Enum_Item.GiantsBelt, "Basic Attacks deal an additional 3% of the wearer's Maximum Health as magic damage to the target and adjacent enemies behind them."));
                ListItems.Add(new Items("Trap Claw", Enum_Item.TrapClaw, Enum_Item.GiantsBelt, Enum_Item.SparringGloves, "At the beginning of combat, the wearer gains a shield that blocks the first enemy spell that hits them. The enemy that breaks the shield is stunned for 4 seconds."));
                ListItems.Add(new Items("Warden's Mail", Enum_Item.WardensMail, Enum_Item.Spatula, Enum_Item.ChainVest, "Wearer is also a Warden."));
                ListItems.Add(new Items("Warmog's Armor", Enum_Item.WarmogsArmor, Enum_Item.GiantsBelt, Enum_Item.GiantsBelt, "Wearer regenerates 6% of missing health per second."));
                ListItems.Add(new Items("Youmuu's Ghostblade", Enum_Item.YoumuusGhostblade, Enum_Item.BFSword, Enum_Item.Spatula, "Wearer is also an Assassin."));
                ListItems.Add(new Items("Zeke's Herald", Enum_Item.ZekesHerald, Enum_Item.BFSword, Enum_Item.GiantsBelt, "When combat begins, the wearer and all allies within 2 hexes in the same row gain +15% Attack Speed for the rest of combat."));
                ListItems.Add(new Items("Zephyr", Enum_Item.Zephyr, Enum_Item.GiantsBelt, Enum_Item.NegatronCloak, "When combat begins, the wearer summons a whirlwind on the opposite side of the arena that removes the closest enemy from combat for 6 seconds."));


                ListItems.Add(new Items("Bramble Vest", Enum_Item.BrambleVest, Enum_Item.ChainVest, Enum_Item.ChainVest, "Negates bonus damage from incoming critical hits. On being hit by a Basic Attack, deal 100/140/200, magic damage to all nearby enemies (once every 1 second max)."));
                ListItems.Add(new Items("Last Whisper", Enum_Item.LastWhisper, Enum_Item.SparringGloves, Enum_Item.RecurveBow , "Critical hits reduce the target’s Armor by 90% for 3 seconds. This effect does not stack."));
                ListItems.Add(new Items("Titan’s Resolve", Enum_Item.TitansResolve, Enum_Item.ChainVest, Enum_Item.RecurveBow, "When the wearer is hit or inflicts a critical hit, they gain a 2% stacking damage bonus, max 100%. At 50 stacks, the wearer gains 25 Armor and MR; and increases in size."));
            }

            LastItemClicked = ((Items)ListItems[0]).GetItemType;

            //Make ID
            Local_ID = 0;

            foreach (Items item in ListItems)
            {
                item.GetID = Local_ID;
                Local_ID++;
            }

            if (false)
            {
                MakeDebugFile();
            }
        }

        private static void MakeDebugFile()
        {
            string Local_Champion = "";
            string Local_ClasseOrigin = "";
            string Local_Item = "";

            foreach (Champion ItemChampion in ListChampions)
            {
                Local_Champion += $"{ItemChampion.GetTier}|{ItemChampion.GetName}|{ItemChampion.GetHealth}|{ItemChampion.GetDamage}|Enum_Classe.{ItemChampion.GetClasse_One}|Enum_Classe.{ItemChampion.GetClasse_Two}|Enum_Classe.{ItemChampion.GetClasse_Three}|{ItemChampion.GetDebugPath}|false|Images/Icon.png||";
            }

            foreach (OrigineClasse ItemOrigineClasse in ListOrigineClasse)
            {
                try
                {
                    string TempGetBonus = "";
                    string TempGetDescription = "";

                    for (int Index = 0; Index <= 2; Index++)
                    {
                        TempGetBonus += $"{ItemOrigineClasse.GetBonus.ElementAt(Index).Key}|";

                        if (ItemOrigineClasse.GetBonus.ElementAt(Index).Value.Length < 1)
                            TempGetBonus += " |";
                        else
                            TempGetBonus += $"{ItemOrigineClasse.GetBonus.ElementAt(Index).Value}|";
                    }

                    if (ItemOrigineClasse.GetDescription.Length < 1)
                        TempGetDescription = " |";
                    else
                        TempGetDescription = $"{ItemOrigineClasse.GetDescription}|";

                    Local_ClasseOrigin += $"Enum_Classe.{ItemOrigineClasse.GetClasseType}|{ItemOrigineClasse.GetIsClasse}|{TempGetDescription}{TempGetBonus}|";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, ex.Message, "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            foreach (Items ItemItems in ListItems)
            {
                try
                {
                    Local_Item += $"{ItemItems.GetName}|Enum_Item.{ItemItems.GetItemType}|Enum_Item.{ItemItems.GetNeeded_Type1}|Enum_Item.{ItemItems.GetNeeded_Type2}|{ItemItems.GetDescription}||";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, ex.Message, "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }

            try
            {
                new XDocument(
                    new XElement("options",
                    new XElement("Champions", Local_Champion),
                    new XElement("ClasseOrigin", Local_ClasseOrigin),
                    new XElement("Items", Local_Item))
                    ).Save($"{AppDomain.CurrentDomain.BaseDirectory}/Ressources.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Current.MainWindow, ex.Message, "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public static void RefreshUserProfile()
        {
            ArrayList Local_TempProfile = new ArrayList();

            //Récupère les ID des champions en slot
            foreach (SlotChampion SlotChampion in MainRef.SlotChampion)
            {
                if (SlotChampion.ChampionOnSlot != null)
                {
                    Local_TempProfile.Add(SlotChampion.ChampionOnSlot.GetID);
                }
            }

            foreach (int IndexPlacement in TacheFond.GetMainRef.GetPlacementRef.GetPlacement())
            {
                Local_TempProfile.Add(IndexPlacement);
            }

            //Attribue le profile selectionner
            switch (GetUserSettingsRef.SelectedProfile)
            {
                case 1:
                    switch (GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            GetUserSettingsRef.Profile1 = Local_TempProfile;
                            break;
                        case 1:
                            GetUserSettingsRef.Profile1Sub1 = Local_TempProfile;
                            break;
                        case 2:
                            GetUserSettingsRef.Profile1Sub2 = Local_TempProfile;
                            break;
                        case 3:
                            GetUserSettingsRef.Profile1Sub3 = Local_TempProfile;
                            break;
                        case 4:
                            GetUserSettingsRef.Profile1Sub4 = Local_TempProfile;
                            break;
                    }
                    break;

                case 2:
                    switch (GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            GetUserSettingsRef.Profile2 = Local_TempProfile;
                            break;
                        case 1:
                            GetUserSettingsRef.Profile2Sub1 = Local_TempProfile;
                            break;
                        case 2:
                            GetUserSettingsRef.Profile2Sub2 = Local_TempProfile;
                            break;
                        case 3:
                            GetUserSettingsRef.Profile2Sub3 = Local_TempProfile;
                            break;
                        case 4:
                            GetUserSettingsRef.Profile2Sub4 = Local_TempProfile;
                            break;
                    }
                    break;

                case 3:
                    switch (GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            GetUserSettingsRef.Profile3 = Local_TempProfile;
                            break;
                        case 1:
                            GetUserSettingsRef.Profile3Sub1 = Local_TempProfile;
                            break;
                        case 2:
                            GetUserSettingsRef.Profile3Sub2 = Local_TempProfile;
                            break;
                        case 3:
                            GetUserSettingsRef.Profile3Sub3 = Local_TempProfile;
                            break;
                        case 4:
                            GetUserSettingsRef.Profile3Sub4 = Local_TempProfile;
                            break;
                    }
                    break;


                case 4:
                    switch (GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            GetUserSettingsRef.Profile4 = Local_TempProfile;
                            break;
                        case 1:
                            GetUserSettingsRef.Profile4Sub1 = Local_TempProfile;
                            break;
                        case 2:
                            GetUserSettingsRef.Profile4Sub2 = Local_TempProfile;
                            break;
                        case 3:
                            GetUserSettingsRef.Profile4Sub3 = Local_TempProfile;
                            break;
                        case 4:
                            GetUserSettingsRef.Profile4Sub4 = Local_TempProfile;
                            break;
                    }
                    break;

                case 5:
                    switch (GetUserSettingsRef.SelectedSubProfile)
                    {
                        case 0:
                            GetUserSettingsRef.Profile5 = Local_TempProfile;
                            break;
                        case 1:
                            GetUserSettingsRef.Profile5Sub1 = Local_TempProfile;
                            break;
                        case 2:
                            GetUserSettingsRef.Profile5Sub2 = Local_TempProfile;
                            break;
                        case 3:
                            GetUserSettingsRef.Profile5Sub3 = Local_TempProfile;
                            break;
                        case 4:
                            GetUserSettingsRef.Profile5Sub4 = Local_TempProfile;
                            break;
                    }
                    break;
            }
        }

        public static void SaveOptionSettings()
        {
            try
            {
                using (StreamWriter TextWriter = new StreamWriter($"{PathActualFolder}/MyProfile.xml"))
                {
                    new XmlSerializer(typeof(UserSettings)).Serialize(TextWriter, OptionsUserSettingsRef);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Current.MainWindow, $"Error on save UserSettings : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void SaveUserSettings()
        {
            try
            {
                using (StreamWriter TextWriter = new StreamWriter($"{PathActualFolder}/MyProfile.xml"))
                {
                    new XmlSerializer(typeof(UserSettings)).Serialize(TextWriter, GetUserSettingsRef);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Current.MainWindow, $"Error on save UserSettings : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                OptionsUserSettingsRef = new UserSettings();
                OptionsUserSettingsRef = (UserSettings)UserSettingsRef.Clone();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Current.MainWindow, $"Error on clone OptionsUserSettings (Save) : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void LoadUserSettings(bool _KeepSelectedProfile)
        {
            if (File.Exists($"{PathActualFolder}/MyProfile.xml"))
            {
                int Local_Profile = 0;
                int Local_ProfileSub = 0;

                try
                {
                    Local_Profile = UserSettingsRef.SelectedProfile;
                    Local_ProfileSub = UserSettingsRef.SelectedSubProfile;
                }
                catch
                {

                }           

                try
                {
                    using (StreamReader textReader = new StreamReader($"{PathActualFolder}/MyProfile.xml"))
                    {
                        UserSettingsRef = (UserSettings)new XmlSerializer(typeof(UserSettings)).Deserialize(textReader);
                    }

                    if (UserSettingsRef.UpdateLink != null)
                    {
                        if (UserSettingsRef.UpdateLink.Length <= 8)
                            UserSettingsRef.MakeDefaultVariables();
                    }
                    else
                        UserSettingsRef.MakeDefaultVariables();

                    if (_KeepSelectedProfile)
                    {
                        UserSettingsRef.SelectedProfile = Local_Profile;
                        UserSettingsRef.SelectedSubProfile = Local_ProfileSub;
                    }

                    MainRef.UpdateName();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"Error on load UserSettings : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            try
            {
                OptionsUserSettingsRef = new UserSettings();
                OptionsUserSettingsRef = (UserSettings)UserSettingsRef.Clone();
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Current.MainWindow, $"Error on clone OptionsUserSettings (Load) : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            MainRef.Refresh_Slots();
            MainRef.Refresh_OnlyOneType(true);
        }

        public static void SaveUserItems()
        {
            try
            {
                MakeItem(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Current.MainWindow, $"Error on make item : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                using (StreamWriter TextWriter = new StreamWriter($"{PathActualFolder}/UserItems.xml"))
                {
                    new XmlSerializer(typeof(UserItems)).Serialize(TextWriter, UserItemsRef);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(Application.Current.MainWindow, $"Error on save item : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static void LoadUserItems()
        {
            if (File.Exists($"{PathActualFolder}/UserItems.xml"))
            {
                try
                {
                    using (StreamReader textReader = new StreamReader($"{PathActualFolder}/UserItems.xml"))
                    {
                        UserItemsRef = (UserItems)new XmlSerializer(typeof(UserItems)).Deserialize(textReader);
                    }

                    MakeItem(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(Application.Current.MainWindow, $"Error on load : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        public static void MakeItem(bool _Load)
        {
            int[] Local_ChampItems = new int[4] { -1, -1, -1, -1 };
            string Local_TempBrut = "";

            if (_Load)
            {
                foreach (string ChampionItemBrut in UserItemsRef.ChampionItems)
                {
                    //Variable pour récupérer
                    Local_TempBrut = ChampionItemBrut;

                    //Verification du dernier character
                    if (ChampionItemBrut.Substring(ChampionItemBrut.Length - 1, 1) == "|")
                        Local_TempBrut = ChampionItemBrut.Substring(0, ChampionItemBrut.Length - 1);

                    //Récupère l'item et le champion
                    Local_ChampItems = Array.ConvertAll(Local_TempBrut.Split('|'), int.Parse);

                    //Verifie si le champion existe | -1 pas initialiser
                    if (Local_ChampItems[0] >= 0 && (Local_ChampItems[1] >= 0 || Local_ChampItems[2] >= 0 || Local_ChampItems[3] >= 0))
                    {
                        //Si le champion existe initialise les items avec ceux recuperer
                        Items Item1 = null;
                        Items Item2 = null;
                        Items Item3 = null;

                        if (Local_ChampItems[1] >= 0)
                            Item1 = (Items)ListItems[Local_ChampItems[1]];

                        if (Local_ChampItems[2] >= 0)
                            Item2 = (Items)ListItems[Local_ChampItems[2]];

                        if (Local_ChampItems[3] >= 0)
                            Item3 = (Items)ListItems[Local_ChampItems[3]];

                        //Attrbue les items au champion
                        ((Champion)ListChampions[Local_ChampItems[0]]).MyItem = new Items[3] { Item1, Item2, Item3 };
                    }
                }
            }
            else
            {
                UserItemsRef.ChampionItems.Clear();

                foreach (Champion Champ in ListChampions)
                {
                    Local_TempBrut = "";

                    if (Champ.MyItem[0] != null || Champ.MyItem[1] != null || Champ.MyItem[2] != null)
                    {
                        Local_ChampItems[0] = Champ.GetID;

                        if (Champ.MyItem[0] != null)
                            Local_ChampItems[1] = Champ.MyItem[0].GetID;

                        if (Champ.MyItem[1] != null)
                            Local_ChampItems[2] = Champ.MyItem[1].GetID;

                        if (Champ.MyItem[2] != null)
                            Local_ChampItems[3] = Champ.MyItem[2].GetID;

                        foreach (int ItemChamp in Local_ChampItems)
                        {
                            Local_TempBrut += $"{ItemChamp}|";
                        }

                        UserItemsRef.ChampionItems.Add(Local_TempBrut);
                    }
                }
            }
        }

        public static SolidColorBrush GetColorTier(int _Tier)
        {
            switch (_Tier)
            {
                case 1:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
                case 2:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#21A04C"));
                case 3:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1D65B7"));
                case 4:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A53BA5"));
                case 5:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#EAC233"));
                default:
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFFFFF"));
            }
        }

        public static void RealignerOnRefreshSize()
        {
            if (UserSettingsRef.PositionLeft)
                MainRef.GetDetailOrigineClasseRef.Left = MainRef.Left - MainRef.GetDetailOrigineClasseRef.ActualWidth;
            else
                MainRef.GetDetailOrigineClasseRef.Left = MainRef.Left + MainRef.ActualWidth;
        }

        public static void RealignerFenetre(Enum_State _State, bool _Hide)
        {
            if (MainRef.GetListOfChampionRef != null && MainRef.GetGestionItemRef != null && MainRef.GetDetailChampionRef != null && MainRef.GetPlacementRef != null)
            {
                if (InStateTraitement == false)
                {
                    InStateTraitement = true;

                    if (_State == Enum_State.MovedFromMain || _State == Enum_State.MovedFromListChamp || _State == Enum_State.MovedFromGestionItem || _State == Enum_State.SizedFromMain || _State == Enum_State.MovedFromPlacement)
                    {
                        if (_State == Enum_State.SizedFromMain)
                        {
                            //Resize la fenetre - liste des champions                    
                            MainRef.GetListOfChampionRef.Width = MainRef.ActualWidth;
                        }

                        if (_State == Enum_State.MovedFromPlacement)
                        {
                            MainRef.Top = MainRef.GetPlacementRef.Top - MainRef.ActualHeight;

                            if (UserSettingsRef.PositionLeft)
                                MainRef.Left = MainRef.GetPlacementRef.Left - MainRef.ActualWidth + MainRef.GetPlacementRef.ActualWidth;
                            else
                                MainRef.Left = MainRef.GetPlacementRef.Left;
                        }

                        if (_State == Enum_State.MovedFromMain || _State == Enum_State.SizedFromMain || _State == Enum_State.MovedFromPlacement)
                        {
                            //Realigne la fenetre - list des champions
                            MainRef.GetListOfChampionRef.Left = MainRef.Left;
                            MainRef.GetListOfChampionRef.Top = MainRef.Top + MainRef.ActualHeight;

                            //Realigne la fenetre - Liste item    
                            MainRef.GetGestionItemRef.Top = MainRef.Top + MainRef.GetDetailChampionRef.ActualHeight;

                            if (UserSettingsRef.PositionLeft)
                                MainRef.GetGestionItemRef.Left = MainRef.Left - MainRef.GetGestionItemRef.ActualWidth;
                            else
                                MainRef.GetGestionItemRef.Left = MainRef.Left + MainRef.ActualWidth;
                        }
                        else if (_State == Enum_State.MovedFromListChamp)
                        {
                            //Realigne la fenetre - MainWindow 
                            MainRef.Top = MainRef.GetListOfChampionRef.Top - MainRef.ActualHeight;
                            MainRef.Left = MainRef.GetListOfChampionRef.Left;

                            //Realigne la fenetre - Liste item
                            MainRef.GetGestionItemRef.Top = MainRef.Top + MainRef.GetDetailChampionRef.ActualHeight;

                            if (UserSettingsRef.PositionLeft)
                                MainRef.GetGestionItemRef.Left = MainRef.Left - MainRef.GetGestionItemRef.Width;
                            else
                                MainRef.GetGestionItemRef.Left = MainRef.Left + MainRef.ActualWidth;

                        }
                        else if (_State == Enum_State.MovedFromGestionItem)
                        {
                            //Realigne la fenetre - list des champions     
                            MainRef.Top = MainRef.GetGestionItemRef.Top - MainRef.GetDetailChampionRef.ActualHeight;

                            if (UserSettingsRef.PositionLeft)
                                MainRef.Left = MainRef.GetGestionItemRef.Left + MainRef.GetGestionItemRef.ActualWidth;
                            else
                                MainRef.Left = MainRef.GetGestionItemRef.Left - MainRef.ActualWidth;

                            //Resize la fenetre - liste des champions   
                            MainRef.GetListOfChampionRef.Top = MainRef.Top + MainRef.ActualHeight;
                            MainRef.GetListOfChampionRef.Left = MainRef.Left;
                        }

                        //Realigne la fenetre - Detail de champion    
                        MainRef.GetDetailChampionRef.Top = MainRef.Top;

                        if (UserSettingsRef.PositionLeft)
                            MainRef.GetDetailChampionRef.Left = MainRef.Left - MainRef.GetDetailChampionRef.ActualWidth;
                        else
                            MainRef.GetDetailChampionRef.Left = MainRef.Left + MainRef.ActualWidth;

                        //Realigne la fenetre - Detail orignine/classe    
                        MainRef.GetDetailOrigineClasseRef.Top = MainRef.Top;

                        if (UserSettingsRef.PositionLeft)
                            MainRef.GetDetailOrigineClasseRef.Left = MainRef.Left - MainRef.GetDetailOrigineClasseRef.ActualWidth;
                        else
                            MainRef.GetDetailOrigineClasseRef.Left = MainRef.Left + MainRef.ActualWidth;

                        //Realigne la fenetre - Placement  
                        if (_State != Enum_State.MovedFromPlacement)
                        {
                            MainRef.GetPlacementRef.Top = MainRef.Top + MainRef.ActualHeight;

                            if (UserSettingsRef.PositionLeft)
                                MainRef.GetPlacementRef.Left = MainRef.Left + MainRef.ActualWidth - MainRef.GetPlacementRef.ActualWidth;
                            else
                                MainRef.GetPlacementRef.Left = MainRef.Left;
                        }
                    }
                    else if (_State == Enum_State.SizedFromListChamp)
                    {
                        //Resize la fenetre - liste des champions                      
                        MainRef.Width = MainRef.GetListOfChampionRef.ActualWidth;
                    }

                    if (_State == Enum_State.All || _State == Enum_State.SizedFromMain || _State == Enum_State.SizedFromListChamp)
                    {
                        if (_Hide)
                        {
                            MainRef.GetDetailChampionRef.Show();
                            MainRef.GetDetailOrigineClasseRef.Show();
                            MainRef.GetListOfChampionRef.Show();
                            MainRef.GetGestionItemRef.Show();
                            MainRef.GetPlacementRef.Show();
                        }

                        //Realigne la fenetre - Detail de champion    
                        MainRef.GetDetailChampionRef.Top = MainRef.Top;

                        if (UserSettingsRef.PositionLeft)
                            MainRef.GetDetailChampionRef.Left = MainRef.Left - MainRef.GetDetailChampionRef.ActualWidth;
                        else
                            MainRef.GetDetailChampionRef.Left = MainRef.Left + MainRef.ActualWidth;

                        //Realigne la fenetre - Detail orignine/classe    
                        MainRef.GetDetailOrigineClasseRef.Top = MainRef.Top;

                        if (UserSettingsRef.PositionLeft)
                            MainRef.GetDetailOrigineClasseRef.Left = MainRef.Left - MainRef.GetDetailOrigineClasseRef.ActualWidth;
                        else
                            MainRef.GetDetailOrigineClasseRef.Left = MainRef.Left + MainRef.ActualWidth;

                        if (_State == Enum_State.All)
                        {
                            //Realigne et resize la fenetre - list des champions 
                            MainRef.GetListOfChampionRef.Width = MainRef.ActualWidth;

                            MainRef.GetListOfChampionRef.Top = MainRef.Top + MainRef.ActualHeight;
                            MainRef.GetListOfChampionRef.Left = MainRef.Left;
                        }

                        //Realigne la fenetre - Liste item    
                        MainRef.GetGestionItemRef.Top = MainRef.Top + MainRef.GetDetailChampionRef.ActualHeight;

                        if (UserSettingsRef.PositionLeft)
                            MainRef.GetGestionItemRef.Left = MainRef.Left - MainRef.GetGestionItemRef.ActualWidth;
                        else
                            MainRef.GetGestionItemRef.Left = MainRef.Left + MainRef.ActualWidth;

                        //Realigne la fenetre - Placement  
                        MainRef.GetPlacementRef.Top = MainRef.Top + MainRef.ActualHeight;

                        if (UserSettingsRef.PositionLeft)
                            MainRef.GetPlacementRef.Left = MainRef.Left + MainRef.ActualWidth - MainRef.GetPlacementRef.ActualWidth;
                        else
                            MainRef.GetPlacementRef.Left = MainRef.Left;

                        if (_Hide)
                        {
                            MainRef.GetDetailChampionRef.Hide();
                            MainRef.GetDetailOrigineClasseRef.Hide();
                            MainRef.GetListOfChampionRef.Hide();
                            MainRef.GetGestionItemRef.Hide();
                            MainRef.GetPlacementRef.Hide();
                        }
                    }

                    InStateTraitement = false;
                }
            }
        }

        public static void ChangeVer(string _Ver)
        {
            try
            {
                if (MyVer < 0)
                {
                    MyVer = double.Parse(_Ver);
                }
            }
            catch
            {
                try
                {
                    if (MyVer < 0)
                    {
                        MyVer = double.Parse(_Ver.Replace(".", ","));
                    }
                }
                catch (Exception ex)
                {
                    if (GetUserSettingsRef.ShowErrors == true)
                        MessageBox.Show(Application.Current.MainWindow, ex.Message);
                }
            }
        }

        public static void CheckForUpdate(bool _StartUp)
        {
            if (InCheckForUpdate == false)
            {
                if (_StartUp && UserSettingsRef.EnableCheckForUpdate == false)
                {
                    return;
                }

                MainRef.StateCheckForUpdate(Enum_State.UpdateSearch);
                InCheckForUpdate = true;

                try
                {
                    WebClient TempWebClient = new WebClient();
                    string Local_Web = TempWebClient.DownloadString(GetUserSettingsRef.UpdateLink);
                    TempWebClient.Dispose();

                    double Local_OnlineVer = GetOnlineVersion(@Local_Web, GetUserSettingsRef.UpdateFindStart, GetUserSettingsRef.UpdateFindEnd);

                    if (Local_OnlineVer > MyVer)
                        MainRef.StateCheckForUpdate(Enum_State.UpdateAvailable);
                    else
                        MainRef.StateCheckForUpdate(Enum_State.UpdateUnAvailable);

                    InCheckForUpdate = false;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("not be resolved"))
                    {
                        if (GetUserSettingsRef.ShowErrors == true)
                            MessageBox.Show(Application.Current.MainWindow, ex.Message, "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        if (GetUserSettingsRef.ShowErrors == true)
                            MessageBox.Show(Application.Current.MainWindow, $"Error on 'CheckForUpdate' : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    MainRef.StateCheckForUpdate(Enum_State.UpdateUnAvailable);
                    InCheckForUpdate = false;
                }
            }
        }

        public static double GetOnlineVersion(string _Source, string _Start, string _End)
        {
            string local_VerBrut = "";

            if (_Source.Contains(_Start))
            {
                try
                {
                    int Local_Start = _Source.IndexOf(_Start, 0);
                    int Local_End = _Source.IndexOf(_End, Local_Start);

                    local_VerBrut = _Source.Substring(Local_Start + _Start.Length, Local_End - Local_Start - _Start.Length);

                    return double.Parse(local_VerBrut);
                }
                catch
                {
                    try
                    {
                        return double.Parse(local_VerBrut.Replace(".", ","));
                    }
                    catch (Exception ex)
                    {
                        if (GetUserSettingsRef.ShowErrors == true)
                            MessageBox.Show(Application.Current.MainWindow, $"Error on 'GetOnlineVersion' : {ex.Message}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);

                        return -1;
                    }
                }
            }
            else
            {
                if (GetUserSettingsRef.ShowErrors == true)
                    MessageBox.Show(Application.Current.MainWindow, $"Error on 'GetOnlineVersion' : _Start not found in _Source : { _Start}", "Antize TFT", MessageBoxButton.OK, MessageBoxImage.Error);

                return -1;
            }
        }

        public static void LoadRessourcesFile()
        {
            if (File.Exists($"{PathActualFolder}/Localization.xml"))
            {
                try
                {
                    using (StreamReader textReader = new StreamReader($"{PathActualFolder}/Localization.xml"))
                    {
                        LocalizationRef = (MyLanguage)new XmlSerializer(typeof(MyLanguage)).Deserialize(textReader);
                    }
                }
                catch
                {

                }
            }
            else
            {
                /*using (StreamWriter TextWriter = new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "/Localization.xml"))
                {
                    new XmlSerializer(typeof(MyLanguage)).Serialize(TextWriter, LocalizationRef);
                }*/
            }
        }
    }
}
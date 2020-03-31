using System.Xml.Serialization;

namespace Antize_TFT.Class
{
    [XmlRoot("Localization")]
    public class MyLanguage
    {
        public MyLanguage()
        {
            this.SetDefaultLanguage();

            this.LocalizationVersion = "0.04";

             /*this.Main_Profile = "Profil";
             this.Main_SubProfile = "Sub Profil";

             this.Main_ItemOnlyOneTypeOn = "Seulement un type: On";
             this.Main_ItemOnlyOneTypeOff = "Seulement un type: Off";

             this.Main_ItemEditOn = "Modification des objets: On";
             this.Main_ItemEditOff = "Modification des objets: Off";

             this.Main_ItemSave = "Sauvegarder";
             this.Main_ItemLoad = "Recharger";

             this.Main_ItemSortChampion = "Trie la liste de champion";

             this.Main_ItemResetProfile = "Reinitialiser le profil sélectionné";

             this.Main_ItemPositionLeft = "Position: Gauche";
             this.Main_ItemPositionRight = "Position: Droite";

             this.Main_ItemAutoUpdateOn = "Rechercher MaJ: On";
             this.Main_ItemAutoUpdateOff = "Rechercher MaJ: Off";

             this.Main_ItemShowErrorsOn = "Voir les erreurs: On";
             this.Main_ItemShowErrorsOff = "Voir les erreurs: Off";

             this.Main_ItemOption = "Options";

             this.Main_ItemShowHideShow = "Voir";
             this.Main_ItemShowHideHide = "Cacher";

             this.Main_ItemCheckingForUpdates = "Recherche des mise à jours";
             this.Main_ItemUpdatesAvailable = "Mise à jour disponible";
             this.Main_ItemNoUpdatesFound = "Pas de mise à jour trouvée";

             this.Main_ItemPlacementOn = "Placement: On";
             this.Main_ItemPlacementOff = "Placement: Off";

             this.Main_SelectedProfileToolTip = "Clic gauche - Trie la liste de champion | Clic droit - Renomer le profil";
             this.Main_SelectedSubProfileToolTip = "Clic gauche - Trie la liste de champion | Clic droit - Renomer le sub profil";

             this.Main_PlusToolTip = "Clic gauche - Augmente le sub profil | Click droit - Augmente le profil";
             this.Main_MoinsToolTip = "Clic gauche - Diminue le sub profil | Click droit - Diminue le profil";

             this.MyCheckBox_IconToolTip = "Clic gauche - Coche/Décoche | Clic droit - Filtre seulement ";

             this.SlotItem_IconToolTip = "Clic droit - Voir la liste des créations";

             this.Placement_Placement = "Sélectionné :  ";
             this.Placement_Reset = "Réinitialiser";
             
             this.AutoShowHideOn = "Voir/Cacher Auto: On";
             this.AutoShowHideOff = "Voir/Cacher Auto: Off";*/
        }

        private void SetDefaultLanguage()
        {
            this.Main_ItemOnlyOneTypeOn = "Only one type: On";
            this.Main_ItemOnlyOneTypeOff = "Only one type: Off";

            this.Main_ItemEditOn = "Edit items: On";
            this.Main_ItemEditOff = "Edit items: Off";

            this.Main_ItemSave = "Save";
            this.Main_ItemLoad = "Load";

            this.Main_ItemSortChampion = "Sort champion list";

            this.Main_Profile = "Profile";
            this.Main_SubProfile = "Sub Profile";

            this.Main_ItemResetProfile = "Reset selected profile";

            this.Main_ItemPositionLeft = "Position: Left";
            this.Main_ItemPositionRight = "Position: Right";

            this.Main_ItemAutoUpdateOn = "Check for Updates: On";
            this.Main_ItemAutoUpdateOff = "Check for Updates: Off";

            this.Main_ItemShowErrorsOn = "Show errors: On";
            this.Main_ItemShowErrorsOff = "Show errors: Off";

            this.Main_ItemOption = "Options";

            this.Main_ItemShowHideShow = "Show";
            this.Main_ItemShowHideHide = "Hide";

            this.Main_ItemCheckingForUpdates = "Searching for updates";
            this.Main_ItemUpdatesAvailable = "Updates available";
            this.Main_ItemNoUpdatesFound = "No updates found";

            this.Main_ItemPlacementOn = "Placement: On";
            this.Main_ItemPlacementOff = "Placement: Off";

            this.Main_SelectedProfileToolTip = "Left click - sort champion list | Right click - Rename Profile";
            this.Main_SelectedSubProfileToolTip = "Left click - sort champion list | Right click - Rename Sub Profile";

            this.Main_PlusToolTip = "Left click - Up sub profile | Right click - Up profile";
            this.Main_MoinsToolTip = "Left click - Down sub profile | Right click - Down profile";

            this.MyCheckBox_IconToolTip = "Left click - Check/Uncheck | Right click - Filter only ";

            this.SlotItem_IconToolTip = "Right click - Item Builder Sheet";

            this.Placement_Placement = "Selected :  ";
            this.Placement_Reset = "Reset";

            //Ver 0.49
            this.AutoShowHideOn = "Auto Show/Hide: On";
            this.AutoShowHideOff = "Auto Show/Hide: Off";

            //Ver 0.52
            this.NoteOn = "Note: On";
            this.NoteOff = "Note: Off";
        }

        public string LocalizationVersion
        {
            get; set;
        }

        public string Main_Profile
        {
            get; set;
        }

        public string Main_SubProfile
        {
            get; set;
        }

        public string Main_SelectedProfileToolTip
        {
            get; set;
        }

        public string Main_SelectedSubProfileToolTip
        {
            get; set;
        }

        public string Main_ItemOnlyOneTypeOn
        {
            get; set;
        }

        public string Main_ItemOnlyOneTypeOff
        {
            get; set;
        }

        public string Main_ItemEditOn
        {
            get; set;
        }

        public string Main_ItemEditOff
        {
            get; set;
        }

        public string Main_ItemPositionLeft
        {
            get; set;
        }

        public string Main_ItemPositionRight
        {
            get; set;
        }

        public string Main_ItemSave
        {
            get; set;
        }

        public string Main_ItemLoad
        {
            get; set;
        }

        public string Main_ItemSortChampion
        {
            get; set;
        }

        public string Main_ItemResetProfile
        {
            get; set;
        }

        public string Main_ItemShowHideShow
        {
            get; set;
        }

        public string Main_ItemShowHideHide
        {
            get; set;
        }

        public string Main_ItemAutoUpdateOn
        {
            get; set;
        }

        public string Main_ItemAutoUpdateOff
        {
            get; set;
        }

        public string Main_ItemShowErrorsOn
        {
            get; set;
        }

        public string Main_ItemShowErrorsOff
        {
            get; set;
        }

        public string Main_ItemOption
        {
            get; set;
        }

        public string Main_ItemCheckingForUpdates
        {
            get; set;
        }

        public string Main_ItemUpdatesAvailable
        {
            get; set;
        }

        public string Main_ItemNoUpdatesFound
        {
            get; set;
        }

        public string Main_ItemPlacementOn
        {
            get; set;
        }

        public string Main_ItemPlacementOff
        {
            get; set;
        }

        public string Main_PlusToolTip
        {
            get; set;
        }

        public string Main_MoinsToolTip
        {
            get; set;
        }

        public string MyCheckBox_IconToolTip
        {
            get; set;
        }

        public string SlotItem_IconToolTip
        {
            get; set;
        }

        public string Placement_Placement
        {
            get; set;
        }

        public string Placement_Reset
        {
            get; set;
        }

        //Ver 0.49
        public string AutoShowHideOn
        {
            get; set;
        }

        public string AutoShowHideOff
        {
            get; set;
        }

        //Ver 0.50
        public string NoteOn
        {
            get; set;
        }

        public string NoteOff
        {
            get; set;
        }
    }
}

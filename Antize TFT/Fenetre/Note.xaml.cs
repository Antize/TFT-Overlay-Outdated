using Antize_TFT.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Antize_TFT.Fenetre
{
    /// <summary>
    /// Interaction logic for Note.xaml
    /// </summary>
    public partial class Note : Window
    {
        public Note()
        {
            InitializeComponent();

            this.Top = SystemParameters.WorkArea.Height - 200;
            this.Left = 5;
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
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityIn;
        }

        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Opacity = TacheFond.GetUserSettingsRef.OpacityOut / 2;
        }

        public void SaveLoadNote(bool _Save)
        {
            int Local_SelectedProfile = TacheFond.GetUserSettingsRef.SelectedProfile;
            int Local_SelectedSubProfile = TacheFond.GetUserSettingsRef.SelectedSubProfile;

            if (_Save == false)
                this.TextProfile.Text = $"Profile : {TacheFond.GetMainRef.SelectedProfile.Text} - {TacheFond.GetMainRef.SubProfile.Text}";

            switch (Local_SelectedProfile)
            {
                case 1:
                    if (_Save)
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                TacheFond.GetUserSettingsRef.NoteProfile1 = this.TextBox_Note.Text;
                                break;
                            case 1:
                                TacheFond.GetUserSettingsRef.NoteProfile1Sub1 = this.TextBox_Note.Text;
                                break;
                            case 2:
                                TacheFond.GetUserSettingsRef.NoteProfile1Sub2 = this.TextBox_Note.Text;
                                break;
                            case 3:
                                TacheFond.GetUserSettingsRef.NoteProfile1Sub3 = this.TextBox_Note.Text;
                                break;
                            case 4:
                                TacheFond.GetUserSettingsRef.NoteProfile1Sub4 = this.TextBox_Note.Text;
                                break;
                        }
                    }
                    else
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile1;
                                break;
                            case 1:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile1Sub1;
                                break;
                            case 2:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile1Sub2;
                                break;
                            case 3:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile1Sub3;
                                break;
                            case 4:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile1Sub4;
                                break;
                        }
                    }
                    break;

                case 2:
                    if (_Save)
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                TacheFond.GetUserSettingsRef.NoteProfile2 = this.TextBox_Note.Text;
                                break;
                            case 1:
                                TacheFond.GetUserSettingsRef.NoteProfile2Sub1 = this.TextBox_Note.Text;
                                break;
                            case 2:
                                TacheFond.GetUserSettingsRef.NoteProfile2Sub2 = this.TextBox_Note.Text;
                                break;
                            case 3:
                                TacheFond.GetUserSettingsRef.NoteProfile2Sub3 = this.TextBox_Note.Text;
                                break;
                            case 4:
                                TacheFond.GetUserSettingsRef.NoteProfile2Sub4 = this.TextBox_Note.Text;
                                break;
                        }
                    }
                    else
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile2;
                                break;
                            case 1:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile2Sub1;
                                break;
                            case 2:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile2Sub2;
                                break;
                            case 3:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile2Sub3;
                                break;
                            case 4:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile2Sub4;
                                break;
                        }
                    }
                    break;

                case 3:
                    if (_Save)
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                TacheFond.GetUserSettingsRef.NoteProfile3 = this.TextBox_Note.Text;
                                break;
                            case 1:
                                TacheFond.GetUserSettingsRef.NoteProfile3Sub1 = this.TextBox_Note.Text;
                                break;
                            case 2:
                                TacheFond.GetUserSettingsRef.NoteProfile3Sub2 = this.TextBox_Note.Text;
                                break;
                            case 3:
                                TacheFond.GetUserSettingsRef.NoteProfile3Sub3 = this.TextBox_Note.Text;
                                break;
                            case 4:
                                TacheFond.GetUserSettingsRef.NoteProfile3Sub4 = this.TextBox_Note.Text;
                                break;
                        }
                    }
                    else
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile3;
                                break;
                            case 1:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile3Sub1;
                                break;
                            case 2:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile3Sub2;
                                break;
                            case 3:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile3Sub3;
                                break;
                            case 4:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile3Sub4;
                                break;
                        }
                    }
                    break;

                case 4:
                    if (_Save)
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                TacheFond.GetUserSettingsRef.NoteProfile4 = this.TextBox_Note.Text;
                                break;
                            case 1:
                                TacheFond.GetUserSettingsRef.NoteProfile4Sub1 = this.TextBox_Note.Text;
                                break;
                            case 2:
                                TacheFond.GetUserSettingsRef.NoteProfile4Sub2 = this.TextBox_Note.Text;
                                break;
                            case 3:
                                TacheFond.GetUserSettingsRef.NoteProfile4Sub3 = this.TextBox_Note.Text;
                                break;
                            case 4:
                                TacheFond.GetUserSettingsRef.NoteProfile4Sub4 = this.TextBox_Note.Text;
                                break;
                        }
                    }
                    else
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile4;
                                break;
                            case 1:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile4Sub1;
                                break;
                            case 2:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile4Sub2;
                                break;
                            case 3:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile4Sub3;
                                break;
                            case 4:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile4Sub4;
                                break;
                        }
                    }
                    break;

                case 5:
                    if (_Save)
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                TacheFond.GetUserSettingsRef.NoteProfile5 = this.TextBox_Note.Text;
                                break;
                            case 1:
                                TacheFond.GetUserSettingsRef.NoteProfile5Sub1 = this.TextBox_Note.Text;
                                break;
                            case 2:
                                TacheFond.GetUserSettingsRef.NoteProfile5Sub2 = this.TextBox_Note.Text;
                                break;
                            case 3:
                                TacheFond.GetUserSettingsRef.NoteProfile5Sub3 = this.TextBox_Note.Text;
                                break;
                            case 4:
                                TacheFond.GetUserSettingsRef.NoteProfile5Sub4 = this.TextBox_Note.Text;
                                break;
                        }
                    }
                    else
                    {
                        switch (Local_SelectedSubProfile)
                        {
                            case 0:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile5;
                                break;
                            case 1:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile5Sub1;
                                break;
                            case 2:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile5Sub2;
                                break;
                            case 3:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile5Sub3;
                                break;
                            case 4:
                                this.TextBox_Note.Text = TacheFond.GetUserSettingsRef.NoteProfile5Sub4;
                                break;
                        }
                    }
                    break;
            }
        }

        private void Note_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.SaveLoadNote(true);
        }
    }
}
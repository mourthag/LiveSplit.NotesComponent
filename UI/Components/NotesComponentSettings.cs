using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LiveSplit.Model;
using LiveSplit.Utility;

namespace LiveSplit.UI.Components
{
    public partial class NotesComponentSettings : UserControl
    {
        public LiveSplitState CurrentState { get; set; }
        public LayoutMode Mode { get; set; }

        public string FilePath { get; set; }
        public bool FilePathChanged { get; set; }

        public Color BackgroundColor { get; set; }
        public Color LevelUpBgColor { get; set; }
        public Color LevelUpTxtColor { get; set; }
        public Color ItemTrackingBgColor { get; set; }
        public Color ItemTrackingTxtColor { get; set; }
        public Color TeleportBgColor { get; set; }
        public Color TeleportTxtColor { get; set; }
        public Color DropBgColor { get; set; }
        public Color DropTxtColor { get; set; }
        public Color KillBgColor { get; set; }
        public Color KillTxtColor { get; set; }
        public Color SkipBgColor { get; set; }
        public Color SkipTxtColor { get; set; }
        public Color TrophyBgColor { get; set; }
        public Color TrophyTxtColor { get; set; }

        public NotesComponentSettings(LiveSplitState state)
        {
            InitializeComponent();

            CurrentState = state;

            FilePath = "";
            FilePathChanged = true;

            BackgroundColor = Color.Black;

            LevelUpTxtColor = Color.FromArgb(255,255,255);
            ItemTrackingTxtColor = Color.FromArgb(255, 255, 255);
            TeleportTxtColor = Color.FromArgb(255, 255, 255);
            DropTxtColor = Color.FromArgb(255, 255, 255);
            KillTxtColor = Color.FromArgb(255, 255, 255);
            SkipTxtColor = Color.FromArgb(255, 255, 255);
            TrophyTxtColor = Color.FromArgb(255, 255, 255);

            LevelUpBgColor = Color.Blue;
            ItemTrackingBgColor = Color.Violet;
            TeleportBgColor = Color.Yellow;
            DropBgColor = Color.Pink;
            KillBgColor = Color.Red;
            SkipBgColor = Color.Brown;
            TrophyBgColor = Color.Green;

            btnClrLvlupTxt.DataBindings.Add("BackColor", this, "LevelUpTxtColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrLvlupBg.DataBindings.Add("BackColor", this, "LevelUpBgColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrItemTxt.DataBindings.Add("BackColor", this, "ItemTrackingTxtColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrItemBg.DataBindings.Add("BackColor", this, "ItemTrackingBgColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrTpTxt.DataBindings.Add("BackColor", this, "TeleportTxtColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrTpBg.DataBindings.Add("BackColor", this, "TeleportBgColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrDrpTxt.DataBindings.Add("BackColor", this, "DropTxtColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrDrpBg.DataBindings.Add("BackColor", this, "DropBgColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrKillTxt.DataBindings.Add("BackColor", this, "KillTxtColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrKllBg.DataBindings.Add("BackColor", this, "KillBgColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrSkpTxt.DataBindings.Add("BackColor", this, "SkipTxtColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrSkpBg.DataBindings.Add("BackColor", this, "SkipBgColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrTrphyTxt.DataBindings.Add("BackColor", this, "TrophyTxtColor", false, DataSourceUpdateMode.OnPropertyChanged);
            btnClrTrphyBg.DataBindings.Add("BackColor", this, "TrophyBgColor", false, DataSourceUpdateMode.OnPropertyChanged);

        }

        void NotesComponentSettings_Load(object sender, EventArgs e)
        { }

        public void SetSettings(XmlNode node)
        {
            var element = (XmlElement)node;
            FilePath = SettingsHelper.ParseString(element["FilePath"]);
            BackgroundColor = SettingsHelper.ParseColor(element["BackgroundColor"]);
            LevelUpTxtColor = SettingsHelper.ParseColor(element["LevelUpTxtColor"]);
            LevelUpBgColor = SettingsHelper.ParseColor(element["LevelUpBgColor"]);
            ItemTrackingTxtColor = SettingsHelper.ParseColor(element["ItemTrackingTxtColor"]);
            ItemTrackingBgColor = SettingsHelper.ParseColor(element["ItemTrackingBgColor"]);
            TeleportTxtColor = SettingsHelper.ParseColor(element["TeleportTxtColor"]);
            TeleportBgColor = SettingsHelper.ParseColor(element["TeleportBgColor"]);
            DropTxtColor = SettingsHelper.ParseColor(element["DropTxtColor"]);
            DropBgColor = SettingsHelper.ParseColor(element["DropBgColor"]);
            KillTxtColor = SettingsHelper.ParseColor(element["KillTxtColor"]);
            KillBgColor = SettingsHelper.ParseColor(element["KillBgColor"]);
            SkipTxtColor = SettingsHelper.ParseColor(element["SkipTxtColor"]);
            SkipBgColor = SettingsHelper.ParseColor(element["SkipBgColor"]);
            TrophyTxtColor = SettingsHelper.ParseColor(element["TrophyTxtColor"]);
            TrophyBgColor = SettingsHelper.ParseColor(element["TrophyBgColor"]);
        }

        public XmlNode GetSettings(XmlDocument document)
        {
            var parent = document.CreateElement("Settings");
            CreateSettingsNode(document, parent);
            return parent;
        }

        public int GetSettingsHashCode()
        {
            return CreateSettingsNode(null, null);
        }

        private int CreateSettingsNode(XmlDocument document, XmlElement parent)
        {
            return SettingsHelper.CreateSetting(document, parent, "Version", "1.6") ^
                SettingsHelper.CreateSetting(document, parent, "FilePath", FilePath) ^
                SettingsHelper.CreateSetting(document, parent, "BackgroundColor", BackgroundColor) ^
                SettingsHelper.CreateSetting(document, parent, "LevelUpTxtColor", LevelUpTxtColor) ^
                SettingsHelper.CreateSetting(document, parent, "LevelUpBgColor", LevelUpBgColor) ^
                SettingsHelper.CreateSetting(document, parent, "ItemTrackingTxtColor", ItemTrackingTxtColor) ^
                SettingsHelper.CreateSetting(document, parent, "ItemTrackingBgColor", ItemTrackingBgColor) ^
                SettingsHelper.CreateSetting(document, parent, "TeleportTxtColor", TeleportTxtColor) ^
                SettingsHelper.CreateSetting(document, parent, "TeleportBgColor", TeleportBgColor) ^
                SettingsHelper.CreateSetting(document, parent, "DropTxtColor", DropTxtColor) ^
                SettingsHelper.CreateSetting(document, parent, "DropBgColor", DropBgColor) ^
                SettingsHelper.CreateSetting(document, parent, "KillTxtColor", KillTxtColor) ^
                SettingsHelper.CreateSetting(document, parent, "KillBgColor", KillBgColor) ^
                SettingsHelper.CreateSetting(document, parent, "SkipTxtColor", SkipTxtColor) ^
                SettingsHelper.CreateSetting(document, parent, "SkipBgColor", SkipBgColor) ^
                SettingsHelper.CreateSetting(document, parent, "TrophyTxtColor", TrophyTxtColor) ^
                SettingsHelper.CreateSetting(document, parent, "TrophyBgColor", TrophyBgColor);

        }

        private void ColorButtonClick(object sender, EventArgs e)
        {
            SettingsHelper.ColorButtonClick((Button)sender, this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog loadNotesDialog = new OpenFileDialog();
            loadNotesDialog.Filter = "Textfiles|*.txt";
            loadNotesDialog.Multiselect = false;
            loadNotesDialog.Title = "Select notes";
            if(loadNotesDialog.ShowDialog() == DialogResult.OK)
            {
                Stream stream = null;

                try
                {
                    if ((stream = loadNotesDialog.OpenFile()) != null)
                    {
                        FilePath = Path.GetFullPath(loadNotesDialog.FileName);
                        FilePathChanged = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }
    }
}

using ImageMagick;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace FEH_Hero_Visualizer {
    public partial class Form1 : Form {
        private string __DataPath;
        private string __ThisPath;
        private JArray __HeroData;
        private PrivateFontCollection __FEHFontCollection;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.__DataPath = ConfigurationManager.AppSettings.Get("DataPath").ToUpperInvariant().Trim();
            this.__ThisPath = Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            // Load custom font
            string FEHFontPath       = Path.Combine(this.__ThisPath, "assets" + Path.DirectorySeparatorChar + "feh-font.ttf");
            this.__FEHFontCollection = new PrivateFontCollection();
            this.__FEHFontCollection.AddFontFile(FEHFontPath);

            this.pictureBox1.ImageLocation = Path.Combine(this.__ThisPath, "assets" + Path.DirectorySeparatorChar + "background.jpg");
        }

        private void Form1_Shown(object sender, EventArgs e) {
        }

        private void __LoadHeroesFile(string file) {
            this.__HeroData = JArray.Parse(File.ReadAllText(file));
            //this.__HeroData = JArray.Parse(File.ReadAllText(@"C:\FEHDownload\Data\Extracted\Common\SRPG\Person\200102_gensou.json"));

            HeroesListbox.Items.Clear();

            foreach (JToken Hero in this.__HeroData) {
                HeroesListbox.Items.Add(Hero["Name"].ToString() + " - " + Hero["Epithet"].ToString());
            }

            HeroesListbox.SelectedIndex = 0;

        }

        private void __RenderHero(JToken HeroData) {
            this.RawHeroTextbox.Text = HeroData.ToString();

            this.HeroHpTextbox.Text  = HeroData["BaseStats"]["Hp"].ToString() ;
            this.HeroAtkTextbox.Text = HeroData["BaseStats"]["Atk"].ToString();
            this.HeroSpdTextbox.Text = HeroData["BaseStats"]["Spd"].ToString();
            this.HeroDefTextbox.Text = HeroData["BaseStats"]["Def"].ToString();
            this.HeroResTextbox.Text = HeroData["BaseStats"]["Res"].ToString();

            this.HeroHp40Textbox.Text  = HeroData["MaxStats"]["Hp"].ToString() ;
            this.HeroAtk40Textbox.Text = HeroData["MaxStats"]["Atk"].ToString();
            this.HeroSpd40Textbox.Text = HeroData["MaxStats"]["Spd"].ToString();
            this.HeroDef40Textbox.Text = HeroData["MaxStats"]["Def"].ToString();
            this.HeroRes40Textbox.Text = HeroData["MaxStats"]["Res"].ToString();

            string FaceName     = HeroData["FaceName"].ToString();
            string HeroFacePath = Path.Combine(
                this.__DataPath,
                "Common" + Path.DirectorySeparatorChar +
                "Face"   + Path.DirectorySeparatorChar +
                FaceName + Path.DirectorySeparatorChar +
                "Face.png"
            );

            string HeroImagePath      = Path.Combine(this.__ThisPath, "assets" + Path.DirectorySeparatorChar + "hero" + Path.DirectorySeparatorChar + FaceName + ".png");
            string HeroImageDirectory = Path.GetDirectoryName(HeroImagePath);
            if (!Directory.Exists(HeroImageDirectory)) {
                Directory.CreateDirectory(HeroImageDirectory);
            }

            if (!File.Exists(HeroImagePath)) {
                // Convert image so it gets readable by GDI+
                if (File.Exists(HeroFacePath)) {
                    using (MagickImage image = new MagickImage(HeroFacePath)) {
                        image.Write(HeroFacePath);
                    }
                }

                this.SuperboonsTextbox.Text = "";
                if (
                    HeroData["Superboon"].Count() == 0
                    ||
                    HeroData["Superboon"][0].ToString().Trim() == string.Empty
                ) {
                    this.SuperboonsTextbox.Text = "None";
                }
                foreach (JToken Superboon in HeroData["Superboon"]) {
                    // Skip empty items
                    if(Superboon.ToString().Trim() == string.Empty) {
                        continue;
                    }

                    if(this.SuperboonsTextbox.Text != string.Empty) {
                        this.SuperboonsTextbox.Text += ", ";
                    }
                    this.SuperboonsTextbox.Text += Superboon.ToString();
                }

                this.SuperbanesTextbox.Text = "";
                if (HeroData["Superbane"].Count() == 0) {
                    this.SuperbanesTextbox.Text = "None";
                }
                foreach (JToken Superbane in HeroData["Superbane"]) {
                    // Skip empty items
                    if (Superbane.ToString().Trim() == string.Empty) {
                        continue;
                    }

                    if (this.SuperbanesTextbox.Text != string.Empty) {
                        this.SuperbanesTextbox.Text += ", ";
                    }
                    this.SuperbanesTextbox.Text += Superbane.ToString();
                }

                string BackgroundPath = Path.Combine(this.__ThisPath, "assets" + Path.DirectorySeparatorChar + "background.jpg");
                string ForegroundPath = Path.Combine(this.__ThisPath, "assets" + Path.DirectorySeparatorChar + "foreground.png");

                using (Bitmap bitmap = new Bitmap(720, 1280)) {
                    using(Graphics Canvas = Graphics.FromImage(bitmap)) {
                        Canvas.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        using (Image Background = Image.FromFile(BackgroundPath)) {
                            // Draw background first
                            Canvas.DrawImage(
                                Background,
                                new Rectangle(
                                    0,
                                    0,
                                    720,
                                    1280
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    Background.Width,
                                    Background.Height
                                ),
                                GraphicsUnit.Pixel
                            );
                        }

                        if (File.Exists(HeroFacePath)) {
                            using (Image Unit = Image.FromFile(HeroFacePath)) {
                                double WRatio = (double) ((double) Unit.Width  / (double) bitmap.Width );
                                double HRatio = (double) ((double) Unit.Height / (double) bitmap.Height);
                                Canvas.DrawImage(
                                    Unit,
                                    new Rectangle(
                                        -280,
                                        0,
                                        1280,
                                        1536
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        Unit.Width,
                                        Unit.Height
                                    ),
                                    GraphicsUnit.Pixel
                                );
                            }
                        }

                        using (Image Foreground = Image.FromFile(ForegroundPath)) {
                            Canvas.DrawImage(
                                Foreground,
                                new Rectangle(
                                    0,
                                    0,
                                    720,
                                    1280
                                ),
                                new Rectangle(
                                    0,
                                    0,
                                    Foreground.Width,
                                    Foreground.Height
                                ),
                                GraphicsUnit.Pixel
                            );
                        }

                        // Now write the custom shitstuff
                        this.__DrawStat(HeroData["MaxStats"]["Hp"].ToString(),  Canvas, 226f, 802f, this.__GetStatColor(HeroData, "Hp" ));
                        this.__DrawStat(HeroData["MaxStats"]["Atk"].ToString(), Canvas, 226f, 851f, this.__GetStatColor(HeroData, "Atk"));
                        this.__DrawStat(HeroData["MaxStats"]["Spd"].ToString(), Canvas, 226f, 900f, this.__GetStatColor(HeroData, "Spd"));
                        this.__DrawStat(HeroData["MaxStats"]["Def"].ToString(), Canvas, 226f, 949f, this.__GetStatColor(HeroData, "Def"));
                        this.__DrawStat(HeroData["MaxStats"]["Res"].ToString(), Canvas, 226f, 998f, this.__GetStatColor(HeroData, "Res"));

                        this.__DrawStat("9999", Canvas, 182f, 1047f, "green");
                        this.__DrawStat("6000", Canvas, 182f, 1096f, "green");

                        this.__DrawStat("40", Canvas, 124f, 742f, "white");

                        string WeaponFile   = Path.Combine(this.__ThisPath, "assets" + Path.DirectorySeparatorChar + "weapon"   + Path.DirectorySeparatorChar + HeroData["WeaponType"].ToString() + ".png");
                        string MovementFile = Path.Combine(this.__ThisPath, "assets" + Path.DirectorySeparatorChar + "movement" + Path.DirectorySeparatorChar + HeroData["MoveType"].ToString()   + ".png");

                        if (File.Exists(WeaponFile)) {
                            using (Image WeaponIcon = Image.FromFile(WeaponFile)) {
                                Canvas.DrawImage(
                                    WeaponIcon,
                                    new Rectangle(
                                        22,
                                        736,
                                        42,
                                        42
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        42,
                                        42
                                    ),
                                    GraphicsUnit.Pixel
                                );
                            }
                        }
                        if (File.Exists(MovementFile)) {
                            using (Image MovementIcon = Image.FromFile(MovementFile)) {
                                Canvas.DrawImage(
                                    MovementIcon,
                                    new Rectangle(
                                        200,
                                        739,
                                        36,
                                        36
                                    ),
                                    new Rectangle(
                                        0,
                                        0,
                                        36,
                                        36
                                    ),
                                    GraphicsUnit.Pixel
                                );
                            }
                        }

                        float EpithetBaseX = 192f;
                        float NameBaseX    = 216f;
                        float SkillBaseX   = 420f;

                        using (Font FEHFont = new Font((FontFamily) this.__FEHFontCollection.Families[0], 16, FontStyle.Bold)) {
                            List<KeyValuePair<string, float>> Skills = new List<KeyValuePair<string, float>> {
                                // Weapon
                                new KeyValuePair<string, float> (
                                    (HeroData["Skills"]["5 Star"]["Unlocked Weapon"].ToString() != "") ?
                                    HeroData["Skills"]["5 Star"]["Unlocked Weapon"].ToString()
                                    :
                                    HeroData["Skills"]["5 Star"]["Default Weapon"].ToString(),
                                    802f
                                ),
                                new KeyValuePair<string, float> (
                                    (HeroData["Skills"]["5 Star"]["Unlocked Assist"].ToString() != "") ?
                                    HeroData["Skills"]["5 Star"]["Unlocked Assist"].ToString()
                                    :
                                    HeroData["Skills"]["5 Star"]["Default Assist"].ToString(),
                                    851f
                                ),
                                new KeyValuePair<string, float> (
                                    (HeroData["Skills"]["5 Star"]["Unlocked Special"].ToString() != "") ?
                                    HeroData["Skills"]["5 Star"]["Unlocked Special"].ToString()
                                    :
                                    HeroData["Skills"]["5 Star"]["Default Special"].ToString(),
                                    900f
                                ),
                                new KeyValuePair<string, float> (HeroData["Skills"]["5 Star"]["Passive A"].ToString(), 950f),
                                new KeyValuePair<string, float> (HeroData["Skills"]["5 Star"]["Passive B"].ToString(), 1000f),
                                new KeyValuePair<string, float> (HeroData["Skills"]["5 Star"]["Passive C"].ToString(), 1050f)
                            };

                            foreach (KeyValuePair<string, float> Skill in Skills) {
                                Canvas.DrawString(Skill.Key, FEHFont, Brushes.White, SkillBaseX, Skill.Value);
                            }
                        }

                        using (Font FEHFont = new Font((FontFamily)this.__FEHFontCollection.Families[0], 24, FontStyle.Bold)) {
                            float EpithetWidth = Canvas.MeasureString(HeroData["Epithet"].ToString(), FEHFont).Width;
                            float NameWidth    = Canvas.MeasureString(HeroData["Name"].ToString(),    FEHFont).Width;

                            Canvas.DrawString(HeroData["Epithet"].ToString(), FEHFont, Brushes.White, new PointF(EpithetBaseX - (EpithetWidth / 2), 560f));
                            Canvas.DrawString(HeroData["Name"].ToString(),    FEHFont, Brushes.White, new PointF(NameBaseX    - (NameWidth    / 2), 640f));
                        }

                        //Canvas.DrawString(HeroData["Name"]["Hp"].ToString(), FEHShadow, Brushes.Black, new PointF(240f, 806f));
                        //Canvas.DrawString(HeroData["Epithet"]["Hp"].ToString(), FEHFont,   Brushes.White, new PointF(240f, 806f));

                        bitmap.Save(HeroImagePath, ImageFormat.Png);
                    }

                }
            }

            if (File.Exists(HeroImagePath)) {
                pictureBox1.ImageLocation = HeroImagePath;
            }
        }

        private void __DrawStat(string StatValue, Graphics Canvas, float X, float Y, string Color) {
            float CurrentX = X;

            foreach(char StatValueChar in StatValue) {
                string CharFile = Path.Combine(this.__ThisPath, "assets" + Path.DirectorySeparatorChar + StatValueChar + "_" + Color + ".png");
                if (File.Exists(CharFile)) {
                    using (Image Foreground = Image.FromFile(CharFile)) {
                        Canvas.DrawImage(
                            Foreground,
                            new Rectangle(
                                (int) CurrentX,
                                (int) Y,
                                26,
                                30
                            ),
                            new Rectangle(
                                0,
                                0,
                                36,
                                44
                            ),
                            GraphicsUnit.Pixel
                        );
                    }
                }

                CurrentX += 21;
            }
        }

        private void HeroesListbox_SelectedIndexChanged(object sender, EventArgs e) {
            this.__RenderHero(this.__HeroData[HeroesListbox.SelectedIndex]);
        }

        private string __GetStatColor(JToken HeroData, string Stat) {
            string Color = "yellow";

            foreach (JToken Superboon in HeroData["Superboon"]) {
                if (Superboon.ToString().Trim() == Stat) {
                    Color = "green";
                }
            }

            foreach (JToken Superbane in HeroData["Superbane"]) {
                if(Superbane.ToString().Trim() == Stat) {
                    Color = "red";
                }
            }

            return Color;
        }

        private void Button1_Click(object sender, EventArgs e) {
            HeroJsonFileSelect.Filter = "Hero file (.json)|*.json";
            HeroJsonFileSelect.Title  = "Select heroes .json file";
            HeroJsonFileSelect.CheckFileExists = true;

            HeroJsonFileSelect.ShowDialog();
        }

        private void HeroJsonFileSelect_FileOk(object sender, CancelEventArgs e) {
            this.__LoadHeroesFile(HeroJsonFileSelect.FileName);
        }
    }
}

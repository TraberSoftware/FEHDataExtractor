namespace FEH_Hero_Visualizer {
    partial class Form1 {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent() {
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.HeroesGroup = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.HeroesListbox = new System.Windows.Forms.ListBox();
            this.HeroStatsGroup = new System.Windows.Forms.GroupBox();
            this.HeroResTextbox = new System.Windows.Forms.TextBox();
            this.HeroDefTextbox = new System.Windows.Forms.TextBox();
            this.HeroSpdTextbox = new System.Windows.Forms.TextBox();
            this.HeroAtkTextbox = new System.Windows.Forms.TextBox();
            this.HeroHpTextbox = new System.Windows.Forms.TextBox();
            this.HeroResLabel = new System.Windows.Forms.Label();
            this.HeroDefLabel = new System.Windows.Forms.Label();
            this.HeroSpdLabel = new System.Windows.Forms.Label();
            this.HeroAtkLabel = new System.Windows.Forms.Label();
            this.HeroHpLabel = new System.Windows.Forms.Label();
            this.HeroJsonFileSelect = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.HeroRes40Textbox = new System.Windows.Forms.TextBox();
            this.HeroDef40Textbox = new System.Windows.Forms.TextBox();
            this.HeroSpd40Textbox = new System.Windows.Forms.TextBox();
            this.HeroAtk40Textbox = new System.Windows.Forms.TextBox();
            this.HeroHp40Textbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.StatModifiersGroup = new System.Windows.Forms.GroupBox();
            this.SuperboonLabel = new System.Windows.Forms.Label();
            this.SuperboonsTextbox = new System.Windows.Forms.TextBox();
            this.SuperbaneLabel = new System.Windows.Forms.Label();
            this.SuperbanesTextbox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.HeroesGroup.SuspendLayout();
            this.HeroStatsGroup.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.StatModifiersGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(13, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(360, 640);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // HeroesGroup
            // 
            this.HeroesGroup.Controls.Add(this.button1);
            this.HeroesGroup.Controls.Add(this.HeroesListbox);
            this.HeroesGroup.Location = new System.Drawing.Point(380, 13);
            this.HeroesGroup.Name = "HeroesGroup";
            this.HeroesGroup.Size = new System.Drawing.Size(200, 154);
            this.HeroesGroup.TabIndex = 1;
            this.HeroesGroup.TabStop = false;
            this.HeroesGroup.Text = "Heroes";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(7, 122);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(183, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Load heroes";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // HeroesListbox
            // 
            this.HeroesListbox.FormattingEnabled = true;
            this.HeroesListbox.Location = new System.Drawing.Point(7, 20);
            this.HeroesListbox.Name = "HeroesListbox";
            this.HeroesListbox.Size = new System.Drawing.Size(183, 95);
            this.HeroesListbox.TabIndex = 0;
            this.HeroesListbox.SelectedIndexChanged += new System.EventHandler(this.HeroesListbox_SelectedIndexChanged);
            // 
            // HeroStatsGroup
            // 
            this.HeroStatsGroup.Controls.Add(this.HeroResTextbox);
            this.HeroStatsGroup.Controls.Add(this.HeroDefTextbox);
            this.HeroStatsGroup.Controls.Add(this.HeroSpdTextbox);
            this.HeroStatsGroup.Controls.Add(this.HeroAtkTextbox);
            this.HeroStatsGroup.Controls.Add(this.HeroHpTextbox);
            this.HeroStatsGroup.Controls.Add(this.HeroResLabel);
            this.HeroStatsGroup.Controls.Add(this.HeroDefLabel);
            this.HeroStatsGroup.Controls.Add(this.HeroSpdLabel);
            this.HeroStatsGroup.Controls.Add(this.HeroAtkLabel);
            this.HeroStatsGroup.Controls.Add(this.HeroHpLabel);
            this.HeroStatsGroup.Location = new System.Drawing.Point(586, 13);
            this.HeroStatsGroup.Name = "HeroStatsGroup";
            this.HeroStatsGroup.Size = new System.Drawing.Size(200, 154);
            this.HeroStatsGroup.TabIndex = 2;
            this.HeroStatsGroup.TabStop = false;
            this.HeroStatsGroup.Text = "Hero Stats - Level 1";
            // 
            // HeroResTextbox
            // 
            this.HeroResTextbox.Enabled = false;
            this.HeroResTextbox.Location = new System.Drawing.Point(62, 123);
            this.HeroResTextbox.Name = "HeroResTextbox";
            this.HeroResTextbox.Size = new System.Drawing.Size(128, 20);
            this.HeroResTextbox.TabIndex = 14;
            // 
            // HeroDefTextbox
            // 
            this.HeroDefTextbox.Enabled = false;
            this.HeroDefTextbox.Location = new System.Drawing.Point(62, 97);
            this.HeroDefTextbox.Name = "HeroDefTextbox";
            this.HeroDefTextbox.Size = new System.Drawing.Size(128, 20);
            this.HeroDefTextbox.TabIndex = 13;
            // 
            // HeroSpdTextbox
            // 
            this.HeroSpdTextbox.Enabled = false;
            this.HeroSpdTextbox.Location = new System.Drawing.Point(62, 71);
            this.HeroSpdTextbox.Name = "HeroSpdTextbox";
            this.HeroSpdTextbox.Size = new System.Drawing.Size(128, 20);
            this.HeroSpdTextbox.TabIndex = 12;
            // 
            // HeroAtkTextbox
            // 
            this.HeroAtkTextbox.Enabled = false;
            this.HeroAtkTextbox.Location = new System.Drawing.Point(62, 45);
            this.HeroAtkTextbox.Name = "HeroAtkTextbox";
            this.HeroAtkTextbox.Size = new System.Drawing.Size(128, 20);
            this.HeroAtkTextbox.TabIndex = 11;
            // 
            // HeroHpTextbox
            // 
            this.HeroHpTextbox.Enabled = false;
            this.HeroHpTextbox.Location = new System.Drawing.Point(62, 19);
            this.HeroHpTextbox.Name = "HeroHpTextbox";
            this.HeroHpTextbox.Size = new System.Drawing.Size(128, 20);
            this.HeroHpTextbox.TabIndex = 10;
            // 
            // HeroResLabel
            // 
            this.HeroResLabel.Location = new System.Drawing.Point(6, 126);
            this.HeroResLabel.Name = "HeroResLabel";
            this.HeroResLabel.Size = new System.Drawing.Size(50, 13);
            this.HeroResLabel.TabIndex = 4;
            this.HeroResLabel.Text = "RES";
            this.HeroResLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HeroDefLabel
            // 
            this.HeroDefLabel.Location = new System.Drawing.Point(6, 100);
            this.HeroDefLabel.Name = "HeroDefLabel";
            this.HeroDefLabel.Size = new System.Drawing.Size(50, 13);
            this.HeroDefLabel.TabIndex = 3;
            this.HeroDefLabel.Text = "DEF";
            this.HeroDefLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HeroSpdLabel
            // 
            this.HeroSpdLabel.Location = new System.Drawing.Point(6, 74);
            this.HeroSpdLabel.Name = "HeroSpdLabel";
            this.HeroSpdLabel.Size = new System.Drawing.Size(50, 13);
            this.HeroSpdLabel.TabIndex = 2;
            this.HeroSpdLabel.Text = "SPD";
            this.HeroSpdLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HeroAtkLabel
            // 
            this.HeroAtkLabel.Location = new System.Drawing.Point(6, 48);
            this.HeroAtkLabel.Name = "HeroAtkLabel";
            this.HeroAtkLabel.Size = new System.Drawing.Size(50, 13);
            this.HeroAtkLabel.TabIndex = 1;
            this.HeroAtkLabel.Text = "ATK";
            this.HeroAtkLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HeroHpLabel
            // 
            this.HeroHpLabel.Location = new System.Drawing.Point(6, 22);
            this.HeroHpLabel.Name = "HeroHpLabel";
            this.HeroHpLabel.Size = new System.Drawing.Size(50, 13);
            this.HeroHpLabel.TabIndex = 0;
            this.HeroHpLabel.Text = "HP";
            this.HeroHpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // HeroJsonFileSelect
            // 
            this.HeroJsonFileSelect.FileName = "HeroJsonFileSelect";
            this.HeroJsonFileSelect.FileOk += new System.ComponentModel.CancelEventHandler(this.HeroJsonFileSelect_FileOk);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.HeroRes40Textbox);
            this.groupBox1.Controls.Add(this.HeroDef40Textbox);
            this.groupBox1.Controls.Add(this.HeroSpd40Textbox);
            this.groupBox1.Controls.Add(this.HeroAtk40Textbox);
            this.groupBox1.Controls.Add(this.HeroHp40Textbox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Location = new System.Drawing.Point(793, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(200, 154);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hero Stats - Level 40";
            // 
            // HeroRes40Textbox
            // 
            this.HeroRes40Textbox.Enabled = false;
            this.HeroRes40Textbox.Location = new System.Drawing.Point(62, 123);
            this.HeroRes40Textbox.Name = "HeroRes40Textbox";
            this.HeroRes40Textbox.Size = new System.Drawing.Size(128, 20);
            this.HeroRes40Textbox.TabIndex = 29;
            // 
            // HeroDef40Textbox
            // 
            this.HeroDef40Textbox.Enabled = false;
            this.HeroDef40Textbox.Location = new System.Drawing.Point(62, 97);
            this.HeroDef40Textbox.Name = "HeroDef40Textbox";
            this.HeroDef40Textbox.Size = new System.Drawing.Size(128, 20);
            this.HeroDef40Textbox.TabIndex = 28;
            // 
            // HeroSpd40Textbox
            // 
            this.HeroSpd40Textbox.Enabled = false;
            this.HeroSpd40Textbox.Location = new System.Drawing.Point(62, 71);
            this.HeroSpd40Textbox.Name = "HeroSpd40Textbox";
            this.HeroSpd40Textbox.Size = new System.Drawing.Size(128, 20);
            this.HeroSpd40Textbox.TabIndex = 27;
            // 
            // HeroAtk40Textbox
            // 
            this.HeroAtk40Textbox.Enabled = false;
            this.HeroAtk40Textbox.Location = new System.Drawing.Point(62, 45);
            this.HeroAtk40Textbox.Name = "HeroAtk40Textbox";
            this.HeroAtk40Textbox.Size = new System.Drawing.Size(128, 20);
            this.HeroAtk40Textbox.TabIndex = 26;
            // 
            // HeroHp40Textbox
            // 
            this.HeroHp40Textbox.Enabled = false;
            this.HeroHp40Textbox.Location = new System.Drawing.Point(62, 19);
            this.HeroHp40Textbox.Name = "HeroHp40Textbox";
            this.HeroHp40Textbox.Size = new System.Drawing.Size(128, 20);
            this.HeroHp40Textbox.TabIndex = 25;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 24;
            this.label1.Text = "RES";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(6, 100);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "DEF";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(6, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 22;
            this.label3.Text = "SPD";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "ATK";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(6, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "HP";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // StatModifiersGroup
            // 
            this.StatModifiersGroup.Controls.Add(this.SuperbanesTextbox);
            this.StatModifiersGroup.Controls.Add(this.SuperbaneLabel);
            this.StatModifiersGroup.Controls.Add(this.SuperboonsTextbox);
            this.StatModifiersGroup.Controls.Add(this.SuperboonLabel);
            this.StatModifiersGroup.Location = new System.Drawing.Point(380, 174);
            this.StatModifiersGroup.Name = "StatModifiersGroup";
            this.StatModifiersGroup.Size = new System.Drawing.Size(613, 51);
            this.StatModifiersGroup.TabIndex = 4;
            this.StatModifiersGroup.TabStop = false;
            this.StatModifiersGroup.Text = "Stat Modifiers";
            // 
            // SuperboonLabel
            // 
            this.SuperboonLabel.AutoSize = true;
            this.SuperboonLabel.Location = new System.Drawing.Point(8, 23);
            this.SuperboonLabel.Name = "SuperboonLabel";
            this.SuperboonLabel.Size = new System.Drawing.Size(64, 13);
            this.SuperboonLabel.TabIndex = 0;
            this.SuperboonLabel.Text = "Superboons";
            // 
            // SuperboonsTextbox
            // 
            this.SuperboonsTextbox.Location = new System.Drawing.Point(78, 20);
            this.SuperboonsTextbox.Name = "SuperboonsTextbox";
            this.SuperboonsTextbox.Size = new System.Drawing.Size(218, 20);
            this.SuperboonsTextbox.TabIndex = 1;
            // 
            // SuperbaneLabel
            // 
            this.SuperbaneLabel.AutoSize = true;
            this.SuperbaneLabel.Location = new System.Drawing.Point(302, 23);
            this.SuperbaneLabel.Name = "SuperbaneLabel";
            this.SuperbaneLabel.Size = new System.Drawing.Size(64, 13);
            this.SuperbaneLabel.TabIndex = 2;
            this.SuperbaneLabel.Text = "Superbanes";
            // 
            // SuperbanesTextbox
            // 
            this.SuperbanesTextbox.Location = new System.Drawing.Point(372, 20);
            this.SuperbanesTextbox.Name = "SuperbanesTextbox";
            this.SuperbanesTextbox.Size = new System.Drawing.Size(231, 20);
            this.SuperbanesTextbox.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1003, 667);
            this.Controls.Add(this.StatModifiersGroup);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.HeroStatsGroup);
            this.Controls.Add(this.HeroesGroup);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.HeroesGroup.ResumeLayout(false);
            this.HeroStatsGroup.ResumeLayout(false);
            this.HeroStatsGroup.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.StatModifiersGroup.ResumeLayout(false);
            this.StatModifiersGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox HeroesGroup;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox HeroesListbox;
        private System.Windows.Forms.GroupBox HeroStatsGroup;
        private System.Windows.Forms.Label HeroResLabel;
        private System.Windows.Forms.Label HeroDefLabel;
        private System.Windows.Forms.Label HeroSpdLabel;
        private System.Windows.Forms.Label HeroAtkLabel;
        private System.Windows.Forms.Label HeroHpLabel;
        private System.Windows.Forms.OpenFileDialog HeroJsonFileSelect;
        private System.Windows.Forms.TextBox HeroHpTextbox;
        private System.Windows.Forms.TextBox HeroResTextbox;
        private System.Windows.Forms.TextBox HeroDefTextbox;
        private System.Windows.Forms.TextBox HeroSpdTextbox;
        private System.Windows.Forms.TextBox HeroAtkTextbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox HeroRes40Textbox;
        private System.Windows.Forms.TextBox HeroDef40Textbox;
        private System.Windows.Forms.TextBox HeroSpd40Textbox;
        private System.Windows.Forms.TextBox HeroAtk40Textbox;
        private System.Windows.Forms.TextBox HeroHp40Textbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox StatModifiersGroup;
        private System.Windows.Forms.TextBox SuperbanesTextbox;
        private System.Windows.Forms.Label SuperbaneLabel;
        private System.Windows.Forms.TextBox SuperboonsTextbox;
        private System.Windows.Forms.Label SuperboonLabel;
    }
}


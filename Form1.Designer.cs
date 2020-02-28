namespace Coach
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnHackerTool = new System.Windows.Forms.Button();
            this.BtnTorch = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnAlien = new System.Windows.Forms.Button();
            this.btnAndroid = new System.Windows.Forms.Button();
            this.btnHeavyAndroid = new System.Windows.Forms.Button();
            this.btnFaceHugger = new System.Windows.Forms.Button();
            this.btnRiotGuards = new System.Windows.Forms.Button();
            this.btnCivilian = new System.Windows.Forms.Button();
            this.btnSecGuards = new System.Windows.Forms.Button();
            this.btnEnableGasMask = new System.Windows.Forms.Button();
            this.btnDisableIntroMovies = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHackerTool
            // 
            this.btnHackerTool.Location = new System.Drawing.Point(12, 89);
            this.btnHackerTool.Name = "btnHackerTool";
            this.btnHackerTool.Size = new System.Drawing.Size(215, 23);
            this.btnHackerTool.TabIndex = 4;
            this.btnHackerTool.Text = "Enable Hacker Tool (lvl3)";
            this.btnHackerTool.UseVisualStyleBackColor = true;
            this.btnHackerTool.Click += new System.EventHandler(this.btnHackerTool_Click);
            // 
            // BtnTorch
            // 
            this.BtnTorch.Location = new System.Drawing.Point(12, 130);
            this.BtnTorch.Name = "BtnTorch";
            this.BtnTorch.Size = new System.Drawing.Size(215, 23);
            this.BtnTorch.TabIndex = 19;
            this.BtnTorch.Text = "Enable Ion Torch (lvl3)";
            this.BtnTorch.UseVisualStyleBackColor = true;
            this.BtnTorch.Click += new System.EventHandler(this.BtnTorch_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Novice",
            "Easy",
            "Medium",
            "Hard",
            "Nightmare"});
            this.comboBox1.Location = new System.Drawing.Point(12, 51);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(215, 21);
            this.comboBox1.TabIndex = 20;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // btnAlien
            // 
            this.btnAlien.Location = new System.Drawing.Point(12, 209);
            this.btnAlien.Name = "btnAlien";
            this.btnAlien.Size = new System.Drawing.Size(215, 23);
            this.btnAlien.TabIndex = 21;
            this.btnAlien.Text = "Disable Alien";
            this.btnAlien.UseVisualStyleBackColor = true;
            this.btnAlien.Click += new System.EventHandler(this.btnAlien_Click);
            // 
            // btnAndroid
            // 
            this.btnAndroid.Location = new System.Drawing.Point(12, 249);
            this.btnAndroid.Name = "btnAndroid";
            this.btnAndroid.Size = new System.Drawing.Size(215, 23);
            this.btnAndroid.TabIndex = 22;
            this.btnAndroid.Text = "Disable Normal Androids";
            this.btnAndroid.UseVisualStyleBackColor = true;
            this.btnAndroid.Click += new System.EventHandler(this.btnAndroid_Click);
            // 
            // btnHeavyAndroid
            // 
            this.btnHeavyAndroid.Location = new System.Drawing.Point(12, 290);
            this.btnHeavyAndroid.Name = "btnHeavyAndroid";
            this.btnHeavyAndroid.Size = new System.Drawing.Size(215, 23);
            this.btnHeavyAndroid.TabIndex = 23;
            this.btnHeavyAndroid.Text = "Disable Hazmat Androids";
            this.btnHeavyAndroid.UseVisualStyleBackColor = true;
            this.btnHeavyAndroid.Click += new System.EventHandler(this.btnHeavyAndroid_Click);
            // 
            // btnFaceHugger
            // 
            this.btnFaceHugger.Location = new System.Drawing.Point(12, 330);
            this.btnFaceHugger.Name = "btnFaceHugger";
            this.btnFaceHugger.Size = new System.Drawing.Size(215, 23);
            this.btnFaceHugger.TabIndex = 24;
            this.btnFaceHugger.Text = "Disable Facehuggers";
            this.btnFaceHugger.UseVisualStyleBackColor = true;
            this.btnFaceHugger.Click += new System.EventHandler(this.btnFaceHugger_Click);
            // 
            // btnRiotGuards
            // 
            this.btnRiotGuards.Location = new System.Drawing.Point(12, 369);
            this.btnRiotGuards.Name = "btnRiotGuards";
            this.btnRiotGuards.Size = new System.Drawing.Size(215, 23);
            this.btnRiotGuards.TabIndex = 25;
            this.btnRiotGuards.Text = "Disable Riot Guards";
            this.btnRiotGuards.UseVisualStyleBackColor = true;
            this.btnRiotGuards.Click += new System.EventHandler(this.btnRiotGuards_Click);
            // 
            // btnCivilian
            // 
            this.btnCivilian.Location = new System.Drawing.Point(12, 452);
            this.btnCivilian.Name = "btnCivilian";
            this.btnCivilian.Size = new System.Drawing.Size(215, 23);
            this.btnCivilian.TabIndex = 26;
            this.btnCivilian.Text = "Disable Civilians (hostile)";
            this.btnCivilian.UseVisualStyleBackColor = true;
            this.btnCivilian.Click += new System.EventHandler(this.btnCivilian_Click);
            // 
            // btnSecGuards
            // 
            this.btnSecGuards.Location = new System.Drawing.Point(12, 412);
            this.btnSecGuards.Name = "btnSecGuards";
            this.btnSecGuards.Size = new System.Drawing.Size(215, 23);
            this.btnSecGuards.TabIndex = 27;
            this.btnSecGuards.Text = "Disable Security Guards";
            this.btnSecGuards.UseVisualStyleBackColor = true;
            this.btnSecGuards.Click += new System.EventHandler(this.btnSecGuards_Click);
            // 
            // btnEnableGasMask
            // 
            this.btnEnableGasMask.Location = new System.Drawing.Point(12, 169);
            this.btnEnableGasMask.Name = "btnEnableGasMask";
            this.btnEnableGasMask.Size = new System.Drawing.Size(215, 23);
            this.btnEnableGasMask.TabIndex = 28;
            this.btnEnableGasMask.Text = "Enable Gas mask";
            this.btnEnableGasMask.UseVisualStyleBackColor = true;
            this.btnEnableGasMask.Click += new System.EventHandler(this.btnGasmask_Click);
            // 
            // btnDisableIntroMovies
            // 
            this.btnDisableIntroMovies.Location = new System.Drawing.Point(12, 12);
            this.btnDisableIntroMovies.Name = "btnDisableIntroMovies";
            this.btnDisableIntroMovies.Size = new System.Drawing.Size(215, 23);
            this.btnDisableIntroMovies.TabIndex = 29;
            this.btnDisableIntroMovies.Text = "Disable Intro movies";
            this.btnDisableIntroMovies.UseVisualStyleBackColor = true;
            this.btnDisableIntroMovies.Click += new System.EventHandler(this.btnDisableIntroMovies_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(243, 485);
            this.Controls.Add(this.btnDisableIntroMovies);
            this.Controls.Add(this.btnEnableGasMask);
            this.Controls.Add(this.btnSecGuards);
            this.Controls.Add(this.btnCivilian);
            this.Controls.Add(this.btnRiotGuards);
            this.Controls.Add(this.btnFaceHugger);
            this.Controls.Add(this.btnHeavyAndroid);
            this.Controls.Add(this.btnAndroid);
            this.Controls.Add(this.btnAlien);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.BtnTorch);
            this.Controls.Add(this.btnHackerTool);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AI v1.3";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnHackerTool;
        private System.Windows.Forms.Button BtnTorch;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button btnAlien;
        private System.Windows.Forms.Button btnAndroid;
        private System.Windows.Forms.Button btnHeavyAndroid;
        private System.Windows.Forms.Button btnFaceHugger;
        private System.Windows.Forms.Button btnRiotGuards;
        private System.Windows.Forms.Button btnCivilian;
        private System.Windows.Forms.Button btnSecGuards;
        private System.Windows.Forms.Button btnEnableGasMask;
        private System.Windows.Forms.Button btnDisableIntroMovies;
    }
}


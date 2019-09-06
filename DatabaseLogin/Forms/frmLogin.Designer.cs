namespace DatabaseLogin.Forms
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.gbServer = new System.Windows.Forms.GroupBox();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.txtChosenDatabaseFile = new System.Windows.Forms.TextBox();
            this.btnOdspoji = new System.Windows.Forms.Button();
            this.btnSpojiSe = new System.Windows.Forms.Button();
            this.cboServer = new System.Windows.Forms.ComboBox();
            this.gbBaza = new System.Windows.Forms.GroupBox();
            this.btnPrijava = new System.Windows.Forms.Button();
            this.cboBaza = new System.Windows.Forms.ComboBox();
            this.btnOdustani = new System.Windows.Forms.Button();
            this.btnPostavke = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.mainPanel = new System.Windows.Forms.Panel();
            this.txtInstallationFolder = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.fbDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.btnFolder = new System.Windows.Forms.Button();
            this.gbServer.SuspendLayout();
            this.gbBaza.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.mainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbServer
            // 
            this.gbServer.Controls.Add(this.btnChooseFile);
            this.gbServer.Controls.Add(this.txtChosenDatabaseFile);
            this.gbServer.Controls.Add(this.btnOdspoji);
            this.gbServer.Controls.Add(this.btnSpojiSe);
            this.gbServer.Controls.Add(this.cboServer);
            this.gbServer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbServer.ForeColor = System.Drawing.SystemColors.Control;
            this.gbServer.Location = new System.Drawing.Point(10, 87);
            this.gbServer.Name = "gbServer";
            this.gbServer.Size = new System.Drawing.Size(305, 140);
            this.gbServer.TabIndex = 0;
            this.gbServer.TabStop = false;
            this.gbServer.Text = "Server";
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.BackColor = System.Drawing.Color.White;
            this.btnChooseFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChooseFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnChooseFile.Location = new System.Drawing.Point(261, 51);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(26, 20);
            this.btnChooseFile.TabIndex = 4;
            this.btnChooseFile.Text = "...";
            this.btnChooseFile.UseVisualStyleBackColor = false;
            this.btnChooseFile.Visible = false;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // txtChosenDatabaseFile
            // 
            this.txtChosenDatabaseFile.Location = new System.Drawing.Point(17, 51);
            this.txtChosenDatabaseFile.Name = "txtChosenDatabaseFile";
            this.txtChosenDatabaseFile.ReadOnly = true;
            this.txtChosenDatabaseFile.Size = new System.Drawing.Size(238, 20);
            this.txtChosenDatabaseFile.TabIndex = 3;
            this.txtChosenDatabaseFile.Text = "*.accdb file";
            this.txtChosenDatabaseFile.Visible = false;
            // 
            // btnOdspoji
            // 
            this.btnOdspoji.BackColor = System.Drawing.Color.White;
            this.btnOdspoji.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOdspoji.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnOdspoji.Image = global::DatabaseLogin.Properties.Resources.Disconnected;
            this.btnOdspoji.Location = new System.Drawing.Point(176, 77);
            this.btnOdspoji.Name = "btnOdspoji";
            this.btnOdspoji.Size = new System.Drawing.Size(111, 47);
            this.btnOdspoji.TabIndex = 2;
            this.btnOdspoji.Text = "Disconnect";
            this.btnOdspoji.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdspoji.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOdspoji.UseVisualStyleBackColor = false;
            this.btnOdspoji.Click += new System.EventHandler(this.btnOdspoji_Click);
            // 
            // btnSpojiSe
            // 
            this.btnSpojiSe.BackColor = System.Drawing.Color.White;
            this.btnSpojiSe.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSpojiSe.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSpojiSe.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnSpojiSe.Image = global::DatabaseLogin.Properties.Resources.Connected;
            this.btnSpojiSe.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSpojiSe.Location = new System.Drawing.Point(17, 77);
            this.btnSpojiSe.Name = "btnSpojiSe";
            this.btnSpojiSe.Size = new System.Drawing.Size(111, 47);
            this.btnSpojiSe.TabIndex = 1;
            this.btnSpojiSe.Text = "Connect";
            this.btnSpojiSe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSpojiSe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnSpojiSe.UseVisualStyleBackColor = false;
            this.btnSpojiSe.Click += new System.EventHandler(this.btnSpojiSe_Click);
            // 
            // cboServer
            // 
            this.cboServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServer.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboServer.FormattingEnabled = true;
            this.cboServer.Location = new System.Drawing.Point(17, 24);
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size(270, 21);
            this.cboServer.TabIndex = 0;
            this.cboServer.SelectedIndexChanged += new System.EventHandler(this.cboServer_SelectedIndexChanged);
            // 
            // gbBaza
            // 
            this.gbBaza.Controls.Add(this.btnPrijava);
            this.gbBaza.Controls.Add(this.cboBaza);
            this.gbBaza.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.gbBaza.ForeColor = System.Drawing.SystemColors.Control;
            this.gbBaza.Location = new System.Drawing.Point(321, 87);
            this.gbBaza.Name = "gbBaza";
            this.gbBaza.Size = new System.Drawing.Size(305, 140);
            this.gbBaza.TabIndex = 1;
            this.gbBaza.TabStop = false;
            this.gbBaza.Text = "Database";
            // 
            // btnPrijava
            // 
            this.btnPrijava.BackColor = System.Drawing.Color.White;
            this.btnPrijava.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPrijava.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnPrijava.Image = global::DatabaseLogin.Properties.Resources.Enter;
            this.btnPrijava.Location = new System.Drawing.Point(106, 77);
            this.btnPrijava.Name = "btnPrijava";
            this.btnPrijava.Size = new System.Drawing.Size(92, 47);
            this.btnPrijava.TabIndex = 1;
            this.btnPrijava.Text = "Login";
            this.btnPrijava.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPrijava.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPrijava.UseVisualStyleBackColor = false;
            this.btnPrijava.Click += new System.EventHandler(this.btnPrijava_Click);
            // 
            // cboBaza
            // 
            this.cboBaza.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBaza.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboBaza.FormattingEnabled = true;
            this.cboBaza.Location = new System.Drawing.Point(17, 24);
            this.cboBaza.Name = "cboBaza";
            this.cboBaza.Size = new System.Drawing.Size(270, 21);
            this.cboBaza.TabIndex = 0;
            // 
            // btnOdustani
            // 
            this.btnOdustani.BackColor = System.Drawing.Color.White;
            this.btnOdustani.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOdustani.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOdustani.Image = global::DatabaseLogin.Properties.Resources.Cancel32;
            this.btnOdustani.Location = new System.Drawing.Point(432, 265);
            this.btnOdustani.Name = "btnOdustani";
            this.btnOdustani.Size = new System.Drawing.Size(94, 41);
            this.btnOdustani.TabIndex = 2;
            this.btnOdustani.Text = "Cancel";
            this.btnOdustani.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOdustani.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOdustani.UseVisualStyleBackColor = false;
            this.btnOdustani.Click += new System.EventHandler(this.btnOdustani_Click);
            // 
            // btnPostavke
            // 
            this.btnPostavke.BackColor = System.Drawing.Color.White;
            this.btnPostavke.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnPostavke.Image = global::DatabaseLogin.Properties.Resources.Settings32;
            this.btnPostavke.Location = new System.Drawing.Point(10, 265);
            this.btnPostavke.Name = "btnPostavke";
            this.btnPostavke.Size = new System.Drawing.Size(94, 41);
            this.btnPostavke.TabIndex = 5;
            this.btnPostavke.Text = "Settings";
            this.btnPostavke.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnPostavke.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnPostavke.UseVisualStyleBackColor = false;
            this.btnPostavke.Click += new System.EventHandler(this.btnPostavke_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = global::DatabaseLogin.Properties.Resources.loginLogo1;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(616, 71);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // mainPanel
            // 
            this.mainPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.mainPanel.Controls.Add(this.btnFolder);
            this.mainPanel.Controls.Add(this.txtInstallationFolder);
            this.mainPanel.Controls.Add(this.label1);
            this.mainPanel.Controls.Add(this.btnOk);
            this.mainPanel.Controls.Add(this.btnOdustani);
            this.mainPanel.Controls.Add(this.gbBaza);
            this.mainPanel.Controls.Add(this.pictureBox1);
            this.mainPanel.Controls.Add(this.btnPostavke);
            this.mainPanel.Controls.Add(this.gbServer);
            this.mainPanel.Location = new System.Drawing.Point(0, 0);
            this.mainPanel.MaximumSize = new System.Drawing.Size(640, 320);
            this.mainPanel.MinimumSize = new System.Drawing.Size(640, 320);
            this.mainPanel.Name = "mainPanel";
            this.mainPanel.Size = new System.Drawing.Size(640, 320);
            this.mainPanel.TabIndex = 0;
            // 
            // txtInstallationFolder
            // 
            this.txtInstallationFolder.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInstallationFolder.Location = new System.Drawing.Point(105, 236);
            this.txtInstallationFolder.Name = "txtInstallationFolder";
            this.txtInstallationFolder.Size = new System.Drawing.Size(421, 20);
            this.txtInstallationFolder.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(10, 240);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Installation folder:";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.Color.White;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOk.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOk.Image = global::DatabaseLogin.Properties.Resources.Accept32;
            this.btnOk.Location = new System.Drawing.Point(532, 265);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(94, 41);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOk.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnFolder
            // 
            this.btnFolder.BackColor = System.Drawing.Color.White;
            this.btnFolder.BackgroundImage = global::DatabaseLogin.Properties.Resources.folderSettings32;
            this.btnFolder.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnFolder.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFolder.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.btnFolder.Location = new System.Drawing.Point(532, 234);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(94, 25);
            this.btnFolder.TabIndex = 8;
            this.btnFolder.UseVisualStyleBackColor = false;
            this.btnFolder.Click += new System.EventHandler(this.btnFolder_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnPrijava;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DimGray;
            this.CancelButton = this.btnOdustani;
            this.ClientSize = new System.Drawing.Size(640, 320);
            this.ControlBox = false;
            this.Controls.Add(this.mainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 320);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(640, 320);
            this.Name = "frmLogin";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Database Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmLogin_KeyDown);
            this.gbServer.ResumeLayout(false);
            this.gbServer.PerformLayout();
            this.gbBaza.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.mainPanel.ResumeLayout(false);
            this.mainPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbServer;
        private System.Windows.Forms.Button btnOdspoji;
        private System.Windows.Forms.Button btnSpojiSe;
        private System.Windows.Forms.ComboBox cboServer;
        private System.Windows.Forms.GroupBox gbBaza;
        private System.Windows.Forms.Button btnPrijava;
        private System.Windows.Forms.ComboBox cboBaza;
        private System.Windows.Forms.Button btnOdustani;
        private System.Windows.Forms.Button btnPostavke;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.TextBox txtChosenDatabaseFile;
        private System.Windows.Forms.Panel mainPanel;
        private System.Windows.Forms.TextBox txtInstallationFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fbDialog;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnFolder;
    }
}
namespace DatabaseLogin.Forms
{
    partial class frmLoginAccdbPostavke
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLoginAccdbPostavke));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.btnConfigFolder = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.txtFilePath);
            this.groupBox1.Controls.Add(this.btnConfigFolder);
            this.groupBox1.Location = new System.Drawing.Point(12, 86);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(759, 301);
            this.groupBox1.TabIndex = 8;
            this.groupBox1.TabStop = false;
            // 
            // txtFilePath
            // 
            this.txtFilePath.BackColor = System.Drawing.SystemColors.Control;
            this.txtFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtFilePath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.txtFilePath.Location = new System.Drawing.Point(6, 77);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(747, 26);
            this.txtFilePath.TabIndex = 7;
            // 
            // btnConfigFolder
            // 
            this.btnConfigFolder.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnConfigFolder.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnConfigFolder.Image = global::DatabaseLogin.Properties.Resources.folderSettings32;
            this.btnConfigFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnConfigFolder.Location = new System.Drawing.Point(6, 19);
            this.btnConfigFolder.Name = "btnConfigFolder";
            this.btnConfigFolder.Size = new System.Drawing.Size(119, 52);
            this.btnConfigFolder.TabIndex = 6;
            this.btnConfigFolder.Text = "Select Folder for Backup";
            this.btnConfigFolder.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnConfigFolder.UseVisualStyleBackColor = true;
            this.btnConfigFolder.Click += new System.EventHandler(this.btnConfigFolder_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.AutoSize = true;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnOK.Image = global::DatabaseLogin.Properties.Resources.Accept32;
            this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnOK.Location = new System.Drawing.Point(671, 393);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(94, 44);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Image = global::DatabaseLogin.Properties.Resources.Cancel32;
            this.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnCancel.Location = new System.Drawing.Point(18, 393);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(94, 44);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::DatabaseLogin.Properties.Resources.sett_db;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(783, 80);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // frmLoginAccdbPostavke
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(783, 449);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLoginAccdbPostavke";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login settings";
            this.Load += new System.EventHandler(this.frmLoginPostavke_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btnConfigFolder;
        private System.Windows.Forms.TextBox txtFilePath;
    }
}
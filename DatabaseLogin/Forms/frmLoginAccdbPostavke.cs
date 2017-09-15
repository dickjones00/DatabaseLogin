using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseLogin.Forms
{
    public partial class frmLoginAccdbPostavke : Form
    {
        public frmLoginAccdbPostavke()
        {
            InitializeComponent();
        }

        private void frmLoginPostavke_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.BackupFolder))
            {
                txtFilePath.Text = Properties.Settings.Default.BackupFolder;
            }
            else
            {
                txtFilePath.Text = "";
                btnOK.Enabled = false;
            }
        }
        
        
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                var backupFolder = "";

                if (txtFilePath.Text.ToLower().Contains("backup"))
                {
                    backupFolder = txtFilePath.Text;
                }
                else
                {
                    backupFolder = Path.Combine(txtFilePath.Text, "MedialBackup");
                }
                
                Properties.Settings.Default.BackupFolder = backupFolder;
                Properties.Settings.Default.Save();
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnConfigFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select backup folder";
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
            fbd.ShowNewFolderButton = true;
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                var backupFolder = "";

                if (fbd.SelectedPath.ToLower().Contains("backup"))
                {
                    backupFolder = fbd.SelectedPath;
                }
                else
                {
                    backupFolder = Path.Combine(fbd.SelectedPath, "MedialBackup");
                }
                txtFilePath.Text = backupFolder;
                btnOK.Enabled = true;
            }
        }
    }
}

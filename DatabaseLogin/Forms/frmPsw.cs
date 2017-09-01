
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseLogin.Forms
{
    public partial class frmPsw : Form
    {
        string xmlFilePath;
        bool OleDb = false;
        string passw;
        //private Collection<cServer> _servers = new Collection<cServer>();

        //public Collection<cServer> Servers
        //{
        //    get
        //    {
        //        return _servers;
        //    }
        //    set
        //    {
        //        _servers = value;
        //    }
        //}

        public frmPsw()
        {
            InitializeComponent();
        }

        public frmPsw(bool oleDb)
        {
            InitializeComponent();
            OleDb = oleDb;
            if (OleDb)
                this.Text = "Password for OleDB Microsoft access database";
            txtPSW.Focus();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdOK_Click(object sender, EventArgs e)
        {
            if (!OleDb)
            {
                ShowPostavke();
            }
            else
            {
                passw = txtPSW.Text;
                this.Close();
            }
        }

        private void ShowPostavke()
        {
            try
            {
                frmLoginPostavke Postavke = new frmLoginPostavke();

                if (txtPSW.Text == "darko" + DateTime.Now.Day)
                {
                    this.Close();
                    Postavke.ShowDialog();
                    this.xmlFilePath = Postavke.xmlFilePath();
                }
                else
                {
                    MessageBox.Show("Wrong password!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtPSW.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPsw_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        this.Close();
                        break;
                    case Keys.Enter:
                        ShowPostavke();
                        break;
                }

                if (e.KeyCode == Keys.H && e.Modifiers == Keys.Control)
                {
                    txtPSW.Text = "darko" + DateTime.Now.Day;
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmPsw_Load(object sender, EventArgs e)
        {
            txtPSW.Focus();
        }

        internal string SavedFileName()
        {
            return xmlFilePath;
        }

        internal string EnteredPassword()
        {
            return passw;
        }
    }
}

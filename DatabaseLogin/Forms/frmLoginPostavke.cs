using DatabaseLogin.Class;
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
using System.Xml.Linq;

namespace DatabaseLogin.Forms
{
    public partial class frmLoginPostavke : Form
    {
        cLoginClass LoginClass = new cLoginClass();
        private Collection<cServer> _servers = new Collection<cServer>();
        private Collection<cBaza> _baze = new Collection<cBaza>();
        string xmlPathString;
        public Collection<cServer> Servers
        {
            get
            {
                return _servers;
            }
            set
            {
                _servers = value;
            }
        }

        public Collection<cBaza> Baze
        {
            get
            {
                return _baze;
            }
            set
            {
                _baze = value;
            }
        }

        public frmLoginPostavke()
        {
            InitializeComponent();
        }

        private void frmLoginPostavke_Load(object sender, EventArgs e)
        {
            xmlPathString = Path.Combine(Path.GetDirectoryName(Application.UserAppDataPath), "servers.xml");
            if (File.Exists(xmlPathString))
            {
                PopuniServersFromXml();
            }
            NapraviKoloneGrida();
            
            //popunjavam grid sa svim serverima u postavkama 
            if (this.Servers.Count != 0)
            {
                OsvjeziGrid();
            }

            this.Baze.Clear();
        }

        private void PopuniServersFromXml()
        {
            XElement element = XElement.Load(xmlPathString);
            var storeElement = element;
            var serverElements = storeElement.Elements().Where(x => x.Name == "cServer");

            foreach (var serverElement in serverElements)
            {
                string decryptedPassword = cCryption.DecryptStringAES(serverElement.Element("Psw").Value.ToString(), "darko000");
                cServer server = new cServer()
                {
                    
                    Server = serverElement.Element("Server").Value.ToString(),
                    User = serverElement.Element("User").Value.ToString(),
                    Psw = decryptedPassword,
                    Prikazi = (bool)serverElement.Element("Prikazi")
                };

                Servers.Add(server);
            }
        }

        private void OsvjeziGrid()
        {
            try
            {
                int row = dgServers.RowCount;

                foreach (cServer Server in this.Servers)
                {
                    dgServers.Rows.Add();
                    dgServers.Rows[row].Cells["Server"].Value = Server.Server;
                    dgServers.Rows[row].Cells["User"].Value = Server.User;
                    dgServers.Rows[row].Cells["Psw"].Value = Server.Psw;
                    dgServers.Rows[row].Cells["Prikazi"].Value = Server.Prikazi;

                    row += 1;
                }

                PostaviKontroleNaGridu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PostaviKontroleNaGridu()
        {
            try
            {
                for (int i = 0; i <= dgServers.RowCount - 1; i++)
                {
                    if (Convert.ToBoolean(dgServers.Rows[i].Cells["Prikazi"].Value) == false)
                    {
                        dgServers.Rows[i].Cells["Baze"].ReadOnly = true;
                    }
                    else
                    {
                        dgServers.Rows[i].Cells["Baze"].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NapraviKoloneGrida()
        {
            try
            {
                //za servere
                DataGridViewCellStyle cHeaderStyle = new DataGridViewCellStyle();
                cHeaderStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                dgServers.ColumnHeadersDefaultCellStyle = cHeaderStyle;
                dgServers.AutoResizeColumnHeadersHeight();

                dgServers.ColumnCount = 3;
                dgServers.ColumnHeadersVisible = true;
                dgServers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                dgServers.Columns[0].Name = "Server";
                dgServers.Columns[0].HeaderText = "Server:";
                dgServers.Columns[0].Width = 200;

                dgServers.Columns[1].Name = "User";
                dgServers.Columns[1].HeaderText = "Korisničko ime:";
                dgServers.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                dgServers.Columns[2].Name = "Psw";
                dgServers.Columns[2].HeaderText = "Lozinka:";
                dgServers.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                DataGridViewCheckBoxColumn colPrikazi = new DataGridViewCheckBoxColumn();
                colPrikazi.HeaderText = "Prikaži:";
                colPrikazi.Name = "Prikazi";
                colPrikazi.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                colPrikazi.FlatStyle = FlatStyle.Standard;
                colPrikazi.CellTemplate = new DataGridViewCheckBoxCell();
                colPrikazi.ToolTipText = "Da li se prikazuje u listi na login formi?";
                dgServers.Columns.Insert(3, colPrikazi);

                DataGridViewButtonColumn colBaze = new DataGridViewButtonColumn();
                colBaze.HeaderText = "Odabir baza:";
                colBaze.Name = "Baze";
                colBaze.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                colBaze.FlatStyle = FlatStyle.Standard;
                colBaze.CellTemplate = new DataGridViewButtonCell();
                colBaze.Text = "...";
                colBaze.UseColumnTextForButtonValue = true;
                colBaze.ReadOnly = false;
                dgServers.Columns.Insert(4, colBaze);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        internal string xmlFilePath()
        {
            return xmlPathString;
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            try
            {
                int X = 0;

                dgServers.Rows.Add();

                X = dgServers.RowCount - 1;
                dgServers.Rows[X].Cells["Prikazi"].Value = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUkloni_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= dgServers.RowCount - 1; i++)
                {
                    if (dgServers.Rows[i].Selected == true)
                    {
                        dgServers.Rows.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
            try
            {
                this.Servers.Clear();

                foreach (DataGridViewRow dtRow in dgServers.Rows)
                {
                    var encryptedPassword = cCryption.EncryptStringAES(Convert.ToString(dtRow.Cells["Psw"].Value), "darko000");
                    cServer cSrv = new cServer
                                (
                                    Convert.ToString(dtRow.Cells["Server"].Value)
                                  , Convert.ToString(dtRow.Cells["User"].Value)
                                  , encryptedPassword
                                  , Convert.ToBoolean(dtRow.Cells["Prikazi"].Value)
                                );

                    this.Servers.Add(cSrv);
                }

                //sada te podatke treba negdje spremiti

                LoginClass.Servers = this.Servers;
                LoginClass.Korisnik = WindowsIdentity.GetCurrent().Name;
                LoginClass.SaveAll();
                this.xmlPathString = LoginClass.xmlFilePath;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgServers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    for (int i = 0; i <= dgServers.RowCount - 1; i++)
                    {
                        if (dgServers.Rows[i].Selected == true)
                        {
                            if (dgServers.Columns[e.ColumnIndex].Name == "Baze" && Convert.ToBoolean(dgServers.Rows[e.RowIndex].Cells["Prikazi"].Value) == true)
                            {
                                if (UpdateData(e.RowIndex) == true)
                                {
                                    if (LoginClass.BuildConnectionString() == true)
                                    {
                                        frmBaze baze = new frmBaze();
                                        baze.NazivServera = Convert.ToString(dgServers.Rows[e.RowIndex].Cells["Server"].Value);
                                        baze.User = WindowsIdentity.GetCurrent().Name;
                                        baze.ConnString = LoginClass.ConnString;
                                        baze.ShowDialog();

                                        this.Baze = baze.Baze;
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Nisu upisani svi podatci za spajanje na bazu!!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool UpdateData(int row)
        {
            try
            {
                if (Convert.ToString(dgServers.Rows[row].Cells["User"].Value) != string.Empty && Convert.ToString(dgServers.Rows[row].Cells["Server"].Value) != string.Empty)
                {
                    LoginClass.NazivServera = Convert.ToString(dgServers.Rows[row].Cells["Server"].Value);
                    LoginClass.User = Convert.ToString(dgServers.Rows[row].Cells["User"].Value);
                    LoginClass.Pass = Convert.ToString(dgServers.Rows[row].Cells["Psw"].Value);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void dgServers_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            try
            {
                int colUser = dgServers.Columns["User"].Index;
                int colPsw = dgServers.Columns["Psw"].Index;
                int colServer = dgServers.Columns["Server"].Index;
                int colPrikazi = dgServers.Columns["Prikazi"].Index;

                if (e.ColumnIndex == colUser
                    || e.ColumnIndex == colPsw || e.ColumnIndex == colServer
                    || e.ColumnIndex == colPrikazi)
                {
                    if (e.ColumnIndex == colUser || e.ColumnIndex == colPsw)
                    {
                        if (Convert.ToString(dgServers.Rows[e.RowIndex].Cells["Server"].Value) == string.Empty)
                        {
                            e.Cancel = true;
                        }
                        else
                        {
                            e.Cancel = false;
                        }
                    }
                    else if (e.ColumnIndex == colServer && Convert.ToString(dgServers.Rows[e.RowIndex].Cells["Server"].Value) != string.Empty)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        e.Cancel = false;
                    }
                }
                else
                {
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgServers_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int colServer = dgServers.Columns["Server"].Index;
                int colPrikazi = dgServers.Columns["Prikazi"].Index;

                if (e.ColumnIndex == colServer && Convert.ToString(dgServers.Rows[e.RowIndex].Cells["Server"].Value) != string.Empty)
                {
                    dgServers.Rows[e.RowIndex].Cells["Server"].Style.BackColor = Color.LightGreen;
                }
                else if (e.ColumnIndex == colPrikazi)
                {
                    if (Convert.ToInt32(dgServers.Rows[e.RowIndex].Cells[colPrikazi].Value) == 0)
                    {
                        dgServers.Rows[e.RowIndex].Cells["Baze"].ReadOnly = true;
                    }
                    else
                    {
                        dgServers.Rows[e.RowIndex].Cells["Baze"].ReadOnly = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgServers_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgServers.Columns[e.ColumnIndex].Index == 2)
            {
                if (e.Value != null)
                {
                    e.Value = new String('*', e.Value.ToString().Length);
                    e.FormattingApplied = true;
                }
            }
        }

        private void dgServers_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control.GetType() == typeof(DataGridViewTextBoxEditingControl) && dgServers.CurrentCell.ColumnIndex == 2)
            {
                TextBox txt = (TextBox)e.Control;
                txt.PasswordChar = '*';
            }
            else
            {
                TextBox txt = (TextBox)e.Control;
                txt.PasswordChar = new char();
            }
        }

        private void btnConfigFolder_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Path.GetDirectoryName(xmlPathString));
        }
    }
}

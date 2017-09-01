using DatabaseLogin.Class;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace DatabaseLogin.Forms
{
    public partial class frmLogin : Form
    {        
        cLoginClass LoginClass = new cLoginClass();
        private Collection<cServer> _servers = new Collection<cServer>();
        private Collection<cBaza> _baze = new Collection<cBaza>();
        OpenFileDialog ofd = new OpenFileDialog();
        bool oleDbSelected;

        public string ConnectionString()
        {
            return LoginClass.ConnString;
        }

        private string _connString;
        string xmlFilePath = Path.Combine(Path.GetDirectoryName(Application.UserAppDataPath), "servers.xml");
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

        public frmLogin()
        {
            InitializeComponent();
            Random rnd = new Random();
            int rndNumber = rnd.Next(1, 3);
            if (rndNumber == 1)
            {
                pictureBox1.BackgroundImage = Properties.Resources.loginLogo;
            }
            else
            {
                pictureBox1.BackgroundImage = Properties.Resources.loginLogo1;
            }
            
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            try
            {
                LoginClass.OcistiKontrole();
                
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal).FilePath;

                if (File.Exists(xmlFilePath))
                {
                    FillServersAndSettingsFromXml();
                }

                ProvjeriPostavke(LoginClass.NazivServera);
                string accessDbLastUsed = Properties.Settings.Default.LastUsedAccdb;
                DodajServere();
                
                if (string.IsNullOrEmpty(accessDbLastUsed) && (!string.IsNullOrEmpty(LoginClass.NazivServera) || !string.IsNullOrEmpty(LoginClass.NazivBaze)))
                {
                    int index = cboServer.Items.IndexOf(LoginClass.NazivServera);
                    cboServer.SelectedIndex = index;
                    cboBaza.Items.Add(LoginClass.NazivBaze);
                    cboBaza.SelectedIndex = 0;
                    PostaviKontrole(true);
                    oleDbSelected = false;
                }
                else
                {
                    btnOdspoji.Enabled = false;
                    if (!string.IsNullOrEmpty(accessDbLastUsed))
                    {
                        cboServer.SelectedIndex = cboServer.Items.Count - 1;
                        txtChosenDatabaseFile.Text = Properties.Settings.Default.LastUsedAccdb;
                        oleDbSelected = true;
                    }
                }
                btnSpojiSe.Select();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FillServersAndSettingsFromXml()
        {
            Servers.Clear();
            try
            {
                XElement element = XElement.Load(xmlFilePath);
                var storeElement = element;

                var serverElements = storeElement.Elements().Where(x => x.Name == "cServer");
                var NazivServera = storeElement.Elements().Where(x => x.Name == "zadnjiServer").FirstOrDefault();
                var NazivBaze = storeElement.Elements().Where(x => x.Name == "zadnjaBaza").FirstOrDefault();
                if (NazivBaze == null || NazivServera == null)
                {
                    LoginClass.NazivServera = string.Empty;
                    LoginClass.NazivBaze = string.Empty;
                }
                else
                {
                    LoginClass.NazivBaze = NazivBaze.Value;
                    LoginClass.NazivServera = NazivServera.Value;
                }
                foreach (var serverElement in serverElements)
                {
                    string decryptedPassword = cCryption.DecryptStringAES(serverElement.Element("Psw").Value.ToString(), "darko000");
                    cServer server = new cServer();
                    server.Server = serverElement.Element("Server").Value.ToString();
                    server.User = serverElement.Element("User").Value.ToString();
                    server.Psw = serverElement.Element("Psw").Value.ToString();
                    server.Prikazi = (bool)serverElement.Element("Prikazi");

                    Servers.Add(server);
                }
            }
            catch (Exception)
            {
                File.Move(xmlFilePath, xmlFilePath + "_bak");
                MessageBox.Show("Greška prilikom čitanja konfiguracijske datoteke. Napravljena je sigurnosna kopija datoteke:\r\n" + xmlFilePath, 
                                "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PostaviKontrole(bool PostojePodaci)
        {
            try
            {
                if (PostojePodaci == true)
                {
                    btnSpojiSe.Enabled = false;
                    btnOdspoji.Enabled = true;
                    cboServer.Enabled = false;
                    cboBaza.Enabled = false;
                    btnPrijava.Enabled = true;
                    btnOdustani.Enabled = true;
                }
                else
                {
                    btnSpojiSe.Enabled = true;
                    btnOdspoji.Enabled = false;
                    cboServer.Enabled = true;
                    cboBaza.Enabled = false;
                    cboServer.Items.Clear();
                    cboBaza.Items.Clear();
                    btnOdustani.Enabled = true;
                    btnPrijava.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmLogin_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.P && e.Modifiers == Keys.Control && !oleDbSelected)
            {
                OtvoriPostavke();
            }
        }

        private void OtvoriPostavke()
        {
            try
            {
                frmPsw Psw = new frmPsw();
                Psw.ShowDialog();
                var file = Psw.SavedFileName();
                if (File.Exists(xmlFilePath))
                {
                    FillServersAndSettingsFromXml();
                }
                PostaviKontrole(false);
                DodajServere();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPostavke_Click(object sender, EventArgs e)
        {
            OtvoriPostavke();
        }

        private void btnOdustani_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboBaza.Text == "")
                {
                    MessageBox.Show("Izaberite bazu na koju se spajate!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    SnimiPostavke();
                    //SpojiSe();

                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SnimiPostavke()
        {
            try
            {
                //snimam zadnje odabrane postavke
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlFilePath);

                XmlNode nodeServer = doc.CreateNode(XmlNodeType.Element, "zadnjiServer", null);
                nodeServer.InnerText = cboServer.Text;
                XmlNode nodeBaza = doc.CreateNode(XmlNodeType.Element, "zadnjaBaza", null);
                nodeBaza.InnerText = cboBaza.Text;
                XmlElement root = doc.DocumentElement;
                if (!root.ChildNodes.Cast<XmlNode>().Any(y => y.Name == "zadnjiServer"))
                {
                    root.AppendChild(nodeServer);
                }
                else
                {
                    var old = doc.SelectSingleNode("descendant::zadnjiServer");
                    root.ReplaceChild(nodeServer, old);
                }
                if (!root.ChildNodes.Cast<XmlNode>().Any(y => y.Name == "zadnjaBaza"))
                {
                    root.AppendChild(nodeBaza);
                }
                else
                {
                    var old = doc.SelectSingleNode("descendant::zadnjaBaza");
                    root.ReplaceChild(nodeBaza, old);
                }
                Properties.Settings.Default.LastUsedAccdb = "";
                doc.Save(xmlFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ProvjeriPostavke(string server)
        {
            try
            {
                cServer sv = Servers.Where(x => x.Server == server).FirstOrDefault();
                if (sv != null)
                {
                    LoginClass.User = sv.User;
                    LoginClass.NazivServera = sv.Server;
                    LoginClass.Prikazi = sv.Prikazi;
                    LoginClass.Pass = sv.Psw;
                    LoginClass.Servers = this.Servers;
                    LoginClass.OtvoriPostavkeZaServera(server);
                }
                if (string.IsNullOrEmpty(LoginClass.User))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void DodajServere()
        {
            try
            {
                cboServer.Items.Clear();

                foreach (cServer srv in Servers)
                {
                    if (srv.Prikazi == true)
                    {
                        cboServer.Items.Add(srv.Server);
                    }
                }

                if (cboServer.Items.Count > 0)
                {
                    cboServer.SelectedIndex = 0;
                }
                cboServer.Items.Add("");
                cboServer.Items.Add("Microsoft Access Database file (*.accdb)");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSpojiSe_Click(object sender, EventArgs e)
        {
            if (cboServer.SelectedIndex == cboServer.Items.Count - 1)
            {
                if (!string.IsNullOrEmpty(Properties.Settings.Default.LastUsedAccdb))
                {
                    string password = "";
                    string connError = "";
                    string connString = "Provider = Microsoft.ACE.OLEDB.12.0; " + 
                                        "Data Source = " + 
                                        Properties.Settings.Default.LastUsedAccdb + "; " + 
                                        "Persist Security Info = False; " + 
                                        "Mode = Share Deny None";
                    System.Data.OleDb.OleDbConnection conn = new System.Data.OleDb.OleDbConnection();
                    conn.ConnectionString = connString;
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        connError = ex.Message;
                    }
                    finally
                    {
                        if (connError == "Not a valid password.")
                        {
                            frmPsw pass = new frmPsw(true);
                            pass.ShowDialog();
                            password = pass.EnteredPassword();
                            connString = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = " + 
                                         Properties.Settings.Default.LastUsedAccdb + "; Persist Security Info = False; Mode = Share Deny None;Jet OLEDB:Database Password = " + password;
                            conn.ConnectionString = connString;
                        }
                        else
                        {
                            conn.Close();
                        }
                        
                    }
                    try
                    {
                        connError = "";
                        conn.Open();
                        MessageBox.Show("Connection succesfull!");
                        LoginClass.ConnString = connString;
                        btnPrijava.Enabled = true;
                        conn.Close();
                        btnPrijava.Focus();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message +   
                                        "\r\nPossible reason for this error: Missing installation of " + 
                                        "AccessDatabaseEngine.exe or AccessDatabaseEnginex64.exe " + 
                                        "(depends on ACCDB file version)."); 
                        return;
                    }
                    
                    Properties.Settings.Default.LastConnBase = "";
                    Properties.Settings.Default.LastConnServer = cboBaza.SelectedText;
                    Properties.Settings.Default.LastUsedAccdb = txtChosenDatabaseFile.Text;
                    Properties.Settings.Default.Save();
                    cboBaza.Items.Add("Using Microsoft Access database file. Click \"Login\".");
                    cboBaza.SelectedIndex = 0;
                    cboBaza.Enabled = false;
                }
                else
                {
                    MessageBox.Show("You must choose Microsoft Access database file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else
            {
                if (cboServer.Text == string.Empty)
                {
                    MessageBox.Show("Izaberite server na koji se spajate!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (ProvjeriPostavke(cboServer.Text) == false)
                {
                    MessageBox.Show("U postavkama nije upisan korisnik!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                try
                {
                    DodajBaze(cboServer.Text);
                    cboServer.Enabled = false;
                    btnSpojiSe.Enabled = false;
                    btnOdspoji.Enabled = true;

                    cboBaza.Enabled = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void DodajBaze(object text)
        {
            try
            {
                cboBaza.Enabled = false;
                cboBaza.Items.Clear();

                UpdateData(false);

                if (LoginClass.BuildConnectionString() == true)
                {
                    _connString = LoginClass.ConnString;

                    if (LoginClass.PostojiObjektUBazi("LoginPostavke", "Tabela"))
                    {
                        LoginClass.AddDatabases();
                    }
                    else
                    {
                        //ne postoji tablica u master bazi znači da nisu definirane postavke
                        MessageBox.Show("Nisu definirane postavke za korisnika!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    this.Baze = LoginClass.Baze;
                    foreach (cBaza dbs in this.Baze)
                    {
                        cboBaza.Items.Add(dbs.Baza);
                    }

                    if (cboBaza.Items.Count > 0)
                    {
                        cboBaza.SelectedIndex = 0;
                    }

                    cboBaza.Enabled = true;
                    btnPrijava.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message == "The network path was not found")
                {
                    MessageBox.Show("Nije uspjela veza na bazu, provjerite login podatke u postavkama.", ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void UpdateData(bool ado)
        {
            try
            {
                LoginClass.Korisnik = WindowsIdentity.GetCurrent().Name;
                LoginClass.NazivBaze = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOdspoji_Click(object sender, EventArgs e)
        {
            try
            {
                //moram iz postavki uzeti sve servere koji su odabrani
                PostaviKontrole(false);

                this.Servers = LoginClass.Servers;

                foreach (cServer Server in this.Servers)
                {
                    if (Server.Prikazi)
                    {
                        cboServer.Items.Add(Server.Server);
                    }
                }
                if (cboServer.Items.Count > 0)
                {
                    cboServer.SelectedIndex = 0;
                }
                cboServer.Items.Add("");
                cboServer.Items.Add("Microsoft Access Database file (*.accdb)");
                btnSpojiSe.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnPrijava_Click(object sender, EventArgs e)
        {
            try
            {
                if (oleDbSelected)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    LoginClass.BuildConnectionString();
                    if (cboBaza.Text == "")
                    {
                        MessageBox.Show("Izaberite bazu na koju se spajate!", "Login", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    else
                    {
                        SnimiPostavke();
                        //frmMultiset ms = new frmMultiset();
                        //ms.ConnectionString = _connString;
                        //ms.Show();
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cboServer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboServer.Text == "Microsoft Access Database file (*.accdb)")
            {
                txtChosenDatabaseFile.Visible = true;
                btnChooseFile.Visible = true;
                btnPostavke.Enabled = false;
                btnPrijava.Enabled = false;
                oleDbSelected = true;
                btnSpojiSe.Text = "Test connection";
            }
            else
            {
                cboBaza.Enabled = true;
                cboBaza.Items.Clear();
                txtChosenDatabaseFile.Visible = false;
                btnChooseFile.Visible = false;
                btnPostavke.Enabled = true;
                btnPrijava.Enabled = false;
                oleDbSelected = false;
                btnSpojiSe.Text = "Connect";
            }
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            ofd.Filter = "Access Db (2007/10/13/16)|*.accdb";
            ofd.InitialDirectory = Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "Database");
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                cboBaza.Items.Clear();
                txtChosenDatabaseFile.Text = ofd.FileName;
                Properties.Settings.Default.LastUsedAccdb = ofd.FileName;
                Properties.Settings.Default.Save();
                btnPrijava.Enabled = false;
                btnSpojiSe.Select();
            }
        }
    }
}

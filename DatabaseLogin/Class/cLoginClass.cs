using SqlServerHelper;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace DatabaseLogin.Class
{
    class cLoginClass
    {
        const int PROFILE_CONFIG = 0;
        SqlConnectionStringBuilder sqlBuilder = new SqlConnectionStringBuilder();
        //ADODB.Connection adoBuilder = new ADODB.Connection();
        string mSQL;
        public string xmlFilePath;
        DbUtil db = new DbUtil();
        SqlDataReader Reader;

        private Collection<cServer> _servers = new Collection<cServer>();
        private Collection<cBaza> _baze = new Collection<cBaza>();
        private string _nazivBaze;
        private string _connString;
        private string _nazivServera;
        private string _user;
        private string _pass { get; set; }
        private bool _prikazi;
        private string _korisnik;

        public string NazivBaze
        {
            get
            {
                return _nazivBaze;
            }
            set
            {
                _nazivBaze = value;
            }
        }
        public string ConnString
        {
            get
            {
                return _connString;
            }
            set
            {
                _connString = value;
            }
        }
        public string NazivServera
        {
            get
            {
                return _nazivServera;
            }
            set
            {
                _nazivServera = value;
            }
        }
        public string User
        {
            get
            {
                return _user;
            }
            set
            {
                _user = value;
            }
        }
        public string Pass
        {
            get
            {
                return _pass;
            }
            set
            {
                _pass = value;
            }
        }
        public bool Prikazi
        {
            get
            {
                return _prikazi;
            }
            set
            {
                _prikazi = value;
            }
        }
        public string Korisnik
        {
            get
            {
                return _korisnik;
            }
            set
            {
                _korisnik = value;
            }
        }
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

        public void OcistiKontrole()
        {
            _nazivBaze = "";
            _connString = "";
        }

        public void OtvoriPostavkeZaServera(string server)
        {
            try
            {
                foreach (cServer srv in Servers)
                {
                    if (srv.Server == server)
                    {
                        //ako je to odabrani server popuni varijable
                        _nazivServera = srv.Server;
                        _user = srv.User;
                        _pass = srv.Psw;
                        _prikazi = srv.Prikazi;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        

        public bool BuildConnectionString()
        {
            try
            {
                sqlBuilder.ConnectTimeout = 15;

                //SQL user

                sqlBuilder["Integrated Security"] = false;
                sqlBuilder["User ID"] = _user;
                _pass = "medial00";
                //MessageBox.Show(_pass);
                if (cCryption.IsBase64String(_pass) && _pass.Length > 15)
                {
                    
                    sqlBuilder["Password"] = cCryption.DecryptStringAES(_pass, "darko000");
                }
                else
                {
                    //MessageBox.Show(_pass + " Test");
                    sqlBuilder["Password"] = _pass;
                }
                //MessageBox.Show("Tests");
                if (_nazivBaze != string.Empty)
                {
                    sqlBuilder["Initial Catalog"] = _nazivBaze;
                }
                //TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                sqlBuilder["Data Source"] = _nazivServera;
                sqlBuilder["Persist Security Info"] = true;
                //sqlBuilder["TrustServerCertificate"] = false;
                //sqlBuilder["ApplicationIntent"] = "ReadWrite";
                //sqlBuilder["MultiSubnetFailover"] = false;

                _connString = sqlBuilder.ConnectionString;

                if (_connString != string.Empty)
                {
                    return true;
                }
                else
                {
                    return false;
                }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PostojiObjektUBazi(string ImeObjekta, string TipObjekta)
        {
            try
            {
                string sVrstaObjekta;
                sVrstaObjekta = TipObjekta.ToUpper();

                if (sVrstaObjekta != "TABELA" && sVrstaObjekta != "VIEW" && sVrstaObjekta != "PROCEDURA")
                {
                    return false;
                }

                if (sVrstaObjekta == "TABELA")
                {
                    sVrstaObjekta = "IsUserTable";
                }
                else if (sVrstaObjekta == "VIEW")
                {
                    sVrstaObjekta = "IsView";
                }
                else if (sVrstaObjekta == "PROCEDURA")
                {
                    sVrstaObjekta = "IsProcedure";
                }

                mSQL = "select * from dbo.sysobjects where id = object_id(N'[dbo].[" + ImeObjekta + "]') " +
                       " and OBJECTPROPERTY(id, N'" + sVrstaObjekta + "') = 1";

                db.OpenConnection(_connString);
                Reader = db.GetSqlDataReader(mSQL);

                if (Reader.HasRows == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void AddDatabases()
        {
            try
            {
                //uzima samo baze koje je korisnik odabrao u postavkama
                //postavke su spremljene u master bazi

                mSQL = " SELECT Baza " +
                       " FROM LoginPostavke " +
                       " WHERE [User]='" + _korisnik + "'" +
                       " AND [Server]='" + _nazivServera + "'" +
                       " AND isnull(Status,0)=1" +
                       " order by Baza asc";

                db.OpenConnection(_connString);
                Reader = db.GetSqlDataReader(mSQL);

                this.Baze.Clear();

                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        cBaza b = new cBaza(Reader.GetString(0));
                        Baze.Add(b);
                    }
                }

                db.CloseConnection();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string ObjectToXml<T>(T objectToSerialise)
        {
            StringWriter Output = new StringWriter(new StringBuilder());
            XmlSerializer xs = new XmlSerializer(objectToSerialise.GetType());
            //XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            //ns.Add("LoginData", "urn:LoginData"); // add as many or few as you need
            xs.Serialize(Output, objectToSerialise);
            return Output.ToString();
        }

        public void SaveAll()
        {
            try
            {
                ObjectToXml(Servers);
                var xmlDocData = ObjectToXml(Servers);
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlDocData);
                xmlFilePath = Path.Combine(Path.GetDirectoryName(System.Windows.Forms.Application.UserAppDataPath), "servers.xml");
                doc.Save(xmlFilePath);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void CreateTablePostavke()
        {
            try
            {
                if (PostojiObjektUBazi("LoginPostavke", "TABELA") == false)
                {
                    mSQL = " CREATE TABLE [dbo].[LoginPostavke] ( " + "\n" +
                           "           [ID] [int] IDENTITY (1, 1) NOT NULL , " + "\n" +
                           "	        [User] [varchar] (200) COLLATE Croatian_CI_AS NULL , " + "\n" +
                           "	        [Server] [varchar] (200) COLLATE Croatian_CI_AS NULL,  " + "\n" +
                           "           [Baza] [varchar] (200) COLLATE Croatian_CI_AS NOT NULL , " + "\n" +
                           "           [Status] [tinyint] NOT NULL " + "\n" +
                           ") ON [PRIMARY] ";

                    db.OpenConnection(_connString);
                    db.ExecSql(mSQL);
                    db.CloseConnection();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

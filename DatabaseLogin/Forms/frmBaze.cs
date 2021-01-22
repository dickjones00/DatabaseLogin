using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using SqlServerHelper;
using System.Data.SqlClient;
using DatabaseLogin.Class;

namespace DatabaseLogin.Forms
{
    public partial class frmBaze : Form
    {
        private string _nazivServera;
        private string _user;
        private Collection<cBaza> _baze = new Collection<cBaza>();

        private string mSQL;
        DbUtil db = new DbUtil();
        private SqlDataReader SQLReader;
        private string _connString;

        cLoginClass LoginClass = new cLoginClass();

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

        public frmBaze()
        {
            InitializeComponent();
        }

        private void NapraviKoloneGrida()
        {
            try
            {
                //za baze
                DataGridViewCellStyle columnHeaderStyle = new DataGridViewCellStyle();
                DataGridViewCheckBoxColumn column = new DataGridViewCheckBoxColumn();

                columnHeaderStyle.Font = new Font("Microsoft Sans Serif", 8, FontStyle.Bold);
                vsBaze.ColumnHeadersDefaultCellStyle = columnHeaderStyle;
                vsBaze.AutoResizeColumnHeadersHeight();

                vsBaze.ColumnCount = 1;
                vsBaze.ColumnHeadersVisible = true;

                vsBaze.Columns[0].Name = "Baza";
                vsBaze.Columns[0].HeaderText = "Baza";
                vsBaze.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                column.HeaderText = "Prikaži";
                column.Name = "Prikazi";
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                column.FlatStyle = FlatStyle.Standard;
                column.CellTemplate = new DataGridViewCheckBoxCell();
                column.CellTemplate.Style.BackColor = Color.Beige;
                column.ReadOnly = false;
                vsBaze.Columns.Insert(1, column);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OsvjeziGrid()
        {
            try
            {
                int row = 0;

                mSQL =
                    " SELECT Name as Baza,0 as Status " +
                        " FROM sysdatabases " +
                        " where name <>'master' and Name <>'tempdb' " +
                        " and Name<>'model' and Name<>'msdb' " +
                        " and Name<>'pubs' and name<>'Northwind' " +
                        " and DATABASEPROPERTYEX(Name,'Status')='ONLINE'" +
                        " order by Name ";
                //MessageBox.Show("testc");
                db.OpenConnection(_connString);
                SQLReader = db.GetSqlDataReader(mSQL);
                //MessageBox.Show("testdc");

                while (SQLReader.Read())
                {
                    if (SQLReader.HasRows == true)
                    {
                        vsBaze.Rows.Add();
                        vsBaze.Rows[row].Cells["Baza"].Value = SQLReader["Baza"];

                        //provjeriti da li postoji postavka u bazi
                        int status = 0;
                        vsBaze.Rows[row].Cells["Prikazi"].Value = GetStatus(Convert.ToString(SQLReader["Baza"]), ref status);//SQLReader["Status"];

                        row += 1;
                    }
                }
            }
            catch (SqlException sqlEX)
            {
                MessageBox.Show(sqlEX.Message, sqlEX.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           finally
           {
               db.CloseConnection();
           }
       }

        private int GetStatus(string baza,ref int status)
        {
            try
            {
                SqlDataReader SQLRd;

                LoginClass.ConnString = _connString;
                if (LoginClass.PostojiObjektUBazi("LoginPostavke", "Tabela"))
                {
                    //ako postoji ta tablica već imaju neke postavke upisane

                    mSQL = " Select Baza,Status  from LoginPostavke where [User]='" + _user + "' and Server='" + _nazivServera + "' and Baza='" + baza + "'";
                    db.OpenConnection(_connString);
                    SQLRd = db.GetSqlDataReader(mSQL);

                    while (SQLRd.Read())
                    {
                        if (SQLRd.HasRows == true)
                        {
                            status = Convert.ToInt32(SQLRd["Status"]);
                        }
                    }
                   // SQLRd.Close();
                }

                return status;
            }
            catch (SqlException sqlEX)
            {
                MessageBox.Show(sqlEX.Message, sqlEX.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return 0;
            }
            finally
            {
               // db.CloseConnection();
            }
        }

        private void Spremi()
        {
            try
            {
                foreach (DataGridViewRow dtRow in vsBaze.Rows)
                {

                    cBaza cBz = new cBaza
                                (
                                    Convert.ToString(dtRow.Cells["Baza"].Value)
                                    , Convert.ToInt32(dtRow.Cells["Prikazi"].Value)
                                    , this.NazivServera
                                );

                    this.Baze.Add(cBz);
                }

                SpremiUBazu();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SpremiUBazu()
        {
            try
            {
                //ako se taj server prikazuje i ako se baze ne uzimaju iz procedure onda ću spremiti njegove postavke za bazu
                //spremaju se odabrane baze u tablicu na serveru na master bazi

                //brišem trenutne postavke za korisnika i dodajem nove
                //moram orvo provjeriti da li mi postoji ta tablica

                LoginClass.ConnString = _connString;
                if (LoginClass.PostojiObjektUBazi("LoginPostavke", "TABELA") == false)
                {
                    LoginClass.CreateTablePostavke();
                }

                mSQL = " delete from LoginPostavke where [User]='" + _user + "' and Server = '" + _nazivServera + "' ";

                db.OpenConnection(_connString);
                db.ExecSql(mSQL);

                foreach (cBaza baza in this.Baze)
                {
                    mSQL =
                        " insert into LoginPostavke " +
                                        "([User],Server,Baza,Status) " +
                        " values ('" + _user + "','" + baza.Server + "','" + baza.Baza + "'," + baza.Status + " )";

                    db.ExecSql(mSQL);
                }

                db.CloseConnection();
            }
            catch (SqlException sqlEX)
            {
                MessageBox.Show(sqlEX.Message, sqlEX.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void vsBaze_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0)
                {
                    if (vsBaze.Columns[e.ColumnIndex].Name == "Prikazi")
                    {
                        if (Convert.ToBoolean(vsBaze.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true)
                        {
                            vsBaze.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = false;
                        }
                        else
                        {
                            vsBaze.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = true;
                        }
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
            Spremi();
            this.Close();
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmBaze_Load(object sender, EventArgs e)
        {
            try
            {
                label1.Text += " " + NazivServera.ToString();
                NapraviKoloneGrida();
                OsvjeziGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ex.Source, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

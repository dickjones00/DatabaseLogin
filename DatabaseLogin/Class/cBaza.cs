using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DatabaseLogin.Class
{
    public class cBaza
    {
        private string _baza;
        private int _status;
        private string _server;

        public string Baza
        {
            get
            {
                return _baza;
            }

            set
            {
                _baza = value;
            }
        }

        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        public string Server
        {
            get
            {
                return _server;
            }
            set
            {
                _server = value;
            }
        }

        public cBaza(string baza)
        {
            this.Baza = baza;
        }

        public cBaza(string baza, int status, string server)
        {
            this.Baza = baza;
            this.Status = status;
            this.Server = server;
        }
    }
}

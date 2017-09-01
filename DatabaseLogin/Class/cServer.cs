using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace DatabaseLogin.Class
{
    public class cServer
    {
        public cServer() { }
        private string _server;
        private string _user;
        private string _psw;
        private bool _prikazi;
        [XmlElement("Server")]
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
        [XmlElement("User")]
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
        [XmlElement("Psw")]
        public string Psw
        {
            get
            {
                return _psw;
            }
            set
            {
                _psw = value;
            }
        }
        [XmlElement("Prikazi")]
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

        public cServer(string server)
        {
            this.Server = server;
        }

        public cServer(string server, string user, string psw, bool prikazi)
        {
            this.Server = server;
            this.User = user;
            this.Psw = psw;
            this.Prikazi = prikazi;
        }
    }
}

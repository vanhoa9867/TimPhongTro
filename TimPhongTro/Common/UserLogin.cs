using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TimPhongTro.Common
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Ten { set; get; }
    }
    public class AdminLogin
    {
        public int UserID { set; get; }
        public string UserName { set; get; }
        public string Ten { set; get; }
    }
}
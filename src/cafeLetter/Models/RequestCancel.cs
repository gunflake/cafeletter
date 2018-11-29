using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cafeLetter.Models
{
    public class RequestCancel
    {
        public string pgcode { get; set; }
        public string client_id { get; set; }
        public string user_id { get; set; }
        public string tid     { get; set; }
        public int    amount { get; set; }
        public string ip_addr { get; set; }
    }
}
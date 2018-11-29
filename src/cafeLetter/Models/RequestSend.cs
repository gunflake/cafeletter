using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cafeLetter.Models
{
    public class RequestSend
    {
        public string pgcode           { get; set; }
        public string client_id        { get; set; }
        public string service_name     { get; set; }
        public string user_id          { get; set; }
        public string user_name        { get; set; }
        public string order_no         { get; set; }
        public int    amount           { get; set; }
        public string product_name     { get; set; }
        public string email_flag       { get; set; }
        public string email_addr       { get; set; }
        public string autopay_flag     { get; set; }
        public string receipt_flag     { get; set; }
        public string keyin_flag       { get; set; }
        public string custom_parameter { get; set; }
        public string return_url       { get; set; }
        public string callback_url     { get; set; }
        public string cancel_url       { get; set; }
        public string inapp_flag       { get; set; }
        public string app_return_url   { get; set; }
        public string app_cancel_url   { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Net;

namespace cafeLetter.Cash
{
    /// <summary>
    /// CashHandler의 요약 설명입니다.
    /// </summary>
    public class CashHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        [WebMethod]
        public void printHello()
        {
            
        }
    }
}
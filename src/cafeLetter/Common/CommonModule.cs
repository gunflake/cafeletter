using BOQv7Das_Net;
using System;
using System.Data;
using System.Web;
using System.Web.UI;

namespace cafeLetter
{
    public class CommonModule : System.Web.UI.Page
    {
     

        //ErrMsg 출력
        protected void PrintMsg(string errMsg)
        {
            Response.Write(@"<script>alert('" + errMsg + "');</script>");
        }

        protected void PrintMsg(string errMsg, string redirectURL)
        {
            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert("+errMsg+"); location.href="+redirectURL+";</script>; ");
        }

        protected IDas ConnectionIDas()
        {

            IDas pl_objDas = null;

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas = new IDas();
                pl_objDas.Open("10.10.120.20:33444");
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;
            }
            catch
            {
                pl_objDas = null;
            }

            return pl_objDas;
        }




    }
}

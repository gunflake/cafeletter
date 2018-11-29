using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Cash
{
    public partial class CashComplete : System.Web.UI.Page
    {
        private int intPayAmount = 0;
        private string strPayTool = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            PaymentSucessView();
            Response.Write("<script>");
            Response.Write("window.opener.location.href ='/Cash/CashResult.aspx?intPayAmount="+ intPayAmount+ "&strPayTool="+ strPayTool + "';");
            Response.Write("window.close();");
            Response.Write("</script>");
        }

        private void PaymentSucessView()
        {
            string pl_strUserName = Request.Form["user_name"];
            string pl_strUserID = Request.Form["user_id"];
            string pl_strOrder_no = Request.Form["order_no"];
            string pl_strAmount = Request.Form["amount"];
            string pl_strproduct_name = Request.Form["product_name"];
            string pl_strtransaction_date = Request.Form["transaction_date"];
            string pl_strPGCode = Request.Form["pgcode"];

            intPayAmount = Convert.ToInt32(pl_strAmount);

            Console.Write(pl_strAmount);
            Console.Write(pl_strPGCode);
            Response.Write(pl_strAmount);
            Response.Write(pl_strPGCode);

            if (pl_strPGCode != null && pl_strPGCode.Equals("mobile"))
            {
                strPayTool = "핸드폰";
            }
            else if (pl_strPGCode != null && pl_strPGCode.Equals("creditcard"))
            {
                strPayTool = "신용카드";
            }

        }
    }
}
using BOQv7Das_Net;
using cafeLetter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Cash
{
    public partial class CashResult : System.Web.UI.Page
    {

        CommonModule objModule = new CommonModule();
        protected string intPayAmount = string.Empty;
        protected string intMyCash = string.Empty;
        protected string strPayTool = string.Empty;
        private string strUserID = string.Empty;

        //권한 체크
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                objModule.saveSession("beforeURL", "/Item/ItemList.aspx");
                objModule.PrintAlert("로그인이 필요합니다", "/Member/Login.aspx");
                return;
            }
            strUserID = objModule.getSession("userID");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            strPayTool = Request.Params["strPayTool"];
            intPayAmount = Request.Params["intPayAmount"] != null  ? Convert.ToInt32(Request.Params["intPayAmount"]).ToString("#,##0") : "0";
            MyCashDB();
            //ResultPaymentCheck();
            //PaymentSucessView();
            strPayTool = objModule.getSession("payTool");
            if (strPayTool != null && strPayTool.Equals("mobile"))
            {
                strPayTool = "핸드폰";
            }
            else if (strPayTool != null && strPayTool.Equals("creditcard"))
            {
                strPayTool = "신용카드";
            }
        }

        private void MyCashDB()
        {
            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intMyCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRealCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intBonusCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_MY_NT_GET");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    intMyCash = pl_objDas.GetParam("@po_intMyCash");
                    return;
                }
                else
                {
                    objModule.PrintAlert(pl_strOutputMsg, "/Item/ItemList.aspx");
                    return;
                }

            }
            catch
            {

            }
            finally
            {
                if (pl_objDas != null)
                {
                    pl_objDas.Close();
                    pl_objDas = null;
                }
            }
        }

        private void ResultPaymentCheck()
        {

         //   Thread.Sleep(1000);

            Response.Write("<script>");
            Response.Write("window.opener.location.href ='/Home.aspx';");
            Response.Write("</script>");

            /*
            if (objModule.getSession("Noti") != null && objModule.getSession("Noti").Equals("success"))
            {
                PaymentSucessView();
            }
            else
            {
                PaymentFailView();
            }

            Session.Remove("Noti");
            */

        }

        private void PaymentFailView()
        {

        } 

        private void PaymentSucessView()
        {
            string pl_strUserName         = Request.Form["user_name"];
            string pl_strUserID           = Request.Form["user_id"];
            string pl_strOrder_no         = Request.Form["order_no"];
            string pl_strAmount           = Request.Form["amount"];
            string pl_strproduct_name     = Request.Form["product_name"];
            string pl_strtransaction_date = Request.Form["transaction_date"];
            string pl_strPGCode           = Request.Form["pgcode"];

            intPayAmount = pl_strAmount;

            Console.Write(pl_strAmount);
            Console.Write(pl_strPGCode);
            Response.Write(pl_strAmount);
            Response.Write(pl_strPGCode);

            if (pl_strPGCode!=null && pl_strPGCode.Equals("mobile"))
            {
                strPayTool = "핸드폰";
            }else if (pl_strPGCode != null && pl_strPGCode.Equals("creditcard"))
            {
                strPayTool = "신용카드";
            }

        }

        protected void MyInfoCash_Click(object sender, EventArgs e)
        {
            objModule.moveURL("/Cash/MyCashInfo.aspx");
        }
    }
}
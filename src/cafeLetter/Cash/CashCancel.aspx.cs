using BOQv7Das_Net;
using cafeLetter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.IO;
using System.Net;

namespace cafeLetter.Cash
{
    public partial class CashCancel : System.Web.UI.Page
    {
        private CommonModule objModule = new CommonModule();
        private RequestCancel objCancel = new RequestCancel();
        private string strUserID = string.Empty;
        private string strPGName = string.Empty;
        private string strTID    = string.Empty;
        private string strCID    = string.Empty;
        private int    intAmount = 0; 

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (objModule.getSession("userID") == null)
            {
                objModule.PrintAlert("잘못된 접근입니다", "/Member/Home.aspx");
                return;
            }
            strUserID = objModule.getSession("userID");



            if (Request.Params["strPGName"] == null || Request.Params["strTID"] == null || Request.Params["strAmount"] == null)
            {
                objModule.PrintAlert("잘못된 접근입니다", "/Member/Home.aspx");
                return;
            }

            strPGName = Request.Params["strPGName"].ToString();
            strTID    = Request.Params["strTID"].ToString();
            intAmount = Convert.ToInt32(Request.Params["strAmount"]);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!PaymentCancel())
            {
                objModule.PrintAlert("결제취소에 실패했습니다", "/Cash/MyCashInfo.aspx");
                return;
            }
            
            CashCancelDB();
        }

        private bool PaymentCancel()
        {
            bool cancelFlag = false;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://testpgapi.payletter.com/v1.0/payments/cancel");
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "PLKEY NUQyQ0RGNTBEODQ5NTg4OTc1MEQyNTdCRjY5NTRFNjg=");
            httpWebRequest.ContentType = "application/json";

            objCancel.pgcode = strPGName;
            objCancel.user_id = strUserID;
            objCancel.tid = strTID;
            objCancel.client_id = "testintern";
            objCancel.amount = intAmount;
            objCancel.ip_addr = "127.0.0.1";

            string json = JsonConvert.SerializeObject(objCancel);


            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                var jsonParse = JObject.Parse(result);

                //응답 성공시
                if (jsonParse["tid"] != null && jsonParse["cid"] != null && jsonParse["amount"] != null && jsonParse["cancel_date"] != null)
                {
                    cancelFlag = true;
                    strCID = jsonParse["cid"].ToString();
                    //intAmount = Convert.ToInt32(jsonParse["amount"].ToString());
                    //Response.Write(jsonParse["tid"].ToString()+"  " + jsonParse["cid"].ToString() + "   " + jsonParse["amount"].ToString() + "          " + jsonParse["cancel_date"].ToString());
                }

                //응답 실패시
                else
                {
                    cancelFlag = false;
                    //Response.Write(jsonParse["code"].ToString() + "  " + jsonParse["message"].ToString());
                }
            }

            return cancelFlag;
        }

        //결제 취소 DB
        private void CashCancelDB()
        {
            //[UP_CASH_CANCEL_TX_UPD] 만들었음... 디비 수정 코드 작성하면됨
            IDas pl_objDas = null;

            try
            {

                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strCashNo", DBType.adVarChar, "", 30, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTID", DBType.adVarChar, strTID, 50, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strCID", DBType.adVarChar, strCID, 50, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPayCashAmt", DBType.adInteger, intAmount, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);

                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_TX_CNL");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    objModule.PrintAlert("결제가 취소되었습니다", "/Cash/MyCashInfo.aspx");
                    return;
                }
                else
                {
                    //PG대사 불일치 INESRT
                    objModule.PrintAlert("결제가 취소되었습니다", "/Cash/MyCashInfo.aspx");
                    return;
                }
            }
            catch
            {
                //PG대사 불일치 INESRT
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
    }
}
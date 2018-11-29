using BOQv7Das_Net;
using cafeLetter.Models;
using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;

namespace cafeLetter.Cash
{
    public partial class CashCallback : System.Web.UI.Page
    {
        RequestReceive objReceive = null;
        JsonResult objResult = null;
        string strResult = string.Empty;
        CommonModule module = new CommonModule();

        protected void Page_Load(object sender, EventArgs e)
        {
            //receive object crreate and get Parameter
            objReceive = new RequestReceive();

            try
            {
                GetRecieveInfo();

            }
            catch
            {
                Response.Write("{\"code\":" + 1 + ", " + "\"message\":\"" + "data parse fail" + "\"}");
                return;
            }
            //result Josn
            objResult = new JsonResult();






            if (objReceive.code == 1)
            {
                objResult.code = objReceive.code;
                objResult.message = "Cash Charge Result Fail";
                strResult = JsonConvert.SerializeObject(objResult);
                Response.Write(strResult);
                return;
            }


            //DB_Insert
            if (CashInsertDB())
            {
                objResult.code = 0;
                objResult.message = "cash charge sucess";
            }

            else
            {
                objResult.code = 1;
                objResult.message = "cash charge fail";
            }

            strResult = JsonConvert.SerializeObject(objResult);
            Response.Write(strResult);

        }

        private bool CashInsertDB()
        {
            IDas pl_objDas = null;

            try
            {

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, objReceive.user_id, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPGName", DBType.adVarWChar, objReceive.pgcode, 30, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPayCashAmt", DBType.adInteger, objReceive.amount, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strOrderNo", DBType.adVarChar, objReceive.order_no, 50, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTID", DBType.adVarChar, objReceive.tid, 50, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_strCID", DBType.adVarChar, objReceive.cid, 50, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    return true;
                }
                else
                {
                    return false; ;
                }
            }
            catch
            {
                return false;
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

        private void GetRecieveInfo()
        {
            string resultJson = string.Empty;
            resultJson = new StreamReader(Request.InputStream).ReadToEnd();
            objReceive = JsonConvert.DeserializeObject<RequestReceive>(resultJson);

            //Response.Write("{\"code\":" + objReceive.code + ", " + "\"message\":\"" + objReceive.message + "\"}");
            //Response.Write("{\"amout\":" + objReceive.amount + ", " + "\"pgcode\":\"" + objReceive.pgcode + "\"}");

            //Response.Write(objReceive.amount);
        }
    }

    class JsonResult
    {
        public int code { get; set; }
        public string message { get; set; }
    }

}
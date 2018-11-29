using BOQv7Das_Net;
using cafeLetter.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Cash
{
    public partial class CashCharge : System.Web.UI.Page
    {
        private int intCashAmt = 0;
        private string strUserID = string.Empty;
        private string strUserName = string.Empty;
        private string strPaytoolName = string.Empty;
        private CommonModule objModule = new CommonModule();
        private RequestSend sendData = null;
        
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (objModule.getSession("userID") == null)
            {
                objModule.saveSession("beforeURL", "/Cash/CashCharge.aspx");
                objModule.PrintAlert("로그인이 필요합니다", "/Member/Login.aspx");
                return;
            }
            strUserID = objModule.getSession("userID");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cashBtn_Click(object sender, EventArgs e)
        {

            if (Cash5000.Checked)
            {
                intCashAmt = 5000;
            }
            else if (Cash10000.Checked)
            {
                intCashAmt = 10000;
            }
            else if (Cash20000.Checked)
            {
                intCashAmt = 20000;
            }
            else
            {
                objModule.PrintAlert("금액을 선택해주세요");
                return;
            }

            if (Mobile.Checked)
            {
                strPaytoolName = "mobile";
            }else if (CreditCard.Checked)
            {
                strPaytoolName = "creditcard";
            }
            else
            {
                objModule.PrintAlert("결제수단을 선택해주세요");
                return;
            }

            //send data 설정
            sendData = new RequestSend();

            //get user info
            GetUserInfoDB();

            //set orderNo
            SetOrderNoDB();

            PaymentRequest();

            
            
        }

        private void SetOrderNoDB()
        {
            IDas pl_objDas = null;
            string pl_intRetKey = string.Empty;

            try
            {
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@po_intKey", DBType.adBigInt, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_KEY_NT_GEN");

                pl_intRetKey = pl_objDas.GetParam("@po_intKey");

                if (pl_intRetKey.Equals("0"))
                {
                    objModule.PrintAlert("주문번호 생성에 실패했습니다", "/Member/MyInfo.aspx");
                    return;
                }

                sendData.order_no = pl_intRetKey;
            }

            catch
            {
                objModule.PrintAlert("유니크 키 생성 오류");
                return;
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

        private void GetUserInfoDB()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;

            try
            {
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERINFO_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    objModule.PrintAlert("회원 정보를 읽을 수 없습니다", "/Member/MyInfo.aspx");
                    return;
                }

                if(pl_objDas.objDT.Rows[0]["USERNAME"] == null)
                {
                    objModule.PrintAlert("이름작성이 필요합니다", "/Member/MyInfo.aspx");
                    return;
                }

                sendData.user_name = pl_objDas.objDT.Rows[0]["USERNAME"].ToString();
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

        private void PaymentRequest()
        {
            int orderNum = new Random().Next(10000000);


            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://testpgapi.payletter.com/v1.0/payments/request");
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "PLKEY NUQyQ0RGNTBEODQ5NTg4OTc1MEQyNTdCRjY5NTRFNjg=");
            httpWebRequest.ContentType = "application/json";

            sendData.pgcode = strPaytoolName;
            sendData.user_id = strUserID;
            sendData.service_name = "카페레터";
            sendData.client_id = "testintern";
            sendData.amount = intCashAmt;
            sendData.product_name = "원두 충전";
            sendData.autopay_flag = "N";
            sendData.return_url = "http://hmnam.cafeletter.com/Cash/CashComplete.aspx";
            sendData.callback_url = "http://hmnam.cafeletter.com/Cash/CashCallback.aspx";
            sendData.cancel_url = "http://hmnam.cafeletter.com/Cash/CashWindowClose.aspx";

            string json = JsonConvert.SerializeObject(sendData);


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
                if (jsonParse["code"] == null && jsonParse["token"] != null)
                {
                    objModule.saveSession("payTool", strPaytoolName);
                    string online_url = jsonParse["online_url"].ToString();
                    Response.Write("<script>");
                    Response.Write("window.open('" + online_url + "','payment', 'width=700, height=700, toolbar=no, location=no,  resizable=no' );");
                    Response.Write("window.focus();");
                    Response.Write("</script>");
                }

                //응답 실패시
                else
                {
                    string strErrMsg = jsonParse["message"].ToString();
                    objModule.PrintAlert("결제 요청 오류\n"+ strErrMsg);
                    return;
                }
            }
        }
    }
}
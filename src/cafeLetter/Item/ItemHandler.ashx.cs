using BOQv7Das_Net;
using cafeLetter.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;

namespace cafeLetter.Item
{
    /// <summary>
    /// ItemHandler의 요약 설명입니다.
    /// </summary>
    public class ItemHandler : IHttpHandler
    {
        CommonModule objModule = new CommonModule();
        RequestParam objReqParam = new RequestParam();
        ResponseParam objResponseParam = new ResponseParam();

        public void ProcessRequest(HttpContext context)
        {
            string strErrMsg = string.Empty;
            string strJsonResult = string.Empty;
            string strReqParam = string.Empty;

            try
            {
                context.Response.ContentType = "text/json";
                context.Response.ContentEncoding = Encoding.UTF8;

                //Json 형태 요청 param Object로 변환
                strReqParam = new StreamReader(context.Request.InputStream).ReadToEnd();

                objReqParam = JsonConvert.DeserializeObject<RequestParam>(strReqParam);


                //Item 구매할 때
                if (objReqParam.strMethod.Equals("BuyItem"))
                {
                    if (PurchaseProcessDB(objReqParam))
                    {
                        objResponseParam.intRetVal = 0;
                        objResponseParam.strResult = "성공";
                    }
                    else
                    {
                    }
                    return;
                }
                //보너스캐시 발행할 때
                else if (objReqParam.strMethod.Equals("BonusCashIssue"))
                {
                    if (BonusCashInsertDB())
                    {
                        objResponseParam.intRetVal = 0;
                        objResponseParam.strResult = "성공";
                    }
                    else
                    {
                        objResponseParam.intRetVal = 1;
                        objResponseParam.strResult = "실패";
                    }
                }
                //보너스캐시 회수할때
                else if (objReqParam.strMethod.Equals("BonusCashCancel"))
                {
                    if (BonusCashCancelDB())
                    {
                        objResponseParam.intRetVal = 0;
                        objResponseParam.strResult = "성공";
                    }
                    else
                    {
                        objResponseParam.intRetVal = 1;
                        objResponseParam.strResult = "실패";
                    }
                }
                else if (objReqParam.strMethod.Equals("MyBasketList"))
                {
                    if (MyBasketListDB())
                    {
                        objResponseParam.intRetVal = 0;
                        objResponseParam.strResult = "성공";
                    }
                    else
                    {
                        objResponseParam.intRetVal = 1;
                        objResponseParam.strResult = "실패";
                    }
                }
                else if (objReqParam.strMethod.Equals("MyBasketItemDelete"))
                {
                    if (DeleteBasketDB())
                    {
                        objResponseParam.intRetVal = 0;
                        objResponseParam.strResult = "성공";
                    }
                    else
                    {
                        objResponseParam.intRetVal = 1;
                        objResponseParam.strResult = "실패";
                    }
                }
                //보너스캐시 엑셀 발행
                else if (objReqParam.strMethod.Equals("BonusCashManyIssue"))
                {
                    if (BonusCashManyInsertDB())
                    {
                        objResponseParam.intRetVal = 0;
                        objResponseParam.strResult = "성공";
                    }
                    else
                    {
                    }
                }
                else
                {
                    strErrMsg = "잘못된 메소드";
                    return;
                }
            }
            catch
            {
                objResponseParam.intRetVal = 1;
                objResponseParam.strResult = "에러";
            }
            finally
            {

                // JSON 결과 리턴
                strJsonResult = JsonConvert.SerializeObject(objResponseParam);
                context.Response.Write(strJsonResult);

                objReqParam = null;
                objResponseParam = null;
            }

        }

        //장바구니 아이템 삭제 DB
        private bool DeleteBasketDB()
        {
            bool result = false;
            IDas pl_objDas = null;
            try
            {
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, objReqParam.strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intItemNo", DBType.adInteger, objReqParam.intItemNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BASKET_TX_DEL");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    result = true;
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
            return result;
        }

        //보너스 캐시 회수 DB
        private bool BonusCashCancelDB()
        {
            bool result = false;

            IDas pl_objDas = null;
            int pl_intAmount = 0;
            try
            {
                pl_intAmount = Convert.ToInt32(objReqParam.strAmount);
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, objReqParam.strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intAmount", DBType.adInteger, pl_intAmount, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_BONUS_TX_USE");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    result = true;
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

            return result;
        }

        //보너스 캐시 발행 DB
        private bool BonusCashInsertDB()
        {
            bool result = false;

            IDas pl_objDas = null;
            int pl_intAmount = 0;
            try
            {
                pl_intAmount = Convert.ToInt32(objReqParam.strAmount);
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, objReqParam.strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPGName", DBType.adVarWChar, "", 30, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPayCashAmt", DBType.adInteger, pl_intAmount, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strOrderNo", DBType.adVarChar, "", 50, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTID", DBType.adVarChar, "", 50, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_strCID", DBType.adVarChar, "", 50, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    result = true;
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

            return result;
        }

        //대량 보너스 캐시 발행 DB
        private bool BonusCashManyInsertDB()
        {
            bool result = false;

            IDas pl_objDas = null;
            try
            {
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, objReqParam.strUserString, 3000, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPayCashAmt", DBType.adVarChar, objReqParam.strAmountString, 3000, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BONUS_ISSUE_EXCEL_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    result = true;
                }
                else
                {
                    objResponseParam = new ResponseParam();
                    objResponseParam.intRetVal = pl_intRetVal;
                    objResponseParam.strResult = pl_strOutputMsg;
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

            return result;
        }

        //장바구니 리스트 출력 DB
        private bool MyBasketListDB()
        {
            bool result = false;

            objResponseParam = null;
            IDas pl_objDas = objModule.ConnetionDB();
            int pl_intTotalPrice = 0;
            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, objReqParam.strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, 1, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, 999, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.SetQuery("dbo.UP_BASKET_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));

                //ListPanel.DataSource = pl_objDas.objDT;
                //ListPanel.DataBind();

                if (pl_intRecordCnt == 0)
                {
                    return false;
                }

                result = true;
                //총 가격 구하기

                for (int i = 0; i < pl_intRecordCnt; i++)
                {
                    pl_intTotalPrice += Convert.ToInt32(pl_objDas.objDT.Rows[i]["ITEMTOTALPRICE"].ToString());
                }

                //Setting 
                objResponseParam = new ResponseParam();
                objResponseParam.objDT = pl_objDas.objDT;
                objResponseParam.intRecordCnt = pl_intRecordCnt;
                objResponseParam.intItemTotalPrice = pl_intTotalPrice;

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

            return result;
        }

        //아이템 물품 구매
        private bool PurchaseProcessDB(RequestParam objReqParam)
        {
            bool result = false;

            IDas pl_objDas = null;
            int pl_intAmount = 0;
            int pl_intShipPrice = 0;
            try
            {
                pl_intAmount = Convert.ToInt32(objReqParam.intAmount);
                pl_intShipPrice = Convert.ToInt32(objReqParam.intShipPrice);
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, objReqParam.strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strName", DBType.adVarWChar, objReqParam.strName, 10, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPhone", DBType.adVarChar, objReqParam.strPhone, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strAddress", DBType.adVarWChar, objReqParam.strAddress, 50, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPostCode", DBType.adVarChar, objReqParam.strPostCode, 6, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_strPurchaseInfo", DBType.adVarWChar, objReqParam.strPurchaseInfo, 500, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strMsg", DBType.adVarWChar, objReqParam.strMsg, 30, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intAmount", DBType.adInteger, pl_intAmount, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intAmount", DBType.adInteger, pl_intShipPrice, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);

                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PURCHASE_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    
                    result = true;
                }
                else
                {
                    objResponseParam.strResult = pl_strOutputMsg;
                    objResponseParam.intRetVal = pl_intRetVal;

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

            return result;
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }

    public class RequestParam
    {
        public string strMethod { get; set; }
        public string strPostCode { get; set; }
        public int intAmount { get; set; }
        public string strMsg { get; set; }
        public string strName { get; set; }
        public string strPhone { get; set; }
        public string strAddress { get; set; }
        public string strUserID { get; set; }
        public string strPurchaseInfo { get; set; }
        public int intShipPrice { get; set; }
        public string strAmount { get; set; }
        public int intItemNo { get; set; }
        public string strUserString { get; set; }
        public string strAmountString { get; set; }
    }

    public class ResponseParam
    {
        public DataTable objDT { get; set; }
        public int intRecordCnt { get; set; }
        public string strResult { get; set; }
        public string strPostCode { get; set; }
        public int intRetVal { get; set; }
        public int intItemTotalPrice { get; set; }
    }
}
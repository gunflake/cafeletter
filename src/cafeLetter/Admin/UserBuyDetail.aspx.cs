using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Admin
{
    public partial class UserBuyDetail : System.Web.UI.Page
    {
        CommonModule objModule = new CommonModule();
        private string strPurchaseNo = string.Empty;
        protected string strUserName = string.Empty;
        protected string strPhone = string.Empty;
        protected string strAddress = string.Empty;
        protected string strMsg = string.Empty;

        protected int intTotalPrice = 0;
        protected int intShipPrice = 0;
        protected int intOrderPrice = 0;


        protected string strPurchaseInfo = string.Empty;
        protected int intPayPrice = 0;
        protected int intPageSize = 999;
        protected int intPageNo = 1;
        public string strUserID = string.Empty;
        protected string strItemCode = "I01";
        protected int intMyCash = 0;
        protected int intPaymentCash = 0;
        protected string strCnlState = string.Empty;

        //권한 체크
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.Params["strPurchaseNo"] == null)
            {
                objModule.PrintAlert("잘못된 접근입니다", "/Home.aspx");
                return;
            }

            strPurchaseNo = Request.Params["strPurchaseNo"];

            if (Session["userID"] == null)
            {
                objModule.saveSession("beforeURL", "/Admin/UserBuyDetail.aspx");
                objModule.PrintAlert("로그인이 필요합니다", "/Member/Login.aspx");
                return;
            }

            if(Request.Params["strUserID"] == null)
            {
                objModule.PrintAlert("잘못된 접근입니다", "/Home.aspx");
                return;
            }
            strUserID = Request.Params["strUserID"];

        }

        protected void Page_Load(object sender, EventArgs e)
        {


            //나의 구매 내역
            MyBuyListDB();





            //나의 구매 상세 리스트
            MyBuyDetailListDB();
        }



        //나의 구매내역
        private void MyBuyListDB()
        {
            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPurchaseNo", DBType.adVarChar, strPurchaseNo, 30, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.SetQuery("dbo.UP_PURCHASE_MY_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));

                if (pl_intRecordCnt == 1)
                {
                    strUserName = pl_objDas.objDT.Rows[0]["RCVNAME"].ToString();
                    strPhone = pl_objDas.objDT.Rows[0]["RCVPHONE"].ToString();
                    strAddress = pl_objDas.objDT.Rows[0]["RCVADDRESS"].ToString();
                    strMsg = pl_objDas.objDT.Rows[0]["RCVMESSAGE"].ToString();
                    intTotalPrice = Convert.ToInt32(pl_objDas.objDT.Rows[0]["TOTALPRICE"].ToString());
                    intShipPrice = Convert.ToInt32(pl_objDas.objDT.Rows[0]["SHIPPRICE"].ToString());
                    intOrderPrice = intTotalPrice - intShipPrice;
                    strCnlState = pl_objDas.objDT.Rows[0]["CNLSTATE"].ToString();
                }
                else
                {
                    objModule.PrintAlert("구매정보가 없습니다", "/Member/BuyList.aspx");
                    return;
                }

            }
            catch
            {
                objModule.PrintAlert("구매정보 조회 오류", "/Member/BuyList.aspx");
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

        //구매상세내역 리스트
        private void MyBuyDetailListDB()
        {

            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기
            string pl_strErrMsg = string.Empty;
            int pl_intRetVal = 0;

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPurchaseNo", DBType.adVarChar, strPurchaseNo, 30, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PURCHASE_DETATIL_NT_GET");

                pl_strErrMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    objModule.PrintAlert(pl_strErrMsg, "/Member/BuyList.aspx");
                    return;
                }

                ListPanel.DataSource = pl_objDas.objDT;
                ListPanel.DataBind();


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

        //나의 정보
        private void MyInfoDB()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
            try
            {
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERINFO_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    objModule.PrintAlert("회원 정보를 읽을 수 없습니다", "/Member/Login.aspx");
                    return;
                }

                strUserName = pl_objDas.objDT.Rows[0]["USERNAME"].ToString();
                strPhone = pl_objDas.objDT.Rows[0]["PHONE"].ToString();
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

        protected void BuyListBtn_Click(object sender, EventArgs e)
        {
            objModule.moveURL("/Member/BuyList.aspx");
        }

        protected void CancelBuyItemBtn_Click(object sender, EventArgs e)
        {
            CancelBuyItemDB();
        }

        //구매취소 DB
        private void CancelBuyItemDB()
        {
            IDas pl_objDas = null;
            try
            {
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPurchaseNo", DBType.adVarChar, strPurchaseNo, 30, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PURCHASE_TX_CNL");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    objModule.PrintAlert("구매가 취소되었습니다.", "/Member/BuyList.aspx");
                    return;
                }
                else
                {
                    objModule.PrintAlert("구매취소에 실패했습니다.");
                    Response.Write(pl_strOutputMsg);
                    return;
                }
            }
            catch
            {
                objModule.PrintAlert("구매취소가 실패했습니다. 잠시 후 다시 시도해주세요", "/Member/BuyList.aspx");
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
    }
}
using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Item
{
    public partial class BuyItem : System.Web.UI.Page
    {
        protected string strPurchaseInfo = string.Empty;
        protected int intPayPrice = 0;
        protected string strUserName = string.Empty;
        protected string strEmail = string.Empty;
        protected string strPhone = string.Empty;

        CommonModule objModule = new CommonModule();
        protected int intPageSize = 999;
        protected int intPageNo = 1;
        public string strUserID = string.Empty;
        protected string strItemCode = "I01";
        protected int intMyCash = 0;
        protected int intPaymentCash = 0;
        protected int intRemainCash = 0;

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
            //징바구니 리스트
            MyBasketListDB();

            //내 보유 캐시
            MyCashDB();

            //나의 정보(구매자 정보)
            MyInfoDB();

            intPaymentCash = intPayPrice + 2500;
            intRemainCash = intMyCash - intPayPrice - 2500;
        }

        //내 보유 캐시
        private void MyCashDB()
        {
            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intMyCash", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRealCash", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intBonusCash", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_MY_NT_GET1");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    intMyCash = Convert.ToInt32(pl_objDas.GetParam("@po_intMyCash"));
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

        //장바구니 리스트
        private void MyBasketListDB()
        {

            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.SetQuery("dbo.UP_BASKET_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));

                

                ListPanel.DataSource = pl_objDas.objDT;
                ListPanel.DataBind();

                
                if (pl_intRecordCnt > 1)
                {
                    strPurchaseInfo = pl_objDas.objDT.Rows[0]["ITEMNAME"].ToString() + " 외 " + (pl_intRecordCnt - 1) + "개";
                }else if(pl_intRecordCnt == 1)
                {
                    strPurchaseInfo = pl_objDas.objDT.Rows[0]["ITEMNAME"].ToString();
                }

                //총 가격 구하기

                for (int i = 0; i < pl_intRecordCnt; i++)
                {
                    intPayPrice += Convert.ToInt32(pl_objDas.objDT.Rows[i]["ITEMTOTALPRICE"].ToString());
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

                strEmail = pl_objDas.objDT.Rows[0]["EMAIL"].ToString();
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


    }
}
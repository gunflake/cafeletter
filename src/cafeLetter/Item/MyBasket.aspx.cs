using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Item
{
    public partial class MyBasket : System.Web.UI.Page
    {
        CommonModule objModule = new CommonModule();
        protected int intPageSize = 999;
        protected int intPageNo = 1;
        private string strUserID = string.Empty;
        protected string strItemCode = "I01";
        protected int intPayPrice = 0;
        protected int intMyCash = 0;

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
            MyBasketListDB();
            MyCashDB();

        }

        //내가 보유한 캐시 조회
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

        //물품 리스트
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

        //물품 삭제
        private void DeleteBasketDB(int intItemNo)
        {
            IDas pl_objDas = null;

            try
            {
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intItemNo", DBType.adInteger, intItemNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BASKET_TX_DEL");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    objModule.PrintAlert("장바구니에서 물품을 삭제했습니다", "/Item/MyBasket.aspx");
                    return;
                }
                else
                {
                    objModule.PrintAlert(pl_strOutputMsg, "/Item/MyBasket.aspx");
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

        protected void DeleteBasket_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            if (b != null)
            {
                RepeaterItem ri = b.Parent as RepeaterItem;
                if (ri != null)
                {
                    //Fetch data
                    Literal ItemNo = ri.FindControl("ItemNo") as Literal;

                    int pl_intItemNo = Convert.ToInt32(ItemNo.Text);

                    DeleteBasketDB(pl_intItemNo);

                }
            }
        }

        protected void BuyItem_Click(object sender, EventArgs e)
        {
            objModule.moveURL("/Item/BuyItem.aspx");
        }
    }
}
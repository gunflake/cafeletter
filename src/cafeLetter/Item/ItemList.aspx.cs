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
    public partial class ItemList : System.Web.UI.Page
    {
        public CommonModule module = new CommonModule();
        protected int intPageSize = 999;
        protected int intPageNo = 1;
        protected string strUserID = string.Empty;
        protected string strItemCode = "I01";

        //권한 체크
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                strUserID = module.getSession("userID");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            //ItemCodeType
            if (Request.Params["strItemCode"] != null)
            {
                strItemCode = Request.Params["strItemCode"];
            }

            ItemListDB();
        }

        //물품 리스트
        private void ItemListDB()
        {

            IDas pl_objDas = module.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_intItemNo", DBType.adInteger, 0, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strItemCode", DBType.adVarChar, strItemCode, 3, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchQuery", DBType.adVarWChar, "", 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intSearchFlag", DBType.adInteger, "", 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.SetQuery("dbo.UP_ITEM_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));


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

        protected void AddBasket_Click(object sender, EventArgs e)
        {

            if (Session["userID"] == null)
            {
                module.saveSession("beforeURL", "/Item/ItemList.aspx");
                module.PrintAlert("로그인이 필요합니다", "/Member/Login.aspx");
                return;
            }
            strUserID = module.getSession("userID");

            Button b = sender as Button;
            if (b != null)
            {
                RepeaterItem ri = b.Parent as RepeaterItem;
                if (ri != null)
                {
                    //Fetch data

                    TextBox ItemCnt = ri.FindControl("ItemCount") as TextBox;
                    Literal ItemNo = ri.FindControl("ItemNo") as Literal;
                    Literal ItemRemainCnt = ri.FindControl("ItemRemain") as Literal;

                    if (ItemCnt.Text.Length < 1)
                    {
                        module.PrintAlert("구입할 개수를 입력해주세요");
                        return;
                    }

                    int pl_intItemNo = Convert.ToInt32(ItemNo.Text);
                    int pl_intItemCount = Convert.ToInt32(ItemCnt.Text);
                    int pl_intItemRemainCount = Convert.ToInt32(ItemRemainCnt.Text);


                    if (pl_intItemCount > pl_intItemRemainCount)
                    {
                        module.PrintAlert("재고가 부족합니다", "/Item/ItemList.aspx?strItemCode=" + strItemCode);
                        return;
                    }

                    AddBasketDB(pl_intItemNo, pl_intItemCount);

                }
            }
        }

        //장바구니 추가
        private void AddBasketDB(int intItemNo, int intItemCount)
        {
            IDas pl_objDas = null;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intItemNo", DBType.adInteger, intItemNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intItemCount", DBType.adInteger, intItemCount, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BASKET_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("장바구니에 추가되었습니다", "/Item/ItemList.aspx?strItemCode="+strItemCode);
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/Item/ItemList.aspx");
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

        protected void Basket_Click(object sender, EventArgs e)
        {
            module.moveURL("/Item/MyBasket.aspx");
        }
    }

}
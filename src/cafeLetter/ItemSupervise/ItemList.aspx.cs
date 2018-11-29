using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.ItemSupervise
{
    public partial class ItemList : System.Web.UI.Page
    {
        CommonModule module = new CommonModule();
        protected int intPageSize = 999;
        protected int intPageNo = 1;
        protected string strSearchQuery = string.Empty;
        protected int intSearchFlag = 0;
        protected string strQueryURL = string.Empty;
        protected string strhrefURL = string.Empty;
        protected string strItemCode = string.Empty;

        //권한 체크
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["userID"] == null || !Session["userRank"].Equals("마스터"))
            {
                if (Session["userID"] != null && Session["userRank"].Equals("운영진"))
                {
                    module.PrintAlert("권한이 없습니다.", "/Admin/AdminInfo.aspx");
                }
                else
                {
                    module.PrintAlert("권한이 없습니다.", "/Home.aspx");
                }
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["intPageNo"] != null)
            {
                intPageNo = Convert.ToInt32(Request.Params["intPageNo"]);
            }

            if (Request.Params["intPageSize"] != null)
            {
                intPageSize = Convert.ToInt32(Request.Params["intPageSize"]);
            }

            //Search Condition 
            if (Request.Params["intSearchFlag"] != null && Request.Params["strSearchQuery"] != null)
            {
                intSearchFlag = Convert.ToInt32(Request.Params["intSearchFlag"]);
                strSearchQuery = Request.Params["strSearchQuery"];
            }

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
                pl_objDas.AddParam("@pi_intItemNo",      DBType.adInteger,  0,              0,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strItemCode",    DBType.adVarChar,  strItemCode,    3,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchQuery", DBType.adVarWChar, strSearchQuery, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intSearchFlag",  DBType.adInteger,  intSearchFlag,  0,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo",      DBType.adInteger,  intPageNo,      0,   ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intPageSize",    DBType.adInteger,  intPageSize,    0,   ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt",   DBType.adInteger,  0,              0,   ParameterDirection.Output);
                pl_objDas.SetQuery("dbo.UP_ITEM_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));


                AuthorityListView.DataSource = pl_objDas.objDT;
                AuthorityListView.DataBind();


                string hrefParam = string.Empty;
                string hrefURL = "/ItemSupervise/ItemList.aspx";

                if (!strSearchQuery.Equals(string.Empty) && intSearchFlag != 0)
                {
                    hrefParam += "&intSearchFlag=" + intSearchFlag + "&strSearchQuery=" + strSearchQuery;
                }

               // module.Pagination(pl_intRecordCnt, intPageNo, intPageSize, hrefURL, hrefParam, PageNumber);

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

        //검색버튼
        protected void SearchBtn_Click(object sender, EventArgs e)
        {
            //SearchMenu
            string pl_strSearchValue = SearchValue.Text;

            if (SearchMenu.SelectedItem.Text.Equals("물품명"))
            {
                intSearchFlag = 1;
                strSearchQuery = pl_strSearchValue;
            }
            else if (SearchMenu.SelectedItem.Text.Equals("종류"))
            {
                intSearchFlag = 2;
                strSearchQuery = pl_strSearchValue;
            }
            
            intPageNo = 1;

            module.moveURL("/ItemSupervise/ItemList.aspx?intPageNo=" + intPageNo + "&intPageSize=" + intPageSize + "&intSearchFlag=" + intSearchFlag + "&strSearchQuery=" + strSearchQuery);
        }
    }
}
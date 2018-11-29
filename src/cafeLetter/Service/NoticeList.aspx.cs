using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Service
{
    public partial class NoticeList : System.Web.UI.Page
    {
        protected CommonModule module = new CommonModule();
        protected int intPageSize = 10;
        protected int intPageNo = 1;
        protected string strBoardName = string.Empty;
        protected string strSearchID = string.Empty;
        protected string strSearchTitle = string.Empty;


        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Params["PageNo"] != null)
            {
                intPageNo = Convert.ToInt32(Request.Params["PageNo"]);
            }

            if (Request.Params["PageSize"] != null)
            {
                intPageSize = Convert.ToInt32(Request.Params["PageSize"]);
            }


            PostList(strSearchID, strSearchTitle, intPageNo, intPageSize);
        }

        void PostList(string strSearchID, string strSearchTitle, int intPageNo, int intPageSize)
        {

            IDas pl_objDas = null;
            pl_objDas = module.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strBoardTypeCode", DBType.adVarChar, "B03", 3, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchID", DBType.adVarChar, strSearchID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchTitle", DBType.adVarChar, strSearchTitle, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchTag", DBType.adVarChar, "", 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 4, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 4, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, DBNull.Value, 4, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_POST_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));


                string hrefURL = "/Service/NoticeList.aspx";
                string hrefParam = "";

                module.Pagination(pl_intRecordCnt, intPageNo, intPageSize, hrefURL, hrefParam, PageNumber);


                NoticeListPanel.DataSource = pl_objDas.objDT;
                NoticeListPanel.DataBind();

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


        //검색
        protected void SearchButton_Click(object sender, EventArgs e)
        {
            //SearchMenu
            string pl_strSearchValue = SearchValue.Text;

            if (SearchMenu.SelectedItem.Text.Equals("아이디"))
            {
                strSearchID = pl_strSearchValue;
            }
            else if (SearchMenu.SelectedItem.Text.Equals("제목"))
            {
                strSearchTitle = pl_strSearchValue;
            }


            intPageNo = 1;
            PostList(strSearchID, strSearchTitle, intPageNo, intPageSize);
        }

    }
}
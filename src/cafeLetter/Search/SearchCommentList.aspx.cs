using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Search
{
    public partial class SearchCommentList : System.Web.UI.Page
    {
        protected CommonModule module = new CommonModule();
        protected int intPageSize = 10;
        protected int intPageNo = 1;
        protected string strBoardName = string.Empty;
        protected string strSearchID = string.Empty;
        protected string strSearchTitle = string.Empty;
        protected string strPhotoName = string.Empty;
        protected string strSearchTag = string.Empty;
        protected int intOrderFlag = 1;
        protected string strQueryURL = string.Empty;


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

            if (Request.Params["OrderFlag"] != null)
            {
                intOrderFlag = Convert.ToInt32(Request.Params["OrderFlag"]);
            }



            //board type query
            if (Request.Params["searchType"] != null && Request.Params["searchQuery"] != null)
            {
                if (Request.Params["searchType"].Equals("1"))
                {
                    strSearchTitle = Request.Params["searchQuery"];
                }
                else if (Request.Params["searchType"].Equals("2"))
                {
                    strSearchID = Request.Params["searchQuery"];
                }
                

                strQueryURL = "&searchType=" + Request.Params["searchType"] + "&searchQuery=" + Request.Params["searchQuery"];
            }


            strPhotoName = "자유게시판";


            PostList(strSearchID, strSearchTitle, strSearchTag, intOrderFlag, intPageNo, intPageSize);

            //다시 게시 사용 체크

        }

        protected void PostList(string strSearchID, string strSearchTitle, string strSearchTag, int intOrderFlag, int intPageNo, int intPageSize)
        {

            IDas pl_objDas = null;
            pl_objDas = module.ConnetionDB();

            // intOrderFlag 1 = 등록한 순서, 2 = 조회수 3 = 댓글 수 

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, 0, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, 0, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strSearchID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBody", DBType.adVarWChar, strSearchTitle, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_COMMENTSEARCH_NT_LST");
                
                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));



                string hrefURL = "/Search/SearchCommentList.aspx";
                string hrefParam = "&OrderFlag=" + intOrderFlag + strQueryURL;

                module.Pagination(pl_intRecordCnt, intPageNo, intPageSize, hrefURL, hrefParam, PageNumber);
                PostListPanel.DataSource = pl_objDas.objDT;
                PostListPanel.DataBind();



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

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            //SearchMenu
            string pl_strSearchValue = SearchValue.Text;
            string pl_strSearchQuery = string.Empty;
            int pl_intSearchType = 0;

            if (SearchMenu.SelectedItem.Text.Equals("아이디"))
            {
                pl_intSearchType = 2;
                pl_strSearchQuery = pl_strSearchValue;
            }
            else if (SearchMenu.SelectedItem.Text.Equals("내용"))
            {
                pl_intSearchType = 1;
                pl_strSearchQuery = pl_strSearchValue;
            }
            

            intPageNo = 1;
            module.moveURL("/Search/SearchCommentList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag + "&searchType=" + pl_intSearchType + "&searchQuery=" + pl_strSearchQuery);



        }

        protected void Page10_Click(object sender, EventArgs e)
        {
            intPageSize = 10;
            intPageNo = 1;
            module.moveURL("/Search/SearchCommentList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Page20_Click(object sender, EventArgs e)
        {
            intPageSize = 20;
            intPageNo = 1;
            module.moveURL("/Search/SearchCommentList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Page30_Click(object sender, EventArgs e)
        {
            intPageSize = 30;
            intPageNo = 1;
            module.moveURL("/Search/SearchCommentList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Newest_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 1;
            module.moveURL("/Search/SearchCommentList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Hot_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 2;
            module.moveURL("/Search/SearchCommentList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Comment_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 3;
            module.moveURL("/Search/SearchCommentList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }
    }
}
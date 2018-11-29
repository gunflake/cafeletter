using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Data;
using System.Web.UI;

namespace cafeLetter.Admin
{
    public partial class AuthorityList : System.Web.UI.Page
    {
        protected CommonModule module = new CommonModule();
        protected int intPageNo = 1;
        protected int intPageSize = 10;
        protected string strSearchID = string.Empty;
        protected string strSearchName = string.Empty;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // session check
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
             
            if (Request.Params["PageNo"] != null)
            {
                intPageNo = Convert.ToInt32(Request.Params["PageNo"]);
            }

            if (Request.Params["PageSize"] != null)
            {
                intPageSize = Convert.ToInt32(Request.Params["PageSize"]);
            }

            AdminList(strSearchID, strSearchName, intPageNo, intPageSize);

        }

        //관리자 리스트 출력
        protected void AdminList(string strSearchID, string strSearchName, int intPageNo, int intPageSize)
        {

            IDas pl_objDas = module.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strSearchID", DBType.adVarWChar, strSearchID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@@pi_strSearchName", DBType.adVarWChar, strSearchName, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ADMIN_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));

                AuthorityListView.DataSource = pl_objDas.objDT;
                AuthorityListView.DataBind();

                string hrefURL = "/Admin/AdminList.aspx";
                string hrefParam = "";
                module.Pagination(pl_intRecordCnt, intPageNo, intPageSize, hrefURL, hrefParam, PageNumber);

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

        protected void SearchAdmin_Click(object sender, EventArgs e)
        {
            string pl_strSearchValue = SearchValue.Text;

            if (SearchMenu.SelectedItem.Text.Equals("아이디"))
            {
                strSearchID = pl_strSearchValue;
            }
            else if (SearchMenu.SelectedItem.Text.Equals("이름"))
            {
                strSearchName = pl_strSearchValue;
            }

            intPageNo = 1;

            AdminList(strSearchID, strSearchName, intPageNo, intPageSize);

        }
    }
}
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
    public partial class UserList : System.Web.UI.Page
    {
        protected string strSearchID = string.Empty;
        protected string strSearchName = string.Empty;
        protected string strUserID = string.Empty;
        protected int intPageNo = 1;
        protected int intPageSize = 10;
        protected string strUserRank = string.Empty;
        protected CommonModule module = new CommonModule();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // session check
            if (Session["userID"] == null || Session["userRank"].Equals("회원"))
            {
                module.PrintAlert("권한이 없습니다.", "/Home.aspx");
                return;
            }

            strUserID = Session["userID"].ToString();
            strUserRank = Session["userRank"].ToString();

            //회원권한있는사람만 볼수있도록 설정!!
            if (Session["userSupervise"] != null && Session["userSupervise"].Equals("X"))
            {
                module.PrintAlert("권한이 없습니다.", "/Admin/AdminInfo.aspx");
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

            UserInfoList(strSearchID, strSearchName, intPageNo, intPageSize);
        }

    
        //유저 정보 조회
        private void UserInfoList(string strSearchID, string strSearchName, int intPageNo, int intPageSize)
        {
            IDas pl_objDas = null;
            // 페이즈 사이즈 받아오기
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strSearchID", DBType.adVarChar, strSearchID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@@pi_strSearchName", DBType.adVarChar, strSearchName, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USER_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));


                string hrefURL = "/Admin/UserList.aspx";
                string hrefParam = "";
                module.Pagination(pl_intRecordCnt, intPageNo, intPageSize, hrefURL, hrefParam, PageNumber);

                UserListView.DataSource = pl_objDas.objDT;
                UserListView.DataBind();
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

        protected void SearchUser_Click(object sender, EventArgs e)
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
            UserInfoList(strSearchID, strSearchName, intPageNo, intPageSize);
        }
    }
}
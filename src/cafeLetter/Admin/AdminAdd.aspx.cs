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
    public partial class AdminAdd : System.Web.UI.Page
    {
        protected string strAdminUserID = string.Empty;
        protected string strUserName = string.Empty;
        protected string strBoardAuthority = string.Empty;
        protected string strGalleryAuthority = string.Empty;
        protected string strUserAuthority = string.Empty;
        protected CommonModule module = new CommonModule();

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

            // param check
            if (Request.Params["UserID"] == null || Request.Params["UserName"] == null)
            {
                module.PrintAlert("잘못된 접근입니다.", "/Home.aspx");
            }


        }

        protected void Page_Load(object sender, EventArgs e)
        {
            strAdminUserID = Request.Params["UserID"].ToString();
            strUserName = Request.Params["UserName"].ToString();

            if (!IsPostBack)
            {
                ManageRead();
            }

        }

        //회원 상세정보 조회
        private void ManageRead()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strAdminUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intBoardCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intCommentCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ADMININFO_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    module.PrintAlert("회원 상세정보 조회 실패", "/Admin/UserList.aspx");
                    return;
                }
                strUserName = pl_objDas.objDT.Rows[0]["USERNAME"].ToString();
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


        private void AdminInsertWorkDB()
        {
            IDas pl_objDas = null;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strAdminUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strRank", DBType.adVarChar, "운영진", 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBoardAuthority", DBType.adVarWChar, strBoardAuthority, 2, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPhotoAuthority", DBType.adVarWChar, strGalleryAuthority, 2, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserAuthority", DBType.adVarWChar, strUserAuthority, 2, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);

                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ADMINAUTHORITY_TX_UPD");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("관리자를 추가했습니다.", "/Admin/AdminList.aspx");
                    return;
                }
                else
                {
                    module.PrintAlert("관리자 추가에 실패했습니다.", "/Admin/AdminList.aspx");
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

        protected void Cancel_Click(object sender, EventArgs e)
        {
            module.moveURL("/Admin/UserList.aspx");
        }

        protected void AdminInsertBtn_Click(object sender, EventArgs e)
        {

            //게시판 권한 체크 확인
            if (BoardCheckBox.Checked)
            {
                strBoardAuthority = "O";
            }
            else
            {
                strBoardAuthority = "X";
            }

            //갤러리 권한 체크 확인
            if (GalleryCheckBox.Checked)
            {
                strGalleryAuthority = "O";
            }
            else
            {
                strGalleryAuthority = "X";
            }
            //유저 권한 체크 확인
            if (UserCheckBox.Checked)
            {
                strUserAuthority = "O";
            }
            else
            {
                strUserAuthority = "X";
            }

            //최소 하나의 권한이 체크되어야한다.
            if (strUserAuthority.Equals("X") && strBoardAuthority.Equals("X") && strGalleryAuthority.Equals("X"))
            {
                module.PrintAlert("권한이 최소 한개이상 체크가 필요합니다. 관리자 추가를 원치않는다면 취소버튼을 누르세요");
                return;
            }

            AdminInsertWorkDB();
        }
    }
}
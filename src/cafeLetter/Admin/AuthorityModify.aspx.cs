using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Data;
using System.Web.UI;

namespace cafeLetter.Admin
{
    public partial class AuthorityModify : System.Web.UI.Page
    {
        protected string strAdminUserID = string.Empty;
        protected string strName = string.Empty;
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
            strName = Request.Params["UserName"].ToString();

            if (!IsPostBack)
            {
                ManageRead();
            }
        }

        private void ManageRead()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
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
                    module.PrintAlert("관리자 정보 조회 실패", "/Admin/AdminInfo.aspx");
                    return;
                }

                strName = pl_objDas.objDT.Rows[0]["USERNAME"].ToString();
                strBoardAuthority = pl_objDas.objDT.Rows[0]["BOARDAUTHORITY"].ToString();
                strGalleryAuthority = pl_objDas.objDT.Rows[0]["PHOTOAUTHORITY"].ToString();
                strUserAuthority = pl_objDas.objDT.Rows[0]["USERAUTHORITY"].ToString();

                
                if (strBoardAuthority.Equals("O"))
                {
                    BoardCheckBox.Checked = true;
                }
                if (strGalleryAuthority.Equals("O"))
                {
                    GalleryCheckBox.Checked = true;
                }
                if (strUserAuthority.Equals("O"))
                {
                    UserCheckBox.Checked = true;
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

        protected void AuthorityModifyUpdate_Click(object sender, EventArgs e)
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
                module.PrintAlert("권한이 최소 한개이상 체크가 필요합니다. 권한삭제를 원하시면 삭제버튼을 누르세요");
                return;
            }

            AuthorityUpdateDB();
        }

        // 권한 수정 완료
        private void AuthorityUpdateDB()
        {
            IDas pl_objDas = null;
            try
            {
                //UserNo 받아서 권한 수정하는 SP 만들기

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
                    module.PrintAlert("관리자 권한을 수정했습니다", "/Admin/AdminList.aspx");
                    return;
                }
                else
                {
                    module.PrintAlert("관리자 권한 수정에 실패했습니다", "/Admin/AdminList.aspx");
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

        protected void AuthorityDelete_Click(object sender, EventArgs e)
        {
            AdminDeleteDB();
        }

        private void AdminDeleteDB()
        {
            IDas pl_objDas = null;

            try
            {

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strAdminUserID, 20, ParameterDirection.Input);               
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ADMIN_TX_DEL");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("관리자를 삭제했습니다.",  "/Admin/AdminList.aspx");
                    return;
                }
                else
                {
                    module.PrintAlert("관리자 삭제에 실패했습니다.", "/Admin/AdminList.aspx");
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
    }
}
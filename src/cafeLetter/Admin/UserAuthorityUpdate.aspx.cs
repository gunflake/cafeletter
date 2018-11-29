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
    public partial class UserAuthorityUpdate : System.Web.UI.Page
    {
        protected CommonModule objModule = new CommonModule();
        protected string strUserID = string.Empty;
        protected string strUserEmail = string.Empty;
        protected string strSex = string.Empty;
        protected string strName = string.Empty;
        protected string strLocation = string.Empty;
        protected string strBirthday = string.Empty;
        protected string strSNS = string.Empty;
        protected string strIntroduce = string.Empty;
        protected string strLastLogin = string.Empty;
        protected string strBoardCnt = string.Empty;
        protected string strCommentCnt = string.Empty;
        protected string strBoardAuthority = string.Empty;
        protected string strGalleryAuthority = string.Empty;
        protected string    intBonusCash = string.Empty;
        protected string    intRealCash  = string.Empty;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // session check
            if (Session["userID"] == null || Session["userRank"].Equals("회원"))
            {
                objModule.PrintAlert("권한이 없습니다.", "/Home.aspx");
                return;
            }


            //회원권한있는사람만 볼수있도록 설정!!
            if (Session["userSupervise"] != null && Session["userSupervise"].Equals("X"))
            {
                objModule.PrintAlert("권한이 없습니다.", "/Admin/AdminInfo.aspx");
                return;
            }
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            strUserID = Request.Params["UserID"].ToString();

            if (!IsPostBack)
            {
                ReadInfo();
                UserCashDB();
            }           

        }

        //유저 캐시 정보
        private void UserCashDB()
        {
            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intMyCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRealCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intBonusCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_MY_NT_GET");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    intRealCash = pl_objDas.GetParam("@po_intRealCash");
                    intBonusCash = pl_objDas.GetParam("@po_intBonusCash");
                    return;
                }
                else
                {
                    objModule.PrintAlert(pl_strOutputMsg, "/Admin/UserList.aspx");
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

        //회원정보 읽기
        private void ReadInfo()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
            try
            {
                pl_objDas = pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERINFO_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    objModule.PrintAlert("회원 정보조회 실패", "/Admin/UserList.aspx");
                }

                strName = pl_objDas.objDT.Rows[0]["USERNAME"].ToString();
                strLocation = pl_objDas.objDT.Rows[0]["LOCATION"].ToString();
                strBirthday = pl_objDas.objDT.Rows[0]["BIRTHDAY"].ToString();
                strSNS = pl_objDas.objDT.Rows[0]["SNS"].ToString();
                strIntroduce= pl_objDas.objDT.Rows[0]["INTRODUCE"].ToString();
                strSex = pl_objDas.objDT.Rows[0]["GENDER"].ToString();
                strLastLogin = pl_objDas.objDT.Rows[0]["LASTDATE"].ToString();
                strCommentCnt = pl_objDas.objDT.Rows[0]["COMMENTCNT"].ToString();
                strBoardCnt =pl_objDas.objDT.Rows[0]["BOARDCNT"].ToString();
                strBoardAuthority = pl_objDas.objDT.Rows[0]["BOARDWRITE"].ToString();
                strGalleryAuthority = pl_objDas.objDT.Rows[0]["GALLERYWRITE"].ToString();

                //성별 변환
                if (strSex.Equals("W"))
                {
                    strSex = "여자";
                }
                else if(strSex.Equals("M"))
                {
                    strSex = "남자";
                }

                //권한 체크
                if (strBoardAuthority.Equals("Y"))
                {
                    BoardCheckBox.Checked = true;
                }

                if (strGalleryAuthority.Equals("Y"))
                {
                    GalleryCheckBox.Checked = true;
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

        protected void UserAuthorityUpdateBtn_Click(object sender, EventArgs e)
        {            
            UserUpdate();
        }

        private void UserUpdate()
        {
            IDas pl_objDas = null;


            try
            {

                //게시판 권한 체크 확인
                if (BoardCheckBox.Checked)
                {
                    strBoardAuthority = "Y";
                }
                else
                {
                    strBoardAuthority = "N";
                }

                //갤러리 권한 체크 확인
                if (GalleryCheckBox.Checked)
                {
                    strGalleryAuthority = "Y";
                }
                else
                {
                    strGalleryAuthority = "N";
                }

          

                pl_objDas = pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBoardAuthority", DBType.adVarWChar, strBoardAuthority, 2, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPhotoAuthority", DBType.adVarWChar, strGalleryAuthority, 2, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);

                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERAUTHORITY_TX_UPD");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    objModule.PrintAlert("회원정보를 수정했습니다.", "/Admin/UserList.aspx");
                }
                else
                {
                    objModule.PrintAlert("회원정보 수정에 실패했습니다", "/Admin/UserList.aspx");
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

        //회원삭제클릭
        protected void DeleteBtn_Click(object sender, EventArgs e)
        {
            DeleteUserInfoDB();
        }

        private void DeleteUserInfoDB()
        {

            IDas pl_objDas = null;

            try
            {
                pl_objDas = objModule.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USER_TX_DEL");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    objModule.PrintAlert("회원을 삭제했습니다", "/Admin/UserList.aspx");

                }
                else
                {
                    objModule.PrintAlert("회원삭제에 실패했습니다", "/Admin/UserList.aspx");

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
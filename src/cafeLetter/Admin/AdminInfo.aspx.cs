using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Data;
using System.Web.UI;

namespace cafeLetter.Admin
{
    public partial class AdminInfo : System.Web.UI.Page
    {
        protected CommonModule module = new CommonModule();
        protected string strUserID = string.Empty;
        protected string strName = string.Empty;
        protected int    intBoardCnt = 0;
        protected int    intCommentCnt = 0;

        protected string strAdminRank = string.Empty;
        protected string strBoardAuthority = string.Empty;
        protected string strGalleryAuthority = string.Empty;
        protected string strUserAuthority = string.Empty;

        protected void Page_PreInit(object sender, EventArgs e)
        {

            // session check
            if (Session["userID"] == null || Session["userRank"].Equals("회원"))
            {
                module.PrintAlert("권한이 없습니다.", "/Home.aspx");
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            // session check
            if (Session["userID"] != null)
            {
                strUserID = Session["userID"].ToString();
            }
            ReadMyInfo();
        }


        private void ReadMyInfo()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intBoardCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intCommentCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ADMININFO_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));
                
                if (pl_intRetVal != 0)
                {
                    module.PrintAlert("관리자 정보 조회 실패", "/Home.aspx");
                    return;
                }

                strName = pl_objDas.objDT.Rows[0]["USERNAME"].ToString();
                
                intBoardCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intBoardCnt"));
                intCommentCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intCommentCnt"));

                strAdminRank = pl_objDas.objDT.Rows[0]["USERRANK"].ToString();
                strBoardAuthority = pl_objDas.objDT.Rows[0]["BOARDAUTHORITY"].ToString();
                strGalleryAuthority = pl_objDas.objDT.Rows[0]["PHOTOAUTHORITY"].ToString();
                strUserAuthority = pl_objDas.objDT.Rows[0]["USERAUTHORITY"].ToString();
                           
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
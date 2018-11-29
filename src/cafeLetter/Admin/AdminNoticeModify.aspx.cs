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
    public partial class AdminNoticeModify : System.Web.UI.Page
    {
        private int intBoardNo = 0;
        string strUserID = string.Empty;
        protected CommonModule module = new CommonModule();

        protected void Page_PreInit(object sender, EventArgs e)
        {

            // session check
            if (Session["userID"] == null || Session["userRank"].Equals("회원"))
            {
                module.PrintAlert("권한이 없습니다.", "/Home.aspx");
                return;
            }

            if (Request.Params["BoardNo"] == null)
            {
                module.PrintAlert("잘못된 접근입니다.", "/Admin/AdminNoticeList.aspx");
                return;
            }

            intBoardNo = Convert.ToInt32(Request.Params["BoardNo"]);
            strUserID = Session["userID"].ToString();

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostRead();
            }
        }

        protected void PostRead()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intBoardNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARD_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    module.PrintAlert("공지사항 상세조회 실패", "/Board/BoardList.aspx");
                    return;
                }

                NoticeTitle.Text = pl_objDas.objDT.Rows[0]["BOARDTITLE"].ToString();
                NoticeBody.Text = pl_objDas.objDT.Rows[0]["BOARDCONTENT"].ToString();
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

        protected void NoticeCancel_Click(object sender, EventArgs e)
        {
            module.moveURL("/Admin/AdminNoticeList.aspx");
        }

        protected void NoticeModify_Click(object sender, EventArgs e)
        {
            NoticeModifyDB();
        }

        private void NoticeModifyDB()
        {
            string pl_strTitle = string.Empty;
            string pl_strBody = string.Empty;
            IDas pl_objDas = null;

            try
            {

                pl_strTitle = NoticeTitle.Text;
                pl_strBody = NoticeBody.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intBoardNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTitle", DBType.adVarWChar, pl_strTitle, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBody", DBType.adVarWChar, pl_strBody, 4000, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTag", DBType.adVarWChar, "", 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARD_TX_UPD");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("공지사항이 수정되었습니다", "/Admin/AdminNoticeView.aspx?BoardNo=" + intBoardNo);
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/Admin/AdminNoticeView.aspx?BoardNo=" + intBoardNo);
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
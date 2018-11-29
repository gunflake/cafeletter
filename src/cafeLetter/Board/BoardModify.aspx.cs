using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Board
{
    public partial class BoardUpdate : System.Web.UI.Page
    {
        private int intBoardNo = 0;
        protected CommonModule module = new CommonModule();
        string strUserID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (module.getSession("userID") == null)
            {
                module.PrintAlert("로그인이 필요합니다", "/Board/BoardList.aspx");
                return;

            }
            else if (module.getSession("boardWrite").Equals("N"))
            {
                module.PrintAlert("게시글 수정 권한이 없습니다", "/Board/BoardList.aspx");
                return;
            }
            else if(Request.Params["BoardNo"] == null)
            {
                module.PrintAlert("잘못된 접근입니다.", "/Board/BoardList.aspx");
                return;
            }
            else
            {
                strUserID = module.getSession("userID");
            }
            
            intBoardNo = Convert.ToInt32(Request.Params["BoardNo"]);

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
                    module.PrintAlert("게시글 상세조회 실패", "/Board/BoardList.aspx");
                    return;
                }

                BoardTitle.Text = pl_objDas.objDT.Rows[0]["BOARDTITLE"].ToString();
                BoardBody.Text = pl_objDas.objDT.Rows[0]["BOARDCONTENT"].ToString();
                BoardTags.Text = pl_objDas.objDT.Rows[0]["BOARDTAG"].ToString();
            }
            catch
            {

            }
            finally
            {
                if(pl_objDas != null)
                {
                    pl_objDas.Close();
                    pl_objDas = null;
                }               
            }
        }

        protected void BoardModify_Click(object sender, EventArgs e)
        {
               PostModify();
        }

        //수정한 글 등록
        protected void PostModify()
        {
            string pl_strTitle = string.Empty;
            string pl_strBody = string.Empty;
            string pl_strTags = string.Empty;
            IDas pl_objDas = null;

            try
            {

                pl_strTitle = BoardTitle.Text;
                pl_strBody = BoardBody.Text;
                pl_strTags = BoardTags.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intBoardNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTitle", DBType.adVarWChar, pl_strTitle, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBody", DBType.adVarWChar, pl_strBody, 4000, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTag", DBType.adVarWChar, pl_strTags, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARD_TX_UPD");
                

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("게시글이 수정되었습니다", "/Board/BoardView.aspx?BoardNo=" + intBoardNo);
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/Board/BoardView.aspx?BoardNo=" + intBoardNo);
                    return;
                }
            }
            catch
            {

            }
            finally
            {
                if(pl_objDas != null)
                {
                    pl_objDas.Close();
                    pl_objDas = null;
                }
            }
        }

        protected void BoardCancel_Click(object sender, EventArgs e)
        {
            module.moveURL("/Board/BoardList.aspx");
        }
    }
}
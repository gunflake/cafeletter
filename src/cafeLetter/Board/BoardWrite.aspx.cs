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
    public partial class BoardWrite1 : System.Web.UI.Page
    {

        protected CommonModule module = new CommonModule();

        string strUserID = string.Empty;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (module.getSession("userID") == null)
            {
                module.PrintAlert("로그인이 필요합니다", "/Board/BoardList.aspx");
                return;

            }
            else if (module.getSession("boardWrite").Equals("N"))
            {
                module.PrintAlert("게시글 작성 권한이 없습니다", "/Board/BoardList.aspx");
                return;
            }
            else
            {
                strUserID = module.getSession("userID");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

           
        }

        //작성 버튼 클릭
        protected void BoardWrite_Click(object sender, EventArgs e)
        {

            string pl_strBoardTypeCode = null;

            if (BoardMenu.SelectedItem.Text.Equals("자유게시판"))
            {
                pl_strBoardTypeCode = "B01";
            }
            else if (BoardMenu.SelectedItem.Text.Equals("정보게시판"))
            {
                pl_strBoardTypeCode = "B02";
            }
            else
            {
                module.PrintAlert("작성할 게시판을 선택해주세요");
                return;
            }


            PostWriteDB(pl_strBoardTypeCode);

        }

        protected void PostWriteDB(string strBoardTypeCode)
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

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBoardTypeCode", DBType.adVarWChar, strBoardTypeCode, 3, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTitle", DBType.adVarWChar, pl_strTitle, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBody", DBType.adVarWChar, pl_strBody, 4000, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTag", DBType.adVarWChar, pl_strTags, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARD_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("새 글이 작성되었습니다", "/Board/BoardList.aspx?BoardType=" + strBoardTypeCode);
                    return;
                }
                else
                {
                    module.PrintAlert( pl_strOutputMsg, "/Board/BoardList.aspx?BoardType=" + strBoardTypeCode);
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

        protected void BoardCancel_Click(object sender, EventArgs e)
        {
            module.moveURL("/Board/BoardList.aspx");
        }
    }
}
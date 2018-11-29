﻿using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Data;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter
{
    public partial class BoardView : System.Web.UI.Page
    {
        protected CommonModule module = new CommonModule();

        public string strPostTitle = string.Empty;
        public string strPostContent = string.Empty;
        public string strPostView = string.Empty;
        public string strPostDate = string.Empty;
        public string strPostWriter = string.Empty;

        public string strPostLike = string.Empty;
        private int intBoardNo = 0;
        public string strUserID = string.Empty;
        private string strBoardCode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                strUserID = Session["userID"].ToString();
            }

            if (Request.Params["BoardNo"] == null)
            {
                module.PrintAlert("잘못된 접근입니다.", "/Board/BoardList.aspx");
                return;
            }


            intBoardNo = Convert.ToInt32(Request.Params["BoardNo"]);

            if (Request.Params["BoardCode"] != null)
            {
                strBoardCode = Convert.ToString(Request.Params["BoardCode"]);
            }
            


            if (!IsPostBack)
            {
                PostViewCount();
            }

            PostView();
            CommentList();

        }

        /// ----------------------
        /// <summary>
        /// 게시글 조회수 증가
        /// </summary>
        /// ----------------------
        private void PostViewCount()
        {
            IDas pl_objDas = null;
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intBoardNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARDCOUNT_TX_UPD");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
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

        /// ----------------------
        /// <summary>
        /// 게시글 상세조회
        /// </summary>
        /// ----------------------
        private void PostView()
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

                if(pl_intRetVal != 0)
                {
                    module.PrintAlert("게시글 상세보기 조회에 실패했습니다", "/Board/BoardList.aspx");
                    return;
                }

                if (pl_objDas.RecordCount == 0)
                {
                    module.PrintAlert("해당 게시글이 없습니다.", "/Board/BoardList.aspx");
                    return;
                }

                strPostTitle = pl_objDas.objDT.Rows[0]["BOARDTITLE"].ToString();
                strPostContent = pl_objDas.objDT.Rows[0]["BOARDCONTENT"].ToString();
                strPostView = pl_objDas.objDT.Rows[0]["BOARDVIEW"].ToString();
                strPostDate = pl_objDas.objDT.Rows[0]["BOARDDATE"].ToString();
                strPostLike = pl_objDas.objDT.Rows[0]["BOARDLIKE"].ToString();
                strPostWriter = pl_objDas.objDT.Rows[0]["USERID"].ToString();

                strPostContent = strPostContent.Replace(Environment.NewLine, "<br />");


               

                BoardModify.Visible = strPostWriter.Equals(strUserID);
                BoardDelete.Visible = strPostWriter.Equals(strUserID);

                if(Session["boardSupervise"] != null && Session["boardSupervise"].Equals("O"))
                {
                    BoardDelete.Visible = true;
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

        /// ----------------------
        /// <summary>
        /// 댓글 리스트
        /// </summary>
        /// ----------------------
        private void CommentList()
        {
            IDas pl_objDas = null;
            int pl_pageSize = 10;
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intBoardNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, 0, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, "", 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, pl_pageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, 1, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_COMMENT_NT_LST");

                CommentListPanel.DataSource = pl_objDas.objDT;
                CommentListPanel.DataBind();

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


        //댓글 등록 버튼
        protected void CommentWrite_Click(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다");
                return;
            }else if (Session["boardWrite"] != null && Session["boardWrite"].Equals("N"))
            {
                module.PrintAlert("댓글 작성 권한이 없습니다");
                return;
            }
            else
            {
                string pl_strCommentBody = CommentBody.Text;
                CommentRegister(pl_strCommentBody);
                return;
            }
        }

        //댓글 등록 DB
        private void CommentRegister(string strCommentBody)
        {
            IDas pl_objDas = null;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intBoardNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strContent", DBType.adVarWChar, strCommentBody, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARDCOMMENT_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("댓글이 등록되었습니다", "/Board/BoardView.aspx?BoardNo=" + intBoardNo);
                    return;
                }
                else
                {
                    module.PrintAlert("입력 정보를 다시 확인해주세요", "/Board/BoardView.aspx?BoardNo=" + intBoardNo);
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

        //게시글 수정 버튼 클릭
        protected void BoardModify_Click(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다");
                return;
            }
            else if (!strUserID.Equals(strPostWriter))
            {
                module.PrintAlert("자신이 작성한 글만 수정할 수 있습니다.");
                return;
            }else
            {
                module.moveURL("/Board/BoardModify.aspx?BoardNo=" + intBoardNo);
                return;
            }
                
        }

        //게시글 삭제 버튼 
        protected void BoardDelete_Click(object sender, EventArgs e)
        {
            if(Session["boardSupervise"] != null && Session["boardSupervise"].Equals("O"))
            {
                BoardDeleteDB();
                return;
            }

            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다");
                return;
            }
            else if (!strUserID.Equals(strPostWriter))
            {
                module.PrintAlert("자신이 작성한 글만 삭제할 수 있습니다.");
                return;
            }
            else
            {
                BoardDeleteDB();
                return;
            }
        }

        //게시글 삭제 DB
        private void BoardDeleteDB()
        {
            IDas pl_objDas = null;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intBoardNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARD_TX_DEL");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("글을 삭제했습니다.","/Board/BoardList.aspx");
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/Board/BoardView.aspx?BoardNo=" + intBoardNo);
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

        //자기가 작성한 댓글 수정 버튼
        protected void CmtModify_Click(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            if (b != null)
            {
                RepeaterItem ri = b.Parent as RepeaterItem;
                if (ri != null)
                {
                    
                   //Fetch data
                    TextBox CmtBody = ri.FindControl("CmtModifyContent") as TextBox;
                    Button CmtUpdatebtn = ri.FindControl("CommentUpdate") as Button;
                    Literal CmtContent = ri.FindControl("CmtContent") as Literal;
                    CmtBody.Visible = true;
                    CmtUpdatebtn.Visible = true;
                    CmtContent.Visible = false;

                    CmtBody.Focus();

                }
            }
        }

        //댓글 수정 버튼 클릭
        protected void CommentUpdate_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string pl_strCmtContent = string.Empty;

            if (btn != null)
            {
                RepeaterItem ri = btn.Parent as RepeaterItem;
                if( ri!= null)
                {
                    TextBox CmtBody = ri.FindControl("CmtModifyContent") as TextBox;
                    Button CmtUpdatebtn = ri.FindControl("CommentUpdate") as Button;
                    Literal CmtContent = ri.FindControl("CmtContent") as Literal;
                    Literal CmtNo = ri.FindControl("CommentNo") as Literal;

                    pl_strCmtContent = CmtBody.Text;

                    //IDas연결 업데이트 넣기
                    CommentUpdate(pl_strCmtContent, Convert.ToInt32(CmtNo.Text.ToString()));


             
                    CmtBody.Visible = false;
                    CmtUpdatebtn.Visible = false;
                    CmtContent.Visible = true;
          
                }
            }
        }

        //댓글 수정 
        private void CommentUpdate(string strCmtContent, int intCommentNo)
        {
            IDas pl_objDas = null;


            /// Tags 유효성 체크


            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intCommentNo", DBType.adInteger, intCommentNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBody", DBType.adVarWChar, strCmtContent, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_COMMENT_TX_UPD");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.moveURL("/Board/BoardView.aspx?BoardNo=" + intBoardNo);
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
                if (pl_objDas != null)
                {
                    pl_objDas.Close();
                    pl_objDas = null;
                }
            }
        }

        //댓글 삭제 버튼
        protected void CmtDelete_Click(object sender, EventArgs e)
        {
            LinkButton b = sender as LinkButton;
            if (b != null)
            {
                RepeaterItem ri = b.Parent as RepeaterItem;
                if (ri != null)
                {

                    Literal CmtNo = ri.FindControl("CommentNo") as Literal;



                    if (Session["userID"] == null)
                    {
                        module.PrintAlert("로그인이 필요합니다");
                        return;
                    }
                    else if (Session["boardWrite"] != null && Session["boardWrite"].Equals("N"))
                    {
                        module.PrintAlert("댓글 작성 권한이 없습니다");
                        return;
                    }
                    else
                    {
                        int intCommentNo = Convert.ToInt32(CmtNo.Text.ToString());
                        CommentDeleteDB(intCommentNo);
                        return;
                    }
                }
            }
        }

        //댓글 삭제 DB
        private void CommentDeleteDB(int intCommentNo)
        {
            IDas pl_objDas = null;


            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intCommentNo", DBType.adInteger, intCommentNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intBoardNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARDCOMMENT_TX_DEL");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("댓글이 삭제되었습니다.","/Board/BoardView.aspx?BoardNo=" + intBoardNo);
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
                if (pl_objDas != null)
                {
                    pl_objDas.Close();
                    pl_objDas = null;
                }
            }
        }


    }
}
using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Gallery
{
    public partial class GalleryView : System.Web.UI.Page
    {

        public string strPhotoTitle = string.Empty;
        public string strPhotoURL = string.Empty;
        public string strPhotoTag = string.Empty;
        public string strPhotoDate = string.Empty;
        public string strPhotoWriter = string.Empty;

        public string strPhotoView = string.Empty;
        public string strPhotoLike = string.Empty;
        private int intPhotoNo = 0;
        public string strUserID = string.Empty;
        protected CommonModule module = new CommonModule();



        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                strUserID = Session["userID"].ToString();
            }

            if (Request.Params["PhotoNo"] == null)
            {
                module.PrintAlert("잘못된 접근입니다.", "/Gallery/GalleryList.aspx");
                return;
            }

            intPhotoNo = Convert.ToInt32(Request.Params["PhotoNo"]);


            if (!IsPostBack)
            {
                PhotoViewCount();
            }

            PhotoView();
            CommentList();
        }

        private void CommentList()
        {

            IDas pl_objDas = null;
            int pl_pageSize = 10;
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, 0, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, intPhotoNo, 0, ParameterDirection.Input);
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
                pl_objDas.Close();
            }


        }

        //갤러리 상세 보기
        private void PhotoView()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, intPhotoNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTO_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    module.PrintAlert("갤러리 상세 조회실패", "/Gallery/GalleryList.aspx");
                    return;
                }

                if (pl_objDas.RecordCount == 0)
                {
                    module.PrintAlert("해당 게시글이 없습니다.", "/Gallery/GalleryList.aspx");
                    return;
                }

                strPhotoTitle = pl_objDas.objDT.Rows[0]["PHOTOTITLE"].ToString();
                strPhotoURL = pl_objDas.objDT.Rows[0]["PHOTOURL"].ToString();
                strPhotoView = pl_objDas.objDT.Rows[0]["PHOTOVIEW"].ToString();
                strPhotoDate = pl_objDas.objDT.Rows[0]["PHOTODATE"].ToString();
                strPhotoLike = pl_objDas.objDT.Rows[0]["PHOTOLIKE"].ToString();
                strPhotoWriter = pl_objDas.objDT.Rows[0]["USERID"].ToString();



               

                PhotoModify.Visible = strPhotoWriter.Equals(strUserID);
                PhotoDelete.Visible = strPhotoWriter.Equals(strUserID);

                if (Session["photoSupervise"]!= null && Session["photoSupervise"].Equals("O"))
                {
                    PhotoDelete.Visible = true;
                }

            }
            catch
            {

            }
            finally
            {
                pl_objDas.Close();
            }
        }

        //조회수 증가
        private void PhotoViewCount()
        {
            IDas pl_objDas = null;
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, intPhotoNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTOCOUNT_TX_UPD");

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
                pl_objDas.Close();
            }
        }


        protected void CommentWrite_Click(object sender, EventArgs e)
        {
            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다");
                return;
            }
            else if (Session["galleryWrite"] != null && Session["galleryWrite"].Equals("N"))
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

        /// ----------------------
        /// <summary>
        /// 댓글 등록
        /// </summary>
        /// ----------------------
        private void CommentRegister(string strCommentBody)
        {
            IDas pl_objDas = null;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, intPhotoNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strContent", DBType.adVarWChar, strCommentBody, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTOCOMMENT_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("댓글 등록 완료", "/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
                }
                else
                {
                    module.PrintAlert("댓글 등록 실패", "/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
                }
            }
            catch
            {

            }
            finally
            {
                pl_objDas.Close();
            }
        }


        protected void PhotoModify_Click(object sender, EventArgs e)
        {

            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다");
                return;
            }
            else if (!strUserID.Equals(strPhotoWriter))
            {
                module.PrintAlert("자신이 작성한 글만 수정할 수 있습니다.");
                return;
            }
            else
            {
                module.moveURL("/Gallery/GalleryModify.aspx?PhotoNo=" + intPhotoNo);
                return;
            }
        }

        protected void PhotoDelete_Click(object sender, EventArgs e)
        {
            if (Session["photoSupervise"] != null && Session["photoSupervise"].Equals("O"))
            {
                PhotoDeleteDB();
                return;
            }

            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다");
                return;
            }
            else if (!strUserID.Equals(strPhotoWriter))
            {
                module.PrintAlert("자신이 작성한 글만 삭제할 수 있습니다.");
                return;
            }
            else
            {
                PhotoDeleteDB();
                return;
            }
        }

        private void PhotoDeleteDB()
        {
            IDas pl_objDas = null;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, intPhotoNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTO_TX_DEL");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("갤러리 글을 삭제했습니다.", "/Gallery/GalleryList.aspx");
                }
                else
                {
                    module.PrintAlert("갤러리 글 삭제에 실패했습니다.", "/Gallery/GalleryList.aspx");
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


        //댓글 수정
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

        //댓글 수정 DB
        protected void CommentUpdate_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            string pl_strCmtContent = string.Empty;

            if (btn != null)
            {
                RepeaterItem ri = btn.Parent as RepeaterItem;
                if (ri != null)
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

        //댓글 수정 DB
        private void CommentUpdate(string strCmtContent, int intCommentNo)
        {
            IDas pl_objDas = null;



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

                    module.moveURL("/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
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

        //댓글 삭제
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
                    else if (Session["galleryWrite"] != null && Session["galleryWrite"].Equals("N"))
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
                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, intPhotoNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTOCOMMENT_TX_DEL");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("댓글이 삭제되었습니다.", "/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
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
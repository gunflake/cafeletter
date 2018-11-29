using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Member
{
    public partial class MyInfo : System.Web.UI.Page
    {
        protected CommonModule module = new CommonModule();
        protected string strUserID = string.Empty;
        protected int intPageSize = 3;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            // session check
            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다", "/Member/Login.aspx");
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
            else
            {
                strUserID = null;
            }

            SearchMyPostList();
            SearchMyCommentList();
            SearchMyGallery(strUserID);

        }

        //나의 댓글 리스트
        private void SearchMyCommentList()
        {

            IDas pl_objDas = null;
            try
            {


                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, 0, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, 0, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBody", DBType.adVarWChar, "", 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, 1, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_COMMENTSEARCH_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));

                MyCommentList.DataSource = pl_objDas.objDT;
                MyCommentList.DataBind();
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

        //나의 사진 리스트
        private void SearchMyGallery(string strSearchID)
        {
            //PAGING 따라바꾸기
            int pl_intTotalRecordCnt = 0;
            int pl_intCurrentRecortCnt = 0;
            IDas pl_objDas = null;

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strSearchID", DBType.adVarChar, strSearchID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchTitle", DBType.adVarChar, "", 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchTag", DBType.adVarChar, "", 50, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, 1, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTO_NT_LST");


                pl_intTotalRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));
                pl_intCurrentRecortCnt = pl_objDas.RecordCount;


                int pl_intCount = 0;

                //first
                if(pl_intCount< pl_intTotalRecordCnt)
                {
                    FirstImg.ImageUrl = pl_objDas.objDT.Rows[0]["PHOTOURL"].ToString();
                    FirstImgLink.NavigateUrl = "/Gallery/GalleryView.aspx?PhotoNo="+ pl_objDas.objDT.Rows[0]["PHOTONO"].ToString();
                    pl_intCount++;
                }

                //second
                if (pl_intCount < pl_intTotalRecordCnt)
                {
                    SecondImg.ImageUrl = pl_objDas.objDT.Rows[1]["PHOTOURL"].ToString();
                    SecondImgLink.NavigateUrl = "/Gallery/GalleryView.aspx?PhotoNo=" + pl_objDas.objDT.Rows[1]["PHOTONO"].ToString();
                    pl_intCount++;
                }

                //third
                if (pl_intCount < pl_intTotalRecordCnt)
                {
                    ThirdImg.ImageUrl = pl_objDas.objDT.Rows[2]["PHOTOURL"].ToString();
                    ThirdImgLink.NavigateUrl = "/Gallery/GalleryView.aspx?PhotoNo=" + pl_objDas.objDT.Rows[2]["PHOTONO"].ToString();
                    pl_intCount++;
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

        //나의 게시글 리스트
        private void SearchMyPostList()
        {           
            IDas pl_objDas = null;
            int pl_intRecordCnt = 0;
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strBoardTypeCode", DBType.adVarChar, null, 3, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchTitle", DBType.adVarChar, "", 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchTag", DBType.adVarChar, "", 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, 1, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_BOARD_NT_LST");

                pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));


                MyPostList.DataSource = pl_objDas.objDT;
                MyPostList.DataBind();
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


        protected void SearchMyPost_Click(object sender, EventArgs e)
        {
            module.moveURL("/Search/SearchList.aspx?searchType=2&searchQuery=" + strUserID); 
        }

        protected void SearchMyPhoto_Click(object sender, EventArgs e)
        {
            module.moveURL("/Gallery/GalleryList.aspx?SearchID=" + strUserID);
        }

        protected void SearchMyComment_Click(object sender, EventArgs e)
        {
            module.moveURL("/Search/SearchCommentList.aspx?searchType=2&searchQuery=" + strUserID);
        }
    }
}
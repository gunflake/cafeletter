using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace cafeLetter
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected CommonModule module           = new CommonModule();
        protected  int    intPageSize           = 10;
        protected  int    intPageNo             = 1;
        protected string strBoardName           = string.Empty;
        protected string strBoardTypeCode    = string.Empty;
        protected string strSearchQuery         = string.Empty;
        protected string strSearchID            = string.Empty;
        protected string strSearchTitle         = string.Empty;
        protected int    intSearchFlag          = 0;
        protected string strSearchTag   = string.Empty;
        protected int    intOrderFlag   = 1;
        protected string strQueryURL    = string.Empty;
        protected string strhrefURL     = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
           
            //board type check
            if(Request.Params["strBoardTypeCode"] != null)
            {

                strBoardTypeCode = Request.Params["strBoardTypeCode"];
            }
            else
            {
                strBoardTypeCode = "B01";
            }

            if(Request.Params["intPageNo"] != null)
            {
                intPageNo = Convert.ToInt32(Request.Params["intPageNo"]);
            }

            if(Request.Params["intPageSize"] != null)
            {
                intPageSize = Convert.ToInt32(Request.Params["intPageSize"]);
            }

            if(Request.Params["intOrderFlag"] != null)
            {
                intOrderFlag = Convert.ToInt32(Request.Params["intOrderFlag"]);
            }

         

            //Search Condition 
            if(Request.Params["intSearchFlag"] != null && Request.Params["strSearchQuery"] != null)
            {
                intSearchFlag = Convert.ToInt32(Request.Params["intSearchFlag"]);
                strSearchQuery = Request.Params["strSearchQuery"];
            }


            //BoardTypeCode
            if (strBoardTypeCode == null)
            {
                strBoardTypeCode = "B01";
                this.strBoardName = "자유게시판";
            }
            else if (strBoardTypeCode.Equals("B01"))
            {
                this.strBoardName = "자유게시판";
            }else if(strBoardTypeCode.Equals("B02"))
            {
                this.strBoardName = "정보게시판";
            }
            else if (strBoardTypeCode.Equals("B03"))
            {
                this.strBoardName = "공지사항";
            }           
            else
            {
                strBoardTypeCode = "B01";
                this.strBoardName = "자유게시판";
            }

            strhrefURL = "/Board/BoardList.aspx";

            //게시글 리스트 프린트
            module.PostList(strBoardTypeCode, strSearchQuery, intSearchFlag, intOrderFlag, intPageNo, intPageSize, strhrefURL, PostListPanel, PageNumber);

        }


        protected void BoardNewWrite_Click(object sender, ImageClickEventArgs e)
        {

            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다");
                return;
            }
            else if (Session["boardWrite"] != null && Session["boardWrite"].Equals("N"))
            {
                module.PrintAlert("게시글 작성 권한이 없습니다");
                return;
            }
            else
            {
                module.moveURL("/Board/BoardWrite.aspx");
                return;
            }
        }

       

        protected void SearchButton_Click(object sender, EventArgs e)
        {
            //SearchMenu
            string pl_strSearchValue = SearchValue.Text;

            if (SearchMenu.SelectedItem.Text.Equals("아이디"))
            {
                intSearchFlag = 1;
                strSearchQuery = pl_strSearchValue;
            }
            else if (SearchMenu.SelectedItem.Text.Equals("제목"))
            {
                intSearchFlag = 2;
                strSearchQuery = pl_strSearchValue;
            }
            else if (SearchMenu.SelectedItem.Text.Equals("태그"))
            {
                intSearchFlag = 3;
                strSearchQuery = pl_strSearchValue;
            }

            intPageNo = 1;

            module.moveURL("/Board/BoardList.aspx?strBoardTypeCode=" + strBoardTypeCode + "&intPageNo=" + intPageNo + "&intPageSize=" + intPageSize + "&intOrderFlag=" + intOrderFlag+ "&intSearchFlag=" + intSearchFlag + "&strSearchQuery=" + strSearchQuery);
    
        }

        protected void Page10_Click(object sender, EventArgs e)
        {
            
            intPageSize = 10;
            intPageNo = 1;
            module.moveURL("/Board/BoardList.aspx?strBoardTypeCode=" + strBoardTypeCode + "&intPageNo=" + intPageNo + "&intPageSize=" + intPageSize + "&intOrderFlag=" + intOrderFlag);
        }

        protected void Page20_Click(object sender, EventArgs e)
        {
            intPageSize = 20;
            intPageNo = 1;
            module.moveURL("/Board/BoardList.aspx?strBoardTypeCode=" + strBoardTypeCode + "&intPageNo=" + intPageNo + "&intPageSize=" + intPageSize + "&intOrderFlag=" + intOrderFlag);

        }

        protected void Page30_Click(object sender, EventArgs e)
        {
            intPageSize = 30;
            intPageNo = 1;
            module.moveURL("/Board/BoardList.aspx?strBoardTypeCode=" + strBoardTypeCode + "&intPageNo=" + intPageNo + "&intPageSize=" + intPageSize + "&intOrderFlag=" + intOrderFlag);


        }

        protected void Newest_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 1;
            module.moveURL("/Board/BoardList.aspx?strBoardTypeCode=" + strBoardTypeCode + "&intPageNo=" + intPageNo + "&intPageSize=" + intPageSize + "&intOrderFlag=" + intOrderFlag);


        }

        protected void Hot_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 2;
            module.moveURL("/Board/BoardList.aspx?strBoardTypeCode=" + strBoardTypeCode + "&intPageNo=" + intPageNo + "&intPageSize=" + intPageSize + "&intOrderFlag=" + intOrderFlag);


        }

        protected void Comment_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 3;
            module.moveURL("/Board/BoardList.aspx?strBoardTypeCode=" + strBoardTypeCode + "&intPageNo=" + intPageNo + "&intPageSize=" + intPageSize + "&intOrderFlag=" + intOrderFlag);

        }
    }
}
using BOQv7Das_Net;
using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace cafeLetter.Models
{
    public class CommonModule : System.Web.UI.Page
    {
        /// <summary>
        /// 이 모듈을 사용하려면 먼저 웹의 Web.config 파일에서 모듈을
        /// 구성하고 IIS에 등록해야 합니다.
        /// 다음 링크 참조: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //여기에서 코드를 정리합니다.
        }

        public void Init(HttpApplication context)
        {
            // 다음은 LogRequest 이벤트를 처리하여 사용자 지정 로깅 구현을 
            // 이 이벤트에 제공하는 예입니다.
            context.LogRequest += new EventHandler(OnLogRequest);
        }

        #endregion

        public void OnLogRequest(Object source, EventArgs e)
        {
            //사용자 지정 로깅 논리를 여기에 사용할 수 있습니다.
        }

        //세션 저장
        public void saveSession(string strSessionName, string strValue)
        {
            if (strValue != null)
            {
                Session[strSessionName] = strValue;
            }
        }
        
        //세션값 반환
        public string getSession(string strSessionName)
        {
            string returnValue = null;

            if (Session[strSessionName] != null)
            {
                returnValue = Session[strSessionName].ToString();
            }
            return returnValue;
        }

        
        // IDAS 연결
        public IDas ConnetionDB()
        {
            IDas pl_objDas = null;

            try
            {
                pl_objDas = new IDas();
                pl_objDas.Open("10.10.120.20:33444");

            }
            catch
            {
                Response.Write(@"<script>alert('IDas 연결 오류');</script>");
            }
            

            return pl_objDas;
        }


        //alert 창 
        public void PrintAlert(string msg)
        {
            HttpContext.Current.Response.Write(@"<script>alert('" + msg + "');</script>");
        }

        public void moveURL(string url)
        {
            HttpContext.Current.Response.Write(@"<script> location.href='" + url + "';</script>");
        }

        //alert + url
        public void PrintAlert(string msg, string url)
        {
            HttpContext.Current.Response.Write(@"<script>alert('" + msg + "'); location.href='" + url + "';</script>");
        }

        //confirm + url 창
        public void PrintConfirm(string msg, string URL)
        {
            HttpContext.Current.Response.Write(@"<script language=JavaScript> if(confirm('"+ msg + "')){ location.href='"+URL+"'; } </script> ");
        }

        public void PrintConfirm(string msg, string trueURL, string falseURL)
        {
            HttpContext.Current.Response.Write(@"<script language=JavaScript> if(confirm('" + msg + "')){ location.href='" + trueURL + "'; }else{ location.href='" + falseURL+  "'; } </script> ");
        }


        //confirm 창
        public void PrintConfirm(string msg)
        {
            HttpContext.Current.Response.Write(@"<script language=JavaScript>confirm('" + msg + "'); </script>; ");
        }

        //페이징 처리
        public void Pagination(int intRecortCnt, int intPageNo, int intPageSize, string hrefURL, string hrefParam, HtmlGenericControl PageNumber)
        {
            int pl_intFirst = intPageNo - (intPageNo % 5) + 1;
            int pl_intNextFirst = 0;
            int pl_pageTotalCnt = intRecortCnt / intPageSize + 1;
            int pl_intCnt = 5;

            PageNumber.Controls.Clear();

            //나누어 떨어질 때
            if (intRecortCnt % intPageSize == 0)
            {
                pl_pageTotalCnt--;
            }
            if (intPageNo != 0 && intPageNo % 5 == 0)
            {
                pl_intFirst -= 5;
            }

            pl_intNextFirst = pl_intFirst + 5;

            //현재 페이지가 1번이 아니라면 < 추가
            if (intPageNo > 1)
            {

                int pl_intPrevious = pl_intFirst - 5;
                if (pl_intPrevious < 1)
                {
                    pl_intPrevious = 1;
                }

                HtmlGenericControl li = new HtmlGenericControl("li");
                PageNumber.Controls.Add(li);
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.Attributes.Add("href", hrefURL + "?intPageNo=" + pl_intPrevious + "&intPageSize=" + intPageSize + hrefParam);
                anchor.InnerText = "<";
                li.Controls.Add(anchor);

                HtmlGenericControl prev = new HtmlGenericControl("li");
                PageNumber.Controls.Add(prev);
                HtmlGenericControl anchorp = new HtmlGenericControl("a");
                anchorp.Attributes.Add("href", hrefURL + "?intPageNo=" + (intPageNo - 1) + "&intPageSize=" + intPageSize + hrefParam);
                anchorp.InnerText = "Prev";
                prev.Controls.Add(anchorp);
            }


            //페이지 5개 처리
            while (pl_intFirst <= pl_pageTotalCnt && pl_intCnt > 0)
            {
                HtmlGenericControl li = new HtmlGenericControl("li");

                if (pl_intFirst == intPageNo)
                {
                    li.Attributes.Add("class", "active");
                }

                PageNumber.Controls.Add(li);
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.Attributes.Add("href", hrefURL + "?intPageNo=" + pl_intFirst + "&intPageSize=" + intPageSize + hrefParam);
                anchor.InnerText = pl_intFirst.ToString();
                li.Controls.Add(anchor);
                pl_intFirst++;
                pl_intCnt--;
            }

            //  > NEXT 처리
            if (intPageNo < pl_pageTotalCnt)
            {
                if (pl_intNextFirst > pl_pageTotalCnt)
                {
                    pl_intNextFirst = pl_pageTotalCnt;
                }

                HtmlGenericControl next = new HtmlGenericControl("li");
                PageNumber.Controls.Add(next);
                HtmlGenericControl anchorp = new HtmlGenericControl("a");
                anchorp.Attributes.Add("href", hrefURL + "?intPageNo=" + (intPageNo + 1) + "&intPageSize=" + intPageSize + hrefParam);
                anchorp.InnerText = "Next";
                next.Controls.Add(anchorp);

                HtmlGenericControl li = new HtmlGenericControl("li");
                PageNumber.Controls.Add(li);
                HtmlGenericControl anchor = new HtmlGenericControl("a");
                anchor.Attributes.Add("href", hrefURL + "?intPageNo=" + pl_intNextFirst + "&intPageSize=" + intPageSize + hrefParam);
                anchor.InnerText = ">";
                li.Controls.Add(anchor);
            }
        }

        //게시판 리스트
        //게시판 타입, 검색ID, 검색 제목, 검색 태그, 정렬번호, 페이지번호, 페이즈사이즈, Paging URL, Paging Parameter
        public void PostList(string strBoardTypeCode, string strSearchQuery , int intSearchFlag, int intOrderFlag, int intPageNo, int intPageSize, string strhrefURL, Repeater postListPanel, HtmlGenericControl PageNumber)
        {

            IDas pl_objDas = null;
            pl_objDas = ConnetionDB();

            // intOrderFlag 1 = 등록한 순서, 2 = 조회수 3 = 댓글 수 

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strBoardTypeCode", DBType.adVarChar, strBoardTypeCode, 3, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchQuery", DBType.adVarChar, strSearchQuery, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intSearchFlag", DBType.adInteger, intSearchFlag, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intOrderFlag", DBType.adInteger, intOrderFlag, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_POSTTOTAL_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));

                string hrefParam = "&intOrderFlag=" + intOrderFlag;

                if (!strBoardTypeCode.Equals(string.Empty))
                {
                    hrefParam += "&strBoardTypeCode=" + strBoardTypeCode;
                }

                if(!strSearchQuery.Equals(string.Empty) && intSearchFlag != 0)
                {
                    hrefParam += "&intSearchFlag=" + intSearchFlag + "&strSearchQuery=" + strSearchQuery;
                }
               

                Pagination(pl_intRecordCnt, intPageNo, intPageSize, strhrefURL, hrefParam, PageNumber);
                postListPanel.DataSource = pl_objDas.objDT;
                postListPanel.DataBind();

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

using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Admin
{
    public partial class UserCashList : System.Web.UI.Page
    {
        private string strUserID = string.Empty;
        protected int intPageSize = 10;
        protected int intPageNo = 1;
        protected int intMyCash = 0;
        protected int intRealCash = 0;
        protected int intBonusCash = 0;
        protected string strFilePath = string.Empty;
        private CommonModule objModule = new CommonModule();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            //if (objModule.getSession("userID") == null)
            //{
            //    objModule.saveSession("beforeURL", "/Cash/MyCashInfo.aspx");
            //    objModule.PrintAlert("로그인이 필요합니다", "/Member/Login.aspx");
            //    return;
            //}
            //strUserID = objModule.getSession("userID");
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["intPageNo"] != null)
            {
                intPageNo = Convert.ToInt32(Request.Params["intPageNo"]);
            }

            if (Request.Params["intPageSize"] != null)
            {
                intPageSize = Convert.ToInt32(Request.Params["intPageSize"]);
            }

            //나의 캐시 충전리스트
            MyCashInfoDB();
        }

        


        //나의 캐시 충전 리스트
        private void MyCashInfoDB()
        {
            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, "", 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.SetQuery("dbo.UP_CASH_MY_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));


                MyCashList.DataSource = pl_objDas.objDT;
                MyCashList.DataBind();


                string hrefParam = string.Empty;
                string hrefURL = "/Admin/UserCashList.aspx";


                objModule.Pagination(pl_intRecordCnt, intPageNo, intPageSize, hrefURL, hrefParam, PageNumber);

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

        public Boolean UploadFile()
        {
            int pl_intRandomNum = 0;
            string a = FileUpload.FileName;
            try
            {
                pl_intRandomNum = new Random().Next(100000);
                strFilePath = string.Concat("/file/", pl_intRandomNum, FileUpload.FileName);
                FileUpload.SaveAs(Server.MapPath(strFilePath));
                return true;
            }
            catch
            {
                return false;
            }
        }

        protected void hideIssue_Click(object sender, EventArgs e)
        {
            if (UploadFile())
            {
                objModule.PrintAlert("업로드성공");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "excelDataParse", "excelDataParse()", true);
            }
            else
            {
                objModule.PrintAlert("업로드실패");
            }
        }
    }
}
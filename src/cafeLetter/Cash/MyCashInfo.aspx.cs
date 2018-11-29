using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Cash
{
    public partial class MyCashInfo : System.Web.UI.Page
    {
        private string strUserID = string.Empty;
        protected int intPageSize = 10;
        protected int intPageNo = 1;
        protected string intMyCash = string.Empty;
        protected string intRealCash = string.Empty;
        protected string intBonusCash =string.Empty;
        private CommonModule objModule = new CommonModule();

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (objModule.getSession("userID") == null)
            {
                objModule.saveSession("beforeURL", "/Cash/MyCashInfo.aspx");
                objModule.PrintAlert("로그인이 필요합니다", "/Member/Login.aspx");
                return;
            }
            strUserID = objModule.getSession("userID");
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

            //내가 보유한 캐시 조회
            MyCashDB();

            //나의 캐시 충전리스트
            MyCashInfoDB();
        }

        //내가 보유한 캐시 조회
        private void MyCashDB()
        {
            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intMyCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRealCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intBonusCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_MY_NT_GET");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    intMyCash =    pl_objDas.GetParam("@po_intMyCash");
                    intBonusCash = pl_objDas.GetParam("@po_intBonusCash");
                    intRealCash =  pl_objDas.GetParam("@po_intRealCash");
                    return;
                }
                else
                {
                    objModule.PrintAlert(pl_strOutputMsg, "/Item/ItemList.aspx");
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


        //나의 캐시 충전 리스트
        private void MyCashInfoDB()
        {
            IDas pl_objDas = objModule.ConnetionDB();

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.SetQuery("dbo.UP_CASH_MY_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));


                MyCashList.DataSource = pl_objDas.objDT;
                MyCashList.DataBind();


                string hrefParam = string.Empty;
                string hrefURL = "/Cash/MyCashInfo.aspx";


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

    }
}
using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter
{
    public partial class NavigationBar : System.Web.UI.MasterPage
    {
        protected CommonModule objModule = new CommonModule();
        public string userID;
        protected int intMyCash = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            // session check
            if (Session["userID"] != null)
            {
                userID = Session["userID"].ToString();
                signInTab();
                MyCashDB();

                if (!Session["userRank"].Equals("회원"))
                {
                    ShowAdminTab();
                }

            }
            else
            {
                sighOutTab();
            }

            

            

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
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, userID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intMyCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRealCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intBonusCash", DBType.adVarChar, "", 30, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_CASH_MY_NT_GET1");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    intMyCash = Convert.ToInt32(pl_objDas.GetParam("@po_intMyCash"));
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

        protected void ShowAdminTab()
        {
            AdminTab.Visible = true;
        }

        protected void signInTab()
        {
            MyCash.Visible = true;
            MyInfo.Visible = true;
            Login.Visible = false;
            SignUp.Visible = false;
            Logout.Visible = true;
        }

        protected void sighOutTab()
        {
            MyCash.Visible = false;
            MyInfo.Visible = false;
            Login.Visible = true;
            SignUp.Visible = true;
            Logout.Visible = false;
        }

        

    }
}
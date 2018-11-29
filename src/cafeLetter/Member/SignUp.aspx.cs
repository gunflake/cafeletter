using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace cafeLetter
{
    public partial class SignUp : System.Web.UI.Page
    {

        protected CommonModule module = new CommonModule();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(Session["user"] != null)
            {
                module.PrintAlert("이미 로그인이 하셨습니다", "/Home.aspx");
            }
            
        }

        //회원가입 버튼 클릭
        protected void signUp_click(object sender, EventArgs e)
        {
            //로그인 DB 처리
            if (!SignUpDB())
            {
                return;
            }

            //USERRANK 삽입
            if (!UserRankInsert())
            {
                return;
            }

            module.PrintAlert("회원가입이 되었습니다.", "/Member/Login.aspx");

        }


    

        //USERRANK 삽입
        private bool UserRankInsert()
        {
            IDas pl_objDas = null;
            string pl_strUserID = string.Empty;
            string pl_strErrMsg = string.Empty;
            int pl_intRetVal = 0;
            bool pl_boolResult = false;

            try
            {
                pl_strUserID = userID.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;


                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, pl_strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERRANK_TX_INS");

                pl_strErrMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));



                if (pl_intRetVal == 0)
                {
                    pl_boolResult = true;
                }
                else
                {
                    module.PrintAlert(pl_strErrMsg, "/Member/SignUp.aspx");
                }

            }
            catch
            {
                module.PrintAlert(pl_strErrMsg, "/Member/SignUp.aspx");
            }
            finally
            {
                if (pl_objDas != null)
                {
                    pl_objDas.Close();
                    pl_objDas = null;
                }
            }

            return pl_boolResult;
        }

        

        protected bool SignUpDB()
        {
            bool pl_boolResult = false;
            string pl_strUserID = string.Empty;
            string pl_strUserPW = string.Empty;
            string pl_strUserEmail = string.Empty;
            int pl_intRetVal = 0;

            string pl_strErrMsg = null;
            IDas pl_objDas = null;

            try
            {
                pl_strUserID = userID.Text;
                pl_strUserPW = userPW.Text;
                pl_strUserEmail = userEmail.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, pl_strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserPW", DBType.adVarWChar, pl_strUserPW, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserPW", DBType.adVarWChar, pl_strUserEmail, 30, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USER_TX_INS");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));
                pl_strErrMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));


                if (pl_intRetVal == 0)
                {
                    pl_boolResult =  true;
                }
                else
                {
                    module.PrintAlert(pl_strErrMsg, "/Member/SignUp.aspx");
                }

            }
            catch
            {
                module.PrintAlert(pl_strErrMsg , "/Member/SignUp.aspx");
            }
            finally
            {
                if (pl_objDas != null)
                {
                    pl_objDas.Close();
                    pl_objDas = null;
                }
            }

            return pl_boolResult;
        }

   
    }
}
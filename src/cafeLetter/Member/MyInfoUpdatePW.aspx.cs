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
    public partial class MyInfoUpdatePW : System.Web.UI.Page
    {
        protected string strUserID = string.Empty;
        protected CommonModule module = new CommonModule();


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
                module.PrintAlert("로그인이 필요합니다", "/Home.aspx");
                return;
            }
        }

        protected void ChangePW_Click(object sender, EventArgs e)
        {
            if (!CheckCurrentPW())
            {
                BeforePW.Text = "";
                NewPW.Text = "";
                CheckPW.Text = "";
                return;
            }
            UpdatePW();
            BeforePW.Text = "";
            NewPW.Text = "";
            CheckPW.Text = "";
        }


        //새 비밀번호 업데이트
        private void UpdatePW()
        {
            IDas pl_objDas = null;
            string pl_strNewPW = string.Empty;
            try
            {
                pl_strNewPW = NewPW.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserPW", DBType.adVarWChar, pl_strNewPW, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERPW_TX_UPD");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("비밀번호가 변경되었습니다", "/Member/MyInfo.aspx");
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/Member/MyInfo.aspx");
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

        //기존 비밀번호가 일치하는지 확인
        protected Boolean CheckCurrentPW()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            string pl_strMsgErr = string.Empty;
            string pl_strGetPW = string.Empty;
            string pl_beforePW = string.Empty;
            //GetPW
            try
            {
                pl_beforePW = BeforePW.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strCurrentPW", DBType.adVarChar, pl_beforePW, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strMsgErr", DBType.adVarWChar, "", 256, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERPW_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));
                pl_strMsgErr = Convert.ToString(pl_objDas.GetParam("@po_strMsgErr"));

                if (pl_intRetVal != 0)
                {
                    module.PrintAlert(pl_strMsgErr);
                    return false;
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


            return true;
        }
    }
}
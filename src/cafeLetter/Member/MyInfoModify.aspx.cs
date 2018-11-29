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
    public partial class MyInfoModify : System.Web.UI.Page
    {
        protected string strUserID    = string.Empty;
        protected string strUserEmail = string.Empty;
        protected string strSex       = string.Empty;
        protected string strName      = string.Empty;
        protected string strLocation  = string.Empty;
        protected string strBirthday  = string.Empty;
        protected string strSNS       = string.Empty;
        protected string strIntroduce = string.Empty;
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
                strUserID = null;
            }

            if (!IsPostBack)
            {
                ReadMyInfo();
            }
        }

        private void ReadMyInfo()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERINFO_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    module.PrintAlert("회원 정보를 읽을 수 없습니다", "/Member/MyInfo.aspx");
                    return;
                }

                strUserEmail = pl_objDas.objDT.Rows[0]["EMAIL"].ToString();
                InputName.Text      = pl_objDas.objDT.Rows[0]["USERNAME"].ToString();
                InputLocation.Text  = pl_objDas.objDT.Rows[0]["LOCATION"].ToString();
                InputBirthday.Text  = pl_objDas.objDT.Rows[0]["BIRTHDAY"].ToString();
                InputSNS.Text = pl_objDas.objDT.Rows[0]["SNS"].ToString();

                InputIntroduce.Text = pl_objDas.objDT.Rows[0]["INTRODUCE"].ToString();
                strSex       = pl_objDas.objDT.Rows[0]["GENDER"].ToString();


                
                if (strSex.Equals("W"))
                {
                    InputSexW.Checked= true;                   
                }
                else
                {
                    InputSexM.Checked = true;                   
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

        protected void InfoModify_Click(object sender, EventArgs e)
        {
            InfoModifyUpdate();
        }

        private void InfoModifyUpdate()
        {
            IDas pl_objDas = null;

            try
            {
                strName = InputName.Text;
                strLocation = InputLocation.Text;
                strBirthday = InputBirthday.Text;
                strSNS = InputSNS.Text;
                strIntroduce = InputIntroduce.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strName", DBType.adVarWChar, strName, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strLocation", DBType.adVarWChar, strLocation, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSex", DBType.adVarChar, strSex, 1, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strBirthday", DBType.adVarWChar, strBirthday, 10, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSNS", DBType.adVarWChar, strSNS, 50, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_strIntroduce", DBType.adVarWChar, strIntroduce, 200, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USERINFO_TX_UPD");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("회원 정보가 수정되었습니다.", "/Member/MyInfo.aspx");
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

        protected void Sex_CheckedChanged(object sender, EventArgs e)
        {
            if (InputSexM.Checked)
            {
                strSex = "M";
            }
            else
            {
                strSex = "W";
            }
        }
    }
}
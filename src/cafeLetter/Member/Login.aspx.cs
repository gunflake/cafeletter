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
    public partial class login : System.Web.UI.Page
    {
        protected CommonModule module = new CommonModule();
        string strUserID = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        //로그인 클릭 
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            strUserID = ID.Text;

            if (!LoginDB())
            {
                return;
            }


            if (!SaveSession())
            {
                return;
            }

            if(module.getSession("beforeURL") == null || module.getSession("beforeURL").Length < 3)
            {
                module.PrintAlert("로그인 되었습니다", "/Home.aspx");
                return;
            }
            string moveURL = module.getSession("beforeURL");
            Session["beforeURL"] = null;
            module.PrintAlert("로그인 되었습니다", moveURL);
        }


        //SESSION 저장
        private bool SaveSession()
        {
            bool pl_boolResult = false;
            IDas pl_objDas = null;
            string pl_strErrMsg = string.Empty;
            int pl_intRetVal = 0;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;


                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intBoardCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intCommentCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USER_NT_GET");

                pl_strErrMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));



                if (pl_intRetVal == 0)
                {
                    module.saveSession("userID", pl_objDas.objDT.Rows[0]["USERID"].ToString());
                    module.saveSession("userRank", pl_objDas.objDT.Rows[0]["USERRANK"].ToString());
                    module.saveSession("userSupervise", pl_objDas.objDT.Rows[0]["USERAUTHORITY"].ToString());
                    module.saveSession("boardSupervise", pl_objDas.objDT.Rows[0]["BOARDAUTHORITY"].ToString());
                    module.saveSession("photoSupervise", pl_objDas.objDT.Rows[0]["PHOTOAUTHORITY"].ToString());
                    module.saveSession("boardWrite", pl_objDas.objDT.Rows[0]["BOARDWRITE"].ToString());
                    module.saveSession("galleryWrite", pl_objDas.objDT.Rows[0]["GALLERYWRITE"].ToString());

                    pl_boolResult = true;
                }
                else
                {
                    module.PrintAlert(pl_strErrMsg, "/Member/Login.aspx");
                }

            }
            catch(Exception e)
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

            return pl_boolResult;
        }

        //LOGIN DB 처리 
        protected bool LoginDB()
        {
            IDas pl_objDas = null;
            string pl_strUserID = string.Empty;
            string pl_strUserPW = string.Empty;
            string pl_strErrMsg = string.Empty;
            int pl_intRetVal = 0;
            bool pl_boolResult = false;

            try
            {
                pl_strUserID = ID.Text;
                pl_strUserPW = PW.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;


                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, pl_strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserPW", DBType.adVarWChar, pl_strUserPW, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_USER_NT_CHK");

                pl_strErrMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));


                if (pl_intRetVal == 0)
                {
                    pl_boolResult = true;
                }
                else
                {
                    module.PrintAlert(pl_strErrMsg, "/Member/Login.aspx");
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

            return pl_boolResult;
        }
    }
}
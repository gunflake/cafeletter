using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Member
{
    public partial class LogOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userID"] != null)
            {
                Session.Clear();
            }

            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('로그아웃 되었습니다.'); location.href='/Home.aspx';</script>; ");
        }
    }
}
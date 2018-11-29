using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter
{
    public partial class SidebarMember : System.Web.UI.MasterPage
    {
        public string userID;

        protected void Page_Load(object sender, EventArgs e)
        {
            // session check
            if (Session["userID"] != null)
            {
                userID = Session["userID"].ToString();
            }
            else
            {
                userID = null;
            }
        }
    }
}
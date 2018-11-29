using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Item
{
    public partial class BuyResult : System.Web.UI.Page
    {
        protected int intOrderAmount = 0;
        protected int intShipAmount = 0;
        protected int intTotalAmount = 0;
        protected string strBuyName = string.Empty;
        CommonModule objModule = new CommonModule();

        
        protected void Page_PreInit(object sender, EventArgs e)
        {

            if (objModule.getSession("userID") == null)
            {
                objModule.PrintAlert("잘못된 접근입니다", "/Home.aspx");
                return;
            }

        }


        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(Request.Params["intOrderAmount"] == null || Request.Params["intShipAmount"] ==null || Request.Params["intTotalAmount"] == null || Request.Params["strBuyName"] ==null)
            {
                objModule.PrintAlert("잘못된 접근입니다", "/Home.aspx");
                return;
            }
            intOrderAmount = Convert.ToInt32(Request.Params["intOrderAmount"]);
            intShipAmount = Convert.ToInt32(Request.Params["intShipAmount"]);
            intTotalAmount = Convert.ToInt32(Request.Params["intTotalAmount"]);
            strBuyName = Request.Params["strBuyName"];
        }

        protected void BuyInfo_Click(object sender, EventArgs e)
        {
            objModule.moveURL("/Member/BuyList.aspx");
        }
    }
}
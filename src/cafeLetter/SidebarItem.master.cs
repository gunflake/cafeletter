using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace cafeLetter
{
    public partial class SidebarItem : System.Web.UI.MasterPage
    {
        CommonModule module = new CommonModule();
        protected void Page_Load(object sender, EventArgs e)
        {
            ItemListPrint();
        }

        private void ItemListPrint()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            HtmlGenericControl li = null;
            HtmlGenericControl anchor = null;

            //BoardView
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ITEMTYPE_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));


                for (int i = 0; i < pl_objDas.RecordCount; i++)
                {
                    string strItemTypeName = pl_objDas.objDT.Rows[i]["ITEMTYPENAME"].ToString();
                    string strItemCode = pl_objDas.objDT.Rows[i]["ITEMCODE"].ToString();
                    li = new HtmlGenericControl("li");
                    ItemListShow.Controls.Add(li);
                    anchor = new HtmlGenericControl("a");
                    anchor.Attributes.Add("href", "/Item/ItemList.aspx?strItemCode=" + strItemCode);
                    anchor.InnerText = strItemTypeName;
                    li.Controls.Add(anchor);
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
    }
}
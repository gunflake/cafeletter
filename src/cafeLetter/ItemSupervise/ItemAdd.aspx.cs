using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.ItemSupervise
{
    public partial class ItemAdd : System.Web.UI.Page
    {
        CommonModule module = new CommonModule();
        private string strUserID = string.Empty;
        private string strFilePath = string.Empty;

        //권한 체크
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Session["userID"] == null || !Session["userRank"].Equals("마스터"))
            {
                if (Session["userID"] != null && Session["userRank"].Equals("운영진"))
                {
                    module.PrintAlert("권한이 없습니다.", "/Admin/AdminInfo.aspx");
                }
                else
                {
                    module.PrintAlert("권한이 없습니다.", "/Home.aspx");
                }
                return;
            }

            strUserID = Session["userID"].ToString();
        }

        
        protected void Page_Load(object sender, EventArgs e)
        {
            ListItemTypeCodeAdd();
        }

        //ITEMCODE 추가
        private void ListItemTypeCodeAdd()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ITEMTYPE_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0 || pl_objDas.RecordCount == 0)
                {
                    module.PrintAlert("아이템 상세보기 조회에 실패했습니다", "/ItemSupervise/ItemList.aspx?strItemCode=I01");
                    return;
                }

                for(int i = 0; i < pl_objDas.RecordCount; i++)
                {
                    ItemCode.Items.Add(new ListItem(pl_objDas.objDT.Rows[i]["ITEMTYPENAME"].ToString(), pl_objDas.objDT.Rows[i]["ITEMCODE"].ToString()));
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

      
        //ITEM 추가 버튼
        protected void ItemAddBtn_Click(object sender, EventArgs e)
        {
            string pl_strItemCode = ItemCode.SelectedItem.Value;
            string pl_photoName = FileUpload.FileName;

            if (pl_photoName.Contains(".png") || pl_photoName.Contains(".jpg") || pl_photoName.Contains(".jpeg") || pl_photoName.Contains(".PNG") || pl_photoName.Contains(".bmp"))
            {
                if (UploadFile())
                {
                    ItemAddDB(pl_strItemCode);
                }
                else
                {
                    module.PrintAlert("파일 업로드에 실패했습니다");
                    return;
                }
            }
            else
            {
                module.PrintAlert("사진 형식을 확인해주세요. 사진은 꼭 1장 등록해야 등록가능합니다. (.png, .jpeg, .png .bmp 형식만 업로드 가능합니다");
                return;
            }
        }

        //ITEM 추가 DB
        private void ItemAddDB(string strItemCode)
        {
            string pl_strItemName = string.Empty;
            string pl_strItemDesc = string.Empty;
            int pl_intItemOrgPrice = 0;
            int pl_intItemCount = 0;
            IDas pl_objDas = null;

            try
            {

                pl_strItemName = ItemName.Text;
                pl_intItemOrgPrice = Convert.ToInt32(ItemOrgPrice.Text);
                pl_intItemCount     = Convert.ToInt32(ItemCount.Text);
                pl_strItemDesc = ItemDesc.Text;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_strItemName",      DBType.adVarWChar, pl_strItemName,      100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strItemCode",      DBType.adVarChar,  strItemCode,         3,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intItemPrice",     DBType.adInteger,  pl_intItemOrgPrice,  0,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intItemCount",     DBType.adInteger,  pl_intItemCount,     0,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strPhotoURL",      DBType.adVarChar,  strFilePath,         100, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_strItemDesc",      DBType.adVarWChar, pl_strItemDesc,      100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID",        DBType.adVarChar,  strUserID,           20,  ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg",        DBType.adVarWChar, "",                  256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal",        DBType.adInteger,  0,                   0,   ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ITEM_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("물품이 추가되었습니다", "/ItemSupervise/ItemList.aspx?strItemCode="+ strItemCode);
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/ItemSupervise/ItemList.aspx?strItemCode=I01");
                    return;
                }
            }
            catch
            {
                module.PrintAlert("물품 등록에 실패했습니다", "/ItemSupervise/ItemList.aspx?strItemCode=I01");
                return;
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

        //취소버튼
        protected void ItemAddCancel_Click(object sender, EventArgs e)
        {
            module.moveURL("/ItemSupervise/ItemList.aspx?strItemCode=I01");
        }

        //사진 업로드
        private Boolean UploadFile()
        {
            int pl_intRandomNum = 0;
            try
            {
                pl_intRandomNum = new Random().Next(100000);
                strFilePath = string.Concat("/ItemPhoto/", pl_intRandomNum, FileUpload.FileName);
                FileUpload.SaveAs(Server.MapPath(strFilePath));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
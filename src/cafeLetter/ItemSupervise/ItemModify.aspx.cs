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
    public partial class ItemModify : System.Web.UI.Page
    {
        CommonModule module = new CommonModule();
        private int intItemNo = 0;
        private string strItemCode = string.Empty;
        private string strUserID = string.Empty;
        private string strFilePath = string.Empty;

        //권한체크
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

            if (Request.Params["intItemNo"] == null || Request.Params["strItemCode"] == null)
            {
                module.PrintAlert("잘못된 접근입니다", "/ItemSupervise/ItemList.aspx?strItemCode=I01");
                return;
            }

            intItemNo = Convert.ToInt32(Request.Params["intItemNo"]);
            strItemCode = Request.Params["strItemCode"];
            strUserID = Session["userID"].ToString();
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetItemInfoDB();
            }

        }

        private void GetItemInfoDB()
        {
            IDas pl_objDas = module.ConnetionDB();
            // 페이즈 사이즈 받아오기
            try
            {
                pl_objDas.CommandType = CommandType.StoredProcedure;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_intItemNo", DBType.adInteger, intItemNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strItemCode", DBType.adVarChar, "", 3, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchQuery", DBType.adVarWChar, "", 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intSearchFlag", DBType.adInteger, 0, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, 1, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, 1, 0, ParameterDirection.Input);

                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);
                pl_objDas.SetQuery("dbo.UP_ITEM_NT_LST");

                int pl_intRecordCnt = 0;
                pl_intRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));

                if(pl_objDas.RecordCount != 1)
                {
                    module.PrintAlert("물품이 없습니다", "/ItemSupervise/ItemList.aspx");
                    return;
                }

                ItemNo.Value = intItemNo.ToString();
                ItemName.Text = pl_objDas.objDT.Rows[0]["ITEMNAME"].ToString();
                ItemCode.Text = pl_objDas.objDT.Rows[0]["ITEMTYPENAME"].ToString();
                ItemOrgPrice.Text = pl_objDas.objDT.Rows[0]["ITEMPRICE"].ToString();
                ItemImg.Value = pl_objDas.objDT.Rows[0]["ITEMIMG"].ToString();
                ItemDesc.Text = pl_objDas.objDT.Rows[0]["ITEMDESC"].ToString();

                //판매여부 체크
                if (pl_objDas.objDT.Rows[0]["SALESTATE"].ToString().Equals("Y")){
                    SaleTrue.Checked = true;
                }
                else
                {
                    SaleFalse.Checked = true;
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
        
        //수정 버튼
        protected void ItemModifyBtn_Click(object sender, EventArgs e)
        {
            string pl_photoName = FileUpload.FileName;
            //사진이 추가되지 않았으면
            if (pl_photoName.Length > 0)
            {
                if (pl_photoName.Contains(".png") || pl_photoName.Contains(".jpg") || pl_photoName.Contains(".jpeg") || pl_photoName.Contains(".PNG") || pl_photoName.Contains(".bmp"))
                {
                    if (UploadFile())
                    {
                        ItemModifyDB();
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
            else
            {
                ItemModifyDB();
            }

        }

        //수정 DB
        private void ItemModifyDB()
        {
            int pl_intItemOrgPrice = 0;
            string pl_strItemImg = string.Empty;
            string pl_strSaleState = "Y";
            string pl_strItemName = string.Empty;
            string pl_strItemDesc = string.Empty;
            IDas pl_objDas = null;
        
            try
            {
                //판매상태 확인
                if (SaleFalse.Checked)
                {
                    pl_strSaleState = "N";
                }

                pl_strItemName = ItemName.Text;
                pl_strItemDesc = ItemDesc.Text;

                intItemNo = Convert.ToInt32(ItemNo.Value);
                pl_intItemOrgPrice = Convert.ToInt32(ItemOrgPrice.Text);

                pl_strItemImg = ItemImg.Value;

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_intItemNo",        DBType.adVarWChar, intItemNo,            0,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intItemOrgPrice",  DBType.adInteger,  pl_intItemOrgPrice,   0,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strItemName",      DBType.adVarWChar, pl_strItemName,     100,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSaleState",     DBType.adVarChar,  pl_strSaleState,      1,   ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strItemDesc",      DBType.adVarWChar, pl_strItemDesc,      100, ParameterDirection.Input);
                                                                                                  
                pl_objDas.AddParam("@pi_strPhotoURL",      DBType.adVarChar, pl_strItemImg,         100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID",        DBType.adVarChar,  strUserID,            20,  ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg",        DBType.adVarWChar, "",                   256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal",        DBType.adInteger,  0,                    0,   ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ITEM_TX_UPD");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("물품이 수정되었습니다", "/ItemSupervise/ItemList.aspx?strItemCode=" + strItemCode);
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/ItemSupervise/ItemList.aspx?strItemCode=" + strItemCode);
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


        private Boolean UploadFile()
        {
            int pl_intRandomNum = 0;
            try
            {
                pl_intRandomNum = new Random().Next(100000);
                strFilePath = string.Concat("/ItemPhoto/", pl_intRandomNum, FileUpload.FileName);
                FileUpload.SaveAs(Server.MapPath(strFilePath));
                ItemImg.Value = strFilePath;
                return true;
            }
            catch
            {
                return false;
            }
        }


        //삭제 버튼
        protected void ItemDeleteBtn_Click(object sender, EventArgs e)
        {
            ItemDeleteDB();
        }

        //삭제 DB
        private void ItemDeleteDB()
        {
            IDas pl_objDas = null;

            try
            {
                intItemNo = Convert.ToInt32(ItemNo.Value);

                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;

                pl_objDas.AddParam("@pi_intItemNo", DBType.adInteger, intItemNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strUserID", DBType.adVarChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_ITEM_TX_DEL");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("물품이 삭제되었습니다", "/ItemSupervise/ItemList.aspx?strItemCode=" + strItemCode);
                    return;
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/ItemSupervise/ItemList.aspx?strItemCode=" + strItemCode);
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
    }
}
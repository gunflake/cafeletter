using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Gallery
{
    public partial class GalleryModify : System.Web.UI.Page
    {
        string strUserID = string.Empty;
        protected CommonModule module      = new CommonModule();
        private   int          intPhotoNo  = 0;
        protected string       strPhotoURL = string.Empty;

        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (module.getSession("userID") == null)
            {
                module.PrintAlert("로그인이 필요합니다", "/Gallery/GalleryList.aspx");
                return;
            }
            else if (module.getSession("galleryWrite").Equals("N"))
            {
                module.PrintAlert("갤러리 작성 권한이 없습니다", "/Gallery/GalleryList.aspx");
                return;
            }
            else
            {
                strUserID = module.getSession("userID");
            }
            if (Request.Params["PhotoNo"] == null)
            {
                module.PrintAlert("잘못된 접근입니다.", "/Gallery/GalleryList.aspx");
                return;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            intPhotoNo = Convert.ToInt32(Request.Params["PhotoNo"]);
            
            if (!IsPostBack)
            {
                PhotoView();
            }
        }

        //갤러리 상세 보기
        private void PhotoView()
        {
            IDas pl_objDas = null;
            int pl_intRetVal = 0;
            //BoardView
            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intPhotoNo", DBType.adInteger, intPhotoNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTO_NT_GET");

                pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal != 0)
                {
                    module.PrintAlert("갤러리 상세보기 실패", "/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
                }

                GalleryTitle.Text = pl_objDas.objDT.Rows[0]["PHOTOTITLE"].ToString();
                GalleryTags.Text = pl_objDas.objDT.Rows[0]["PHOTOTAG"].ToString();
                strPhotoURL = pl_objDas.objDT.Rows[0]["PHOTOURL"].ToString();
                HiddenUrl.Text = strPhotoURL;
            }
            catch
            {

            }
            finally
            {
                pl_objDas.Close();
            }
        }

        protected void GalleryModify_Click(object sender, EventArgs e)
        {
            GalleryModfyDB();
        }

        private void GalleryModfyDB()
        {
            string pl_strTitle = GalleryTitle.Text;
            string pl_strTags = GalleryTags.Text;
            string pl_strURL = HiddenUrl.Text;
            IDas pl_objDas = null;

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_intBoardNo", DBType.adInteger, intPhotoNo, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTitle", DBType.adVarWChar, pl_strTitle, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strURL", DBType.adVarWChar, pl_strURL, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTag", DBType.adVarWChar, pl_strTags, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);
                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTO_TX_UPD");


                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("사진이 수정되었습니다", "/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
                }
                else
                {
                    module.PrintAlert("사진 수정에 실패했습니다", "/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
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

        

        protected void GalleryCancel_Click(object sender, EventArgs e)
        {
            module.moveURL("/Gallery/GalleryView.aspx?PhotoNo=" + intPhotoNo);
        }


        //사진 수정 버튼 눌렀을 때 수정된 사진으로 바뀜
        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            string pl_photoName = FileUpload.FileName;


            if (pl_photoName.Contains(".PNG")  || pl_photoName.Contains(".png") || pl_photoName.Contains(".jpg") || pl_photoName.Contains(".jpeg") || pl_photoName.Contains(".png") || pl_photoName.Contains(".bmp"))
            {
                if (UploadFile())
                {
                    module.PrintAlert("사진이 업로드 되었습니다");
                }
                else
                {
                    module.PrintAlert("사진 업로드에 실패했습니다", "/Gallery/GalleryModify.aspx?PhotoNo=" + intPhotoNo);
                    return;
                }
            }
            else
            {
                module.PrintAlert("사진 형식을 확인해주세요. 사진은 꼭 1장 등록해야 등록가능합니다. (.png, .jpeg, .png .bmp 형식만 업로드 가능합니다", "/Gallery/GalleryModify.aspx?PhotoNo=" + intPhotoNo);
                return;
            }
           
        }

        private Boolean UploadFile()
        {
            int pl_intRandomNum = 0;
            string pl_strFilePath = string.Empty;
            try
            {
                pl_intRandomNum = new Random().Next(100000);
                pl_strFilePath = string.Concat("/photo/", pl_intRandomNum, FileUpload.FileName);
                FileUpload.SaveAs(Server.MapPath(pl_strFilePath));
                strPhotoURL = String.Copy(pl_strFilePath);
                HiddenUrl.Text = strPhotoURL;
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
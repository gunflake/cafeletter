using BOQv7Das_Net;
using cafeLetter.Models;
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Gallery
{
    public partial class GalleryWrite : System.Web.UI.Page
    {
        string strUserID = string.Empty;
        string strFilePath = string.Empty;
        protected CommonModule module = new CommonModule();


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
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }


        protected void GalleryRegister_Click(object sender, EventArgs e)
        {
            string pl_photoName = FileUpload.FileName;
            

            if(pl_photoName.Contains(".png") || pl_photoName.Contains(".jpg") || pl_photoName.Contains(".jpeg") || pl_photoName.Contains(".png") || pl_photoName.Contains(".bmp"))
            {
                if (UploadFile())
                {
                    PhotoWrite();
                }
                else
                {
                    Response.Write(@"<script>alert('파일 업로드에 실패했습니다.');</script>");
                    return;
                }
            }
            else
            {
                module.PrintAlert("사진 형식을 확인해주세요. 사진은 꼭 1장 등록해야 등록가능합니다. (.png, .jpeg, .png .bmp 형식만 업로드 가능합니다");
                return;
            }

            
        }

        
        private void PhotoWrite()
        {
            string pl_strTitle = BoardTitle.Text;
            string pl_strTags = BoardTags.Text;
            IDas pl_objDas = null;


            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                pl_objDas.AddParam("@pi_strUserID", DBType.adVarWChar, strUserID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strTitle", DBType.adVarWChar, pl_strTitle, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strURL", DBType.adVarWChar, strFilePath, 100, ParameterDirection.Input);                
                pl_objDas.AddParam("@pi_strTag", DBType.adVarWChar, pl_strTags, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@po_strErrMsg", DBType.adVarWChar, "", 256, ParameterDirection.Output);

                pl_objDas.AddParam("@po_intRetVal", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTO_TX_INS");

                String pl_strOutputMsg = Convert.ToString(pl_objDas.GetParam("@po_strErrMsg"));
                int pl_intRetVal = Convert.ToInt32(pl_objDas.GetParam("@po_intRetVal"));

                if (pl_intRetVal == 0)
                {
                    module.PrintAlert("새 갤러리 글이 작성되었습니다", "/Gallery/GalleryList.aspx");
                    return;
                    
                }
                else
                {
                    module.PrintAlert(pl_strOutputMsg, "/Gallery/GalleryList.aspx");
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
                strFilePath = string.Concat("/photo/", pl_intRandomNum, FileUpload.FileName);
                FileUpload.SaveAs(Server.MapPath(strFilePath));                
                return true;
            }
            catch
            { 
                return false;
            }
        }

        protected void GalleryCancel_Click(object sender, EventArgs e)
        {
            module.moveURL("/Gallery/GalleryList.aspx");
        }
    }
}
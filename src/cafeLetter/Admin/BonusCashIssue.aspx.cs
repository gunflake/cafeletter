using cafeLetter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace cafeLetter.Admin
{
    public partial class BonusCashIssue : System.Web.UI.Page
    {
        protected string strFileURL = string.Empty;
        CommonModule objModule = new CommonModule();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void UploadBtn_Click(object sender, EventArgs e)
        {
            if (FileUpload.FileName.Length < 2)
            {
                objModule.PrintAlert("업로드 파일을 선택해주세요");
                return;
            }

            if (UploadFile())
            {
                FileUpload.Visible = false;
                hiddenL.Visible = true;
                UploadBtn.Visible = false;
                objModule.PrintAlert("업로드 되었습니다. 발행을 원하시면 발행버튼을 눌러주세요.");
            }
        }

        private Boolean UploadFile()
        {
            int pl_intRandomNum = 0;
            string pl_strFilePath = string.Empty;
            try
            {
                pl_intRandomNum = new Random().Next(100000);
                pl_strFilePath = string.Concat("/file/", pl_intRandomNum, FileUpload.FileName);
                FileUpload.SaveAs(Server.MapPath(pl_strFilePath));
                strFileURL = String.Copy(pl_strFilePath);
                HiddenUrl.Text = strFileURL;
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
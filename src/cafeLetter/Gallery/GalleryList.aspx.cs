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
    public partial class GalleryList : System.Web.UI.Page
    {
        protected CommonModule module = new CommonModule();
        protected int intPageSize = 12;
        protected int intPageNo = 1;
        protected string strBoardName = string.Empty;
        protected static string strBoardCode = string.Empty;
        protected string strSearchID = string.Empty;
        protected string strSearchTitle = string.Empty;

        protected string strSearchTag = string.Empty;
        protected int intOrderFlag = 1;


        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.Params["PageNo"] != null)
            {
                intPageNo = Convert.ToInt32(Request.Params["PageNo"]);
            }

            if (Request.Params["PageSize"] != null)
            {
                intPageSize = Convert.ToInt32(Request.Params["PageSize"]);
            }

            if (Request.Params["OrderFlag"] != null)
            {
                intOrderFlag = Convert.ToInt32(Request.Params["OrderFlag"]);
            }

            if (Request.Params["searchTitle"] != null)
            {
                strSearchTitle = Convert.ToString(Request.Params["searchTitle"]);
            }

            if (Request.Params["searchID"] != null)
            {
                strSearchID = Convert.ToString(Request.Params["searchID"]);
            }


            GalleryListView(strSearchID, strSearchTitle, strSearchTag, intOrderFlag, intPageNo, intPageSize);
        }


        private void GalleryListView(string strSearchID, string strSearchTitle, string strSearchTag, int intOrderFlag, int intPageNo, int intPageSize)
        {
            //PAGING 따라바꾸기
            int pl_intTotalRecordCnt = 0;
            int pl_intCurrentRecortCnt = 0;
            IDas pl_objDas = null;

            // 페이즈 사이즈 받아오기

            try
            {
                pl_objDas = module.ConnetionDB();
                pl_objDas.CommandType = CommandType.StoredProcedure;
                pl_objDas.CodePage = 0;

                //검색 변수 추가 하기
                pl_objDas.AddParam("@pi_strSearchID", DBType.adVarChar, strSearchID, 20, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchTitle", DBType.adVarChar, strSearchTitle, 100, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_strSearchTag", DBType.adVarChar, strSearchTag, 50, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageSize", DBType.adInteger, intPageSize, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@pi_intPageNo", DBType.adInteger, intPageNo, 0, ParameterDirection.Input);

                pl_objDas.AddParam("@pi_intOrderFlag", DBType.adInteger, intOrderFlag, 0, ParameterDirection.Input);
                pl_objDas.AddParam("@po_intRecordCnt", DBType.adInteger, 0, 0, ParameterDirection.Output);

                pl_objDas.SetQuery("dbo.UP_PHOTOORDER_NT_LST");


                pl_intTotalRecordCnt = Convert.ToInt32(pl_objDas.GetParam("@po_intRecordCnt"));
                pl_intCurrentRecortCnt = pl_objDas.RecordCount;


                List<MyData> first = new List<MyData>();
                List<MyData> second = new List<MyData>();
                List<MyData> third = new List<MyData>();

                           

                for(int i = 0; i < pl_intCurrentRecortCnt; i++)
                {
                    //1열
                    if (i % 3 == 0)
                    {
                        first.Add(new MyData() {      strPhotoNo = pl_objDas.objDT.Rows[i]["PHOTONO"].ToString(),
                                              strPhotoURL = pl_objDas.objDT.Rows[i]["PHOTOURL"].ToString()
                         });
                    }
                    //2열
                    else if (i % 3 == 1)
                    {
                        second.Add(new MyData()
                        {
                            strPhotoNo = pl_objDas.objDT.Rows[i]["PHOTONO"].ToString(),
                            strPhotoURL = pl_objDas.objDT.Rows[i]["PHOTOURL"].ToString()
                        });
                    }
                    //3열
                    else if(i%3 == 2)
                    {
                        third.Add(new MyData()
                        {
                            strPhotoNo = pl_objDas.objDT.Rows[i]["PHOTONO"].ToString(),
                            strPhotoURL = pl_objDas.objDT.Rows[i]["PHOTOURL"].ToString()
                        });
                    }
                }
                
                
                firstImgs.DataSource = first;
                firstImgs.DataBind();

                secondImgs.DataSource = second;
                secondImgs.DataBind();

                thirdImgs.DataSource = third;
                thirdImgs.DataBind();

                //Paging
                string hrefURL = "/Gallery/GalleryList.aspx";
                string hrefParam = "&OrderFlag=" + intOrderFlag;

                module.Pagination(pl_intTotalRecordCnt, intPageNo, intPageSize, hrefURL, hrefParam, PageNumber);


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

        protected void GalleryNewWrite_Click(object sender, ImageClickEventArgs e)
        {

            if (Session["userID"] == null)
            {
                module.PrintAlert("로그인이 필요합니다");
                return;
            }
            else if (Session["galleryWrite"] != null && Session["galleryWrite"].Equals("N"))
            {
                module.PrintAlert("갤러리 작성 권한이 없습니다");
                return;
            }
            else
            {
                module.moveURL("/Gallery/GalleryWrite.aspx");
                return;
            }

        }


        protected void SearchButton_Click(object sender, EventArgs e)
        {
            //SearchMenu
            string pl_strSearchValue = SearchValue.Text;

            if (SearchMenu.SelectedItem.Text.Equals("아이디"))
            {
                strSearchID = pl_strSearchValue;
            }
            else if (SearchMenu.SelectedItem.Text.Equals("제목"))
            {
                strSearchTitle = pl_strSearchValue;
            }
            else if (SearchMenu.SelectedItem.Text.Equals("태그"))
            {
                strSearchTag = pl_strSearchValue;
            }

            intPageNo = 1;
            GalleryListView(strSearchID, strSearchTitle, strSearchTag, intOrderFlag, intPageNo, intPageSize);
        }


        protected void Page12_Click(object sender, EventArgs e)
        {
            intPageSize = 12;
            intPageNo = 1;
            module.moveURL("/Gallery/GalleryList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Page24_Click(object sender, EventArgs e)
        {
            intPageSize = 24;
            intPageNo = 1;
            module.moveURL("/Gallery/GalleryList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Page36_Click(object sender, EventArgs e)
        {
            intPageSize = 36;
            intPageNo = 1;
            module.moveURL("/Gallery/GalleryList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Newest_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 1;
            module.moveURL("/Gallery/GalleryList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Hot_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 2;
            module.moveURL("/Gallery/GalleryList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }

        protected void Comment_Click(object sender, EventArgs e)
        {
            intPageNo = 1;
            intOrderFlag = 3;
            module.moveURL("/Gallery/GalleryList.aspx?PageNo=" + intPageNo + "&PageSize=" + intPageSize + "&OrderFlag=" + intOrderFlag);
        }
    }


    public class MyData
    {
        public string strPhotoURL { get; set; }
        public string strPhotoNo { get; set; }
    }

}
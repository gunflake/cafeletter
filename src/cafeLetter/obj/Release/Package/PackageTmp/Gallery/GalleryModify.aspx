<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="GalleryModify.aspx.cs" Inherits="cafeLetter.Gallery.GalleryModify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationBarHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationBarBody" runat="server">



    <div class="container-fluid" style="width: 60%; margin: 0 auto;">

        <!-- title -->
        <div class="row">

            <div class="col-md-11">
                <div class="page-header">
                    <h1>갤러리 수정 </h1>
                </div>
            </div>

        </div>

        <hr class="featurette-divider">

        <div class="row">
            <div class="col-md-10">
                <h4>제목</h4>
                <asp:TextBox runat="server" ID="GalleryTitle" onkeyup="checkTitle(this)" type="text" class="form-control" placeholder="제목을 입력하세요" />
            </div>
        </div>

        <hr class="featurette-divider">
        <div class="row">
            <h4>사진수정</h4>
            <asp:FileUpload ID="FileUpload" Text="파일선택" runat="server"  />
            <asp:Button ID="UploadBtn" runat="server" Text="업로드" OnClick="UploadBtn_Click" />
        </div>
        <div class="row">
             <img src=<%=strPhotoURL %> alt="Responsive image" class="img-responsive" style="margin:0 auto;"  /> 
            <asp:Label ID="HiddenUrl" runat="server" Visible="false"></asp:Label>
           
        </div>
        <hr class="featurette-divider">
        <div class="row">
            <h4>태그</h4>
            <asp:TextBox runat="server" ID="GalleryTags" onkeyup="checkTag(this)" type="text" class="form-control" Rows="10" placeholder="태그를 입력하세요.(형식: #성공 #워크샵 #빨리)" />
        </div>
        <hr />
        <div class="row">
            <asp:Button ID="GalleryModifyBtn" runat="server" class="btn btn-primary" Text="수정" OnClientClick="return checkValidation();"  OnClick="GalleryModify_Click"></asp:Button>
            <asp:Button ID="GalleryCancelBtn" runat="server" class="btn btn-primary" Text="취소" OnClientClick="return confirmPostCancel();"  OnClick="GalleryCancel_Click"></asp:Button>
        </div>


    </div>

     <script type="text/javascript">

        var check_title = true;
    

        //글 작성 최종 체크
        function checkValidation() {

            var title = document.getElementById('<%=GalleryTitle.ClientID %>').value;

            if (title.length < 2) {
                alert("제목은 최소 2자 이상 입력해야합니다.")
                return false;
            }

      
            if (!check_title) {
                return false;
            }

            if (confirm("글을 수정하시겠습니까?")) {
                return true;
            } else {
                return false;
            }

            return true;


        }

        //제목 길이 체크
        function checkTitle(obj) {
           var comment = obj.value;

          if (comment.length > 100) {
              alert("제목은 최대 100자까지 입력가능합니다.");
              comment = comment.substring(0, 98);
              document.getElementById('<%=GalleryTitle.ClientID %>').value = comment;
              
              check_title = false;
              return false;
          }
          check_title = true; 
          return true;

        }
         

        //태그 길이 체크
        function checkTag(obj) {
           var comment = obj.value;

          if (comment.length > 100) {
              alert("태그는 최대 50자까지 입력가능합니다.");
              comment = comment.substring(0, 48);
              document.getElementById('<%=GalleryTags.ClientID %>').value = comment;
              
              check_tag = false;
              return false;
          }
          check_tag = true; 
          return true;

        }


        function confirmPostCancel() {
            if (confirm("글 수정을 취소하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

  
    </script>


</asp:Content>

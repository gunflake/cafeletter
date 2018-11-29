<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="GalleryWrite.aspx.cs" Inherits="cafeLetter.Gallery.GalleryWrite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationBarHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationBarBody" runat="server">

    <div class="container-fluid">
        <div class="row" style="width: 40%; margin: 0 auto;">

            <div class="row"  style="margin-top: 40px;">
               
                    <h3>새 갤러리 등록</h3>
               
            </div>

            <hr class="featurette-divider">

            <div class="row">
               
                    <h4>제목</h4>
                    <asp:TextBox runat="server" ID="BoardTitle" onkeyup="checkTitle(this)"  type="text" class="form-control" placeholder="제목을 입력하세요" />
             
            </div>
            <hr class="featurette-divider">
            <div class="row">
                <h4>사진추가</h4>
                <asp:FileUpload ID="FileUpload" Text="파일선택" runat="server" />                                             
            </div>
            <hr class="featurette-divider">
            <div class="row">              
                    <h4>태그</h4>
                    <asp:TextBox runat="server" ID="BoardTags" type="text" class="form-control" Rows="10"  onkeyup="checkTag(this)" placeholder="태그를 입력하세요.(형식: #성공 #워크샵 #빨리)" />
                </div>       
            <hr />
            <div class="row" >
                <asp:Button ID="GalleryRegister" runat="server" class="btn btn-primary" Text="작성" OnClientClick="return checkValidation();" OnClick="GalleryRegister_Click"></asp:Button>
                 <asp:Button ID="GalleryCancel" runat="server" Text="취소" class="btn btn-primary" OnClientClick="return confirmPostCancel();" OnClick="GalleryCancel_Click" />
            </div>

        </div>
    </div>


     <script type="text/javascript">

        var check_title = false;
    

        //글 작성 최종 체크
        function checkValidation() {

            var title = document.getElementById('<%=BoardTitle.ClientID %>').value;

            if (title.length < 2) {
                alert("제목은 최소 2자 이상 입력해야합니다.")
                return false;
            }


            if (!check_title) {
                return false;
            }

            if (confirm("글을 작성하시겠습니까?")) {
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
              document.getElementById('<%=BoardTitle.ClientID %>').value = comment;
              
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
              document.getElementById('<%=BoardTags.ClientID %>').value = comment;
              
              check_tag = false;
              return false;
          }
          check_tag = true; 
          return true;

        }


        function confirmPostCancel() {
            if (confirm("글 작성을 취소하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

  
    </script>


</asp:Content>

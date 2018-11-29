<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarItem.master" AutoEventWireup="true" CodeBehind="ItemWrite.aspx.cs" Inherits="cafeLetter.Item.ItemWrite" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarItemHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarItemBody" runat="server">

    
    <div class="col-sm-7 col-md-8" style="top: 40px;">


        <!-- Page Name & 드롭다운 -->
        <div class="row">
            <div class="col-md-8">
                <h3>새 글 작성</h3>
            </div>
        </div>

        <hr class="featurette-divider">

        <div class="row">
            <div class="col-md-10">
                <h4>제목</h4>
                <asp:TextBox runat="server" ID="BoardTitle" onkeyup="checkTitle(this)" type="text" class="form-control" placeholder="제목을 입력하세요" />
            </div>
        </div>
        <hr class="featurette-divider">
        <div class="row">
            <div class="col-md-10">
                <h4>본문</h4>

                <asp:TextBox runat="server" ID="BoardBody" onkeyup="checkBody(this)" MaxLength="4000" TextMode="MultiLine" Rows="10" class="form-control" placeholder="내용을 입력하세요" />
                <!--   <textarea class="form-control" rows="10"></textarea> -->
            </div>
        </div>
        <hr class="featurette-divider">
      
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            <asp:Button ID="BoardWrite" runat="server" class="btn btn-primary" Text="작성" OnClientClick="return checkValidation();" OnClick="BoardWrite_Click"></asp:Button>
            <asp:Button ID="BoardCancel" runat="server" Text="취소" class="btn btn-primary" OnClientClick="return confirmPostCancel();" OnClick="BoardCancel_Click" />
        </div>

    </div>

    <script type="text/javascript">

        var check_title = false;
        var check_body = false;
    

        //글 작성 최종 체크
        function checkValidation() {

            var title = document.getElementById('<%=BoardTitle.ClientID %>').value;
            var body = document.getElementById('<%=BoardBody.ClientID %>').value;

            if (title.length < 5) {
                alert("제목은 최소 5자 이상 입력해야합니다.")
                return false;
            }

            if (body.length < 5) {
                alert("본문은 최소 5자까지 입력가능합니다.")
                return false;
            }

            if (!check_title || !check_body) {
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

        //본문 길이 체크
        function checkBody(obj) {
            var comment = obj.value;

          if (comment.length > 4000) {
              alert("본문은 최대 4000자까지 입력가능합니다.");
              comment = comment.substring(0, 3998);
              document.getElementById('<%=BoardBody.ClientID %>').value = comment;
              
              check_body = false;
              return false;
          }
          check_body = true; 
          return true;
        }

 


        function confirmPostCancel() {
            if (confirm("물품 등록을 취소하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

  
    </script>



</asp:Content>

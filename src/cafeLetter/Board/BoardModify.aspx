<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarBoard.master" AutoEventWireup="true" CodeBehind="BoardModify.aspx.cs" Inherits="cafeLetter.Board.BoardUpdate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarBoardHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarBoardBody" runat="server">
    <div class="col-sm-7 col-md-8" style="top: 40px;">


        <!-- Page Name & 드롭다운 -->
        <div class="row">
            <div class="col-md-8">
                <h3>글 수정</h3>      
            </div>
        </div>

        <hr class="featurette-divider">

        <div class="row">
            <div class="col-md-10">
                <h4>제목</h4>
                <asp:TextBox runat="server" ID="BoardTitle" type="text" onkeyup="checkTitle(this)" class="form-control" placeholder="제목을 입력하세요" />
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
        <div class="row">
            <div class="col-md-10">
                <h4>태그</h4>
                <asp:TextBox runat="server" ID="BoardTags" onkeyup="checkTag(this)" type="text" class="form-control" Rows="10" placeholder="태그를 입력하세요.(형식: #성공 #워크샵 #빨리)" />
            </div>
        </div>
        <hr />
        <div class="row" style="margin-left: 0px; margin-right: 0px;">          
            <asp:Button ID="BoardModify" runat="server" class="btn btn-primary" Text="수정" OnClientClick="return checkValidation();" OnClick="BoardModify_Click"></asp:Button>
            <asp:Button ID="BoardCancel" runat="server" Text="취소" class="btn btn-primary" OnClientClick="return confirmPostCancel();" OnClick="BoardCancel_Click" />
        </div>

    </div>


    <script type="text/javascript">

        var check_title = true;
        var check_body = true;
    

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
            if (confirm("글 수정을 취소하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

  
    </script>



</asp:Content>

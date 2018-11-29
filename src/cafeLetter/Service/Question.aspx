<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarService.master" AutoEventWireup="true" CodeBehind="Question.aspx.cs" Inherits="cafeLetter.Service.Question" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarServiceHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarServiceBody" runat="server">



    <div class="col-sm-7 col-md-8" style="top: 40px;">


        <!-- Page Name & 드롭다운 -->
        <div class="row">
            <h3>1:1 문의하기</h3>
        </div>

        <hr class="featurette-divider">


        
        <div class="row">
            <div class="col-md-10">
                <h4>제목</h4>
                <asp:TextBox runat="server" ID="NoticeTitle" onkeyup="checkTitle(this)" type="text" class="form-control" placeholder="제목을 입력하세요" />
            </div>
        </div>
        <hr class="featurette-divider">
        <div class="row">
            <div class="col-md-10">
                <h4>본문</h4>
                <asp:TextBox runat="server" ID="NoticeBody" onkeyup="checkBody(this)" MaxLength="4000" TextMode="MultiLine" Rows="12" class="form-control" placeholder="내용을 입력하세요" />
                <!--   <textarea class="form-control" rows="10"></textarea> -->
            </div>
        </div>
        <hr class="featurette-divider">
        <hr />
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            <asp:Button ID="NoticeWrite" runat="server" class="btn btn-primary" Text="작성" OnClientClick="return checkValidation();" OnClick="NoticeWrite_Click"></asp:Button>
            <asp:Button ID="NoticeCancel" runat="server" Text="취소" class="btn btn-primary" OnClientClick="return confirmPostCancel();" OnClick="NoticeCancel_Click" />
        </div>

    </div>

    <script type="text/javascript">

        var check_title = false;
        var check_body = false;


        //글 작성 최종 체크
        function checkValidation() {

            var title = document.getElementById('<%=NoticeTitle.ClientID %>').value;
            var body = document.getElementById('<%=NoticeBody.ClientID %>').value;

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

            if (confirm("1:1 문의사항을 작성하시겠습니까?")) {
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
                document.getElementById('<%=NoticeTitle.ClientID %>').value = comment;

                check_title = false;
                return false;
            }
            check_title = true;
            return true;

        }

        //본문 길이 체크
        function checkBody(obj) {
            var comment = obj.value;

            if (comment.length > 100) {
                alert("본문은 최대 4000자까지 입력가능합니다.");
                comment = comment.substring(0, 3998);
                document.getElementById('<%=NoticeBody.ClientID %>').value = comment;

                check_body = false;
                return false;
            }
            check_body = true;
            return true;
        }

        function confirmPostCancel() {
            if (confirm("1:1 문의하기 작성을 취소하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }


    </script>



</asp:Content>

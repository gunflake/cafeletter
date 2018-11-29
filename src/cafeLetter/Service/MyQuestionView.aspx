<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarService.master" AutoEventWireup="true" CodeBehind="MyQuestionView.aspx.cs" Inherits="cafeLetter.Service.MyQuestionView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarServiceHead" runat="server">

    <style>
        .panel-default {
            border-color: #fff;
        }

        .jumbotron {
            background-color: #fff;
        }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarServiceBody" runat="server">


    <div class="col-sm-9 col-md-10">



        <!-- title -->
        <div class="row">

            <div class="col-md-11">
                <div class="page-header">
                    <h1><%=strPostTitle %></h1>
                </div>
            </div>
            <div class="col-md-1">
                <div class="page-header">
                    <h1 style="color: #428bca"><%=strPostView %></h1>
                </div>
            </div>
        </div>
        <!-- date -->
        <div class="row">
            <div class="col-md-9">
                <p>등록 날짜: <%=strPostDate %></p>
                <hr />
            </div>
            <div class="col-md-3" style="text-align: right">
                <p><%=strPostWriter %> </p>
                <hr />
            </div>
        </div>

        <div class="jumbotron">

            <p class="lead"><%=strPostContent %></p>
        </div>

        <!-- delete update -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            <asp:Button ID="BoardDelete" runat="server" class="btn btn-primary" Text="삭제" OnClientClick="return confirmPostDelete();" OnClick="BoardDelete_Click"></asp:Button>
        </div>
        <hr class="featurette-divider">

        <div class="row" style="margin-left: 0px; margin-right: 0px; margin-top: 15px;">
            <h5>댓글</h5>
            <asp:TextBox runat="server" ID="CommentBody" MaxLength="4000" TextMode="MultiLine" Rows="5" class="form-control" placeholder="댓글을 입력하세요" onkeyup="commentCheck(this)" />
            <asp:Button ID="CommentWrite" runat="server" class="btn btn-primary" Text="등록" Style="top: 10px;" OnClientClick="return validationCommentBtn();" OnClick="CommentWrite_Click"></asp:Button>
        </div>


        <!-- 댓글 리스트 -->
        <div class="row" style="margin-left: 0px; margin-right: 0px; margin-top: 30px;">
            <asp:Repeater ID="CommentListPanel" runat="server">
                <ItemTemplate>
                    <div class="panel panel-default" style="width: 97%; margin: 0 auto; margin-bottom: 10px;">
                        <div class="panel-body">
                            <div class="row">


                                <span class="data D"><%# Eval("USERID") %> &nbsp;  <%# Eval("COMMENTDATE") %> </span>


                                <!-- 아이디와 작성자가 일치할경우 수정 삭제 -->
                                <asp:LinkButton runat="server" ID="CmtModify" Text="수정" Visible='<%# Eval("USERID").Equals(strUserID)  ? true : false %>' OnClick="CmtModify_Click"></asp:LinkButton>
                                <asp:LinkButton runat="server" ID="CmtDelete" Text="삭제" Visible='<%# Eval("USERID").Equals(strUserID)  ? true : false %>' OnClientClick="return confirmCommentDelte();" OnClick="CmtDelete_Click"></asp:LinkButton>
                                <!-- 아이디와 작성자가 일치할경우 수정 삭제 END-->

                                <!-- 수정했을때 뜨는 text box 및 button -->
                                <div class="row">
                                    <div class="col-md-10">
                                        <asp:TextBox ID="CmtModifyContent" Text='<%# Eval("COMMENTCONTENT") %>' onkeyup="checkModify(this)" Visible="false" MaxLength="4000" TextMode="MultiLine" Rows="4" class="form-control" runat="server"></asp:TextBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Button ID="CommentUpdate" Visible="false" runat="server" class="btn btn-primary" Text="수정" Style="top: 10px;" OnClientClick="return validationCommentUpdateBtn();" OnClick="CommentUpdate_Click"></asp:Button>
                                    </div>
                                </div>
                                <!--수정했을때 뜨는 text box 및 button END -->
                                <br />
                                <span class="data D">
                                    <asp:Literal runat="server" ID="CmtContent" Text='<%# Eval("COMMENTCONTENT") %>'></asp:Literal></span>
                                <asp:Literal runat="server" ID="CommentNo" Text='<%# Eval("COMMENTNO") %>' Visible="false"></asp:Literal>


                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>


        </div>
        <!-- 댓글 리스트 끝 -->

    </div>


    <script type="text/javascript">

        var check_Comment = false;
        var check_modify = false;
        var modify_id = "";


        //댓글 등록 버튼 활성화
        function validationCommentBtn() {

            var content = document.getElementById('<%=CommentBody.ClientID %>').value;
            if (content.length < 2) {
                alert("댓글은 최소 2자 이상 입력해야합니다.")
                return false;
            }

            if (content.length > 101) {
                alert("댓글은 최소 100자까지 입력가능합니다.")
                return false;
            }

            if (confirm("댓글을 등록하시겠습니까?")) {
                return true;
            } else {
                return false;
            }

            return true;
        }

        // 댓글 길이 최대 100자
        function commentCheck(txt) {

            var comment = txt.value;

            if (comment.length > 100) {
                alert("댓글은 최대 100자까지 입력가능합니다.");
                comment = comment.substring(0, 99);
                document.getElementById('<%=CommentBody.ClientID %>').value = comment;

                check_Comment = false;
                return false;
            }
            check_Comment = true;
            return true;

        }

        //댓글 수정 길이 체크
        function checkModify(obj) {
            var comment = obj.value;
            modify_id = obj.id;


            if (comment.length > 100) {
                alert("댓글은 최대 100자까지 입력가능합니다.");
                comment = comment.substring(0, 99);
                document.getElementById(obj.id).value = comment;

                check_modify = false;
                return false;
            }
            check_modify = true;
            return true;
        }

        //댓글 수정 버튼 활성화
        function validationCommentUpdateBtn() {

            var content = document.getElementById(modify_id).value;
            if (content.length < 2) {
                alert("댓글은 최소 2자 이상 입력해야합니다.")
                return false;
            }

            if (content.length > 101) {
                alert("댓글은 최소 100자까지 입력가능합니다.")
                return false;
            }

            if (confirm("댓글을 수정하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

        function confirmCommentDelte() {
            if (confirm("댓글을 삭제하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

        function confirmPostDelete() {
            if (confirm("내 질문을 삭제하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

        function confirmPostUpdate() {
            if (confirm("질문을 수정하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }





    </script>

</asp:Content>

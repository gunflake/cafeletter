<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="UserAuthorityUpdate.aspx.cs" Inherits="cafeLetter.Admin.UserAuthorityUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">



    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <div class="col-md-11">
                <h4>회원 관리</h4>
            </div>
        </div>

        <hr class="featurette-divider">

        <!-- UserName -->
        <div class="row" style="width: 70%; margin-left: 0px; margin-right: 0px;">
            <table class="table table-bordered">
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">ID</th>
                        <td><%=strUserID %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">이름</th>
                        <td><%=strName %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">성별</th>
                        <td><%=strSex %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">지역</th>
                        <td><%=strLocation %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">생일</th>
                        <td><%=strBirthday %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">SNS</th>
                        <td><%=strSNS %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">자기소개</th>
                        <td><%=strIntroduce %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">마지막 로그인 시간</th>
                        <td><%=strLastLogin %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">게시글 작성 수</th>
                        <td><%=strBoardCnt %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">댓글 작성 수</th>
                        <td><%=strCommentCnt %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">게시판 제한</th>
                        <td>
                            <asp:CheckBox ID="BoardCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">갤러리 제한</th>
                        <td>
                            <asp:CheckBox ID="GalleryCheckBox" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:Button ID="DeleteBtn" runat="server" class="btn btn-danger" Text="삭제" OnClientClick="return confirmDelete();" OnClick="DeleteBtn_Click"></asp:Button>
                <asp:Button ID="UserAuthorityUpdateBtn" runat="server" class="btn btn-primary" Text="수정" OnClientClick="return confirmComplete();" OnClick="UserAuthorityUpdateBtn_Click"></asp:Button>
            </div>
        </div>
    </div>


    <script type="text/javascript">
        function confirmComplete() {
            if (confirm("회원 권한을 수정하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

        function confirmDelete() {
            if (confirm("회원을 강제탈퇴 시키겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }
    </script>




</asp:Content>

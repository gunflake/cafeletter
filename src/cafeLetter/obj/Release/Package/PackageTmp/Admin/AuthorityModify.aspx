<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="AuthorityModify.aspx.cs" Inherits="cafeLetter.Admin.AuthorityModify" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">

    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
           <div class="col-md-11">
                <h4>권한 수정 </h4>
            </div>
        </div>

        <hr class="featurette-divider">

        <!-- 권한 목록 -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
         


                <table class="table table-bordered">

                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">아이디</th>
                        <th scope="row" style="background-color: #3db1c93b;">이름</th>
                        <th scope="row" style="background-color: #3db1c93b;">게시판 관리</th>
                        <th scope="row" style="background-color: #3db1c93b;">갤러리 관리</th>
                        <th scope="row" style="background-color: #3db1c93b;">회원 관리  </th>
                    </tr>
                    <tbody>
                        <tr>
                            <td><%= strAdminUserID %></td>
                            <td><%= strName %></td>
                            <td>
                                <asp:CheckBox ID="BoardCheckBox" runat="server" />
                            </td>
                            <td>
                                <asp:CheckBox ID="GalleryCheckBox" runat="server" />

                            </td>
                            <td>
                                <asp:CheckBox ID="UserCheckBox" runat="server" />

                            </td>
                        </tr>
                    </tbody>
                </table>
            
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:Button ID="AuthorityModifyUpdate" runat="server" class="btn btn-primary" Text="수정" OnClientClick="return confirmComplete();" OnClick="AuthorityModifyUpdate_Click"></asp:Button>
                <asp:Button ID="AuthorityDelete" runat="server" class="btn btn-primary" Text="강등" OnClientClick="return confirmDelete();" OnClick="AuthorityDelete_Click"></asp:Button>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function confirmComplete() {
            if (confirm("권한을 수정하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

        function confirmDelete() {
            if (confirm("관리자 권한을 삭제하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }
    </script>

</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="AdminAdd.aspx.cs" Inherits="cafeLetter.Admin.AdminAdd"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">


    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <div class="col-md-11">
                <h4>관리자 추가 </h4>
            </div>
        </div>

        <hr class="featurette-divider">

        <!-- UserName -->
        <div class="row" style="width: 60%; margin-left: 0px; margin-right: 0px;">
            <table class="table table-bordered">
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">ID</th>
                        <td><%=strAdminUserID %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">이름</th>
                        <td><%=strUserName %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">회원 관리</th>
                        <td>
                            <asp:CheckBox ID="UserCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">게시판 관리</th>
                        <td>
                            <asp:CheckBox ID="BoardCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">갤러리 관리</th>
                        <td>
                            <asp:CheckBox ID="GalleryCheckBox" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:Button ID="AdminInsertBtn" runat="server" class="btn btn-primary" Text="추가" OnClientClick="return confirmComplete();" OnClick="AdminInsertBtn_Click"></asp:Button>
                <asp:Button ID="Cancel" runat="server" class="btn btn-primary" Text="취소" OnClientClick="return confirmCancel();" OnClick="Cancel_Click"></asp:Button>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function confirmComplete() {
            if (confirm("관리자로 임명하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

        function confirmCancel() {
            if (confirm("관리자 추가를 취소하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }
    </script>


</asp:Content>

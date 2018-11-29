<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarMember.master" AutoEventWireup="true" CodeBehind="CashResult.aspx.cs" Inherits="cafeLetter.Cash.CashResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarMemberHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarMemberBody" runat="server">

    <div class="col-sm-9 col-md-10">

        <!-- UserName -->
        <div class="row">
            <div class="page-header">
                <h3>결제 성공</h3>
            </div>
        </div>

        <!-- UserName -->
        <div class="row" style="width: 65%; margin-right: 0px;">
            <table class="table table-bordered">
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">ID</th>
                        <td>ㅇ</td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">Email</th>
                        <td>ㅇ</td>
                    </tr>
                </tbody>
            </table>
            <div>
                <asp:Button runat="server" ID="completeBtn" OnClientClick="refreshParent()" Text="확인"/>
            </div>
        </div>
 
    </div>

    <script>
        function refreshParent() {
            window.close();
        }
    </script>

</asp:Content>

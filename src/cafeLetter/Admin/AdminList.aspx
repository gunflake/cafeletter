<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="AdminList.aspx.cs" Inherits="cafeLetter.Admin.AuthorityList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">





    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <div class="col-md-11">
                <h4>관리자 목록 </h4>
            </div>
        </div>
        
        <hr class="featurette-divider">

        <!-- 권한 목록 -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            
                <asp:Repeater ID="AuthorityListView" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <tr class="table-active">
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">아이디</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">이름</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">등급</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">게시판 관리 권한</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">갤러리 관리권한</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">회원 관리 권한</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">권한 관리</th>
                                
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><%# Eval("USERID") %></td>
                            <td style="text-align:center"><%# Eval("USERNAME") %></td>
                            <td style="text-align:center"><%# Eval("USERRANK") %></td>
                            <td style="text-align:center"><%# Eval("BOARDAUTHORITY") %></td>
                            <td style="text-align:center"><%# Eval("PHOTOAUTHORITY") %></td>
                            <td style="text-align:center"><%# Eval("USERAUTHORITY") %></td>
                            <td style="text-align:center"><asp:HyperLink ID="AuthorityModify" runat="server" Text='수정' NavigateUrl='<%# String.Format("/Admin/AuthorityModify.aspx?UserID={0}&UserName={1}",Eval("USERID"), Eval("USERNAME")) %>'></asp:HyperLink>  </td>                            
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                 </asp:Repeater>
        </div>

         <div class="row">
            <div class="col-md-6">
                <nav>
                    <ul id="PageNumber" runat="server" class="pagination">
                    </ul>
                </nav>
            </div>
            <div class="col-md-6">
                <div class="input-group pull-right" style="margin-top: 20px; width:305px;" >
                    <div class="input-group-btn" >
                        <asp:DropDownList ID="SearchMenu" runat="server" CssClass="form-control" Style="width:90px;">
                            <asp:ListItem>아이디</asp:ListItem>
                            <asp:ListItem>이름</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <!-- /btn-group -->
                    <asp:TextBox runat="server" ID="SearchValue" type="text" class="form-control" style="width:145px;" />
                    <asp:Button ID="SearchAdmin" runat="server" class="btn btn-primary"  style="width:60px;" Text="검색" OnClick="SearchAdmin_Click" />
                </div>
                <!-- /input-group -->
            </div>
        </div>






    </div>




</asp:Content>

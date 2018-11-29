<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="cafeLetter.Admin.UserList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">


    
    <div class="col-sm-9 col-md-10" style="top: 40px;">

                
                    <h4>회원 목록 </h4>
                
 
        
        <hr class="featurette-divider">

        <!-- 권한 목록 -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            
                <asp:Repeater ID="UserListView" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <tr class="table-active">
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">아이디</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">이름</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">등급</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">게시글 수</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">댓글 수</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">마지막 로그인</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">게시판 쓰기</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">갤러리 쓰기</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">  </th>
                                
                                
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><%# Eval("USERID") %></td>
                            <td style="text-align:center"><%# Eval("USERNAME") %></td>
                            <td style="text-align:center"><%# Eval("USERRANK") %></td>
                            <td style="text-align:center"><%# Eval("BOARDCNT") %></td>
                            <td style="text-align:center"><%# Eval("COMMENTCNT") %></td>
                            <td style="text-align:center"><%# Eval("LASTDATE") %></td>
                            <td style="text-align:center"><%# Eval("BOARDWRITE") %></td>
                            <td style="text-align:center"><%# Eval("GALLERYWRITE") %></td>
                            <td style="text-align:center"><asp:HyperLink ID="AuthorityModify" runat="server" Text='관리' NavigateUrl='<%# String.Format("/Admin/UserAuthorityUpdate.aspx?UserID={0}",Eval("USERID")) %>'></asp:HyperLink>&nbsp; &nbsp;  
                            <asp:HyperLink ID="HyperLink1" Visible='<%# strUserRank.Equals("마스터")  ? true : false %>' runat="server" Text='관리자추가' NavigateUrl='<%# String.Format("/Admin/AdminAdd.aspx?UserID={0}&UserName={1}",Eval("USERID"), Eval("USERNAME")) %>'></asp:HyperLink></td>
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
            <div class="col-md-6" >

                <div class="input-group pull-right" style="margin-top: 20px; width:305px;">
                    <div class="input-group-btn">
                        <asp:DropDownList ID="SearchMenu" runat="server" CssClass="form-control" Style="width: 90px;">
                            <asp:ListItem>아이디</asp:ListItem>
                            <asp:ListItem>이름</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <!-- /btn-group -->
                    <asp:TextBox runat="server" ID="SearchValue" type="text" class="form-control" Style="width: 145px;" />
                    <asp:Button ID="SearchUser" runat="server" class="btn btn-primary" Text="검색"  style="width:60px;" OnClick="SearchUser_Click" />
                </div>
                <!-- /input-group -->
            </div>
        </div>






    </div>




</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="AdminInfo.aspx.cs" Inherits="cafeLetter.Admin.AdminInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">




    <div class="col-sm-9 col-md-10" style="top: 40px;">

            <h4>내 권한 정보</h4>


        
        <hr class="featurette-divider">

        <!-- UserName -->
        <div class="row" style="width:50%; margin-left: 0px; margin-right: 0px;">
           
                <table class="table table-bordered">
                    <tbody>
                        <tr class="table-active" >
                            <th scope="row" style="background-color: #3db1c93b; width:50%;">관리자 등급</th>
                            <td><%=strAdminRank %></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">ID</th>
                            <td><%=strUserID %></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">이름</th>
                            <td><%=strName %></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">게시글 작성 수</th>
                            <td><%=intBoardCnt %></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">댓글 작성 수</th>
                            <td><%=intCommentCnt %></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">게시판 권한</th>
                            <td><%=strBoardAuthority %></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">갤러리 권한</th>
                            <td><%=strGalleryAuthority %></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">회원 권한</th>
                            <td><%=strUserAuthority %></td>
                        </tr>

                    </tbody>
                </table>
         
        </div>       
    </div>



</asp:Content>

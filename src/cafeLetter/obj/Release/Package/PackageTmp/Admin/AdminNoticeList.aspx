<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="AdminNoticeList.aspx.cs" Inherits="cafeLetter.Admin.AdminNoticeList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">



    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <!-- -->
        <div class="row">
            <div class="col-md-11">
                <h4>공지사항 목록 </h4>
            </div>
       
            <div class="col-md-1">
                <a href="/Admin/AdminNoticeWrite.aspx" class="brand">
                    <img alt="Brand" src="/image/plus.png" width="40" height="40" /></a>
            </div>
        </div>


        <hr class="featurette-divider">

        <div class="row" style="margin-top: 12px;">

            <asp:Repeater ID="NoticeListPanel" runat="server">
                <ItemTemplate>
                    <div class="panel panel-default" style="width: 97%; margin: 0 auto; margin-bottom: 10px;">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <span class="data D">조회수 &nbsp; <%# Eval("BOARDVIEW") %></span> &nbsp; &nbsp; 
                                    <br />
                                    <span class="data D">작성자 &nbsp; <%# Eval("BOARDWRITER") %></span> 
                                    <br />
                                    <span class="data D">등록날짜 &nbsp; <%# Eval("BOARDDATE").ToString().Substring(0,10) %></span>                 
                                </div>
                                <div class="col-md-8">
                                    <asp:HyperLink ID="BoardViewLink" runat="server" Text='<%# Eval("BOARDTITLE") + " ["+ Eval("BOARDCOMMENTCNT") + "]"  %>' NavigateUrl='<%# String.Format("/Admin/AdminNoticeView.aspx?BoardNo={0}", Eval("BOARDNO")) %>'></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>

        <!-- 페이징 처리-->
        <div class="row">
            <div class="col-md-6">
                <nav>
                    <ul id="PageNumber" runat="server" class="pagination">
                    </ul>
                </nav>
            </div>

            <div class="col-md-6">

                <div class="input-group pull-right" style="margin-top: 20px;  width:305px;">
                    <div class="input-group-btn">
                        <asp:DropDownList ID="SearchMenu" runat="server" CssClass="form-control" Style="width: 90px">
                            <asp:ListItem>제목</asp:ListItem>
                            <asp:ListItem>아이디</asp:ListItem>

                        </asp:DropDownList>
                    </div>
                    <!-- /btn-group -->
                    <asp:TextBox runat="server" ID="SearchValue" type="text" class="form-control" Style="width: 145px;" />
                    <asp:Button ID="SearchButton" runat="server" class="btn btn-primary" Text="검색" Style="width: 60px" OnClick="SearchButton_Click" />
                </div>
                <!-- /input-group -->
            </div>

        </div>


    </div>



</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarBoard.master" AutoEventWireup="true" CodeBehind="BoardList.aspx.cs" Inherits="cafeLetter.WebForm1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarBoardHead" runat="server">
    <style>
      </style>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarBoardBody" runat="server">


    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <!-- -->
        <div class="row">
            <div class="col-md-11">
                <h2><%=strBoardName %> </h2>
            </div>
            <div class="col-md-1">
                <asp:ImageButton ID="BoardNewWrite" runat="server" ImageUrl="/image/write.png" Width="40px" Height="40px" OnClientClick="return confirmPostWrite();" OnClick="BoardNewWrite_Click" />
            </div>

        </div>



        <div class="btn-group" role="group" aria-label="...">
            <asp:Button ID="Page10" runat="server" type="button" class="btn btn-default" Text="10" OnClick="Page10_Click"></asp:Button>
            <asp:Button ID="Page20" runat="server" type="button" class="btn btn-default" Text="20" OnClick="Page20_Click"></asp:Button>
            <asp:Button ID="Page30" runat="server" type="button" class="btn btn-default" Text="30" OnClick="Page30_Click"></asp:Button>
        </div>
        <span class="text-muted">&nbsp; 페이지 수</span>



        <div class="btn-group" role="group" aria-label="..." style="float: right; margin-right: 15px;">
            <asp:Button ID="Newest" runat="server" type="button" class="btn btn-default" Text="최신순" OnClick="Newest_Click"></asp:Button>
            <asp:Button ID="Hot" runat="server" type="button" class="btn btn-default" Text="조회순" OnClick="Hot_Click"></asp:Button>
            <asp:Button ID="Comment" runat="server" type="button" class="btn btn-default" Text="댓글순" OnClick="Comment_Click"></asp:Button>


        </div>

        <hr class="featurette-divider">

        <div class="row" style="margin-top: 12px;">


            <asp:Repeater ID="PostListPanel" runat="server">
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
                                    <asp:HyperLink ID="BoardViewLink" runat="server" Text='<%# Eval("BOARDTITLE") + " ["+ Eval("BOARDCOMMENTCNT") + "]"  %>' NavigateUrl='<%# String.Format("/Board/BoardView.aspx?BoardCode={0}&BoardNo={1}",Eval("BOARDTYPECODE"), Eval("BOARDNO")) %>'></asp:HyperLink>
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
                    <ul id="PageNumber" runat="server" class="pagination" >
                    </ul>
                </nav>
            </div>

            <div class="col-md-6">

                <!-- 검색 -->
                <div class="input-group pull-right" style="margin-top: 20px; width:305px;">
                    <div class="input-group-btn">
                        <asp:DropDownList ID="SearchMenu" runat="server" CssClass="form-control" Style="width:90px;">
                            <asp:ListItem>제목</asp:ListItem>
                            <asp:ListItem>아이디</asp:ListItem>
                            <asp:ListItem>태그</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <!-- /btn-group -->
                    <asp:TextBox runat="server" ID="SearchValue" type="text" class="form-control"  style="width:145px;"/>
                    <asp:Button ID="SearchButton" runat="server" class="btn btn-primary" Text="검색" style="width:60px;" OnClick="SearchButton_Click" />
                </div>
                <!-- /input-group -->
            </div>
        </div>

    </div>


    <script type="text/javascript">

        function confirmPostWrite() {
            if (confirm("새 글을 작성하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

    </script>


</asp:Content>

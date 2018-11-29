<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarSearch.master" AutoEventWireup="true" CodeBehind="SearchCommentList.aspx.cs" Inherits="cafeLetter.Search.SearchCommentList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarServiceBody" runat="server">



    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <!-- -->
        <div class="row">
            <div class="col-md-11">
                <h2>전체 댓글 검색</h2>
            </div>
        </div>



        <div class="btn-group" role="group" aria-label="...">
            <asp:Button ID="Page10" runat="server" type="button" class="btn btn-default" Text="10" OnClick="Page10_Click"></asp:Button>
            <asp:Button ID="Page20" runat="server" type="button" class="btn btn-default" Text="20" OnClick="Page20_Click"></asp:Button>
            <asp:Button ID="Page30" runat="server" type="button" class="btn btn-default" Text="30" OnClick="Page30_Click"></asp:Button>
        </div>
        <span class="text-muted">&nbsp; 페이지 수</span>



       

        <hr class="featurette-divider">

        <div class="row" style="margin-top: 12px;">


            <asp:Repeater ID="PostListPanel" runat="server">
                <ItemTemplate>
                    <div class="panel panel-default" style="width: 97%; margin: 0 auto; margin-bottom: 10px;">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <span class="data D" style="color:red"><%# Eval("BOARDTYPENAME").ToString().Length <2 ? "갤러리" : Eval("BOARDTYPENAME")  %></span> &nbsp; &nbsp; 
                                    <br />
                                    <span class="data D">작성자 &nbsp; <%# Eval("USERID") %></span> &nbsp; &nbsp;
                                    <br />
                                    <span class="data D">등록날짜 &nbsp; <%# Eval("COMMENTDATE").ToString().Substring(0,10) %></span>
                                </div>
                                <div class="col-md-8">
                                    <asp:HyperLink ID="BoardViewLink" runat="server" Text='<%#  Eval("COMMENTCONTENT")  %>' NavigateUrl='<%#  Eval("BOARDTYPENAME").ToString().Length <2 ?  String.Format("/Gallery/GalleryView.aspx?PhotoNo={0}",Eval("PHOTONO")) :String.Format("/Board/BoardView.aspx?BoardNo={0}", Eval("BOARDNO"))   %>'></asp:HyperLink>
                                </div>

                                <!-- String.Format("/Board/BoardView.aspx?BoardNo={0}", Eval("BOARDNO")) -->
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

                <!-- 검색 -->
                <div class="input-group pull-right" style="margin-top: 20px; width:305px;">
                    <div class="input-group-btn">
                        <asp:DropDownList ID="SearchMenu" runat="server" CssClass="form-control" Style="width: 90px">
                            <asp:ListItem>내용</asp:ListItem>
                            <asp:ListItem>아이디</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <!-- /btn-group -->
                    <asp:TextBox runat="server" ID="SearchValue" type="text" class="form-control" Style="width: 145px;" />
                    <asp:Button ID="SearchButton" runat="server" class="btn btn-primary" Text="검색" Style="width: 60px;" OnClick="SearchButton_Click" />
                </div>
                <!-- /input-group -->
            </div>
        </div>

    </div>




</asp:Content>

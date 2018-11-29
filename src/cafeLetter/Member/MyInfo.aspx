<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarMember.master" AutoEventWireup="true" CodeBehind="MyInfo.aspx.cs" Inherits="cafeLetter.Member.MyInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarMemberHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarMemberBody" runat="server">

    <div class="col-sm-9 col-md-10">
        <!-- UserName -->
        <div class="row">
            <div class="col-md-9">

            
            <div class="page-header">
                <h3>나의 활동</h3>
            </div>
                </div>
        </div>

        <!-- MyWrite -->
        <div class="row">
            <div class="col-md-9">
                <h6>내가 작성한 글</h6>
            </div>
            <div class="col-md-3">
                <asp:Button ID="SearchMyPost" runat="server" class="btn btn-primary" style="float:right" Text="더보기" OnClick="SearchMyPost_Click"></asp:Button>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="MyPostList" runat="server">
                <ItemTemplate>
                    <div class="panel panel-default" style="width: 97%; margin: 0 auto; margin-bottom: 10px;">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <span class="data D" style="color: red"><%# Eval("BOARDTYPENAME") %></span> &nbsp; &nbsp; 
                                            <br />
                                    <span class="data D">조회수 &nbsp; <%# Eval("BOARDVIEW") %></span> &nbsp; &nbsp; 
                                            <br />
                                    <span class="data D">등록날짜 &nbsp; <%# Eval("BOARDDATE") %></span>
                                </div>
                                <div class="col-md-8">
                                    <asp:HyperLink ID="BoardViewLink" runat="server" Text='<%# Eval("BOARDTITLE") + " ["+ Eval("BOARDCOMMENTCNT") + "]"  %>' NavigateUrl=' <%# Eval("BOARDTYPENAME").ToString().Length <2 ?  String.Format("/Gallery/GalleryView.aspx?PhotoNo={0}",Eval("PHOTONO")) :String.Format("/Board/BoardView.aspx?BoardNo={0}", Eval("BOARDNO")) %>'></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <hr class="featurette-divider">
        <!-- MyComment -->
        <div class="row">
            <div class="col-md-9">
                <h6>내가 작성한 댓글</h6>
            </div>
            <div class="col-md-3">
                <asp:Button ID="SearchMyComment" runat="server" class="btn btn-primary" Text="더보기" style="float:right" OnClick="SearchMyComment_Click"></asp:Button>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="MyCommentList" runat="server">
                <ItemTemplate>
                    <div class="panel panel-default" style="width: 97%; margin: 0 auto; margin-bottom: 10px;">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
                                    <span class="data D" style="color: red"><%# Eval("BOARDTYPENAME").ToString().Length <2 ? "갤러리" : Eval("BOARDTYPENAME")  %></span> &nbsp; &nbsp; 
                                         <br />
                                    <span class="data D">등록날짜 &nbsp; <%# Eval("COMMENTDATE") %></span>
                                </div>
                                <div class="col-md-8">
                                    <asp:HyperLink ID="BoardViewLink" runat="server" Text='<%#  Eval("COMMENTCONTENT")  %>' NavigateUrl='<%#  Eval("BOARDTYPENAME").ToString().Length <2 ?  String.Format("/Gallery/GalleryView.aspx?PhotoNo={0}",Eval("PHOTONO")) :String.Format("/Board/BoardView.aspx?BoardNo={0}", Eval("BOARDNO"))   %>'></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <hr class="featurette-divider">

        <!-- MyGallery-->
        <div class="row">
            <div class="col-md-9">
                <h6>내가 올린 사진</h6>
            </div>
            <div class="col-md-3">
                <asp:Button ID="SearchMyPhoto" runat="server" class="btn btn-primary" style="float:right" Text="더보기" OnClick="SearchMyPhoto_Click"></asp:Button>
            </div>
        </div>
        <div class="row">
            <!-- 첫번째 -->
            <div class="col-md-4">
                <asp:HyperLink ID="FirstImgLink" runat="server">
                    <asp:Image runat="server" ID="FirstImg" CssClass="img-responsive" /></asp:HyperLink>
            </div>
            <!-- 두번쨰 -->
            <div class="col-md-4">
                <asp:HyperLink ID="SecondImgLink" runat="server">
                    <asp:Image runat="server" ID="SecondImg" CssClass="img-responsive" /></asp:HyperLink>
            </div>
            <!-- 세번째 -->
            <div class="col-md-4">
                <asp:HyperLink ID="ThirdImgLink" runat="server">
                    <asp:Image runat="server" ID="ThirdImg" CssClass="img-responsive" /></asp:HyperLink>
            </div>
        </div>
    </div>
</asp:Content>

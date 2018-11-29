<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="GalleryList.aspx.cs" Inherits="cafeLetter.Gallery.GalleryList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationBarHead" runat="server">
<style>
        .btn{
            color: #000;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="NavigationBarBody" runat="server">

    <div class="container-fluid">
        <div class="row" style="width: 70%; margin: 0 auto;">
            <!-- 갤러리 Title -->
            <div class="row" style="margin-top: 40px;">
                <div class="col-md-11">
                    <h2>갤러리 </h2>
                </div>
                <div class="col-md-1">
                    <asp:ImageButton ID="GalleryNewWrite" runat="server" ImageUrl="/image/photo.png" Width="40px" Height="40px" OnClientClick="return confirmPostWrite();"  OnClick="GalleryNewWrite_Click" />
                </div>

            </div>

            <hr class="featurette-divider" style="margin-top: 0px;">

            <div class="row">
                <!-- 페이지 수 처리 -->
                <div class="btn-group" role="group" aria-label="...">
                    <asp:Button ID="Page12" runat="server" type="button" class="btn btn-default" Text="12" OnClick="Page12_Click"></asp:Button>
                    <asp:Button ID="Page24" runat="server" type="button" class="btn btn-default" Text="24" OnClick="Page24_Click"></asp:Button>
                    <asp:Button ID="Page36" runat="server" type="button" class="btn btn-default" Text="36" OnClick="Page36_Click"></asp:Button>
                </div>
                <span class="text-muted">&nbsp; 페이지 수</span>


                <!-- 정렬 버튼-->
                <div class="btn-group" role="group" aria-label="..." style="float: right; margin-right: 15px;">
                    <asp:Button ID="Newest" runat="server" type="button" class="btn btn-default" Text="최신순" OnClick="Newest_Click"></asp:Button>
                    <asp:Button ID="Hot" runat="server" type="button" class="btn btn-default" Text="조회순" OnClick="Hot_Click"></asp:Button>
                    <asp:Button ID="Comment" runat="server" type="button" class="btn btn-default" Text="댓글순" OnClick="Comment_Click"></asp:Button>
                </div>
            </div>

            
   


            <!-- 갤러리 사진 -->
            <div class="row" style="margin-top: 20px;">
                <!-- 첫번째 -->
                <div class="col-md-4">
                    <asp:Repeater ID="firstImgs" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("/Gallery/GalleryView.aspx?PhotoNo={0}",Eval("strPhotoNo")) %>'> <img src="<%#Eval("strPhotoURL")%>"  alt="Responsive image" class="img-responsive" style="margin-bottom: 10px;" /> </asp:HyperLink>
                        </ItemTemplate>
                    </asp:Repeater>

                </div>
                <!-- 두번쨰 -->
                <div class="col-md-4">
                    <asp:Repeater ID="secondImgs" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("/Gallery/GalleryView.aspx?PhotoNo={0}",Eval("strPhotoNo")) %>'> <img src="<%#Eval("strPhotoURL")%>" alt="Responsive image" class="img-responsive" style="margin-bottom: 10px;" /> </asp:HyperLink>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <!-- 세번째 -->
                <div class="col-md-4">
                    <asp:Repeater ID="thirdImgs" runat="server">
                        <ItemTemplate>
                            <asp:HyperLink runat="server" NavigateUrl='<%# String.Format("/Gallery/GalleryView.aspx?PhotoNo={0}",Eval("strPhotoNo")) %>'> <img src="<%#Eval("strPhotoURL")%>" alt="Responsive image" class="img-responsive" style="margin-bottom: 10px;" /> </asp:HyperLink>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
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
                                <asp:ListItem>제목</asp:ListItem>
                                <asp:ListItem>아이디</asp:ListItem>
                                <asp:ListItem>태그</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <!-- /btn-group -->
                        <asp:TextBox runat="server" ID="SearchValue" type="text" class="form-control" Style="width: 145px;" />
                        <asp:Button ID="SearchButton" runat="server" class="btn btn-primary" Text="검색" style="width:60px;" OnClick="SearchButton_Click" />
                    </div>
                    <!-- /input-group -->
                </div>
            </div>

        </div>
    </div>



       <script type="text/javascript">

        function confirmPostWrite() {
            if (confirm("새 갤러리를 작성하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }
  
    </script>




      

</asp:Content>

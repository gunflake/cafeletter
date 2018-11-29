<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="ItemList.aspx.cs" Inherits="cafeLetter.ItemSupervise.ItemList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">


    



    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <div class="col-md-11">
                <h4>물품 리스트 </h4>
            </div>
            <div class="col-md-1">
                 <a href="./ItemAdd.aspx" class="brand">
                    <img alt="Brand" src="/image/plus.png" width="40" height="40" /></a>
            </div>
        </div>
        
        <hr class="featurette-divider">

        <!-- 권한 목록 -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            
                <asp:Repeater ID="AuthorityListView" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <tr class="table-active">
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">물품명</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">종류</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">원래가격</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">할인금액</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">판매가격</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">남은개수</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">등록날짜</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">판매여부</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">관리</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><%# Eval("ITEMNAME") %></td>
                            <td style="text-align:center"><%# Eval("ITEMTYPENAME") %></td>
                            <td style="text-align:center"><%# Eval("ITEMORGPRICE") %></td>
                            <td style="text-align:center"><%# Eval("ITEMDISCPRICE") %></td>
                            <td style="text-align:center"><%# Eval("ITEMREALPRICE") %></td>
                            <td style="text-align:center"><%# Eval("ITEMCOUNT") %></td>
                            <td style="text-align:center"><%# Eval("REGDATE") %></td>
                            <td style="text-align:center"><%# Eval("SALESTATE") %></td>
                            <td style="text-align:center"><asp:HyperLink  runat="server" Text='수정' NavigateUrl='<%# String.Format("/ItemSupervise/ItemModify.aspx?intItemNo={0}&strItemCode={1}",Eval("ITEMNO"), Eval("ITEMCODE")) %>'></asp:HyperLink></td>
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
                            <asp:ListItem>물품명</asp:ListItem>
                            <asp:ListItem>종류</asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <!-- /btn-group -->
                    <asp:TextBox runat="server" ID="SearchValue" type="text" class="form-control" style="width:145px;" />
                    <asp:Button ID="SearchBtn" runat="server" class="btn btn-primary"  style="width:60px;" Text="검색" OnClick="SearchBtn_Click" />
                </div>
                <!-- /input-group -->
            </div>
        </div>






    </div>


</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarItem.master" AutoEventWireup="true" CodeBehind="MyBasket.aspx.cs" Inherits="cafeLetter.Item.MyBasket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarItemHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarItemBody" runat="server">


    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <!-- -->
        <div class="row">
            <div class="col-md-8">
                <h2>장바구니 목록</h2>
            </div>
            <div class="col-md-4">
                <asp:Button runat="server" ID="BuyItem" class="btn btn-primary" Text="구매하기" OnClick="BuyItem_Click" style="float:right"/>
            </div>
        </div>
          
        <hr class="featurette-divider">

        <div class="row" style="margin-top: 12px;">

            <asp:Repeater ID="ListPanel" runat="server">
                <ItemTemplate>
                    <div class="panel panel-default" style="width: 97%; margin: 0 auto; margin-bottom: 10px;">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <img src='<%# Eval("ITEMIMG") %>' alt="" height=65 width=65></img>
                                </div>
                                <div class="col-md-4">
                                    <span class="data D" > <%# Eval("ITEMNAME") %></span>
                                    <br />
                                    <span class="data D"> <%# Eval("ITEMDESC") %></span>
                                </div>
                                <div class="col-md-4">
                                    <span class="data D"> <%# Eval("SITEMPRICE") %> X <%# Eval("ITEMCOUNT") %> = <%# Eval("SITEMTOTALPRICE") %> Cash</span>
                                    <asp:Literal runat="server" ID="ItemNo" Visible="false" Text='<%# Eval("ITEMNO") %>'></asp:Literal></span>
                                </div>
                                <div class="col-md-1">
                                    <asp:Button runat="server" ID="DeleteBasket" class="btn btn-primary" Text="삭제" OnClick="DeleteBasket_Click"/>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
        <br />
        <h6>총 가격   : <%= intPayPrice.ToString("#,##0") %> 캐시 </h6>
        <h6>보유 캐시 : <%= intMyCash.ToString("#,##0") %> 캐시 </h6>

    </div>


</asp:Content>

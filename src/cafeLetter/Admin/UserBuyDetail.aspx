<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="UserBuyDetail.aspx.cs" Inherits="cafeLetter.Admin.UserBuyDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">


    <div class="col-sm-9 col-md-10" style="top: 40px;">

                <!-- -->
                <div class="row">
                    <div class="col-md-8">
                        <h3>구매 상품 목록</h3>
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>

            <hr class="featurette-divider">

            <asp:HiddenField runat="server" ID="PaymentPrice"/>
            <asp:HiddenField  runat="server" ID="SessionID"/>
            <!-- 권한 목록 -->
            <div class="row" style="width:100%; margin-left: 0px; margin-right: 0px;">
                <h6>구매 물품</h6>
                <asp:Repeater ID="ListPanel" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <tr class="table-active">
                                <th scope="row" style="background-color: #c4e3f3; text-align: center"></th>
                                <th scope="row" style="background-color: #c4e3f3; text-align: center">물품명</th>
                                <th scope="row" style="background-color: #c4e3f3; text-align: center">가격</th>
                                <th scope="row" style="background-color: #c4e3f3; text-align: center">수량</th>
                                <th scope="row" style="background-color: #c4e3f3; text-align: center">금액</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align: center">
                                <img style=""vertical-align: middle" src='<%# Eval("ITEMIMG") %>' alt="" height="65" width="65"></img></td>
                            <td style="text-align: center; vertical-align: middle"><%# Eval("ITEMNAME") %></td>
                            <td style="text-align: right; vertical-align: middle"><%# Eval("ITEMPRICE") %></td>
                            <td style="text-align: center; vertical-align: middle"><%# Eval("ITEMCOUNT") %></td>
                            <td style="text-align: right; vertical-align: middle"><%# Eval("ITEMTOTALPRICE")%> 원두</td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <br />
            <!-- 구매 상품 끝 -->
            <hr class="featurette-divider">


            <div class="row" style="width:100%; margin-left: 0px; margin-right: 0px;">
                <h6>받는 사람 정보</h6>
                <table class="table table-bordered table-hover" id="trIssueStep2">
                    <colgroup>
                        <col style="width: 20%" />
                        <col style="width: 80%" />
                    </colgroup>
                    <tr>
                        <th class="text-center info">받는 사람</th>
                        <td class="text">
                             <%=strUserName %>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">휴대폰</th>
                        <td class="text">
                             <%=strPhone %>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">주소</th>
                        <td class="text">
                             <%=strAddress %>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">배송메시지</th>
                        <td class="text">
                             <%=strMsg %>
                        </td>
                    </tr>
                </table>
            </div>

            <hr class="featurette-divider">

            <div class="row">
                <div class="col-md-8">
                    <h6>결제 정보</h6>
                </div>
            </div>
            <div class="row" style="width: 100%; margin-left: 0px; margin-right: 0px;">

                <table class="table table-bordered table-hover" id="trIssueStep2">
                    <colgroup>
                        <col style="width: 20%" />
                        <col style="width: 40%" />
                        <col style="width: 40%" />
                    </colgroup>
                    <tr>
                        <th class="text-center info">주문 금액</th>
                        <td class="text">
                             <%=intOrderPrice.ToString("#,##0") %> 원두
                        </td>
                        <td class="text">
                            배송비 <%=intShipPrice.ToString("#,##0") %> 원두
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">결제 금액</th>
                        <td class="text">
                             <%=intTotalPrice.ToString("#,##0") %> 원두
                        </td>
                        <td class="text">
                            원두 구매
                        </td>
                    </tr>
                </table>


            </div>

            <asp:Button ID="BuyListBtn" runat="server" class="btn btn-primary" Text="목록보기"  OnClick="BuyListBtn_Click"></asp:Button>

        </div>


</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="UserBuyList.aspx.cs" Inherits="cafeLetter.Admin.UserBuyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">


    
    

    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <div class="col-md-11">
                <h4>회원 구매 내역 </h4>
            </div>
        </div>
        
        <hr class="featurette-divider">

        <!-- 권한 목록 -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            
                <asp:Repeater ID="MyCashList" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <tr class="table-active">
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">아이디</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">주문일자</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">상품 정보</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">상품금액</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">상태</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><%# Eval("USERID") %></td>
                            <td style="text-align:center"><%# Eval("REGDATE").ToString().Substring(0,10) %></td>
                            <td style="text-align:center"><asp:HyperLink  runat="server" Text='<%# Eval("PURCHASEINFO") %>' NavigateUrl='<%# String.Format("/Admin/UserBuyDetail.aspx?strPurchaseNo={0}&strUserID={1}",Eval("PURCHASENO"), Eval("USERID")) %>'></asp:HyperLink></td>
                            <td style="text-align:right"><%# Eval("STOTALPRICE") %> 원두</td>
                            <td style="text-align:center"><%# Eval("PURCHASESTATE") %></td>
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
            </div>
        </div>






    </div>



</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="BuyResult.aspx.cs" Inherits="cafeLetter.Item.BuyResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationBarHead" runat="server">
    <link href="https://fonts.googleapis.com/css?family=Nanum+Gothic:400,700,800&amp;subset=korean" rel="stylesheet">
    <style>
      p {
        font-family: "Nanum Gothic", sans-serif;
        font-size: 28px;
      }
      p.b {
        font-weight: 600;
      }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationBarBody" runat="server">

    
    
    <div class="container-fluid">
        <div class="row" style="width: 50%; margin: 0 auto;">

            <!-- 결제 완료 이미지 -->
            <img src="/image/BuyResult.PNG" alt="" style="margin-left:10px; margin-top: 30px; margin-bottom: 30px;" />

            <hr class="featurette-divider">

            <div class="row">
                <p class="b" style="text-align:center; margin-bottom: 30px;">구매하신 상품의 <span style="color:#02a4af;">결제가 완료</span>되었습니다.</p>
            </div>
             <div class="row" style="margin:0 auto; width:100%; ">
                <table class="table table-bordered table-hover" id="trIssueStep2">
                    <colgroup>
                        <col style="width: 30%" />
                        <col style="width: 70%" />
                    </colgroup>
                    <tr>
                        <th class="text-center info">결제 항목</th>
                        <td class="text">
                          <span style="font-weight:bold;">  <%= strBuyName %> </span>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">주문 금액</th>
                        <td class="text">
                            <span style="font-weight:bold;"><%=intOrderAmount.ToString("#,##0") %> 원두 </span>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">배송비</th>
                        <td class="text">
                          <span style="font-weight:bold;">  <%= intShipAmount.ToString("#,##0") %> 원두</span>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">결제 금액</th>
                        <td class="text">
                             <span style="font-weight:bold; color:#ff0000"><%=intTotalAmount.ToString("#,##0") %> 원두 </span>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="row">
                <asp:Button runat="server" ID="BuyInfo" style="float:right; margin-right: 15px;" class="btn btn-primary" Text="구매내역 확인" OnClick="BuyInfo_Click"/>
            </div>




        </div>
    </div>

    <script>
    </script>

</asp:Content>

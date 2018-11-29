<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationBar.master" AutoEventWireup="true" CodeBehind="CashResult.aspx.cs" Inherits="cafeLetter.Cash.CashResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationBarHead" runat="server">

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
            <img src="/image/CashResult.png" alt="" style="margin-left: 15px; margin-top: 30px; margin-bottom: 30px;" />

            <hr class="featurette-divider">

            <div class="row">
                <p class="b" style="text-align:center;margin-bottom: 30px;">구매하신 원두의 <span style="color:#02a4af;">충전이 완료</span>되었습니다.</p>
            </div>
            <div class="row" style="width: 100%; margin-left: 0px; margin-right: 0px;">
                 <table class="table table-bordered table-hover" id="trIssueStep2">
                    <colgroup>
                        <col style="width: 20%" />
                        <col style="width: 80%" />
                    </colgroup>
                     <tr>
                        <th class="text-center info">결제 항목</th>
                        <td class="text">
                             원두 구매
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">결제금액</th>
                        <td class="text">
                             <%=intPayAmount %> 원
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">보유 원두</th>
                        <td class="text">
                            <span style="font-weight:bold;"><%=intMyCash %>  원두 </span>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">결제 수단</th>
                        <td class="text">
                            <%=strPayTool %>
                        </td>
                    </tr>
                </table>
                
            </div>

            <div class="row">
                <asp:Button runat="server" ID="MyInfoCash" style="float:right; margin-right: 15px;" class="btn btn-primary" Text="내 결제 정보" OnClick="MyInfoCash_Click"/>
            </div>




        </div>
    </div>

    <script>
    </script>

</asp:Content>

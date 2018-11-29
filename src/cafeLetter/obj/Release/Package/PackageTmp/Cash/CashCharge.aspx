<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarMember.master" AutoEventWireup="true" CodeBehind="CashCharge.aspx.cs" Inherits="cafeLetter.Cash.CashCharge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarMemberHead" runat="server">

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarMemberBody" runat="server">
    

    <div class="col-sm-9 col-md-10">

        <!-- UserName -->
        <div class="row">
                <div class="page-header">
                    <h3>캐시 충전하기</h3>
                </div>
        </div>

        <!-- UserName -->
        <div class="row" style="width: 65%; margin-right: 0px;">
            <table class="table table-bordered">
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">결제 방법</th>
                        <td> 
                            <asp:RadioButton GroupName="PayTool" ID="Mobile" runat="server" Text="핸드폰"  />
                            <asp:RadioButton GroupName="PayTool" ID="CreditCard" runat="server" Text="신용카드"  />
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">충전할 금액 선택</th>
                        <td> 
                            <asp:RadioButton GroupName="Charge" ID="Cash100" runat="server" Text="100원"  />
                            <asp:RadioButton GroupName="Charge" ID="Cash500" runat="server" Text="500원"  />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div>
            <div class="row" style="width: 65%;">
                <asp:Button runat="server" ID="cashBtn" Text="충전" OnClick="cashBtn_Click"/>
            </div>
        </div>
    </div>
   
    <script type="text/javascript">

        



        $('#execute').click(function () {

       

            $.ajax({
                url: "https://stagepg.payletter.com/TLSTest/test.html"
                , type: "GET"
                , success: function (result) {
                    alert("OK");
                }
            })
        })

         $('#e').click(function () {
             $.ajax({
                 url: "/Cash/CashHandler.ashx/printHello"
                 , type: "GET"
                 , success: function (result) {
                     alert("OK" + result);
                }
            })
        })




    </script>





</asp:Content>

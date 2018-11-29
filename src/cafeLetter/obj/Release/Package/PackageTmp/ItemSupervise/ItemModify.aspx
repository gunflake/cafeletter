<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="ItemModify.aspx.cs" Inherits="cafeLetter.ItemSupervise.ItemModify"%>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">




    <div class="col-sm-9 col-md-10" style="top: 40px;">
        <h4>물품 수정</h4>
        <hr class="featurette-divider">

        <!-- UserName -->
        <div class="row" style="width: 50%; margin-left: 0px; margin-right: 0px;">

            <asp:HiddenField runat="server" ID="ItemNo"/>
            <asp:HiddenField runat="server" ID="RealPrice" />
            <table class="table table-bordered">
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b; width: 50%;">물품명</th>
                        <td>
                            <asp:TextBox runat="server" ID="ItemName" Enabled="false"></asp:TextBox></td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">물품종류</th>
                        <td>
                             <asp:TextBox runat="server" ID="ItemCode" Enabled="false"></asp:TextBox>
                        </td>
                            
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">물품원래가격</th>
                        <td>
                            <asp:TextBox runat="server" ID="ItemOrgPrice" onkeyup="Calculate()"></asp:TextBox></td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;" >물품할인가격</th>
                        <td>
                            <asp:TextBox runat="server" ID="ItemDiscPrice" onkeyup="Calculate()" ></asp:TextBox></td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">물품판매가격</th>
                        <td>
                            
                            <asp:TextBox runat="server" ID="ItemRealPrice" Enabled="false"></asp:TextBox></td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">물품판매상태</th>
                        <td>
                            <asp:RadioButton GroupName="Sale" ID="SaleTrue" runat="server" Text="판매"/>
                            <asp:RadioButton GroupName="Sale" ID="SaleFalse" runat="server" Text="판매중지"  />
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
        <!-- Button -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            <asp:Button ID="ItemModifyBtn" runat="server" class="btn btn-primary" Text="수정" OnClientClick="return checkValidation();" OnClick="ItemModifyBtn_Click"></asp:Button>
            <asp:Button ID="ItemDeleteBtn" runat="server" class="btn btn-primary" Text="삭제" OnClick="ItemDeleteBtn_Click"></asp:Button>
        </div>



    </div>

     <script type="text/javascript">
        var realPrice = document.getElementById('<%=ItemRealPrice.ClientID %>').value;


         function Calculate() {
             var orgPrice = document.getElementById('<%=ItemOrgPrice.ClientID %>').value;
             var discPrice = document.getElementById('<%=ItemDiscPrice.ClientID %>').value;
             var regNumber = /^[0-9]*$/;

             if (!regNumber.test(orgPrice)) {
                 alert("숫자만 입력가능합니다");
                 document.getElementById('<%=ItemOrgPrice.ClientID %>').value = "";
                 return;
             }

             if (!regNumber.test(discPrice)) {
                 alert("숫자만 입력가능합니다");
                 document.getElementById('<%=ItemDiscPrice.ClientID %>').value = "";
                 return;
             }

             if (parseInt(orgPrice)  < parseInt(discPrice)) {
                 alert("할인가격보다 원가가 높아야합니다.");
                 document.getElementById('<%=ItemOrgPrice.ClientID %>').value = "";
                 document.getElementById('<%=ItemDiscPrice.ClientID %>').value = "";
                 document.getElementById('<%=ItemRealPrice.ClientID %>').value = "";
                 document.getElementById('<%=RealPrice.ClientID %>').value = "";
                 return;
             }

             document.getElementById('<%=ItemRealPrice.ClientID %>').value = orgPrice - discPrice;
             document.getElementById('<%=RealPrice.ClientID %>').value = orgPrice - discPrice;
         }

         function checkValidation() {

          

             if (confirm("물품을 수정하시겠습니까?")) {
                 return true;
             } else {
                 return false;
             }

         }

     </script>


</asp:Content>

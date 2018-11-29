<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="ItemAdd.aspx.cs" Inherits="cafeLetter.ItemSupervise.ItemAdd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">


    


    <div class="col-sm-9 col-md-10" style="top: 40px;">

            <h4>물품 추가</h4>

        <hr class="featurette-divider">

        <!-- UserName -->
        <div class="row" style="width:50%; margin-left: 0px; margin-right: 0px;">
           
         <asp:HiddenField runat="server" ID="RealPrice" />

                <table class="table table-bordered">
                    <tbody>
                        <tr class="table-active" >
                            <th scope="row" style="background-color: #3db1c93b; width:50%;">물품명</th>
                            <td><asp:TextBox runat="server" ID="ItemName" ></asp:TextBox></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">물품종류</th>
                            <td><asp:DropDownList runat="server" ID="ItemCode"></asp:DropDownList></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">물품가격</th>
                            <td><asp:TextBox runat="server" ID="ItemOrgPrice" onkeyup="Calculate()"></asp:TextBox></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">물품할인가격</th>
                            <td><asp:TextBox runat="server" ID="ItemDiscPrice" onkeyup="Calculate()"></asp:TextBox></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">물품판매가격</th>
                            <td><asp:TextBox runat="server" ID="ItemRealPrice" Enabled="false"></asp:TextBox></td>
                        </tr>
                        <tr class="table-active">
                            <th scope="row" style="background-color: #3db1c93b;">물품수량</th>
                            <td><asp:TextBox runat="server" ID="ItemCount"></asp:TextBox></td>
                        </tr>
                    </tbody>
                </table>
         
        </div>
         <!-- Button -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            <asp:Button ID="ItemAddBtn" runat="server" class="btn btn-primary" Text="추가" OnClientClick="return checkValidation();" OnClick="ItemAddBtn_Click"></asp:Button>
            <asp:Button ID="ItemAddCancel" runat="server" class="btn btn-primary" Text="취소"  OnClick="ItemAddCancel_Click"></asp:Button>
            <asp:FileUpload ID="FileUpload" Text="파일선택" runat="server" /> 
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

             if (parseInt(orgPrice) < parseInt(discPrice)) {
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



            if (confirm("물품을 등록하시겠습니까?")) {
                return true;
            } else {
                return false;
            }

        }

    </script>


</asp:Content>

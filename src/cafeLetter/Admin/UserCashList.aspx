<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="UserCashList.aspx.cs" Inherits="cafeLetter.Admin.UserCashList"  %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">


    

    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <div class="col-md-8">
                <h4>회원 결제 목록
                </h4>
            </div>
            <div class="col-md-4">
                <button type="button" class="btn btn-info" id="MyBasket" data-toggle="modal" style="float:right;margin-left: 5px;" >보너스원두 발행</button>
                <asp:Button runat="server" ID="hideIssue"  OnClick="hideIssue_Click" style="display:none" />
            </div>
        </div>
        
        <hr class="featurette-divider">

        <!-- 권한 목록 -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            
                <asp:Repeater ID="MyCashList" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <tr class="table-active">
                                <th scope="row" style="background-color: #3db1c93b; text-align:center"">아이디</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center"">결제수단</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center"">결제날짜</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center"">취소날짜</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">결제금액</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center"">상태</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><%# Eval("USERID") %></td>
                            <td style="text-align:center"><%# Eval("PGTOOL") %></td>
                            <td style="text-align:center"><%# Eval("PAYYMD").ToString().Substring(0, 10) %></td>
                            <td style="text-align:center"><%# Eval("CNLYMD").ToString().Length>1 ? Eval("CNLYMD").ToString().Substring(0,10) : "X" %></td>
                            <td style="text-align:right"><%# Eval("PAYCASHAMT") %>원</td>
                            <td style="text-align:center">
                                <asp:Literal runat="server" Text='<%# Eval("STATE") %>'></asp:Literal>
                            </td>
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
        

        <!-- Bonus Cash Insert Modal -->
        <div class="modal fade" id="BonusModal" role="dialog">
            <div class="modal-dialog">

                <!-- Modal content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title" id="modalTitle">대량 보너스 원두 발행</h4>
                    </div>
                    <div class="modal-body">
                        <div>
                            A2부터 AXX까지 아이디, B2부터 BXX까지 발행할 금액을 입력하세요.
                        </div>

                            <table class="table table-bordered table-hover" id="trIssueStep2">
                                <colgroup>
                                    <col style="width: 40%" />
                                    <col style="width: 60%" />
                                </colgroup>
                                 <tr>
                                    <th class="text-center info">엑셀파일선택</th>
                                    <td class="text-muted">
                                        <asp:FileUpload ID="FileUpload" Text="파일선택" runat="server" />   
                                    </td>
                                </tr>
                                <tr>
                                <tr>
                                    <th class="text-center info">지급 사유</th>
                                    <td class="text-muted">
                                        <input id="txtIssuePayToolName" name="paytoolname" type="text" class="form-control" maxlength="50" /></td>
                                </tr>
                            </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        <button type="button" id="BonusCashIssue"  class="btn btn-primary" >발행하기</button>
                        
                    </div>
                </div>

            </div>
        </div>
        <!-- /Modal Bonus Cash Issue Area  -->
        
    </div>

    <script>

        $(function () {
            $("#MyBasket").click(function () {
                location.href = "/Admin/BonusCashIssue.aspx";
            });

            $("#BonusCashIssue").click(function () {
                document.getElementById('<%= hideIssue.ClientID %>').click();
           
            });
        });

        function excelDataParse() {

            var id_string = "";
            var pay_string = "";

            /* set up XMLHttpRequest */
            var url = "/file/bonusIssue.xlsx";
            var oReq = null;

            oReq = new XMLHttpRequest();
            oReq.open("GET", url, true);
            oReq.responseType = "arraybuffer";

            oReq.onload = function (e) {
                var arraybuffer = oReq.response;

                /* convert data to binary string */
                var data = new Uint8Array(arraybuffer);
                var arr = new Array();
                for (var i = 0; i != data.length; ++i) arr[i] = String.fromCharCode(data[i]);
                var bstr = arr.join("");

                /* Call XLSX */
                var workbook = XLSX.read(bstr, {
                    type: "binary"
                });

                /* DO SOMETHING WITH workbook HERE */

                var first_sheet_name = workbook.SheetNames[0];

                /* Get worksheet */
                var worksheet = workbook.Sheets[first_sheet_name];


                var start = 0;
                var end = worksheet['!ref'].length;
                var temp = worksheet['!ref'];

                for (var i = end - 1; i >= 0; i--) {
                    if (temp.charAt(i) >= '0' && temp.charAt(i) <= '9') {

                    } else {
                        start = i + 1;
                        break;
                    }
                }



                var length = worksheet['!ref'].substring(start, end);
                console.log(length);


                /* Find desired cell */


                for (var i = 2; i <= length; i++) {
                    var id_cell = 'A' + i;
                    var pay_cell = 'B' + i;
                    id_string += worksheet[id_cell].v;
                    pay_string += worksheet[pay_cell].v;
                    if (i == length) {
                        break;
                    }
                    id_string += ",";
                    pay_string += ",";
                }
                console.log(id_string);
                console.log(pay_string);

            }

            oReq.send();


        }

    </script>

</asp:Content>

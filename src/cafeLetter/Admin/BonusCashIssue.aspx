<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="BonusCashIssue.aspx.cs" Inherits="cafeLetter.Admin.BonusCashIssue" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">
    <script src="https://unpkg.com/xlsx/dist/xlsx.full.min.js"></script>
    <script src="../js/jquery.fileDownload.js"></script>
    <style>
        p {
            font-size: 25px;
        }

            
            p.b {
                font-weight: 500;
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">


    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <input type="button" name="BuyItems" style="float: right;" class="btn btn-info" id="BuyItems" onclick="useExcel()" value="사용방법" />
            <p class="b" style="margin-bottom: 30px;">대량 보너스 원두 발행</p>
        </div>

        <hr class="featurette-divider">
        <asp:Label ID="HiddenUrl" runat="server" Style="display: none"></asp:Label>
        <div class="row">
            <table class="table table-bordered table-hover" id="trIssueStep2">
                <colgroup>
                    <col style="width: 20%" />
                    <col style="width: 80%" />
                </colgroup>
                <tr>
                    <th class="text-center info">엑셀파일선택</th>
                    <td class="text-muted">
                        <asp:FileUpload ID="FileUpload" Text="파일선택" runat="server" />
                        <asp:Literal ID="hiddenL" Text="업로드가 완료되었습니다" runat="server" Visible="false"/>
                    </td>
                </tr>
                <tr>
                <tr>
                    <th class="text-center info">지급 사유</th>
                    <td class="text-muted">
                        <input id="txtIssuePayToolName" name="paytoolname" value="발표 시연 테스트" type="text" class="form-control" maxlength="50" /></td>
                </tr>
            </table>



            <div>
                <button type="button" id="BonusCashIssue" style="float: right;" onclick="issueStart()" class="btn btn-primary">발행하기</button>
                <asp:Button ID="UploadBtn" Style="float: right; margin-right: 5px;" runat="server" class="btn btn-primary" Text="업로드" OnClick="UploadBtn_Click" />
            </div>
        </div>

    </div>







    <!-- Bonus Cash Insert Modal -->
    <div class="modal fade" id="UseExcelModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="modalTitle">사용방법
                        <button type="button" style="float: right;" class="btn btn-info" onclick="sampleDown()" data-dismiss="modal">양식다운</button></h4>
                </div>
                <div class="modal-body">

                    <p>
                        A2 ~ A00 까지 ID를 입력하고,
                        <br />
                        B2 ~ B00 까지 금액을 입력합니다.
                    </p>
                    <img src="/image/useExcel.PNG" />
                    <p>
                        그 후, 엑셀 파일을 업로드 한 후<br />
                        발행하기 버튼을 눌러주세요.
                    </p>

                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary" data-dismiss="modal">닫기</button>
                </div>
            </div>

        </div>
    </div>
    <!-- /Modal Bonus Cash Issue Area  -->



    <script>
        var id_string = "";
        var pay_string = "";

        // $(function () {
        //     $("#BonusCashIssue").click(function () {
        //         excelDataParse();
        //         BonusIssue();
        //    });
        //});

        async function issueStart() {
            excelDataParse();
            var result = await resolveAfter2Seconds();
            BonusIssue();
        }

        function resolveAfter2Seconds() {
            return new Promise(resolve => {
                setTimeout(() => {
                    resolve('resolved');
                }, 2000);
            });
        }

        function excelDataParse() {
            var url = "";
            id_string = "";
            pay_string = "";
            url = document.getElementById('<%=HiddenUrl.ClientID%>').innerText;

            /* set up XMLHttpRequest */
            //var url = "/file/bonusIssue.xlsx";
            var oReq = null;
            var length = null;


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

                console.log(worksheet);

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



                length = worksheet['!ref'].substring(start, end);
                console.log(length);


                /* Find desired cell */

                console.log(length);

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

        function BonusIssue() {

            console.log("123" + id_string);
            console.log("123" + pay_string);

            var postData = {};
            var result = false;

            postData["strMethod"] = "BonusCashManyIssue";
            postData["strUserString"] = id_string;
            postData["strAmountString"] = pay_string;


            $.ajax({
                type: "POST",
                data: JSON.stringify(postData),
                url: "/Item/ItemHandler.ashx",
                dataType: "JSON",
                contentType: "application/json; charset=utf-8",
                error: function (request, status, error) {
                    alert("보너스 원두발행에 실패했습니다.");
                },
                success: function (data) {
                    console.log(data);
                    if (data.intRetVal == 0) {

                        alert("발행에 성공했습니다");
                        location.href = "/Admin/UserCashList.aspx";
                        reseult = true;
                    }
                    else {
                        console.log(data.intRetVal);
                        console.log(data.strResult);
                        alert("보너스 원두 발행에 실패했습니다.");
                    }
                }
            })

            return result;
        }

        function sampleDown() {
            $.fileDownload('/sample/sample.xlsx')
                .done(function () { alert('파일이 다운되었습니다'); })
                .fail(function () { alert('파일 다운로드에 실패했습니다'); });
        }

        function useExcel() {

            $("#UseExcelModal").appendTo("body").modal("show");
        }


    </script>

</asp:Content>

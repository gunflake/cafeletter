<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="Time.aspx.cs" Inherits="cafeLetter.Cash.Time" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationBarHead" runat="server">
    <script src="https://unpkg.com/xlsx/dist/xlsx.full.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationBarBody" runat="server">

        <div>
            <input type="button" value="클릭" onclick="excelDataParse()"/>
           <div id="dt_now"></div>
           
        </div>


        <script>

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
                            start = i+1;
                            break;
                        }
                    }

                    

                    var length = worksheet['!ref'].substring(start, end );
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

<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="BuyItem.aspx.cs" Inherits="cafeLetter.Item.BuyItem" %>

<asp:Content ID="Content1" ContentPlaceHolderID="NavigationBarHead" runat="server">
    <script src="http://dmaps.daum.net/map_js_init/postcode.v2.js"></script>
    <style>
      p {
        
        font-size: 22px;
      }
      p.b {
        font-weight: 600;
      }
    </style>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationBarBody" runat="server">


    <div class="container-fluid">
        <div class="row" style="width: 50%; margin: 0 auto;">

            <img src="/image/BuyProcess.PNG" alt="" style="margin-left:10px; margin-left:10px; margin-top: 30px; margin-bottom: 30px;" />

            <div class="row">
                <div class="col-md-8">
                    <h6>주문 상품</h6>
                </div>
            </div>

            <!-- 권한 목록 -->
            <div class="row" style="margin-left: 0px; margin-right: 0px;">

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
                                <img src='<%# Eval("ITEMIMG") %>' alt="" height="65" width="65"></img></td>
                            <td style="text-align: center"><%# Eval("ITEMNAME") %></td>
                            <td style="text-align: right"><%# Eval("SITEMPRICE") %></td>
                            <td style="text-align: center"><%# Eval("ITEMCOUNT") %></td>
                            <td style="text-align: right"><%# Eval("SITEMTOTALPRICE") %></td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                </asp:Repeater>
            </div>
            <br />
            <p style="text-align:right; color:#000000">상품 가격 : <%=intPayPrice.ToString("#,##0") %> 원두 </p>
            <p style="text-align:right; color:#000000"> 배송비 : 2,500 원두</p>

            <hr class="featurette-divider">

            <div class="row" style=" margin-left: 0px; margin-right: 0px;">
                <h6>구매자 정보</h6>



                <table class="table table-bordered table-hover" id="trIssueStep2">
                    <colgroup>
                        <col style="width: 20%" />
                        <col style="width: 80%" />
                    </colgroup>
                    <tr>
                        <th class="text-center info">주문자</th>
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
                        <th class="text-center info">이메일</th>
                        <td class="text">
                            <%=strEmail%>
                        </td>
                    </tr>
                </table>
            </div>
              

            <hr class="featurette-divider">

            <div class="row" style="margin-left: 0px; margin-right: 0px;">
                 <h6>받는 사람 정보 <input type="checkbox" id="rcvInfo" onclick="rcvInfoVal()"></h6> 


                <table class="table table-bordered table-hover" id="trIssueStep3">
                    <colgroup>
                        <col style="width: 20%" />
                        <col style="width: 80%" />
                    </colgroup>
                    <tr>
                        <th class="text-center info">이름</th>
                        <td class="text-muted">
                            <input id="Name" type="text" placeholder="받는사람 이름" class="form-control" /></td>
                    </tr>
                    <tr>
                        <th class="text-center info">휴대폰</th>
                        <td class="text-muted">
                            <input id="Phone" name="paytoolname" type="text" placeholder="숫자만 입력해주세요" class="form-control" /></td>
                    </tr>
                    <tr>
                        <th class="text-center info">주소</th>
                        <td class="text-muted">
                            <input type="button" onclick="sample6_execDaumPostcode()" class="btn btn-info" style="margin-bottom: 3px;" value="우편번호 찾기"><br />
                            <input id="sample6_postcode"  type="text" placeholder="우편번호" class="form-control" disabled/>
                            <input id="sample6_address"  type="text" placeholder="주소" class="form-control" disabled/>
                            <input id="sample6_address2"  type="text" placeholder="상세주소" class="form-control" />
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">배송메시지</th>
                        <td class="text-muted">
                            <input id="message" type="text" value="빠른 배송 부탁드립니다." class="form-control" /></td>
                    </tr>
                </table>

          
            </div>

            <hr class="featurette-divider">

            <div class="row" style=" margin-left: 0px; margin-right: 0px;">
                 <h6>결제 정보</h6>

                <table class="table table-bordered table-hover" id="trIssueStep4">
                    <colgroup>
                        <col style="width: 20%" />
                        <col style="width: 80%" />
                    </colgroup>
                    <tr>
                        <th class="text-center info">보유 원두</th>
                        <td class="text" style="text-align:right">
                            <span style="font-weight:bold;"><%=intMyCash.ToString("#,##0") %> 원두</span>
                        </td> 
                    </tr>
                    
                    <tr>
                        <th class="text-center info">총 결제 금액</th>
                        <td class="text" style="text-align:right">
                             <span style="font-weight:bold">  ( <%=intPayPrice.ToString("#,##0") %>  </span>
                            <img src="/image/cashplus.png" width="20" height="20" /> <span style="font-weight:bold"> 2,500 ) </span>
                            <span style="font-weight:bold; color:#ff0000"><img src="/image/equal.png" width="20" height="20" /> <%=intPaymentCash.ToString("#,##0") %> 원두 </span>
                        </td>
                    </tr>
                    <tr>
                        <th class="text-center info">결제 후 원두</th>
                        <td class="text" style="text-align:right">
                            <span style="font-weight:bold;"><%=intRemainCash.ToString("#,##0") %> 원두</span>
                        </td>
                    </tr>
                </table>
            </div>

            <input type="button" name="BuyItems" style="float:right;" class="btn btn-info"id="BuyItems" value="결제하기" />


        </div>
    </div>

    <script>
        function sample6_execDaumPostcode() {
            new daum.Postcode({
                oncomplete: function (data) {
                    // 팝업에서 검색결과 항목을 클릭했을때 실행할 코드를 작성하는 부분.

                    // 각 주소의 노출 규칙에 따라 주소를 조합한다.
                    // 내려오는 변수가 값이 없는 경우엔 공백('')값을 가지므로, 이를 참고하여 분기 한다.
                    var fullAddr = ''; // 최종 주소 변수
                    var extraAddr = ''; // 조합형 주소 변수

                    // 사용자가 선택한 주소 타입에 따라 해당 주소 값을 가져온다.
                    if (data.userSelectedType === 'R') { // 사용자가 도로명 주소를 선택했을 경우
                        fullAddr = data.roadAddress;

                    } else { // 사용자가 지번 주소를 선택했을 경우(J)
                        fullAddr = data.jibunAddress;
                    }

                    // 사용자가 선택한 주소가 도로명 타입일때 조합한다.
                    if (data.userSelectedType === 'R') {
                        //법정동명이 있을 경우 추가한다.
                        if (data.bname !== '') {
                            extraAddr += data.bname;
                        }
                        // 건물명이 있을 경우 추가한다.
                        if (data.buildingName !== '') {
                            extraAddr += (extraAddr !== '' ? ', ' + data.buildingName : data.buildingName);
                        }
                        // 조합형주소의 유무에 따라 양쪽에 괄호를 추가하여 최종 주소를 만든다.
                        fullAddr += (extraAddr !== '' ? ' (' + extraAddr + ')' : '');
                    }

                    // 우편번호와 주소 정보를 해당 필드에 넣는다.
                    document.getElementById('sample6_postcode').value = data.zonecode; //5자리 새우편번호 사용
                    document.getElementById('sample6_address').value = fullAddr;

                    // 커서를 상세주소 필드로 이동한다.
                    document.getElementById('sample6_address2').focus();
                }
            }).open();
        }



        $(document).ready(function () {

            //
            $('#BuyItems').click(function () {

                if (!validation()) {
                    alert("입력정보를 확인해주세요");
                    return;
                }

                var postData = {};
                var purchaseInfo = "<%= strPurchaseInfo%>";
                var ID = "<%=strUserID%>";
                var intTotalAmount = <%=intPayPrice %> + 2500; //배송비 계산하기

                var intPayCash = <%= intPaymentCash %>;
                var address = $("#sample6_address").val() + $("#sample6_address2").val();
                postData["strMethod"] = "BuyItem";
                postData["strName"] = $("#Name").val();
                postData["strPhone"] = $("#Phone").val();
                postData["strAddress"] = address;
                postData["strPostCode"] = $("#sample6_postcode").val();
                postData["strMsg"] = $("#message").val();
                postData["intAmount"] = intPayCash;
                postData["strUserID"] = ID;
                postData["strPurchaseInfo"] = purchaseInfo;
                postData["intShipPrice"] = 2500; // 배송비 

                $.ajax({
                    type: "POST",
                    data: JSON.stringify(postData),
                    url: "/Item/ItemHandler.ashx",
                    dataType: "JSON",
                    contentType: "application/json; charset=utf-8",
                    error: function (request, status, error) {
                        alert("일시적 통신 오류");
                    },
                    success: function (data) {
                        console.log(data);
                        if (data.intRetVal == 0) {
                            alert("주문이 완료되었습니다");
                            location.href = "/Item/BuyResult.aspx?intOrderAmount=" +<%=intPayPrice %>+"&intShipAmount=" + 2500 + "&intTotalAmount=" + intTotalAmount + "&strBuyName="+purchaseInfo;
                            return;
                        }
                        else {
                            if (data.intRetVal == 77) {
                                alert(data.strResult);
                                location.href = "/Item/ItemList.aspx";
                            } else {
                                alert("결제가 실패되었습니다");
                                location.href = "/Item/ItemList.aspx";
                            }
                            return;
                        }
                    }
                })

            });
        });

        function rcvInfoVal() {
            if (document.getElementById('rcvInfo').checked == false) {
                 $("#Name").val("");
                 $("#Phone").val("");
                 return;
            }
            $("#Name").val("<%=strUserName %>");
            $("#Phone").val("<%=strPhone %>");
        }

        function validation() {
            var result = false;
            var rcvName = $("#Name").val();
            var rcvPhone = $("#Phone").val();
            var address1 = $("#sample6_address").val();
            var address2 = $("#sample6_address2").val();
            var postCode = $("#sample6_postcode").val();
            var rcvMsg = $("#message").val();

            if (rcvName.length < 2 || rcvPhone < 2 || address1 < 2 || address2 < 2 || postCode < 2 || rcvMsg < 2) {
                result = false;
            } else {
                result = true;
            }

            return result;
        }


    </script>


</asp:Content>

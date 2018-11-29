<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarItem.master" AutoEventWireup="true" CodeBehind="ItemList.aspx.cs" Inherits="cafeLetter.Item.ItemList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarItemHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarItemBody" runat="server">

      <div class="col-sm-9 col-md-10" style="top: 40px;">

        <!-- -->
        <div class="row">
            <div class="col-md-10">
                <h2>커피 </h2>
            </div>
            <div class="col-md-2">
                <button type="button" class="btn btn-info" id="MyBasket" data-toggle="modal" style="float:right;margin-left: 5px;" >장바구니</button>
            </div>
        </div>
          
        <hr class="featurette-divider">

        <div class="row" style="margin-top: 12px;">

            <asp:Repeater ID="ListPanel" runat="server">
                <ItemTemplate>
                    <div class="panel panel-default" style="width: 97%; margin: 0 auto; margin-bottom: 10px;">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <img src='<%# Eval("ITEMIMG") %>' alt="" height=65 width=65></img>
                                </div>
                                <div class="col-md-3">
                                    <span class="data D" style="font-weight:bold;"> <%# Eval("ITEMNAME") %></span>
                                    <br />
                                    <span class="data D"> <%# Eval("ITEMDESC") %></span>
                                </div>
                                <div class="col-md-3">
                                    <span class="data D"> <%# Eval("ITEMPRICE") %> <img alt="beans" src="/image/bean.PNG" width="20" height="20" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                                    <span class="data D" style="float:right">재고 :<span class="data D" style="font-weight:bold;"> <%# Eval("ITEMCOUNT") %>개</span> </span>
                                    <asp:Literal runat="server" ID="ItemNo" Visible="false" Text='<%# Eval("ITEMNO") %>'></asp:Literal></span>
                                    <asp:Literal runat="server" ID="ItemRemain" Visible="false" Text='<%# Eval("ITEMCOUNT") %>'></asp:Literal></span>
                                </div>
                                <div class="col-md-3" >
                                    <div class="input-group pull-right" style="margin-top: 20px; width: 100px;">
                                        <asp:TextBox runat="server" ID="ItemCount" type="text" Text="1" class="form-control" onkeyup="validation(this)" Style="width: 40px; margin-right: 5px;" />
                                        <asp:Button ID="AddBasket" runat="server" class="btn btn-info" Text="추가" Style="width: 50px;" OnClick="AddBasket_Click" />
                                    </div>
                                    
                                    <%--<asp:DropDownList ID="ItemCount" runat="server">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>40</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                    </asp:DropDownList>--%>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>
          
    </div>




    <!-- Bonus Cash Insert Modal -->
    <div class="modal fade" id="MyBasketModal" role="dialog">
    </div>
    <!-- /Modal Bonus Cash Issue Area  -->


    <script type="text/javascript">
        //장바구니 버튼 클릭
        $(function () {
            $("#MyBasket").click(function () {
                getMyBasketDB();
                
            });
        });

        //장바구니 - 구매하기 버튼
        function BuyItemProcess() {
            var a = $("#MyCashPrice").val();
            console.log(a);
            var b = $("#BuyPrice").val();
            console.log(b);
            var checkValidation = a - b;
            console.log(a - b);
            if (checkValidation >= 0) {
                location.href = "/Item/BuyItem.aspx"
            }
            else {
                alert("잔액이 부족합니다. 충전후 이용해주세요");
                location.href = "/Cash/CashCharge.aspx"
            }
        }

        
        //내 장바구니 목록
        function getMyBasketDB() {
            var postData = {};
            var result = false;

                postData["strMethod"] = "MyBasketList";
                postData["strUserID"] = "<%= strUserID %>";

                $.ajax({
                    type: "POST",
                    data: JSON.stringify(postData),
                    url: "/Item/ItemHandler.ashx",
                    dataType: "JSON",
                    contentType: "application/json; charset=utf-8",
                    error: function (request, status, error) {
                        alert("장바구니에 담긴 물품이 없습니다.");
                         $("#MyBasketModal").appendTo("body").modal("hide");
                    },
                    success: function (data) {
                        console.log(data);
                        if (data.intRetVal == 0) {
                            addMyBasketList(data);
                            reseult = true;
                        }
                        else {
                            alert("장바구니에 담긴 물품이 없습니다.");
                            $("#MyBasketModal").appendTo("body").modal("hide");
                        }
                    }
            })

            return result;
        }

        //장바구니 리스트 출력
        function addMyBasketList(data) {

            var intItemTotalPrice = data.intItemTotalPrice;
            var strItemTotalPrice = data.intItemTotalPrice.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");


            var html = "";
            html += "       <div class='modal-dialog modal-lg'>                                                          ";
            html += "       <div class='modal-content'>                                                                  ";
            html += "               <div class='modal-header'>                                                           ";
            html += "                   <button type='button' class='close' data-dismiss='modal'>&times;</button>        ";
            html += "                   <h4 class='modal-title' id='modalTitle'>장바구니</h4>                            ";
            html += "               </div>                                                                               ";
            html += "               <div class='modal-body'>                                                             ";
            html += "                   <div class='row' style='margin-top: 12px;'>                                      ";

            //Reapeater Start

            for (var i = 0; i < data.objDT.length; i++) {

                 html += "       <div class='panel panel-default' style='width: 97%; margin: 0 auto; margin-bottom: 10px;'>              ";
                 html += "           <div class='panel-body'>                                                                            ";
                 html += "               <div class='row'>                                                                               ";
                 html += "                   <div class='col-md-2'>                                                                      ";
                 html += "                       <img src='"+data.objDT[i].ITEMIMG+"' alt='' height='65' width='65'>                   ";
                 html += "                               </div>                                                                          ";
                 html += "                       <div class='col-md-4'>                                                                  ";
                 html += "                           <span class='data D'>"+data.objDT[i].ITEMNAME+"</span>                                  ";
                 html += "                           <br>                                                                                ";
                 html += "                               <span class='data D'>"+data.objDT[i].ITEMDESC+"</span>                                     ";
                 html += "                               </div>                                                                          ";
                 html += "                           <div class='col-md-4'>                                                              ";
                 html += "                               <span class='data D'>"+data.objDT[i].SITEMPRICE+" X "+data.objDT[i].ITEMCOUNT+" = "+data.objDT[i].SITEMTOTALPRICE+" 원두</span>                               ";
                 html += "                                                                                                               ";
                 html += "                           </div>                                                                              ";
                 html += "                           <div class='col-md-2'>                                                              ";
                 html += "                               <button type='button' name='"+data.objDT[i].ITEMNO+"' id='DeleteItem"+data.objDT[i].ITEMNO+"' class='btn btn-primary' style='float:right;' onclick='DeleteBasketItem(this)'>삭제</button>   ";
                 html += "                               </div>                                                                          ";
                 html += "                           </div>                                                                              ";
                 html += "                       </div>                                                                                  ";
                 html += "                   </div>                                                                                      ";

            }
            //Reapeater End

            html += "       </div>                                                                                       ";
            html += "   <br />                                                                 ";
            html += "   <span class='data D' style='float:right; font-weight:bold; font-size:20px;'>총 가격   : " + strItemTotalPrice + " 원두 </span>        ";
            html += "           </div>                                                                                   ";
            html += "           <div class='modal-footer'>                                                               ";
            html += "               <button type='button' class='btn btn-default' data-dismiss='modal'>닫기</button>    ";
            html += "               <button type='button' id='BuyItemBtn' onclick='BuyItemProcess()' class='btn btn-primary'>구매하기</button>     ";
            html += "           </div>                                                                                   ";
            html += "       </div>                                                                                       ";
            html += "                                                                                                    ";
            html += "   </div>                                                                                           ";
            html += " <input type='hidden' id='BuyPrice' value='"+intItemTotalPrice+"' /> ";


            $("#MyBasketModal").html(html);
            $("#MyBasketModal").appendTo("body").modal("show");
        }

        //장바구니에 물품 추가
        function AddBasketItem(data) {
           

        }

        //validation
        function validation(data) {
            var count = data.value;
          var deny_char = /^[0-9]+$/;

          if (count == "") {
              return false;
          } else {
              if (!deny_char.test(count)) {
                  alert("숫자만 입력가능합니다");
                  data.value = "";
                  return false;
              }
              return true;
          }
        }

        //장바구니 목록 삭제
        function DeleteBasketItem(data) {
            var itemNo = data.name;
            console.log(itemNo);

            var postData = {};
            var result = false;

                postData["strMethod"] = "MyBasketItemDelete";
                postData["strUserID"] = "<%= strUserID %>";
                postData["intItemNo"] = itemNo;

                $.ajax({
                    type: "POST",
                    data: JSON.stringify(postData),
                    url: "/Item/ItemHandler.ashx",
                    dataType: "JSON",
                    contentType: "application/json; charset=utf-8",
                    error: function (request, status, error) {
                        alert("장바구니 물품 삭제에 실패했습니다");

                    },
                    success: function (data) {
                        console.log(data);
                        if (data.intRetVal == 0) {
                            alert("장바구니 물품을 삭제했습니다");
                            getMyBasketDB();
                        }
                        else {
                            alert("장바구니 물품 삭제에 실패했습니다");
                        }
                    }
            })

        }


    </script>



</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarAdmin.master" AutoEventWireup="true" CodeBehind="UserAuthorityUpdate.aspx.cs" Inherits="cafeLetter.Admin.UserAuthorityUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarAdminHead" runat="server">


 

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarAdminBody" runat="server">



    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <div class="col-md-11">
                <h4>회원 관리</h4>
            </div>
        </div>

        <hr class="featurette-divider">

        <!-- UserName -->
        <div class="row" style="width: 70%; margin-left: 0px; margin-right: 0px;">
            <table class="table table-bordered">
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">ID</th>
                        <td><%=strUserID %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">이름</th>
                        <td><%=strName %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">성별</th>
                        <td><%=strSex %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">지역</th>
                        <td><%=strLocation %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">생일</th>
                        <td><%=strBirthday %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">SNS</th>
                        <td><%=strSNS %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">자기소개</th>
                        <td><%=strIntroduce %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">마지막 로그인 시간</th>
                        <td><%=strLastLogin %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">게시글 작성 수</th>
                        <td><%=strBoardCnt %> </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">댓글 작성 수</th>
                        <td><%=strCommentCnt %>  </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">게시판 제한</th>
                        <td>
                            <asp:CheckBox ID="BoardCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">갤러리 제한</th>
                        <td>
                            <asp:CheckBox ID="GalleryCheckBox" runat="server" />
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">결제원두</th>
                        <td>
                            <%=intRealCash %> 원두 
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">보너스원두</th>
                        <td>
                            <%=intBonusCash %> 원두 
                            <button type="button" class="btn btn-info" id="BonusCashInsert" data-toggle="modal" style="float:right;margin-left: 5px;" >원두 발행</button>
                            <button type="button" class="btn btn-warning" id="BonusCashCancel" data-toggle="modal" style="float:right;" >원두 회수</button>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="row">
            <div class="col-md-11">
                <asp:Button ID="DeleteBtn" runat="server" class="btn btn-danger" Text="삭제" OnClientClick="return confirmDelete();" OnClick="DeleteBtn_Click"></asp:Button>
                <asp:Button ID="UserAuthorityUpdateBtn" runat="server" class="btn btn-primary" Text="수정" OnClientClick="return confirmComplete();" OnClick="UserAuthorityUpdateBtn_Click"></asp:Button>
                

               
            </div>
        </div>
    </div>



    <!-- Bonus Cash Insert Modal -->
    <div class="modal fade" id="BonusInsertModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" id="modalTitle">보너스 원두 발행</h4>
                </div>
                <div class="modal-body">
                    <form id="frmIssue" name="frmIssue">

                        <table class="table table-bordered table-hover" id="trIssueStep2">
                            <colgroup>
                                <col style="width: 40%" />
                                <col style="width: 60%" />
                            </colgroup>
                            <tr>
                                <th class="text-center info">보너시 원두 지급 개수</th>
                                <td class="text-muted">
                                    <input id="txtIssueCashAmt" name="cashamt" type="text" class="form-control" /></td>
                            </tr>
                            <tr>
                                <th class="text-center info">지급 사유</th>
                                <td class="text-muted">
                                    <input id="txtIssuePayToolName" name="paytoolname" value="발표 시연 테스트입니다" type="text" class="form-control" maxlength="50" /></td>
                            </tr>
                        </table>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">닫기</button>
                    <button type="button" id="btnBonusCashIssue" class="btn btn-primary">발행</button>
                </div>
            </div>

        </div>
    </div>
    <!-- /Modal Bonus Cash Issue Area  -->


    <!-- Bonus Cash Cancel Modal -->
    <div class="modal fade" id="myBonusCancelModalModal" role="dialog">
        <div class="modal-dialog">

            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title" id="modal2Title">보너스 원두 회수</h4>
                </div>
                <div class="modal-body">
                    <form id="frm2Issue" name="frmIssue">

                        <table class="table table-bordered table-hover" id="trIssueStep3">
                            <colgroup>
                                <col style="width: 40%" />
                                <col style="width: 60%" />
                            </colgroup>
                            <tr>
                                <th class="text-center info">보너시 원두 회수 개수</th>
                                <td class="text-muted">
                                    <input id="txtCancelCashAmt" name="cashamt" type="text" class="form-control" /></td>
                            </tr>
                            <tr>
                                <th class="text-center info">회수 사유</th>
                                <td class="text-muted">
                                    <input id="txtCancelPayToolName" name="paytoolname"  value="발표 시연 테스트입니다" type="text" class="form-control" maxlength="50" /></td>
                            </tr>
                        </table>
                    </form>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">닫기</button>
                    <button type="button" id="btnBonusCashCancel" class="btn btn-primary">회수</button>
                </div>
            </div>

        </div>
    </div>
    <!-- /Modal Bonus Cash Cancel Area  -->



    <script type="text/javascript">
        $(function () {
            $("#BonusCashInsert").click(function () {
                $("#txtIssueCashAmt").val("");
                $("#txtIssuePayToolName").val("발표 시연 테스트입니다");
              $("#BonusInsertModal").appendTo("body").modal("show");              
            });

            $("#BonusCashCancel").click(function () {
                $("#txtCancelCashAmt").val("");
                $("#txtCancelPayToolName").val("발표 시연 테스트입니다");
              $("#myBonusCancelModalModal").appendTo("body").modal("show");              
            });

            //보너스 캐시 발행
            $("#btnBonusCashIssue").click(function () {

                var postData = {};

                postData["strMethod"] = "BonusCashIssue";
                postData["strUserID"] = "<%=strUserID%>";
                postData["strAmount"] = $("#txtIssueCashAmt").val();

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
                            alert("보너스 원두가 발행되었습니다");
                            location.href = "/Admin/UserAuthorityUpdate.aspx?UserID=<%=strUserID%>";
                            return;
                        }
                        else {
                            alert("보너스 캐시 발행에 실패했습니다");
                            location.href = "/Admin/UserAuthorityUpdate.aspx?UserID=<%=strUserID%>";
                            return;
                        }
                    }
                })
            });


            //보너스 캐시 회수
            $("#btnBonusCashCancel").click(function () {

                var postData = {};

                postData["strMethod"] = "BonusCashCancel";
                postData["strUserID"] = "<%=strUserID%>";
                postData["strAmount"] = $("#txtCancelCashAmt").val();

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
                            alert("보너스 원두가 회수되었습니다");
                            location.href = "/Admin/UserAuthorityUpdate.aspx?UserID=<%=strUserID%>";
                            return;
                        }
                        else {
                            alert("보너스 캐시 회수에 실패했습니다");
                            location.href = "/Admin/UserAuthorityUpdate.aspx?UserID=<%=strUserID%>";
                            return;
                        }
                    }
                })
            });



        });

        function confirmComplete() {
            if (confirm("회원 권한을 수정하시겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }

        function confirmDelete() {
            if (confirm("회원을 강제탈퇴 시키겠습니까?")) {
                return true;
            } else {
                return false;
            }
        }
    </script>





</asp:Content>



   

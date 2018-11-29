<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarMember.master" AutoEventWireup="true" CodeBehind="MyInfoUpdatePW.aspx.cs" Inherits="cafeLetter.Member.MyInfoUpdatePW" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarMemberHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarMemberBody" runat="server">

    <div class="col-sm-9 col-md-10">

        <!-- UserName -->
        <div class="row">
                <div class="page-header">
                    <h3>비밀 번호 수정</h3>
                </div>
        </div>

        <!-- UserName -->
        <div class="row" style="width:65%; margin-right: 0px;">
            <table class="table table-bordered">
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">기존 비밀번호</th>
                        <td>
                            <asp:TextBox ID="BeforePW" type="password" runat="server" class="form-control"></asp:TextBox></td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">새 비밀번호</th>
                        <td>
                            <asp:TextBox ID="NewPW" type="password" runat="server" onkeyup="passwordCheck(this)" class="form-control"></asp:TextBox>
                            <div id="passwordmessage"></div>
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">새 비밀번호 확인</th>
                        <td>
                            <asp:TextBox ID="CheckPW" type="password" runat="server" onkeyup="secondpasswordCheck()" class="form-control"></asp:TextBox>
                            <div id="checkpasswordmessage"></div>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="row" style="width:65%;">
            <asp:Button ID="ChangePW" runat="server" class="btn btn-primary" Text="변경" Style="float: right;" OnClientClick="return checkValidation();" OnClick="ChangePW_Click"></asp:Button>
        </div>
    </div>


    <script type="text/javascript">

        var check_password = false;
        var check_register_password = false;

        //수정 체크
        function checkValidation() {

            if (!check_password || !check_register_password) {
                alert("입력 정보를 확인해주세요");
                return false;
            }

            if (confirm("비밀번호를 수정하시겠습니까?")) {
                return true;
            } else {
                return false;
            }

            return true;

        }

        // 비밀번호 데이터 유효성 검사
        function passwordCheck(txt) {

            var pw = txt.value;
            var num = pw.search(/[0-9]/g);
            var eng = pw.search(/[a-z]/ig);
            var spe = pw.search(/[`~!@@#$%^&*|₩₩₩'₩";:₩/?]/gi);

            if (pw == "") {
                check_password = false;
                return false;
            } else {
                if (pw.length < 8 || pw.length > 20) {
                    $("#passwordmessage").text("8자리 ~ 20자리 이내로 입력해주세요.").css("color", "red");

                    check_password = false;
                    return false;
                }

                if (num < 0 || eng < 0 || spe < 0) {
                    $("#passwordmessage").text("영문,숫자, 특수문자 3가지를 다 사용해야합니다.");
                    check_password = false;
                    return false;
                }



                check_password = true;
                $("#passwordmessage").text("사용가능한 비밀번호입니다.").css("color", "green");

                if (!secondpasswordCheck()) {
                    return false;
                }


                return true;
            }
        }

        // 비밀번호 재입력 유효성 검사
        function secondpasswordCheck() {
            var pw = document.getElementById('<%= CheckPW.ClientID %>').value;
            var fpw = document.getElementById('<%= NewPW.ClientID %>').value;

            if (pw == "") {
                check_register_password = false;
                return false;
            } else {
                if (fpw != pw) {
                    $("#checkpasswordmessage").text("비밀번호가 일치하지 않습니다.").css("color", "red");

                    check_register_password = false;
                    return false;
                } else {
                    check_register_password = true;
                    $("#checkpasswordmessage").text("비밀번호가 일치합니다.").css("color", "green");
                    return true;
                }
            }
        }


    </script>


</asp:Content>

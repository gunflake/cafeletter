<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarMember.master" AutoEventWireup="true" CodeBehind="MyInfoModify.aspx.cs" Inherits="cafeLetter.Member.MyInfoModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SidebarMemberHead" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SidebarMemberBody" runat="server">



    <div class="col-sm-9 col-md-10">

        <!-- UserName -->
        <div class="row">
                <div class="page-header">
                    <h3>회원 정보 수정</h3>
                </div>
        </div>

        <!-- UserName -->
        <div class="row" style="width: 65%; margin-right: 0px;">
            <table class="table table-bordered">
                <tbody>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">ID</th>
                        <td><%=strUserID %></td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">Email</th>
                        <td><%=strUserEmail %></td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">성별</th>
                        <td>
                            <asp:RadioButton GroupName="Sex" ID="InputSexM" runat="server" Text="남자" OnCheckedChanged="Sex_CheckedChanged" />
                            <asp:RadioButton GroupName="Sex" ID="InputSexW" runat="server" Text="여자" OnCheckedChanged="Sex_CheckedChanged" />
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">이름</th>
                        <td>
                            <asp:TextBox ID="InputName" onkeyup="checkName(this)" runat="server" class="form-control" Text="" placeholder="이름을 입력하세요"></asp:TextBox>
                            <div id="namemessage"></div>
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">지역</th>
                        <td>
                            <asp:TextBox ID="InputLocation" onkeyup="checkLocation(this)" runat="server" class="form-control" Text="" placeholder="지역을 입력하세요"></asp:TextBox>
                            <div id="locationMsg"></div>
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">생일</th>
                        <td>
                            <asp:TextBox ID="InputBirthday" runat="server" class="form-control" Type="date" Text="" placeholder="생일을 입력하세요"></asp:TextBox>
                            <div id="birthdayMsg"></div>
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">SNS</th>
                        <td>
                            <asp:TextBox ID="InputSNS" runat="server" onkeyup="checkSNS(this)" class="form-control" Text="" placeholder="SNS를 입력하세요"></asp:TextBox>
                            <div id="snsMsg"></div>
                        </td>
                    </tr>
                    <tr class="table-active">
                        <th scope="row" style="background-color: #3db1c93b;">자기소개</th>
                        <td>
                            <asp:TextBox ID="InputIntroduce" onkeyup="checkIntroduce(this)" runat="server" class="form-control" Text="" placeholder="자기소개를 입력하세요"></asp:TextBox></td>
                    </tr>

                </tbody>
            </table>
        </div>
        <div>
            <div class="row" style="width: 65%;">
                <asp:Button ID="InfoModify" runat="server" class="btn btn-primary" Text="수정" Style="float: right;" OnClientClick="return checkValidation();" OnClick="InfoModify_Click"></asp:Button>
            </div>
        </div>
    </div>




    <script type="text/javascript">

        var check_name = true;
        var check_location = true;
        var check_SNS = true;
        var check_introduce = true;
        var check_birthday = true;


        //수정 체크
        function checkValidation() {

            if (!check_name || !check_location || !check_SNS || !check_introduce || !check_birthday) {
                alert("입력 정보를 확인해주세요");
                return false;
            }

            if (confirm("회원 정보를 수정하시겠습니까?")) {
                return true;
            } else {
                return false;
            }

            return true;

        }

        //이름 길이 체크
        function checkName(obj) {
            var comment = obj.value;
            if (comment.length > 20) {
                $("#namemessage").text("이름은 최대 20자까지 입력가능합니다").css("color", "red");
                document.getElementById('<%=InputName.ClientID %>').value = comment;
                check_name = false;
                return false;
            }
            $("#namemessage").text("");
            check_name = true;
            return true;
        }

        //지역 길이 체크
        function checkLocation(obj) {
            var comment = obj.value;
            if (comment.length > 20) {
                $("#locationMsg").text("지역은 최대 20자까지 입력가능합니다").css("color", "red");
                document.getElementById('<%=InputLocation.ClientID %>').value = comment;
                check_location = false;
                return false;
            }
            $("#locationMsg").text("");
            check_location = true;
            return true;
        }

        //생일 체크
        function checkBirthDay(obj) {
            var comment = obj.value;
            var dayRegExp = /^(19|20)\d{2}-(0[1-9]|1[012])-(0[1-9]|[12][0-9]|3[0-1])$/;
            if (!dayRegExp.test(comment)) {
                $("#birthdayMsg").text("생일 형식은 YYYY-MM-DD로 입력해야합니다.").css("color", "red");
                check_birthday = false;
                return false;
            }
            $("#birthdayMsg").text("");
            check_birthday = true;
            return true;
        }

        //SNS 체크
        function checkSNS(obj) {
            var comment = obj.value;
            if (comment.length > 50) {
                alert("SNS은 최대 50자까지 입력가능합니다.");
                comment = comment.substring(0, 49);
                document.getElementById('<%=InputSNS.ClientID %>').value = comment;
                check_SNS = false;
                return false;
            }
            check_SNS = true;
            return true;
        }

        //자기소개 체크
        function checkIntroduce(obj) {
            var comment = obj.value;
            if (comment.length > 200) {
                alert("자기소개은 최대 200자까지 입력가능합니다.");
                comment = comment.substring(0, 199);
                document.getElementById('<%=InputIntroduce.ClientID %>').value = comment;
                check_introduce = false;
                return false;
            }
            check_introduce = true;
            return true;
        }


    </script>


</asp:Content>

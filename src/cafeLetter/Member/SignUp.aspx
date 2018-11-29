<%@ Page Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="cafeLetter.SignUp" %>


<asp:Content ID="test1" ContentPlaceHolderID="NavigationBarHead" runat="server">
    <title>cafeLetter 회원가입</title>
    <meta name="generator" content="Bootply" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">




        
</asp:Content>



<asp:Content ID="test2" ContentPlaceHolderID="NavigationBarBody" runat="server">


    <div id="loginModal" class="modal show" tabindex="-1" role="dialog" aria-hidden="true" style="top: 150px;">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">

                    <h2 class="text-center">cafeLetter 회원가입</h2>
                </div>
                <div class="modal-body">
                    <div class="form col-md-12 center-block">
                        <div class="form-group" id="inputID">
                            <asp:TextBox runat="server" ID="userID" type="text" class="form-control input-lg" placeholder="ID" onkeyup="checkID(this)"/>
                             <div id="namemessage"></div>     
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="userPW" type="password" class="form-control input-lg" placeholder="Password" onkeyup="passwordCheck(this)" />
                            <div id="passwordmessage"></div>  
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="userPWCheck" type="password" class="form-control input-lg" placeholder="Password check" onkeyup="secondpasswordCheck(this)" />
                            <div id="checkpasswordmessage"></div>
                        </div>
                        <div class="form-group">
                            <asp:TextBox runat="server" ID="userEmail" type="email" class="form-control input-lg" placeholder="email" onkeyup="emailCheck(this)" />
                            <div id="emailmessage"></div>
                        </div>
                        <div class="form-group">
                            <asp:Button ID="btnSignUp" runat="server" class="btn btn-primary btn-lg btn-block" Text="회원가입" disabled="true" OnClick="signUp_click" />
                            <span class="pull-right"><a href="./login.aspx">로그인 하기</a></span><span>계정이 있으신가요?</span>
                        </div>

                    </div>

                </div>
                <div class="modal-footer">
                </div>
            </div>
        </div>
        </div>
 
   


  <script type="text/javascript">

      var check_name = false;
      var check_email = false;
      var check_password = false;
      var check_register_password = false;

        function validationcheck() {

            if (check_name  && check_email  && check_password  && check_register_password) {
                $("#<%= btnSignUp.ClientID%>").prop("disabled", false);
            } else {
                  $("#<%= btnSignUp.ClientID%>").prop("disabled", true);
            }
        }
         // ID 유효성 검사
      function checkID(txt) {

          var ID = txt.value;
          var deny_char = /^[A-Za-z0-9]+$/;

          if (ID == "") {
              check_name = false;
              validationcheck();
          } else {
              if (ID.length < 8 || ID.length > 20) {
                  $("#namemessage").text("8자리 ~ 20자리 이내로 입력해주세요.").css("color", "red");
                  check_name = false;
                  validationcheck();
                  return false;
              }
              if (!deny_char.test(ID)) {

                  $("#namemessage").text("ID는 영어와 숫자만 입력가능합니다");
                  check_name = false;
                  validationcheck();
                  return false;
              }

              check_name = true;
              validationcheck();

              $("#namemessage").text("");
              return true;

          }
      }
      // 이메일 데이터 유효성 검사
        function emailCheck(txt) {
            var email = txt.value;
            var re = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
            var eng = email.search(/[a-z]/ig);
           
            

            if (email == "") {
                check_email = false;
                validationcheck();
            } else {
                if (!re.test(email)) {
                    check_email = false;
                    validationcheck();
                    $("#emailmessage").text("올바른 이메일 주소를 입력하세요.").css("color", "red");
                    return false;
                }
                check_email = true;
                validationcheck();
                $("#emailmessage").text("");
                return true;   
            } 
      }

      // 비밀번호 데이터 유효성 검사
                function passwordCheck(txt) {

                    var pw = txt.value;
                    var num = pw.search(/[0-9]/g);
                    var eng = pw.search(/[a-z]/ig);
                    var spe = pw.search(/[`~!@@#$%^&*|₩₩₩'₩";:₩/?]/gi);

                    if (pw == "") {
                        check_password = false;
                        validationcheck();
                        return false;
                    } else {
                        if (pw.length < 8 || pw.length > 20) {
                            $("#passwordmessage").text("8자리 ~ 20자리 이내로 입력해주세요.").css("color", "red");

                            check_password = false;
                            validationcheck();
                            return false;
                        }

                        if (num < 0 || eng < 0 || spe<0) {
                            $("#passwordmessage").text("영문,숫자, 특수문자 3가지를 다 사용해야합니다.");
                            check_password = false;
                            validationcheck();
                            return false;
                        }
                        

                        check_password = true;
                        validationcheck();
                        $("#passwordmessage").text("사용가능한 비밀번호입니다").css("color", "green");

                        if (!secondpasswordCheck()) {
                            return false;
                        }
                        
                        return true;
                    }
      }

      // 비밀번호 재입력 유효성 검사
                function secondpasswordCheck() {
                    var pw = document.getElementById('<%= userPWCheck.ClientID %>').value;
                    var fpw = document.getElementById('<%= userPW.ClientID %>').value;
                    
                    

                    if (pw == "") {
                        check_register_password = false;
                        validationcheck();
                        return false;
                    } else {
                        if (fpw != pw) {
                            $("#checkpasswordmessage").text("비밀번호가 일치하지 않습니다.").css("color", "red");

                            check_register_password = false;
                            validationcheck();
                            return false;
                        } else {
                            check_register_password = true;
                            validationcheck();
                            $("#checkpasswordmessage").text("비밀번호가 일치합니다.").css("color", "green");
                            return true;
                        }
                    }
                }



  </script>

</asp:Content>













<%@ Page Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="cafeLetter.login" %>



<asp:Content ID="loginHead" ContentPlaceHolderID="NavigationBarHead" runat="server">
    <title>cafeLetter 로그인</title>
    <meta name="generator" content="Bootply" />
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1">

   
</asp:Content>




<asp:Content ID="loginBody" ContentPlaceHolderID="NavigationBarBody" runat="server">


    <div class="container-fluid">

        <!--login modal-->
        <div id="loginModal" class="modal show" tabindex="-1" role="dialog" aria-hidden="true" style="top: 150px;">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
                        <h2 class="text-center">cafeLetter 로그인</h2>
                    </div>
                    <div class="modal-body">
                        <div class="form col-md-12 center-block">
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="ID" type="text" class="form-control input-lg" placeholder="ID" />
                            </div>
                            <div class="form-group">
                                <asp:TextBox runat="server" ID="PW" type="password" class="form-control input-lg" placeholder="Password" />
                            </div>
                            <div class="form-group">
                                <asp:Button ID="btnLogin" runat="server" class="btn btn-primary btn-lg btn-block" Text="로그인" OnClick="btnLogin_Click" />
                                <span class="pull-right" >
                                    <asp:Literal ID="signUp" runat="server"><a href="./signUp.aspx">회원가입</a></asp:Literal></span>
                               <span>계정이 없으신가요?</span>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                    </div>
                </div>
            </div>
        </div>
      </div>
    </asp:Content>
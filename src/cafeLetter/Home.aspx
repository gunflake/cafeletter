<%@ Page Title="" Language="C#" MasterPageFile="~/NavigationBar.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="cafeLetter.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="NavigationBarHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="NavigationBarBody" runat="server">
    
    <section id="about" class="home-section color-dark bg-white" >
       

        <div class="container">


            <div class="row" style="padding-top: 80px;">


                <div class="col-lg-8 col-lg-offset-2 animatedParent">
                    <div class="text-center">
                        <p>
                            카페 창업자와 카페 예비 창업자를 위한 커뮤니티 공간입니다.
                        </p>
                        <p>
                            카페레터는 카페 창업을 희망하는 모든 사람에게 열려있는 공간입니다.
                        </p>
                        <p>
                             실시간으로 정보를 공유하고 다양한 사람들과 만나보세요.
                        </p>
                        <a href="/Board/BoardList.aspx" class="btn btn-skin btn-scroll">메인페이지로 이동</a>
                    </div>
                </div>


            </div>
        </div>

        <!-- 사진 -->
        <div class="container marginbot-40">
            <div class="row">
                <div class="col-lg-8 col-lg-offset-2">
                    <div class="animatedParent">
                        <div class="section-heading text-center animated bounceInDown">
                            <h2 class="h-bold">About our bocor team</h2>
                            <div class="divider-header"></div>
                        </div>
                    </div>
                </div>
            </div>

        </div>


        <div class="container">


            <div class="row">


                <div class="col-lg-8 col-lg-offset-2 animatedParent">
                    <img src="/photo/191133.jpg" alt="Responsive image" class="img-responsive" style="margin:0 auto;"  /> 
                </div>


            </div>
        </div>
        


    </section>



</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarService.master" AutoEventWireup="true" CodeBehind="MyQuestion.aspx.cs" Inherits="cafeLetter.Service.ServiceMyQuestion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarServiceHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarServiceBody" runat="server">
    

    
    
   <div class="col-sm-9 col-md-10" style="top: 40px;">

        <!-- -->
        <div class="row">
            <div class="col-md-11">
                <h4>내 문의 목록 </h4>
            </div>
       
          
          
        </div>


        <hr class="featurette-divider">

        <div class="row" style="margin-top: 12px;">

            <asp:Repeater ID="NoticeListPanel" runat="server">
                <ItemTemplate>
                    <div class="panel panel-default" style="width: 97%; margin: 0 auto; margin-bottom: 10px;">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-md-4">
    
                                    <span class="data D">작성자 &nbsp; <%# Eval("BOARDWRITER") %></span> 
                                    <br />
                                    <span class="data D">질문 날짜 &nbsp; <%# Eval("BOARDDATE").ToString().Substring(0,10) %></span>                 
                                </div>
                                <div class="col-md-8">
                                    <asp:HyperLink ID="BoardViewLink" runat="server" Text='<%# Eval("BOARDTITLE") + " ["+ Eval("BOARDCOMMENTCNT") + "]"  %>' NavigateUrl='<%# String.Format("/Service/MyQuestionView.aspx?BoardNo={0}", Eval("BOARDNO")) %>'></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>

        </div>

        <!-- 페이징 처리-->
        <div class="row">
            <div class="col-md-6">
                <nav>
                    <ul id="PageNumber" runat="server" class="pagination">
                    </ul>
                </nav>
            </div>
            </div>

    


</asp:Content>

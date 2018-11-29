<%@ Page Title="" Language="C#" MasterPageFile="~/SidebarMember.master" AutoEventWireup="true" CodeBehind="MyCashInfo.aspx.cs" Inherits="cafeLetter.Cash.MyCashInfo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SidebarMemberHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SidebarMemberBody" runat="server">

    



    <div class="col-sm-9 col-md-10" style="top: 40px;">

        <div class="row">
            <div class="col-md-12">
                <h4>나의 결제 내역 
                    <span style="float:right; color:#0e9d20"> 보너스 원두 : <%= intBonusCash %></span>
                    <span style="float:right; color:#137f9c"> 실제 원두 : <%= intRealCash %> &nbsp;</span>
                </h4>
            </div>
        </div>
        
        <hr class="featurette-divider">

        <!-- 권한 목록 -->
        <div class="row" style="margin-left: 0px; margin-right: 0px;">
            
                <asp:Repeater ID="MyCashList" runat="server">
                    <HeaderTemplate>
                        <table class="table table-bordered">
                            <tr class="table-active">
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">결제수단</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">결제날짜</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">취소날짜</th>
                                <th scope="row" style="background-color: #3db1c93b; text-align:center">결제금액</th>
                                <th scope="row" style="background-color: #3db1c93b;text-align:center"">상태</th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td style="text-align:center"><%# Eval("PGTOOL") %></td>
                            <td style="text-align:center"><%# Eval("PAYYMD").ToString().Substring(0,10) %></td>
                            <td style="text-align:center"><%# Eval("CNLYMD").ToString().Length>1 ?  Eval("CNLYMD").ToString().Substring(0,10) : "X" %></td>
                            <td style="text-align:right"><%# Eval("PAYCASHAMT") %>원</td>
                            <td style="text-align:center">
                                <asp:Literal runat="server" Visible='<%# Eval("CNLSTATE").Equals("N") %>' Text='<%# Eval("STATE") %>'></asp:Literal>
                                <asp:HyperLink  runat="server" Visible='<%# Eval("CNLSTATE").Equals("Y") %>' Text='결제취소' NavigateUrl='<%# String.Format("/Cash/CashCancel.aspx?strPGName={0}&strTID={1}&strAmount={2}",Eval("PGNAME"), Eval("TID"), Eval("REMAINCASHAMT1")) %>'></asp:HyperLink>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </table>
                    </FooterTemplate>
                 </asp:Repeater>
        </div>

         <div class="row">
            <div class="col-md-6">
                <nav>
                    <ul id="PageNumber" runat="server" class="pagination">
                    </ul>
                </nav>
            </div>
            <div class="col-md-6">
            </div>
        </div>






    </div>


</asp:Content>

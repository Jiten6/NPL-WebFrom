<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="EmployeeDetails.aspx.cs" Inherits="NewPortal2023.ESS.EmployeeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .underline {
            text-decoration: underline;
            color:blue;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Employee Details</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                           <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>--%>
                                    <div id="form1" runat="server">
                                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>

                                        <div id="div1" runat="server" class="form-group row" style="margin: 10px">
                                            <div class="form-group row">
                                                <div class="col-sm-3">
                                                    <label style="font-size: 15px;">Enter Employee Code :</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:TextBox ID="txtEmpMid" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                                </div>
                                                <div class="col-sm-3">
                                                    <label style="font-size: 15px;">Select Month For Payslip :</label>
                                                </div>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control input-sm">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-sm-12" style="text-align: center;">
                                            <asp:Button ID="btnGenerateReport" runat="server" AutoPostBack="true" OnClick="btnLoadDetails_Click" Text="Generate" CssClass="btn btn-success" Width="100px" />
                                        </div>

                                        <div id="divEmp" runat="server" class="form-group row" visible="false">
                                            <div class="col-sm-3">
                                                <asp:Button ID="btn12BBList" runat="server" Text="Get Form 12BB List"
                                                    OnClick="btn12BBList_Click" />
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:Button ID="btnSupportList" runat="server" Text="Get Supports List"
                                                    OnClick="btnSupportList_Click" />
                                            </div>
                                        </div>


                                        <div id="InvDetails" visible="true" class="col-sm-12" style="text-align: center;">
                                        </div>
                                        <asp:GridView ID="gvEmployeeDetails" runat="server" AutoGenerateColumns="False" 
                                                   GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                    BorderStyle="Solid"  Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed" DataKeyNames="EMP_AID"  OnRowDataBound="gvEmployeeDetails_RowDataBound">
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpId" runat="server" Text='<%# Eval("EMP_AID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpCode" runat="server" Text='<%# Eval("EMP_MID") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Employee Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("EMP_NAME") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpenPayslip" runat="server" Text="Payslip"
                                                                    OnClick="lnkBtnOpenPayslip_Click" Class="underline" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpenTaxComputation" runat="server" Text="Tax Computation"
                                                                    OnClick="lnkBtnOpenTaxComputation_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpenCumulativePayslip" runat="server" Text="Cumulative Payslip"
                                                                    OnClick="lnkBtnOpenCumulativePayslip_Click" Class="underline"/>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                       
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpenForm16" runat="server" Text="Form 16"
                                                                    OnClick="lnkBtnOpenForm16_Click" Class="underline"/>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="">
                                                            <ItemTemplate>

                                                                <asp:LinkButton ID="lnkBtnOpenAppraisalLetter" runat="server" Text="Letter"
                                                                    OnClick="lnkBtnOpenAppraisalLetter_Click" Class="underline"/>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <%--<asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpen12BB" runat="server" Text="Form 12BB"
                                                                    OnClick="lnkBtnOpen12BB_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpenSupports" runat="server" Text="Support Documents"
                                                                    OnClick="lnkBtnOpenSupports_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>--%>
                                                        <%--<asp:TemplateField HeaderText="">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkBtnOpenDiscrepancy" runat="server" Text="Verification Report"
                                                                    OnClick="lnkBtnOpenDiscrepancy_Click" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>--%>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                            <tr>
                                                                <td class="GridViewHeader" style="width: 10%">
                                                                    <asp:Literal ID="Literal6" runat="server" Text="No records." />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>

                                    </div>
                                <%--</ContentTemplate>
                                <Triggers>--%>
                                    <%--  <asp:PostBackTrigger ControlID="drpDptList" />
                                    <asp:PostBackTrigger ControlID="drpEmpCode" />
                                    <asp:PostBackTrigger ControlID="btnGenerateReport" />
                                    <asp:PostBackTrigger ControlID="drpType" />--%>
                                    <%--  <asp:PostBackTrigger ControlID="btnGenerateReport" />
                                    <asp:PostBackTrigger ControlID="btnGenerateReportAllEmp" />--%>
                                    <%-- <asp:PostBackTrigger ControlID="chk7" />
                                    <asp:PostBackTrigger ControlID="chk8" />
                                    <asp:PostBackTrigger ControlID="radioPrimary1" />
                                    <asp:PostBackTrigger ControlID="radioPrimary2" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="drpType" />
                                    <asp:PostBackTrigger ControlID="btnCalTtl" />--%>
                               <%-- </Triggers>
                            </asp:UpdatePanel>--%>

                        </div>

                    </section>
                </div>

            </div>
        </section>
    </section>


</asp:Content>

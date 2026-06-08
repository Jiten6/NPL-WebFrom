<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="NPLEmployeeDetails.aspx.cs" Inherits="NewPortal2023.ESS.NPLEmployeeDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function ValidFile() {
            var file = $("#fupldDocument").val();
            if (file == "") {
                IN4_DisplayErrorMessage("Browse document.");
                return false;
            }
        }
    </script>
    <style type="text/css">
        .tableTitle {
            font-weight: bold;
            font-size: 10pt;
            vertical-align: middle;
            color: #205a94;
            font-family: Tahoma;
            height: 20px;
            text-decoration: none;
        }

        .table5 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
        }

        .table4 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
            background-color: #cccccc;
        }

            .table4 TR {
                padding-right: 1px;
                padding-left: 1px;
                padding-bottom: 1px;
                padding-top: 1px;
                background-color: #ffffff;
            }

        .GridViewItem {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 1px;
            padding-bottom: 1px;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            height: 10px;
            width: 10%;
            font-weight: normal;
        }

        .GridViewHeader {
            font-weight: bold;
            font-size: 8.3;
            filter: progid:DXImageTransform.Microsoft.Gradient(gradientType=0,startColorStr=#FFFFFF,endColorStr=#BBDDFF);
            text-transform: capitalize;
            color: #545454;
            border-top: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-bottom: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            text-align: center;
        }

        .input {
            font-family: Verdana, Arial, Helvetica, sans-serif;
            font-size: 7.5pt;
            color: #004B97;
            border-top: auto inset #CCCCCC;
            border-right: auto inset #DFDFDF;
            border-left: auto inset #CCCCCC;
            border-bottom: auto inset #DFDFDF;
            border-top-color: #CCCCCC;
            border-right-color: #DFDFDF;
            border-bottom-color: #DFDFDF;
            border-left-color: #CCCCCC;
            border-top-style: inset;
            border-right-style: inset;
            border-bottom-style: inset;
            border-left-style: inset;
            border-style: inset;
            border-color: #999999 #CCCCCC #CCCCCC #999999;
            border-top-width: 1px;
            border-right-width: 1px;
            border-bottom-width: 1px;
            border-left-width: 1px;
        }

        .GridViewEmpty {
            padding: 0px;
            margin: 0;
            border: solid 1px #d3dbdf;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            border-top: 1px solid #d3dbdf;
            width: 100%;
            border-spacing: 0px;
        }

        .title {
            font-weight: bold;
            font-size: 11pt;
            vertical-align: middle;
            text-transform: capitalize;
            color: #205a94;
            font-family: Tahoma; /* HEIGHT: 20px; */
            text-decoration: none;
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

                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" EnableViewState="true">
                                <ContentTemplate>
                                    <div id="form1" runat="server">
                                        <div id="divAlertSucc" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessageSucc" runat="server"></asp:Label>
                                        </div>
                                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-sm-3 labels">Select Employee Code :</label>
                                            <div class="col-sm-3">
                                                <%--   <asp:TextBox ID="txtEmpMid" runat="server" CssClass="form-control input-sm" AutoCompleteType="Disabled" AutoPostBack="true"></asp:TextBox>--%>
                                                <asp:DropDownList ID="drpEmpType" runat="server" CssClass="select2" Width="100%" AutoPostBack="true"
                                                    OnSelectedIndexChanged="drpEmpType_SelectedIndexChanged">
                                                </asp:DropDownList>

                                            </div>

                                            <label class="col-sm-3 labels">Select Month Slip :</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control input-sm-3" Width="150px">
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                            <asp:Button ID="btnLoadDetails" CssClass="btn btn-success" runat="server" Text="Submit" OnClick="btnLoadDetails_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                                        </div>

                                        <div class="form-group" style="display: none;">
                                            <asp:Button ID="btn12BBList" runat="server" Text="Get Form 12BB List"
                                                OnClick="btn12BBList_Click" />
                                            <asp:Button ID="btnSupportList" runat="server" Text="Get Supports List"
                                                OnClick="btnSupportList_Click" />
                                        </div>

                                        <div>
                                            <br />
                                        </div>

                                        &emsp;

                                        <div id="InvDetails" visible="true" runat="server">

                                            <%--<asp:GridView ID="gvEmployeeDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                CssClass="table4" DataKeyNames="EMP_AID" Width="100%">--%>
                                            <asp:GridView ID="gvEmployeeDetails" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5"
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
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
                                                            <asp:LinkButton ID="lnkBtnOpenPayslip" runat="server" Text="Payslip" ForeColor="Blue"
                                                                OnClick="lnkBtnOpenPayslip_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenCumulativePayslip" runat="server" Text="Cumulative Payslip" ForeColor="Blue"
                                                                AutoPostBack="true" OnClick="lnkBtnOpenCumulativePayslip_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenTaxComputation" runat="server" Text="Tax Computation" ForeColor="Blue"
                                                                AutoPostBack="true" OnClick="lnkBtnOpenTaxComputation_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenForm16" runat="server" Text="Form 16" ForeColor="Blue"
                                                                AutoPostBack="true" OnClick="lnkBtnOpenForm16_Click" />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" Width="15%" BackColor="white" HorizontalAlign="Center" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnOpenAppraisalLetter" runat="server" Text="Appraisal Letter" ForeColor="Blue"
                                                                AutoPostBack="true" OnClick="lnkBtnOpenAppraisalLetter_Click" />
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
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
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

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnLoadDetails" />
                                    <asp:PostBackTrigger ControlID="gvEmployeeDetails" />
                                    <%-- <asp:PostBackTrigger ControlID="chk3" />
                                    <asp:PostBackTrigger ControlID="chk4" />
                                    <asp:PostBackTrigger ControlID="chk5" />
                                    <asp:PostBackTrigger ControlID="chk6" />
                                    <asp:PostBackTrigger ControlID="chk7" />
                                    <asp:PostBackTrigger ControlID="chk8" />
                                    <asp:PostBackTrigger ControlID="radioPrimary1" />
                                    <asp:PostBackTrigger ControlID="radioPrimary2" />
                                    <asp:PostBackTrigger ControlID="btnSave" />
                                    <asp:PostBackTrigger ControlID="btnClose" />
                                    <asp:PostBackTrigger ControlID="drpType" />
                                    <asp:PostBackTrigger ControlID="btnCalTtl" />--%>
                                </Triggers>
                            </asp:UpdatePanel>

                        </div>
                    </section>
                </div>

            </div>
        </section>
        <script type="text/javascript">
            // Initial load
            $(document).ready(function () {
                $('#<%= drpEmpType.ClientID %>').select2();
            });

            // After every postback
            Sys.Application.add_load(function () {
                $('#<%= drpEmpType.ClientID %>').select2();
            });
        </script>
    </section>


</asp:Content>

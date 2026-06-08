<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="InvestmentDetails.aspx.cs" Inherits="NewPortal2023.ESS.InvestmentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .wrap {
            white-space: normal;
            width: 200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script language="javascript" src="Common.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to clear all investment data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
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

        .table4 {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
            background-color: #FFFFFF;
        }

        .total {
            padding-left: 5px;
            padding-right: 5px;
            padding-top: 1px;
            padding-bottom: 1px;
            border-bottom: 1px solid #d3dbdf;
            border-left: 1px solid #d3dbdf;
            border-right: 1px solid #d3dbdf;
            height: 10px;
            width: 10%;
            font-weight: bold;
            background-color: #efefef;
        }

            .total TD {
                font-weight: bold;
                height: 20px;
                background-color: #efefef;
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
            font-family: Tahoma; /* height: 20px; */
            text-decoration: none;
        }

        .blur {
            filter: blur(5px); /* Adjust the blur radius as needed */
            pointer-events: none; /* Prevent interaction with blurred elements */
        }
    </style>
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Invetsment Declaration</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>


                            <table width="100%" border="0" cellspacing="0" cellpadding="0" id="Table9">
                                <tr id="trPe1" runat="server" visible="false">
                                    <td class="title">Provident Fund Deduction - Declaration
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblTaxMessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trPe2" runat="server" visible="false">
                                    <td>
                                        <table width="100%">
                                            <hr />
                                            <tr>
                                                <td align="left" class="tdlabel" style="width: 20%;"></td>
                                                <td align="left" style="width: 40%; text-align: center; font-weight: bold;">PF - Flat Rs. 1800
                                                </td>
                                                <td align="left" style="width: 40%; text-align: center; font-weight: bold;">PF @ 12%
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trPe3" runat="server" visible="false">
                                    <td>
                                        <table width="100%">
                                            <hr />
                                            <tr>
                                                <td align="left" class="tdlabel" style="width: 20%;">Select Option:
                                                </td>
                                                <td align="left" style="width: 40%; text-align: center; font-weight: bold;">
                                                    <asp:RadioButton ID="rdbOld" runat="server" GroupName="TaxOption" />
                                                </td>
                                                <td align="left" style="width: 40%; text-align: center; font-weight: bold;">
                                                    <asp:RadioButton ID="rdbNew" runat="server" GroupName="TaxOption" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="trPe4" runat="server" visible="false">
                                    <td>
                                        <table width="100%">
                                            <caption>
                                                <hr />
                                                <tr>
                                                    <td>

                                                        <asp:Button ID="btnSaveOption" runat="server" OnClick="btnSaveOption_Click"
                                                            Text="Submit Declaration" />
                                                    </td>
                                                </tr>
                                            </caption>
                                        </table>
                                    </td>
                                </tr>

                                <tr>
                                    <td>
                                        <div id="NoteDclr" runat="server" visible="false">

                                            <tr>
                                                <td align="left" class="tdlabel" style="width: 100%; font-weight: bold;">Note :-
                                       
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="left" class="tdlabel" style="width: 100%; color: red; font-weight: bold;">Employees who choose the New Tax Regime do not need to update their investment declaration.
                                       
                                                </td>
                                            </tr>

                                        </div>
                                    </td>
                                </tr>

                                <tr valign="top">
                                    <td valign="top">
                                        <asp:LinkButton ID="lnkFAQ" runat="server" Visible="false"
                                            Style="float: right; font-weight: bold; color: blue; margin-right: 20px; margin-top: 10px; text-decoration: none;"
                                            OnClick="lnkFAQ_Click">
                                            Download F.A.Q.
                                        </asp:LinkButton>


                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <div id="Declaration" runat="server">
                                                    <table width="100%" border="0" cellspacing="6" cellpadding="0" class="table5" id="Table7">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                                                                <div id="divAlert" class="alert alert-block alert-success fade in" runat="server" visible="false">
                                                                    <button data-dismiss="alert" class="close close-sm" type="button">
                                                                        <i class="fa fa-times"></i>
                                                                    </button>
                                                                </div>
                                                                <br />
                                                                <asp:Button ID="btnPrintIndiafirst" runat="server" Text="Print Form 12BB" CssClass="button-xlarge-primary"
                                                                    OnClick="btnPrintIndiafirst_Click" />

                                                                <br />
                                                                <table>
                                                                    <tr>
                                                                        <td style="width: 50%;">
                                                                            <table id="updBtnTable" runat="server">
                                                                                <tr id="step1" runat="server">
                                                                                    <td style="font-weight: bold;">Step 1:
                                                                                    </td>
                                                                                    <td>Kindly fill out the Investment Declaration below.
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="step2" runat="server">
                                                                                    <td style="font-weight: bold;">Step 2:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btnPrint" runat="server" Text="Print Form 12BB" CssClass="button-xlarge-primary"
                                                                                            OnClick="btnPrint_Click" Width="200px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="step3" runat="server">
                                                                                    <td style="font-weight: bold;">Step 3:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btnUpload12BB" runat="server" OnClick="btnUpload12BB_Click" Text="Upload 12BB"
                                                                                            Width="200px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="step4" runat="server">
                                                                                    <td style="font-weight: bold;">Step 4:
                                                                                    </td>
                                                                                    <td>
                                                                                        <asp:Button ID="btnUploadInv" runat="server" OnClick="btnUploadInv_Click" Text="Upload Investment Proof"
                                                                                            Width="200px" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="step5" runat="server">
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <asp:Button ID="btnDiscrepancy" runat="server" Visible="false"
                                                                                            Text="Investment and Flexi Verification Report" CssClass="wrap"
                                                                                            OnClick="btnDiscrepancy_Click" />
                                                                                        <asp:Button ID="btnFlexiReport" runat="server" Visible="false"
                                                                                            Text="Flexi Verification Report" Width="200px"
                                                                                            OnClick="btnFlexiReport_Click" />
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="step6" runat="server">
                                                                                    <td></td>
                                                                                    <td>
                                                                                        <asp:Button ID="btnClearInv" runat="server" OnClick="btnClearInv_Click" OnClientClick="Confirm()"
                                                                                            Text="Clear All Details" Width="200px" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 50%; text-align: right; vertical-align: top; font-size: 20px;">
                                                                            <table>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lnkDownload" runat="server" OnClick="lnkDownload_Click">Download Forms</asp:LinkButton>
                                                                                        <asp:LinkButton ID="lnkManual" runat="server" OnClick="lnkManual_Click">Download Investment Submission Guide</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:LinkButton ID="lnkDeclarationsAngel" runat="server" OnClick="lnkDeclarationsAngel_Click" Visible="false">Download Declaration Documents</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>

                                                        <br />

                                                        <div id="Note" runat="server">

                                                            <tr>
                                                                <td align="left" class="tdlabel" style="width: 100%; font-weight: bold;">Note :-
                                       
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="tdlabel" style="width: 100%; color: red; font-weight: bold;">1.Employees who choose the Old Tax Regime, investment declaration options will be enabled.
                                       
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="left" class="tdlabel" style="width: 100%; color: red; font-weight: bold;">2.Employees who choose the New Tax Regime do not need to update their investment declaration.
                                                                </td>
                                                            </tr>
                                                        </div>
                                                        <div id="SelTaxOpt" runat="server">
                                                            <%--   <table width="100%" cellspacing="6" cellpadding="0" id="table2">
                                                                <tr>
                                                                    <td align="left" class="tdlabel" style="width: 25%; font-weight: bold;">Select Option Tax Regime:
                                                                    </td>
                                                                    <td align="left" style="width: 25%; text-align: center; font-weight: bold;">
                                                                        <asp:RadioButton ID="RadioButton1" runat="server" GroupName="TaxOption" Text="Old Regime" />
                                                                    </td>
                                                                    <td align="left" style="width: 25%; text-align: center; font-weight: bold;">
                                                                        <asp:RadioButton ID="RadioButton2" runat="server" GroupName="TaxOption" Text="New Regime" />
                                                                    </td>
                                                                    <td align="right" style="width: 25%; text-align: center; font-weight: bold;">
                                                                        <asp:Button ID="Button1" runat="server" OnClick="btnSaveOption_Click"
                                                                            Text="SAVE SELECTED TAX OPTION" />
                                                                    </td>
                                                                </tr>
                                                            </table>--%>

                                                            <div class="form-group" id="Div1" runat="server" visible="true" style="display: flex; align-items: center;">
                                                                <label style="font-size: 20px; color: darkcyan; margin-right: 10px;">Select Option Tax Regime:</label>
                                                                <div class="form-check" style="margin-right: 15px;">
                                                                    <asp:RadioButton ID="RadioButton1" runat="server" GroupName="TaxOption" CssClass="form-check-input" />
                                                                    <label for="RadioButton1" class="form-check-label">Old Regime</label>
                                                                </div>
                                                                <div class="form-check" style="margin-right: 15px;">
                                                                    <asp:RadioButton ID="RadioButton2" runat="server" GroupName="TaxOption" CssClass="form-check-input" />
                                                                    <label for="RadioButton2" class="form-check-label">New Regime</label>
                                                                </div>
                                                                <div class="form-group right-align" id="Div2" runat="server" visible="true">
                                                                    <asp:Button ID="Button1" runat="server" OnClick="btnSaveOption_Click" Text="SAVE SELECTED TAX OPTION" CssClass="btn btn-primary" />
                                                                </div>


                                                            </div>




                                                            <hr />
                                                        </div>

                                                    </table>

                                                    <div id="divAll" runat="server">
                                                        <div id="EmpDetails" runat="server">
                                                            <div class="form-group">
                                                                <asp:LinkButton ID="lnkOther" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                                    OnClick="lnkOther_Click" AutoPostBack="true">1. Employee&#39;s Information</asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <div style="margin: 10px">
                                                            <asp:UpdatePanel ID="updtPnlOther" runat="server" UpdateMode="Conditional">
                                                                <ContentTemplate>
                                                                    <div class="flex-container" id="OtherDetails" visible="true" runat="server" style="display: flex; flex-direction: column; gap: 10px; margin-top: 1.5%; padding: 1.5%;">

                                                                        <div class="form-group">
                                                                            <asp:GridView ID="gvOther" runat="server" AutoGenerateColumns="False"
                                                                                HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvOther_RowDataBound"
                                                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                                <Columns>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblentrytype" runat="Server" Text='<%# Eval("ENTRY_TYPE") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblentrylen" runat="Server" Text='<%# Eval("ENTRY_LENGTH") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField Visible="false">
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblEntryMode" runat="Server" Text='<%# Eval("ENTRY_MODE") %>' />
                                                                                        </ItemTemplate>
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Employee's Information">
                                                                                        <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                                    </asp:TemplateField>
                                                                                    <asp:TemplateField HeaderText="Details">
                                                                                        <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                        <ItemTemplate>
                                                                                            <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="200" ToolTip="Enter other details"
                                                                                                Text='<%# Eval("OTHER_DETAILS") %>' Width="130px"></asp:TextBox>
                                                                                            <asp:CheckBox ID="chkAgree" runat="server" Visible="false" />
                                                                                        </ItemTemplate>
                                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                                    </asp:TemplateField>
                                                                                </Columns>
                                                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                                <EmptyDataTemplate>
                                                                                    <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                        <tr>
                                                                                            <td class="GridViewHeader" style="width: 600px">Other Details
                                                                                            </td>
                                                                                            <td class="GridViewHeader" style="width: 130px">Amount
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </EmptyDataTemplate>
                                                                            </asp:GridView>
                                                                        </div>
                                                                        <div class="row justify-content-md-center">
                                                                            <div class="col-sm-12 mx-auto text-center">
                                                                                <asp:Button CssClass="btn btn-success" ID="lnkUpdateOther" runat="server" OnClick="lnkUpdateOther_Click" AutoPostBack="true" Text="Submit Information" />
                                                                            </div>
                                                                        </div>

                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="lnkOther" EventName="Click" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>

                                                        </div>
                                                    </div>

                                                    <div id="OtherinvDetals" runat="server">
                                                        <div class="form-group">

                                                            <div class="form-group">
                                                                <asp:LinkButton ID="lblInvDetails" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                                    OnClick="lblInvDetails_Click" AutoPostBack="true">2. Investment Declaration</asp:LinkButton>
                                                            </div>

                                                            <div class="flex-container" id="InvDetails" visible="false" runat="server" style="display: flex; flex-direction: column; gap: 10px; margin-top: 1.5%; padding: 1.5%;">
                                                                <div class="form-group col-sm-12">
                                                                    <%--<asp:GridView ID="gvInv" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                        CssClass="table4" HorizontalAlign="Left" ToolTip="Investment details" OnRowDataBound="gvInv_RowDataBound"
                                                        ShowFooter="True">--%>
                                                                    <asp:GridView ID="gvInv" runat="server" AutoGenerateColumns="False"
                                                                        HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvInv_RowDataBound"
                                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC" ShowFooter="True"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                        <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" />
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHead" runat="Server" Text='<%# Eval("is_header") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Details of Investments">
                                                                                <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    Total :
                                                                                </FooterTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="true" HeaderText="Limit">
                                                                                <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                                        ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                                        AutoPostBack="True" OnTextChanged="txtAmt_TextChanged"></asp:TextBox>
                                                                                    <asp:CheckBox ID="chkAgree" runat="server" Visible="false" />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTot" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <EmptyDataTemplate>
                                                                            <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                <tr>
                                                                                    <td class="GridViewHeader" style="width: 600px">Details of Investments
                                                                                    </td>
                                                                                    <td class="GridViewHeader" style="width: 130px">Amount
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>

                                                                <div class="row justify-content-md-center">
                                                                    <div class="col-sm-12 mx-auto text-center">
                                                                        <asp:Button CssClass="btn btn-success" ID="lnkUpdate" runat="server" OnClick="lnkUpdate_Click" AutoPostBack="true" Text="Submit Declaration" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="form-group" id="tblRent" runat="server">

                                                            <div class="form-group">
                                                                <asp:LinkButton ID="lnkRent" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                                    OnClick="lnkRent_Click">3. Rent Declaration</asp:LinkButton>
                                                            </div>

                                                            <div class="flex-container" id="RentDetails" visible="false" runat="server" style="display: flex; flex-direction: column; gap: 10px; margin-top: 1.5%; padding: 1.5%;">
                                                                <div class="form-group col-sm-12">
                                                                    <%--<asp:GridView ID="gvLandlordDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="Landlord Details">--%>
                                                                    <asp:GridView ID="gvLandlordDetails" runat="server" AutoGenerateColumns="False"
                                                                        HorizontalAlign="Left" CellPadding="5"
                                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblentrytype" runat="Server" Text='<%# Eval("ENTRY_TYPE") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblentrylen" runat="Server" Text='<%# Eval("ENTRY_LENGTH") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntryMode" runat="Server" Text='<%# Eval("ENTRY_MODE") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Landlord Information">
                                                                                <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Details">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmtLnd" runat="server" CssClass="input" MaxLength="500" ToolTip="Enter other details"
                                                                                        Text='<%# Eval("OTHER_DETAILS") %>' Width="250px" OnTextChanged="txtAmtLnd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <EmptyDataTemplate>
                                                                            <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                <tr>
                                                                                    <td class="GridViewHeader" style="width: 600px">Other Details
                                                                                    </td>
                                                                                    <td class="GridViewHeader" style="width: 130px">Amount
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Rented Property Address :</label>
                                                                    <asp:TextBox ID="txtAddress" runat="server" Height="53px" TextMode="MultiLine" Width="278px"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group">
                                                                    <b><span style="color: Red;">
                                                                        <asp:Label ID="lblNotes" runat="server" Visible="false"></asp:Label>
                                                                </div>

                                                                <div class="form-group col-sm-12">
                                                                    <%--     <asp:GridView ID="gvRent" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="Rent Declaration"
                                                            ShowFooter="True" OnRowDataBound="gvRent_RowDataBound">--%>

                                                                    <asp:GridView ID="gvRent" runat="server" AutoGenerateColumns="False"
                                                                        HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvRent_RowDataBound"
                                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC" ShowFooter="True"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                        <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" />
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Details of Rent Paid">
                                                                                <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    Total :
                                                                                </FooterTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="true" HeaderText="Limit">
                                                                                <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false" HeaderText="Landlord Limit">
                                                                                <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLimitLand" runat="Server" Text='<%# Eval("LIMIT_LAND") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkSel" runat="server" AutoPostBack="True" OnCheckedChanged="chkSel_CheckedChanged" />
                                                                                    &nbsp;Amount
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                                        ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                                        AutoPostBack="True" OnTextChanged="txtRentAmt_TextChanged"></asp:TextBox>
                                                                                    <asp:CheckBox ID="chkAgree" runat="server" Visible="false" />
                                                                                </ItemTemplate>
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTot" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="PAN of Landlord">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                <HeaderTemplate>
                                                                                    <asp:CheckBox ID="chkPAN" runat="server" AutoPostBack="True" OnCheckedChanged="chkPan_CheckedChanged" />
                                                                                    &nbsp;PAN of Landlord
                                                                                </HeaderTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtPAN" runat="server" CssClass="input" Text='<%# Eval("LANLORD_PAN") %>'
                                                                                        MaxLength="10" Width="130px" OnTextChanged="txtPAN_TextChanged"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <EmptyDataTemplate>
                                                                            <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                <tr>
                                                                                    <td class="GridViewHeader" style="width: 600px">Details of Rent Paid
                                                                                    </td>
                                                                                    <td class="GridViewHeader" style="width: 130px">Amount
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="row justify-content-md-center">
                                                                    <div class="col-sm-12 mx-auto text-center">
                                                                        <asp:Button CssClass="btn btn-success" ID="lnkUpdateRent" runat="server" OnClick="lnkUpdateRent_Click" AutoPostBack="true" Text="Submit Declaration" />
                                                                        <asp:Button CssClass="btn btn-warning" ID="btnClearRent" runat="server" Text="Clear Fields" OnClientClick="return confirm('Are you sure?');"
                                                                            AutoPostBack="true" OnClick="btnClearRent_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="form-group" id="tblRentNew" runat="server" visible="false">

                                                            <div class="form-group">
                                                                <asp:LinkButton ID="lnkRentNew" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                                    OnClick="lnkRentNew_Click">3. Rent Declaration</asp:LinkButton>
                                                            </div>
                                                            <div class="form-group">
                                                                <asp:UpdatePanel ID="UpdpnlRentNew" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <div id="RentDetailsNew" visible="false" runat="server">
                                                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table11">
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <asp:GridView ID="gvLandlordDetailsNew" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="Landlord Details">
                                                                                            <Columns>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblentrytype" runat="Server" Text='<%# Eval("ENTRY_TYPE") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblentrylen" runat="Server" Text='<%# Eval("ENTRY_LENGTH") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblEntryMode" runat="Server" Text='<%# Eval("ENTRY_MODE") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Landlord Information">
                                                                                                    <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Details">
                                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtAmtLnd" runat="server" CssClass="input" MaxLength="500" ToolTip="Enter other details"
                                                                                                            Text='<%# Eval("OTHER_DETAILS") %>' Width="250px" OnTextChanged="txtAmtLnd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <EmptyDataTemplate>
                                                                                                <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                                    <tr>
                                                                                                        <td class="GridViewHeader" style="width: 600px">Other Details
                                                                                                        </td>
                                                                                                        <td class="GridViewHeader" style="width: 130px">Amount
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </EmptyDataTemplate>
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr id="Tr1" runat="server" visible="false">
                                                                                    <td valign="top">
                                                                                        <table>
                                                                                            <tr>
                                                                                                <td><b>Rented Property Address :</b></td>
                                                                                                <td>
                                                                                                    <asp:TextBox ID="txtAddressNew" runat="server" Height="53px" TextMode="MultiLine" Width="278px"></asp:TextBox></td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <asp:GridView ID="gvRentNew" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="Rent Declaration"
                                                                                            ShowFooter="True" OnRowDataBound="gvRentNew_RowDataBound">
                                                                                            <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                                                Font-Strikeout="False" Font-Underline="False" />
                                                                                            <Columns>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Details of Rent Paid">
                                                                                                    <ItemStyle CssClass="GridViewItem" Width="200px" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <FooterTemplate>
                                                                                                        Total :
                                                                                                    </FooterTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="false" HeaderText="Limit">
                                                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField Visible="true" HeaderText="Limit">
                                                                                                    <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblLimitLand" runat="Server" Text='<%# Eval("LIMIT_LAND") %>' />
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:CheckBox ID="chkSel" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelNew_CheckedChanged" />
                                                                                                        &nbsp;Amount
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtAmt" runat="server" CssClass="form-control input-sm" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                                                            ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                                                            AutoPostBack="True" OnTextChanged="txtRentAmtNew_TextChanged"></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                    <FooterTemplate>
                                                                                                        <asp:Label ID="lblTot" runat="server" Text="Label"></asp:Label>
                                                                                                    </FooterTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="PAN of Landlord">
                                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:CheckBox ID="chkPAN" runat="server" AutoPostBack="True" OnCheckedChanged="chkPanNew_CheckedChanged" />
                                                                                                        &nbsp;PAN of Landlord
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtPAN" runat="server" CssClass="input" Text='<%# Eval("LANLORD_PAN") %>'
                                                                                                            MaxLength="10" Width="130px"></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Landlord Name">
                                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:CheckBox ID="chkName" runat="server" AutoPostBack="True" OnCheckedChanged="chkName_CheckedChanged" />
                                                                                                        &nbsp;Landlord Name
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="input" Text='<%# Eval("OTHER_DETAILS") %>'
                                                                                                            MaxLength="50" Width="130px"></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Landlord Address">
                                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                    <HeaderTemplate>
                                                                                                        <asp:CheckBox ID="chkAddr" runat="server" AutoPostBack="True" OnCheckedChanged="chkAddr_CheckedChanged" />
                                                                                                        &nbsp;Landlord Address
                                                                                                    </HeaderTemplate>
                                                                                                    <ItemTemplate>
                                                                                                        <asp:TextBox ID="txtAddr" runat="server" CssClass="form-control input-sm" Text='<%# Eval("PROP_ADDRESS") %>'
                                                                                                            MaxLength="500" Width="130px"></asp:TextBox>
                                                                                                    </ItemTemplate>
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <EmptyDataTemplate>
                                                                                                <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                                    <tr>
                                                                                                        <td class="GridViewHeader" style="width: 600px">Details of Rent Paid
                                                                                                        </td>
                                                                                                        <td class="GridViewHeader" style="width: 130px">Amount
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </EmptyDataTemplate>
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <asp:Button ID="lnkUpdateRentNew" runat="server" OnClick="lnkUpdateRentNew_Click" Text="Submit Declaration" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                    </ContentTemplate>
                                                                    <Triggers>
                                                                        <asp:AsyncPostBackTrigger ControlID="lnkRent" EventName="Click" />
                                                                    </Triggers>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                        </div>

                                                        <div class="form-group" id="HousLoan">

                                                            <div>
                                                                <asp:LinkButton ID="lnk12" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                                    OnClick="lnk12_Click">4. Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary</asp:LinkButton>
                                                            </div>
                                                            <div class="flex-container" id="TwelveDetails" visible="false" runat="server" style="display: flex; flex-direction: column; gap: 10px; margin-top: 1.5%; padding: 1.5%;">
                                                                <div class="form-group col-sm-12">
                                                                    <%--<asp:GridView ID="gvLenderDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="Lender Details">--%>
                                                                    <asp:GridView ID="gvLenderDetails" runat="server" AutoGenerateColumns="False"
                                                                        HorizontalAlign="Left" CellPadding="5"
                                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblentrytype" runat="Server" Text='<%# Eval("ENTRY_TYPE") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblentrylen" runat="Server" Text='<%# Eval("ENTRY_LENGTH") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblEntryMode" runat="Server" Text='<%# Eval("ENTRY_MODE") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Lender Information">
                                                                                <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Details">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmtLnd" runat="server" CssClass="input" MaxLength="500" ToolTip="Enter other details"
                                                                                        Text='<%# Eval("OTHER_DETAILS") %>' Width="250px" OnTextChanged="txtAmtLnd_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <EmptyDataTemplate>
                                                                            <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                <tr>
                                                                                    <td class="GridViewHeader" style="width: 600px">Other Details
                                                                                    </td>
                                                                                    <td class="GridViewHeader" style="width: 130px">Amount
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="form-group col-sm-12">
                                                                    <%--<asp:GridView ID="gvtwelve" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="12c Declaration" OnRowDataBound="gvtwelve_RowDataBound"
                                                            ShowFooter="True">--%>
                                                                    <asp:GridView ID="gvtwelve" runat="server" AutoGenerateColumns="False"
                                                                        HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvtwelve_RowDataBound"
                                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC" ShowFooter="True"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                        <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" />
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblHead" runat="Server" Text='<%# Eval("is_header") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblRead" runat="Server" Text='<%# Eval("IS_READONLY") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Formula" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblFormula" runat="Server" Text='<%# Eval("FORMULA") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="IsTotal" Visible="False">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblIsTotal" runat="Server" Text='<%# Eval("IS_TOTAL") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary">
                                                                                <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                                <FooterTemplate>
                                                                                    Total :
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="true" HeaderText="Limit">
                                                                                <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTot" runat="server" Text="Label"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                                        ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                                        AutoPostBack="True" OnTextChanged="txttwelveAmt_TextChanged"></asp:TextBox>
                                                                                    <asp:CheckBox ID="chkIsSelect" runat="server" Visible="false" AutoPostBack="true"
                                                                                        OnCheckedChanged="chkIsSelect_CheckedChanged" />
                                                                                    <asp:CheckBox ID="chkAgree" runat="server" Visible="false" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Property Address">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtPropAdd" runat="server" CssClass="input" TextMode="MultiLine"
                                                                                        Text='<%# Eval("PROP_ADDRESS") %>' MaxLength="500" Width="130px"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <EmptyDataTemplate>
                                                                            <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                <tr>
                                                                                    <td class="GridViewHeader" style="width: 600px">Interest on Housing Loan / Details of Income/(Loss) From Other Than Salary
                                                                                    </td>
                                                                                    <td class="GridViewHeader" style="width: 130px">Amount
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="row justify-content-md-center">
                                                                    <div class="col-sm-12 mx-auto text-center">
                                                                        <asp:Button CssClass="btn btn-success" ID="lnkUpdate12" runat="server" OnClick="lnkUpdate12_Click" AutoPostBack="true" Text="Submit Declaration" />
                                                                    </div>
                                                                </div>
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <div id="PrevEmployer" runat="server">
                                                        <div class="form-group" id="Table5" runat="server" visible="false">

                                                            <div>
                                                                <asp:LinkButton ID="lnk12b" runat="server" Style="font-size: 20px; font-weight: bold; color: darkcyan;"
                                                                    OnClick="lnk12b_Click">5. Details of Income From Previous Employer</asp:LinkButton>
                                                            </div>
                                                            <div class="flex-container" id="TwelveBDetails" visible="false" runat="server" style="display: flex; flex-direction: column; gap: 10px; margin-top: 1.5%; padding: 1.5%;">
                                                                <div class="form-group col-sm-12">
                                                                    <%--<asp:GridView ID="gvTwelB" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                            CssClass="table4" HorizontalAlign="Left" ToolTip="12c Declaration" ShowFooter="False">--%>
                                                                    <asp:GridView ID="gvTwelB" runat="server" AutoGenerateColumns="False"
                                                                        HorizontalAlign="Left" CellPadding="5"
                                                                        GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC" ShowFooter="False"
                                                                        BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                                        <FooterStyle CssClass="total" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                                            Font-Strikeout="False" Font-Underline="False" />
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblId" runat="Server" Text='<%# Eval("inv_aid") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("DETID") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblMID" runat="Server" Text='<%# Eval("inv_desc") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblsort" runat="Server" Text='<%# Eval("sort_order") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblisprop" runat="Server" Text='<%# Eval("IS_PROP_ADDRESS") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Details of Income From Previous Employer">
                                                                                <ItemStyle CssClass="GridViewItem" Width="600px" />
                                                                                <FooterTemplate>
                                                                                    <%--Total :--%>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("inv_webdesc") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField Visible="false" HeaderText="Limit">
                                                                                <ItemStyle CssClass="GridViewItem" Width="50px" />
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("LIMIT") %>' />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Amount">
                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                <FooterTemplate>
                                                                                    <asp:Label ID="lblTot" runat="server" Text="Label" Visible="false"></asp:Label>
                                                                                </FooterTemplate>
                                                                                <ItemTemplate>
                                                                                    <asp:TextBox ID="txtAmt" runat="server" CssClass="input" MaxLength="13" onkeypress="return validateFloat(event, 0);"
                                                                                        ToolTip="Enter investment amount" Text='<%# Eval("AMOUNT") %>' Width="130px"
                                                                                        AutoPostBack="True" OnTextChanged="txtTwelvBAmt_TextChanged"></asp:TextBox>
                                                                                </ItemTemplate>
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                                        <EmptyDataTemplate>
                                                                            <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                <tr>
                                                                                    <td class="GridViewHeader" style="width: 600px">Details of Income From Previous Employer
                                                                                    </td>
                                                                                    <td class="GridViewHeader" style="width: 130px">Amount
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </div>
                                                                <div class="form-group">
                                                                    <div id="div12BNote" runat="server" style="border: 2px solid gray; padding: 10px; margin-top: 10px; color: Maroon; font-weight: bold; text-align: center;">
                                                                        **Only single file is allowed. In case of multiple files, kindly zip and upload.**
                                                                    </div>

                                                                    <br />

                                                                    <div class="form-group" id="div12BUpload" runat="server">
                                                                        <div class="col-sm-4">
                                                                            <asp:FileUpload ID="fupPrev" runat="server" CssClass="btn fileinput-button" />
                                                                            <asp:TextBox ID="txtPassword12B" runat="server" CssClass="form-control sm-2" placeholder="Password (if any)"></asp:TextBox>

                                                                            <div class="mt-3">
                                                                                <asp:Button ID="btnUpload12BSupport" runat="server" Text="Upload Support Documents" CssClass="btn btn-primary" OnClick="btnUpload12BSupport_Click" />
                                                                                <asp:LinkButton ID="lnkDownload12BSupport" runat="server" CssClass="btn btn-link font-weight-bold" OnClick="lnkDownload12BSupport_Click">
                                                                        Download Saved Documents
                                                                    </asp:LinkButton>
                                                                            </div>
                                                                        </div>
                                                                    </div>


                                                                    <div class="row justify-content-md-center" runat="server" visible="false">
                                                                        <div class="col-sm-12 mx-auto text-center">
                                                                            <asp:Button CssClass="btn btn-success" ID="lnkUpdate12B" runat="server" OnClick="lnkUpdate12B_Click" AutoPostBack="true" Text="Submit Declaration" />
                                                                        </div>
                                                                    </div>
                                                                </div>


                                                            </div>
                                                        </div>

                                                        <hr />
                                                        <div style="width: 100%; text-align: center;">
                                                            <asp:Button ID="btnSubmitAll" runat="server" OnClick="btnSubmitAll_Click" class="btn btn-success" Style="width: 100px; height: 50px; font-weight: bold;"
                                                                Text="Submit" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="lnkDownload" />
                                                <asp:PostBackTrigger ControlID="lnkManual" />
                                                <asp:PostBackTrigger ControlID="lnkDeclarationsAngel" />
                                                <asp:PostBackTrigger ControlID="btnUpload12BSupport" />
                                                <asp:PostBackTrigger ControlID="lnkDownload12BSupport" />
                                                 <asp:PostBackTrigger ControlID="lnkFAQ" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>


                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>
</asp:Content>

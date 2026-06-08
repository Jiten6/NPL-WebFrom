<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="FlexiSupport.aspx.cs" Inherits="NewPortal2023.ESS.FlexiSupport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


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
            font-weight: normal;
            font-size: 14pt;
            vertical-align: middle;
            color: darkcyan;
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
            font-weight: normal;
            font-size: 14pt;
            vertical-align: middle;
            text-transform: capitalize;
            color:darkcyan;
            font-family: Tahoma; /* height: 20px; */
            text-decoration: none;
        }

        p.MsoNormal {
            margin-top: 0in;
            margin-right: 0in;
            margin-bottom: 10.0pt;
            margin-left: 0in;
            line-height: 115%;
            font-size: 11.0pt;
            font-family: "Calibri", "sans-serif";
        }

        .style1 {
            display: block;
            float: left;
            text-align: left;
            width: auto;
            font-weight: bold;
            font-size: 11pt;
            vertical-align: middle;
            text-transform: capitalize;
            color: #205a94;
            font-family: Tahoma;
            height: 41px;
            text-decoration: none;
        }

        .style2 {
            height: 39px;
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
                            <h3 style="color: white">Flexi Support</h3>
                        </header>
                        <div class="panel-body" style="background-color:aliceblue">
                            <div>
                                <asp:LinkButton ID="lnkOther" runat="server" CssClass="tableTitle" ForeColor="darkcyan"
                                    Visible="false">FLEXI COMPENSATION BENEFIT</asp:LinkButton>
                                <h4 class="page-header" style="text-align: left;">
                                    <asp:Label ID="lblOtherLabel" runat="server" CssClass="tableTitle" ForeColor="darkcyan"
                                        Text="FLEXI COMPENSATION BENEFIT"></asp:Label>
                                </h4>
                            </div>

                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>
                            <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwList" runat="server">
                                    <div class="adv-table">
                                        <div>
                                            <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                <asp:Label ID="lblMessage" Style="font-weight: bold; font-size: 18px;" runat="server"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <table width="100%" border="0" cellpadding="3" cellspacing="1" class="table4">
                                                <tr valign="top">
                                                    <td valign="top" style="border:solid; border-color:darkcyan">
                                                        <div style="text-align: right;">
                                                            <asp:LinkButton ID="lnkFAQ" runat="server" Style="font-size: 15px; font-weight: bold;" OnClick="lnkFAQ_Click">Download FAQ</asp:LinkButton>
                                                        </div>

                                                        <div id="instructionsDiv" runat="server" style="border: 2px solid; border-color:darkcyan; margin-top: 5px; padding: 10px;">
                                                            <b><u><span style="font-size: 20px;">Steps for uploading bills data:</span></u></b>
                                                            <br />
                                                            <br />
                                                            <b>Step 1:</b>
                                                            <ol>
                                                                <li>Click on the “Download Supports Form” link to download Excel file for support.
																	<br />
                                                                    Every flexi component has it's own sheet in the excel file. Kindly fill all the required details in their respective sheets based on the available proofs in the excel file.</li>
                                                                <li>Upload the updated excel sheet and the click on <b>“Upload Support Form”.</b></li>
                                                                <li>To make any changes to the details you have already uploaded, click on the link 'Download Saved Supports Form' and download the excel file you have already uploaded. Make changes to this file and upload again to update previously uploaded data.</li>
                                                            </ol>
                                                            <b>Step 2:</b>
                                                            <ol>
                                                                <li>Upload the bill proofs(scanned copies) under 'Upload Bills' section.</li>
                                                            </ol>
                                                        </div>

                                                        <div id="divOtherComp" runat="server" style="border: 2px solid red; margin-top: 5px; padding: 10px;">
                                                            <b><u><span style="font-size: 20px;">Steps for uploading bills data:</span></u></b>
                                                            <br />
                                                            <br />

                                                            <%--<b>Step 1:</b>--%>
                                                            <ol>
                                                                <li style="font-weight: bold;">Upload the bill proofs(scanned copies) under 'Upload Bills' section.</li>
                                                            </ol>
                                                        </div>

                                                        <div id="divUplSupp" runat="server">
                                                            <h2 style="font-weight: bold; margin-bottom: 10px;">Upload Supports Form:</h2>
                                                            <asp:LinkButton ID="lnkXLTemplate" runat="server" Style="font-weight: bold;" OnClick="lnkXLTemplate_Click">Download Supports Form</asp:LinkButton>
                                                            <div style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                                <asp:FileUpload ID="fupInvXL" runat="server" />
                                                                <asp:Button ID="btnUploadXL" runat="server" Style="background-color: Aqua; color: Black; padding: 6px;"
                                                                    Text="Upload Supports Form" CssClass="button-xlarge-primary" OnClick="btnUploadXL_Click" />
                                                                <br />
                                                                <asp:LinkButton ID="lnkXLUploaded" runat="server" Style="font-weight: bold;"
                                                                    OnClick="lnkXLUploaded_Click">Download Saved Supports Form</asp:LinkButton>
                                                            </div>
                                                        </div>

                                                        <hr />

                                                        <div class="title" style="margin-bottom: 5px; text-decoration: underline; display: none;">
                                                            SUPPORTS RECEIVED
                                                        </div>

                                                        <div id="Div1" visible="false" runat="server">
                                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table4">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="gvReceived" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                            CssClass="table4" HorizontalAlign="Left" Width="100%">
                                                                            <Columns>
                                                                                <asp:TemplateField Visible="true" HeaderText="Particulars">
                                                                                    <ItemStyle CssClass="GridViewItem" Width="50%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Accepted Amounts from April 2019 to September 2019">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="50%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Amount"
                                                                                            Text='<%# Eval("AMOUNT") %>' ReadOnly="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <%--<asp:TemplateField Visible="false">
																					<ItemTemplate>
																						<asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
																					</ItemTemplate>
																				</asp:TemplateField>--%>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                    <tr>
                                                                                        <td class="GridViewHeader" style="width: 600px">Details
                                                                                        </td>
                                                                                        <td class="GridViewHeader" style="width: 130px">Amount
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <hr />

                                                        <div class="title" style="margin-bottom: 5px;">
                                                            SUMMARY  &nbsp; &nbsp;&nbsp; &nbsp;
															<asp:LinkButton ID="btnlnkDwnl" runat="server" OnClick="btnlnkDwnl_Click" Style="background-color: #736AFF; color: white; padding: 6px; display: inline-block; margin-bottom: 2px;" Text="Generate Report In Excel"></asp:LinkButton>
                                                        </div>

                                                        <div>


                                                            <table width="100%" border="1" cellpadding="3" cellspacing="1">
                                                                <tr id="tr2" runat="server">

                                                                    <%-- <td>Select Quarter Type </td>--%>
                                                                    <td align="left">Select Quarter Type&nbsp;: &nbsp;
																			<asp:DropDownList ID="drpPreviousQtr" runat="server" Visible="true" OnSelectedIndexChanged="drpPreviousQtr_SelectedIndexChanged"
                                                                                AutoPostBack="true" ForeColor="#205A94">
                                                                                <asp:ListItem Value="0">Select Quarter</asp:ListItem>

                                                                                <asp:ListItem Value="First Quarter">First Quarter</asp:ListItem>
                                                                                <asp:ListItem Value="Second Quarte">Second Quarter</asp:ListItem>
                                                                                <asp:ListItem Value="Thrid Quarter">Third Quarter</asp:ListItem>
                                                                                <asp:ListItem Value="Fourth Quarter">Fourth Quarter</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                    </td>


                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div id="PrevInvDetails" visible="true" runat="server">
                                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table21">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="gvPrevious" runat="server" AutoGenerateColumns="False" BorderWidth="0px" OnRowDataBound="gvPrevious_RowDataBound"
                                                                            CssClass="table4" HorizontalAlign="Left" Width="100%">
                                                                            <%-- OnRowDataBound="gvPrevious_RowDataBound"--%>
                                                                            <Columns>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemStyle CssClass="GridViewItem" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAid" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="true" HeaderText="Particulars">
                                                                                    <ItemStyle CssClass="GridViewItem" Width="50%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="true" HeaderText="Yearly Eligibility">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="15%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLimit" runat="Server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="true" HeaderText="Current Eligibility">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="15%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCurAmt" runat="Server" Text='<%# Eval("Fix_Amount") %>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="true" HeaderText="Quarter">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="50%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblQtr" runat="Server" Text='<%# Eval("QUARTERS") %>' CssClass="form-control input-sm"></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Submitted Bills">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="20%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Amount"
                                                                                            Text='<%# Eval("AMOUNT") %>' onkeypress="return validateFloat(event, 0);"
                                                                                            Width="130px" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged" BackColor="LightGray" ReadOnly="true"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Approved Amount">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAmountAccepted" runat="server" CssClass="input" MaxLength="13"
                                                                                            Text='<%# Eval("AMOUNT_ACCEPTED") %>' Width="130px" ReadOnly="true" BackColor="LightGray"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Rejected Amount">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAmountRejected" runat="server" CssClass="input" MaxLength="13"
                                                                                            Text='<%# Eval("AMOUNT_REJECTED") %>' Width="130px" ReadOnly="true" BackColor="LightGray"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                                    </ItemTemplate>
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
                                                                <tr>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div style="border: 2px solid gray; padding: 10px; margin-top: 25px; color: Red; font-weight: bold; text-align: center;">
                                                            **Please note that the below mentioned eligibility amount is for the entire financial year, however for employees joining in between the year the same will be considered on proportionate basis from the Date of Joining.**
                                                        </div>

                                                        <div>

                                                            <div id="divQut" runat="server" class="title" style="margin-bottom: 5px; text-decoration: underline;">
                                                                <table width="100%" border="1" cellpadding="3" cellspacing="1">
                                                                    <tr id="trQut" runat="server" visible="false">
                                                                        <td align="left">Quarter
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <table width="100%" border="1" cellpadding="3" cellspacing="1">
                                                                <tr id="trQuarter" runat="server" visible="false">

                                                                    <%-- <td>Select Quarter Type </td>--%>
                                                                    <td align="left">Select Quarter Type&nbsp;: &nbsp;
																			<asp:DropDownList ID="drpQuarterType" runat="server" Visible="true" OnSelectedIndexChanged="drpQuarterType_SelectedIndexChanged"
                                                                                AutoPostBack="true" ForeColor="#205A94">
                                                                                <asp:ListItem Value="0">Select Quarter</asp:ListItem>

                                                                                <asp:ListItem Value="First Quarter">First Quarter</asp:ListItem>
                                                                                <asp:ListItem Value="Second Quarte">Second Quarter</asp:ListItem>
                                                                                <asp:ListItem Value="Thrid Quarter">Third Quarter</asp:ListItem>
                                                                                <asp:ListItem Value="Fourth Quarter">Fourth Quarter</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                    </td>


                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div id="InvDetails" visible="true" runat="server">
                                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table18">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                            CssClass="table4" HorizontalAlign="Left" Width="100%" OnRowDataBound="gvCTC_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField Visible="true" HeaderText="Particulars">
                                                                                    <ItemStyle CssClass="GridViewItem" Width="60%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="true" HeaderText="Quarterly Eligibility">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="20%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("Fix_Amount") %>' />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Submitted Bills">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="20%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Amount"
                                                                                            Text='<%# Eval("AMOUNT") %>' onkeypress="return validateFloat(event, 0);"
                                                                                            Width="130px" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged" ReadOnly="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Approved Amount">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAmountAccepted" runat="server" CssClass="input" MaxLength="13"
                                                                                            Text='<%# Eval("AMOUNT_ACCEPTED") %>' Width="130px" ReadOnly="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Rejected Amount">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAmountRejected" runat="server" CssClass="input" MaxLength="13"
                                                                                            Text='<%# Eval("AMOUNT_REJECTED") %>' Width="130px" ReadOnly="false"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemStyle CssClass="GridViewItem" Width="60%" />
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblAID" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
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
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnPrint" runat="server" OnClick="btnFlexiPrint_Click" Text="Print"
                                                                            Style="width: 61px; height: 26px;" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <hr />

                                                        <div class="title" style="margin-bottom: 5px; text-decoration: underline; display: none;">
                                                            View Bills
                                                        </div>

                                                        <table width="100%" style="display: none;">
                                                            <tr>
                                                                <td align="left" style="width: 20%;" class="tdlabel">Flexi Heads:</td>
                                                                <td align="left">
                                                                    <asp:DropDownList ID="drpFlexiHeads" runat="server" CssClass="input"
                                                                        AutoPostBack="true" OnSelectedIndexChanged="drpFlexiHeads_SelectedIndexChanged">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td align="left" style="width: 20%;" class="tdlabel">Balance not availed:</td>
                                                                <td align="left">
                                                                    <asp:TextBox ID="txtEligibility" runat="server" CssClass="input" ReadOnly="true"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr id="reqDocs" runat="server">
                                                                <td align="left" style="width: 20%;" class="tdlabel">Required Documents:</td>
                                                                <td align="left">
                                                                    <ul id="meetingDocs" runat="server">
                                                                        <li>Hotels Original Bills</li>
                                                                        <li>Bills should be on employee name</li>
                                                                    </ul>
                                                                    <ul id="fuelDocs" runat="server">
                                                                        <li>Copy of RC Book in employee name</li>
                                                                        <li>Original Fuel Bills</li>
                                                                    </ul>
                                                                    <ul id="vehicleDocs" runat="server">
                                                                        <li>Original Maintenance Bill</li>
                                                                        <li>Original Car Service / Repairs Bills</li>
                                                                        <li>Copy of RC Book in employee name</li>
                                                                    </ul>
                                                                    <ul id="driverDocs" runat="server">
                                                                        <li>Original Driver Salary Bill</li>
                                                                        <li>RC Book of Driver</li>
                                                                    </ul>
                                                                    <ul id="telDocs" runat="server">
                                                                        <li>Telephone connection in employee name</li>
                                                                        <li>Original telephone bill</li>
                                                                        <li>Monthly Rental + Call charges are allowed</li>
                                                                    </ul>
                                                                </td>
                                                            </tr>
                                                        </table>

                                                        <div style="display: none;">
                                                            <%--<div>--%>
                                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table2">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:GridView ID="gvBills" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                            CssClass="table4" HorizontalAlign="Left" Width="100%"
                                                                            OnRowDataBound="gvBills_RowDataBound">
                                                                            <Columns>
                                                                                <asp:TemplateField ShowHeader="true">
                                                                                    <ItemStyle CssClass="GridViewItem" Width="1%" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkSelect" runat="Server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Particulars">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtParticulars" runat="server" CssClass="input" ToolTip="Enter Particulars"
                                                                                            Text='<%# Eval("PARTICUALRS") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="To">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtParticulars2" runat="server" CssClass="input" ToolTip="Enter Particulars"
                                                                                            Text='<%# Eval("PARTICUALRS2") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Bill No.">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtBillNo" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Bill No."
                                                                                            Text='<%# Eval("BILL_NO") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Bill Date">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtBillDate" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Bill Date"
                                                                                            Text='<%# Eval("BILL_DATE") %>' Placeholder="dd-MM-yyyy"></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Amount">
                                                                                    <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
                                                                                    <ItemTemplate>
                                                                                        <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter Amount"
                                                                                            Text='<%# Eval("AMOUNT") %>'></asp:TextBox>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                </asp:TemplateField>
                                                                                <%--<asp:TemplateField HeaderText="Accepted Amount">
																					<ItemStyle CssClass="GridViewItem" HorizontalAlign="Center"  />
																					<ItemTemplate>
																						<asp:TextBox ID="txtAcceptedAmount" runat="server" CssClass="input" ReadOnly="true"
																							Text='<%# Eval("AMOUNT_ACCEPTED") %>' style="background-color: LightYellow;" Width="100px"></asp:TextBox>
																					</ItemTemplate>
																					<HeaderStyle CssClass="GridViewHeader" />
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Rejected Amount">
																					<ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
																					<ItemTemplate>
																						<asp:TextBox ID="txtRejectedAmount" runat="server" CssClass="input" ReadOnly="true"
																							Text='<%# Eval("AMOUNT_REJECTED") %>' style="background-color: LightYellow;" Width="100px"></asp:TextBox>
																					</ItemTemplate>
																					<HeaderStyle CssClass="GridViewHeader" />
																				</asp:TemplateField>
																				<asp:TemplateField HeaderText="Remarks">
																					<ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" />
																					<ItemTemplate>
																						<asp:TextBox ID="txtRemarks" runat="server" CssClass="input" TextMode="MultiLine"
																							Text='<%# Eval("REMARKS") %>' style="background-color: LightYellow;" Width="100px" ReadOnly="true"></asp:TextBox>
																					</ItemTemplate>
																					<HeaderStyle CssClass="GridViewHeader" />
																				</asp:TemplateField>--%>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDateSaved" runat="Server" Text='<%# Eval("DATE_SAVED") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="false">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblDocDataId" runat="Server" Text='<%# Eval("Doc_Data_Id") %>' />
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <EmptyDataTemplate>
                                                                                <table id="EmptyTable0" runat="server" cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                    <tr>
                                                                                        <td class="GridViewHeader" style="width: 100%;">No data.
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </EmptyDataTemplate>
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <tr id="rowButtons" runat="server">
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkBtnAddDocRow" Text="Add Row" runat="server" Font-Bold="true"
                                                                            OnClick="lnkBtnAddDocRow_Click" CssClass="Title" Visible="false" />
                                                                        <asp:Label ID="Label2" runat="server" Text="|" Font-Bold="true" ForeColor="blue" Visible="false" />
                                                                        <asp:LinkButton ID="lnkBtnDeleteDocRow" runat="server" Font-Bold="true" OnClick="lnkBtnDeleteDocRow_Click"
                                                                            Text="Delete Selected" CssClass="Title" Visible="false" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnSubmit" runat="server" OnClick="lnkUpdate_Click" Text="Save"
                                                                            Style="width: 61px; height: 26px;" Visible="false" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                        <div>
                                                            <table id="tblLTA" runat="server" visible="false" width="100%" border="0" cellpadding="3" cellspacing="1" class="table9">
                                                                <tr>
                                                                    <td>



                                                                        <div class="title" style="margin-bottom: 5px; text-decoration: underline;">
                                                                            LTA From
                                                                        </div>


                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:LinkButton ID="lnkLTCForm" runat="server" Style="font-size: 15px; font-weight: bold;" OnClick="lnkLTCForm_Click">Click here to download LTA Form</asp:LinkButton>
                                                                        <%--OnClick="lnkLTCForm_Click"--%>
                                                                        <br />

                                                                        <br />
                                                                        <hr />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 18px">
                                                                        <div class="title" style="margin-bottom: 5px; text-decoration: underline;">
                                                                            Upload LTA From
																		   
																		   
																			
																			<%--OnClick="btnUploadLTCForm_Click"--%>
                                                                            <br />
                                                                            <asp:LinkButton ID="LinkButton1" runat="server" Visible="false" Style="font-weight: bold;">Download Saved LTC Form</asp:LinkButton>
                                                                            <%--OnClick="lnkXLUploaded_Click"--%>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 18px">
                                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr valign="top">
                                                                    <td valign="top">
                                                                        <div style="border: 2px solid gray; padding: 10px; margin-top: 10px;">
                                                                            <span style="font-weight: bold; text-decoration: underline; font-size: 15px;">Uploaded Documents:</span><br />
                                                                            <br />
                                                                            <div id="Div3" visible="true" runat="server">
                                                                                <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table18">
                                                                                    <tr>
                                                                                        <td valign="top">
                                                                                            <asp:GridView ID="gvViewLTADocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                                                CssClass="table4" DataKeyNames="FILENAME" Width="100%">
                                                                                                <Columns>
                                                                                                    <asp:TemplateField Visible="false">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:Label ID="lblLTATSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                                                        </ItemTemplate>
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="Uploaded Files">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkBtnOpenFileLTA" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                                                OnClick="lnkBtnOpenFileLTA_Click" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                                                    </asp:TemplateField>
                                                                                                    <asp:TemplateField HeaderText="">
                                                                                                        <ItemTemplate>
                                                                                                            <asp:LinkButton ID="lnkBtnDeleteFileLTA" runat="server" Width="150px" Text='Delete'
                                                                                                                OnClick="lnkBtnDeleteFileLTA_Click" />
                                                                                                        </ItemTemplate>
                                                                                                        <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" />
                                                                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                                                                    </asp:TemplateField>
                                                                                                </Columns>
                                                                                                <EmptyDataTemplate>
                                                                                                    <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                                        <tr>
                                                                                                            <td class="GridViewHeader" style="width: 10%">
                                                                                                                <asp:Literal ID="Literal6" runat="server" Text="Your uploaded supports will be listed here" />
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </EmptyDataTemplate>
                                                                                            </asp:GridView>
                                                                                        </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                        <td>&nbsp;
                                                                                        </td>
                                                                                    </tr>
                                                                                </table>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <div class="title" style="margin-bottom: 5px; text-decoration: underline;">
                                                                Upload Bills
                                                            </div>
                                                            <div style="border: 2px solid gray; padding: 10px; margin-top: 25px; color: Red; font-weight: bold; text-align: center;">
                                                                **Only PDF,JPG/JPEG and ZIP files allowed.**
                                                            </div>
                                                            <table width="100%" border="0" cellpadding="3" cellspacing="1" class="table4">
                                                                <tr id="rowFileUpload" runat="server">
                                                                    <td style="height: 18px">
                                                                        <asp:FileUpload ID="fupldDocument" runat="server" />&nbsp;
																		<asp:Button ID="btnUpload" runat="server" Visible="false" Text="Upload" CssClass="button" OnClick="btnUpload_Click" Style="background-color: Aqua; color: Black; padding: 3px;" />
                                                                        <%--<span id="msg" runat="server" visible="false" style="color: Red; font-weight: bold;">First Select Quarter Type Then Click Submit Button.</span>--%>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr id="rowButtonUpload" runat="server">
																	<td style="height: 18px">
																		
																	</td>
																</tr>--%>
                                                                <tr valign="top">
                                                                    <td valign="top">
                                                                        <div runat="server">
                                                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table3">
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                                            CssClass="table4" DataKeyNames="FILENAME" Width="100%"
                                                                                            OnRowDataBound="gvViewDocDetails_RowDataBound">
                                                                                            <Columns>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Uploaded Files">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                                            OnClick="lnkBtnOpenFile_Click" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkBtnDeleteFile" runat="server" Width="150px" Text='Delete'
                                                                                                            OnClick="lnkBtnDeleteFile_Click" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" />
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <EmptyDataTemplate>
                                                                                                <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                                    <tr>
                                                                                                        <td class="GridViewHeader" style="width: 10%">
                                                                                                            <asp:Literal ID="Literal6" runat="server" Text="No Files." />
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </EmptyDataTemplate>
                                                                                        </asp:GridView>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>&nbsp;
                                                                                    </td>
                                                                                </tr>

                                                                            </table>
                                                                        </div>
                                                                    </td>
                                                                </tr>




                                                                <tr>
                                                                    <td colspan="4" style="text-align: center;">
                                                                        <span id="msg" runat="server" visible="false" style="color: Red; font-weight: bold;">First Select Quarter Type Then Click Submit Button .</span>
                                                                    </td>
                                                                </tr>
                                                                <tr>

                                                                    <td colspan="4" style="text-align: center;">
                                                                        <asp:Button ID="btnAllSubmit" runat="server" Text="Submit" Style="background-color: Aqua; color: Black; padding: 6px; margin-top: 10px;"
                                                                            OnClick="btnAllSubmit_Click" />
                                                                    </td>
                                                                    <br />

                                                                    <%-- <td>
																		<asp:Button ID="btnAllSubmit" runat="server" OnClick="btnAllSubmit_Click" Text="Submit" Style="background-color: dodgerblue" />
																	</td>--%>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" class="table5" id="prevTable"
                                            runat="server">
                                            <tr runat="server" id="prevLabel">
                                                <td class="title">Previous Uploads
                                                </td>
                                            </tr>
                                            <tr valign="top" runat="server" id="prevGV">
                                                <td valign="top">
                                                    <%-- </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drpMonth" 
                            EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                                                    <div id="Div2" visible="true" runat="server">
                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table5">
                                                            <tr>
                                                                <td valign="top">
                                                                    <asp:GridView ID="gvPrevFiles" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                        CssClass="table4" DataKeyNames="FILENAME" Width="100%" OnRowDataBound="gvPrevFiles_RowDataBound">
                                                                        <Columns>
                                                                            <asp:TemplateField Visible="false">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:TemplateField HeaderText="Uploaded Files">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                        OnClick="lnkBtnOpenFilePrev_Click" ClientIDMode="Static" />
                                                                                </ItemTemplate>
                                                                                <ItemStyle CssClass="GridViewItem" Width="90%" BackColor="white" />
                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                            </asp:TemplateField>
                                                                        </Columns>
                                                                        <EmptyDataTemplate>
                                                                            <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                <tr>
                                                                                    <td class="GridViewHeader" style="width: 10%">
                                                                                        <asp:Literal ID="Literal6" runat="server" Text="Uploaded Files" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </EmptyDataTemplate>
                                                                    </asp:GridView>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <rsweb:ReportViewer ID="rptPrint" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                                        InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                                                        BackColor="#CCCCFF" Height="600px" Width="100%" ZoomMode="PageWidth" PageCountMode="Actual">
                                                    </rsweb:ReportViewer>
                                                    <%-- </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="drpMonth" 
                            EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>

                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </section>
                    <triggers>
                            <asp:PostBackTrigger ControlID="lnkFAQ" />
                            <asp:PostBackTrigger ControlID="btnUpload" />
                            <asp:PostBackTrigger ControlID="btnAllSubmit" />
                            <asp:PostBackTrigger ControlID="lnkXLTemplate" />
                            <asp:PostBackTrigger ControlID="btnUploadXL" />
                            <asp:PostBackTrigger ControlID="lnkXLUploaded" />
                            <asp:PostBackTrigger ControlID="btnlnkDwnl" />
                            <asp:PostBackTrigger ControlID="lnkLTCForm" />
                            <%-- <asp:PostBackTrigger ControlID="btnUploadLTCForm" />--%>
                        </triggers>
                </div>

            </div>
        </section>

    </section>
</asp:Content>

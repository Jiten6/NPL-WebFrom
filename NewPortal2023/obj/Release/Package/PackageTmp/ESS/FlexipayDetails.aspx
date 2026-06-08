<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="FlexipayDetails.aspx.cs" Inherits="NewPortal2023.ESS.FlexipayDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .wrap {
            white-space: normal;
            width: 200px;
        }
    </style>
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
    <script type="text/javascript">
        function SetTarget() {
            document.forms[0].target = "_blank";
        }
    </script>
    <style type="text/css">
        .labels {
            float: left;
            padding-top: 7px;
            margin-bottom: 0;
            text-align: left;
            font-weight: 300;
            font-size: 15px;
            font-weight: bold;
        }

        .box1 {
            margin-bottom: 20px; /* Add space below Box 1 */
        }

        .page {
            width: 1200px;
            background-color: #fff;
            margin: 20px auto 0px auto;
            border: 1px solid #496077;
        }


        .center-text {
            text-align: center;
        }

        .centered-textbox {
            text-align: left;
            margin: 0 auto;
            display: block;
        }

        .tableTitle {
            font-weight: normal;
            font-size: 14pt;
            vertical-align: middle;
            color: darkcyan;
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

        .table4N {
            border-right: 0px;
            border-top: 0px;
            border-left: 0px;
            border-bottom: 0px;
            background-color: #cccccc;
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
            color: darkcyan;
            font-family: Tahoma; /* height: 20px; */
            text-decoration: none;
        }

        .hr {
            border: 1px solid #000; /* Set the border style and color */
            margin: 20px 0; /* Add vertical space above and below the line */
        }
    </style>
    <style>
        #toggleButton {
            cursor: pointer;
            font-size: 25px;
            color: darkcyan;
            vertical-align: middle;
        }

        #contentDiv {
            max-height: 0;
            overflow: hidden;
            transition: max-height 0.3s; /* Adjust the duration for your desired animation speed. */
        }

        .toggle-button {
            cursor: pointer;
            vertical-align: middle;
            font-size: 25px;
            color: darkcyan;
        }

        .toggle-content {
            max-height: 0;
            overflow: hidden;
            transition: max-height 0.4s; /* Adjust the duration for your desired animation speed. */
        }

        /*.form-group:nth-of-type(2) .toggle-button {
            max-height: 0;
            overflow: hidden;
            transition: max-height 1s;
        }*/
    </style>
    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loader").style.display = 'block';
        }

    </script>

    <script type="text/javascript">
        function clearFileInputField(divId) {
            document.getElementById(divId).innerHTML = document.getElementById(tagId).innerHTML;
        }

        function ConfirmDel() {
            return confirm("Are you sure you want to delete this record?");
        }
    </script>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var toggleButtons = document.querySelectorAll(".toggle-button");

            toggleButtons.forEach(function (button) {
                button.addEventListener("click", function () {
                    var targetId = this.getAttribute("data-toggle");
                    var contentDiv = document.getElementById(targetId);

                    if (contentDiv.style.maxHeight === "0px") {
                        contentDiv.style.maxHeight = contentDiv.scrollHeight + "px";
                        this.textContent = "-";
                    } else {
                        contentDiv.style.maxHeight = "0";
                        this.textContent = "+";
                    }
                });
            });
        });
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Flexi Details</h3>
                        </header>
                        <div class="panel-body" style="background-color: aliceblue">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/Assets/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/Assets/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>
                            <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                                <asp:View ID="vwList" runat="server">
                                    <div class="adv-table">
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0" id="Table1">
                                            <tr>
                                                <td>
                                                    <asp:UpdatePanel ID="UpdpnlCost" runat="server" UpdateMode="Conditional">
                                                        <ContentTemplate>
                                                            <table width="100%" border="0" cellspacing="3" cellpadding="0" class="table5" id="Table17">
                                                                <tr>
                                                                    <td class="title" id="tdtitle" runat="server">Flexi Pay
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <%--<td class="style1">
                                                                            Effective Date from:
                                                                            <asp:DropDownList ID="drpEffectiveDate" runat="server">
                                                                                <asp:ListItem>01-APR-2014</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </td>--%>               
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                                                                        <br />
                                                                        <asp:Button ID="btnFlexiPrint" runat="server" Text="Print Flexipay Details" OnClick="btnFlexiPrint_Click"
                                                                            Visible="False" />
                                                                    </td>
                                                                </tr>
                                                                <tr id="trcheckCtc" runat="server" visible="false">
                                                                    <td>
                                                                        <b>Enter Employee Code to check CTC :</b>
                                                                        <asp:TextBox ID="txtEmpCode" runat="server" CssClass="input" Width="133px"></asp:TextBox>
                                                                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></br>
                                                                    </td>
                                                                </tr>
                                                                <tr id="trName" runat="server" visible="false">
                                                                    <td>
                                                                        <b>Name :</b>
                                                                        <asp:TextBox ID="txtName" runat="server" CssClass="input" Width="133px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td></br>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <b>Monthly CTC :</b>
                                                                        <asp:TextBox ID="txtAnnCTC" runat="server" CssClass="input" Width="133px" ReadOnly="true"
                                                                            Visible="false"></asp:TextBox>
                                                                        &nbsp;<asp:TextBox ID="txtCTC" runat="server" CssClass="input" Width="98px" ReadOnly="true"
                                                                            Visible="true"></asp:TextBox>
                                                                        &nbsp;<%--<b>Monthly CTC:&nbsp; </b>--%><asp:TextBox ID="txtMonCTC" runat="server"
                                                                            CssClass="input" Width="139px" ReadOnly="true" Visible="false"></asp:TextBox>
                                                                        &nbsp;<%--<b>Monthly Gross Salary:&nbsp; </b>--%><asp:TextBox ID="txtGross" runat="server"
                                                                            CssClass="input" Width="149px" ReadOnly="true" Visible="false"></asp:TextBox>
                                                                        <asp:TextBox ID="txtBal" runat="server" Visible="False"></asp:TextBox>
                                                                        <b>Annual CTC :</b>
                                                                        <asp:TextBox ID="txtAnctc" runat="server" CssClass="input" Width="133px" ReadOnly="true"
                                                                            Visible="false"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table width="100%" border="0" cellpadding="3" cellspacing="1" class="table4" style="border:solid; border-color:darkcyan">
                                                                            <tr valign="top">
                                                                                <td valign="top">
                                                                                    <%-- <asp:UpdatePanel ID="UpdpnlCost" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
                                                                                    <div id="InvDetails" visible="true" runat="server">
                                                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table18">
                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    <asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                                                        CssClass="table4" HorizontalAlign="Left" ToolTip="Flexipay" OnRowDataBound="gvCTC_RowDataBound">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField Visible="true">
                                                                                                                <ItemStyle CssClass="GridViewItem" Width="20px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:CheckBox ID="chkSelect" runat="Server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="true" HeaderText="Allowances">
                                                                                                                <ItemStyle CssClass="GridViewItem" Width="470px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblDesc" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false" HeaderText="Limit">
                                                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblLimit" runat="Server" Text='<%# Eval("Limit_Amount") %>' />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblpct" runat="Server" Text='<%# Eval("PCNT") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblgross" runat="Server" Text='<%# Eval("GROSSCTC") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("HEAD_AID") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lbledit" runat="Server" Text='<%# Eval("EDITABLEPCT") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false" HeaderText="%">
                                                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="30px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtpcnt" runat="Server" Width="50px" CssClass="input" onkeypress="return validateFloatN(event, 0);"
                                                                                                                        AutoPostBack="True" OnTextChanged="txtpcnt_TextChanged" Text='<%# Eval("EPCNT") %>' />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="">
                                                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtPRAN" runat="server" CssClass="input" MaxLength="25" ToolTip="Enter PRAN"
                                                                                                                        Text='' Width="130px" AutoPostBack="False" Visible="false" placeholder="Enter PRAN"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Amount (Monthly)" HeaderStyle-Width="1%">
                                                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter amount"
                                                                                                                        Text='<%# Eval("Fix_Amount") %>' onkeypress="return validateFloat(event, 0);"
                                                                                                                        Width="130px" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField HeaderText="Amount (Annual)">
                                                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtAmountYearly" runat="server" CssClass="input" MaxLength="13"
                                                                                                                        Width="130px" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lbldetId" runat="Server" Text='<%# Eval("ALLWDED_AID") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblPRAN" runat="Server" Text='<%# Eval("CARD_TYPE") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblGroup" runat="Server" Text='<%# Eval("GROUP_TYPE") %>' />
                                                                                                                </ItemTemplate>
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField Visible="false">
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblSel" runat="Server" Text='<%# Eval("CHKFLAG") %>' />
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

                                                                                        </table>
                                                                                    </div>
                                                                                    <div id="divctctotal" visible="false" runat="server" style="margin-top: 0px;">
                                                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table2">
                                                                                            <tr>
                                                                                                <td style="padding-left: 5px;">
                                                                                                    <asp:Label ID="lblBasePay" runat="server" Width="149px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                                <td style="padding-left: 38.6%;">
                                                                                                    <asp:Label ID="lblAmtMnt" runat="server" CssClass="input" Width="135px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                                <td style="padding-left: 19.2%;">
                                                                                                    <asp:Label ID="lblAmtAnn" runat="server" CssClass="input" Width="135px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div id="InvDetails1" visible="false" runat="server" style="margin-top: 0px;">
                                                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table30">
                                                                                            <tr>
                                                                                                <td valign="top">
                                                                                                    <asp:GridView ID="gvCTC1" runat="server" AutoGenerateColumns="False" BorderWidth="0px" Width="100%"
                                                                                                        CssClass="table4" HorizontalAlign="Left" ToolTip="Flexipay">
                                                                                                        <Columns>
                                                                                                            <asp:TemplateField HeaderStyle-Width="73.1%">
                                                                                                                <ItemStyle CssClass="GridViewItem" Width="620px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:Label ID="lblDesc1" runat="Server" Text='<%# Eval("ALLWDED_DESC") %>' />
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtAmount" runat="server" CssClass="input" MaxLength="13" ToolTip="Enter amount"
                                                                                                                        Text='<%# Eval("Fix_Amount") %>' onkeypress="return validateFloat(event, 0);"
                                                                                                                        Width="130px" AutoPostBack="True" OnTextChanged="txtAmount_TextChanged"></asp:TextBox>
                                                                                                                </ItemTemplate>
                                                                                                                <HeaderStyle CssClass="GridViewHeader" />
                                                                                                            </asp:TemplateField>
                                                                                                            <asp:TemplateField>
                                                                                                                <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="130px" />
                                                                                                                <ItemTemplate>
                                                                                                                    <asp:TextBox ID="txtAmountYearly" runat="server" CssClass="input" MaxLength="13"
                                                                                                                        Width="130px" AutoPostBack="True" ReadOnly="true" Text='<%# Eval("Anu_Amount") %>'></asp:TextBox>
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
                                                                                        </table>
                                                                                    </div>

                                                                                    <div id="divctctotal1" visible="false" runat="server" style="margin-top: 0px;">
                                                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table3">
                                                                                            <tr>
                                                                                                <td style="padding-left: 5px;">
                                                                                                    <asp:Label ID="lblBasePay1" runat="server" Width="149px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                                <td style="padding-left: 38.6%">
                                                                                                    <asp:Label ID="lblAmtMnt1" runat="server" CssClass="input" Width="135px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                                <td style="padding-left: 19.2%;">
                                                                                                    <asp:Label ID="lblAmtAnn1" runat="server" CssClass="input" Width="135px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <br />

                                                                                    <hr />
                                                                                    <div id="divAlltotal" visible="false" runat="server" style="margin-top: 0px;">
                                                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table4">
                                                                                            <tr>
                                                                                                <td style="padding-left: 5px;">
                                                                                                    <asp:Label ID="lblAllTotal" runat="server" Width="200px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                                <td style="padding-left: 32.2%;">
                                                                                                    <asp:Label ID="lblAllAmtMnt" runat="server" CssClass="input" Width="140px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                                <td style="padding-left: 15.2%;">
                                                                                                    <asp:Label ID="lblAllAmtAnn" runat="server" CssClass="input" Width="135px" ReadOnly="true"></asp:Label>
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <hr />
                                                                                    <div id="divNotes" visible="false" runat="server" style="margin-top: 0px;">
                                                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table5">
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Label ID="lblNotes" runat="server" ReadOnly="true">
                                                                    <span style="color:Red;font-size:14px;font-weight: bold"><br />
                                                                                Note: Above mentioned Cost to Company (CTC) does not include Short Term Incentives (STI)</span>
                                                                                                    </asp:Label>
                                                                                                </td>

                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                                    <div>
                                                                                        <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table4">
                                                                                            <tr id="Tr1" runat="server" visible="false">
                                                                                                <td class="style2">Car Lease : This option is available for grade AVP and above in all functions.
                                                                    <asp:DropDownList ID="drpCarLease" runat="server" Height="22px">
                                                                        <asp:ListItem>No</asp:ListItem>
                                                                        <asp:ListItem>Yes</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr id="trAgree" runat="server">
                                                                                                <td>
                                                                                                    <table class="table4" width="100%">
                                                                                                        <tr>
                                                                                                            <td style="width: 84%; font-weight: bold;">Note:
                                                                                <br />
                                                                                                                1.&nbsp;&nbsp;&nbsp;Mandatory components are made non editable.
                                                                                <br />
                                                                                                                2.&nbsp;&nbsp;&nbsp;Only the flexi component are made editable for employees to
                                                                                opt it. Employee can opt the flexi component as per their need by clicking on the
                                                                                relevant checkbox.
                                                                                <br />
                                                                                                                3.&nbsp;&nbsp;&nbsp;Once opted any Mid-Year correction on flexi policy will not
                                                                                be considered. It will be reopened again in the next financial year.
                                                                                <br />
                                                                                                                4.&nbsp;&nbsp;&nbsp;All officials are eligible for default standard deduction of
                                                                                Rs. 50000/- under Medical & Conveyance Allowance.
                                                                                                            </td>
                                                                                                        </tr>
                                                                                                    </table>
                                                                                                </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                <td>
                                                                                                    <asp:Button ID="btnsubmit" runat="server" OnClick="lnkUpdate_Click" Text="Submit" Enabled="false"
                                                                                                        Style="width: 61px; height: 26px;" />
                                                                                                </td>
                                                                                            </tr>
                                                                                        </table>
                                                                                    </div>
                                                                            </tr>
                                                                        </table>
                                                                        </div>
                                        <%--  </ContentTemplate>
            </asp:UpdatePanel>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            </td> </tr> </table>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:View>
                            </asp:MultiView>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>

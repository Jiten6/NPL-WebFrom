<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="TaxSimulatorSplit.aspx.cs" Inherits="NewPortal2023.ESS.TaxSimulatorSplit" %>

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

        .vertical-align-center {
            vertical-align: middle;
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
            font-family:inherit;
            height: 20px;
            text-decoration: none;
        }

        .tablesubTitle {
            font-weight: normal;
            font-size: 12pt;
            vertical-align: middle;
            color: darkorange;
            font-family: Tahoma;
            height: 20px;
            text-decoration: none;
            text-align: left;
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
            b border-spacing: 0px;
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

        /*.rcorners {
            border-radius: 25px;
            
        }*/
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
            overflow: auto;
            transition: max-height 0.4s;
        }

        .scroll {
            overflow: auto;
        }

        #dynamicDiv {
            /*border: 1px solid #ccc;*/
            overflow: hidden; /* Ensure that content inside is contained */
        }

        .box {
            border: 1px solid #ddd;
        }
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

    <script>
        function adjustDynamicDivHeight() {
            var dynamicDiv = document.getElementById("dynamicDiv");
            var contentDiv21 = document.getElementById("contentDiv21");
            var drill1 = document.getElementById("drill1");
            var drill2 = document.getElementById("drill2");
            var drill3 = document.getElementById("drill3");

            // Calculate the total height of contentDiv21, drill1, drill2, and drill3
            var totalHeight = contentDiv21.scrollHeight + drill1.scrollHeight + drill2.scrollHeight + drill3.scrollHeight;

            // Set the height of dynamicDiv to "auto" to adjust automatically
            dynamicDiv.style.height = "auto";
        }

        // Call the function when the page loads and whenever the content changes
        window.addEventListener("load", adjustDynamicDivHeight);
        window.addEventListener("resize", adjustDynamicDivHeight);
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Tax Calculator</h3>
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
                                                        <asp:LinkButton ID="lnkTaxSlabManual" runat="server" Visible="false" Style="font-weight: bold;"
                                                            OnClick="lnkTaxSlabManual_Click">Tax Slab/Manual</asp:LinkButton>
                                                        <asp:UpdatePanel ID="UpdpnlCost" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <table width="100%" border="0" cellspacing="3" cellpadding="0" class="table5" id="Table17">
                                                                    <tr>
                                                                        <td class="tableTitle">Tax Simulator
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                            <table width="100%" border="0" cellpadding="3" cellspacing="1" class="table4">
                                                                                <tr valign="top">
                                                                                    <td valign="top" style="background-color:aliceblue">
                                                                                        <%--<asp:UpdatePanel ID="UpdpnlCost" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>--%>
                                                                                        <div id="InvDetails" visible="true" runat="server" style="border:solid; border-color:darkcyan; padding:10px" >
                                                                                            <hr />

                                                                                             <table width="100%" cellspacing="6" cellpadding="0" id="Table18" class="input">
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel" style="width: 40%;"></td>
                                                                                                    <td align="left" style="width: 30%; text-align: center; font-weight: bold;" class="input">Old Regime
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center; font-weight: bold;" class="input">New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>

                                                                                            <%--<table width="100%" cellspacing="6" cellpadding="0" id="Table18" class="input">
                                                                                                <tr style="border-bottom: 2 solid black">
                                                                                                    <td align="left" class="tdlabel" style="width: 40%;"></td>
                                                                                                    <td class="tdlabel" align="center" style="width: 30%;">Old Regime
                                                                                                    </td>
                                                                                                    <td class="tdlabel" align="center" style="width: 30%;">New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>--%>

                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table3">
                                                                                                <tr class="input">
                                                                                                    <td align="left" class="tdlabel" style="width: 40%;">Annual Base Pay:
                                                                                                    </td>
                                                                                                    <td align="center" colspan="2" style="width: 60%;" class="input">
                                                                                                        <asp:TextBox ID="txtAnnualCTC" runat="server" BackColor="LightYellow" CssClass="input"
                                                                                                            MaxLength="25" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="divPerq" runat="server" visible="false" class="input">
                                                                                                    <td align="left" class="tdlabel" style="width: 40%;">Perquisite (Contributions exceeding Rs.7,50,000 would be considered as taxable):
                                                                                                    </td>
                                                                                                    <td align="center" colspan="2" style="width: 60%;">
                                                                                                        <asp:TextBox ID="txtPerquisite" runat="server" BackColor="LightYellow" CssClass="input"
                                                                                                            MaxLength="25" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table5" class="input">
                                                                                                <tr class="input">
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Taxable Amount:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtAmountTaxableOld" runat="server" BackColor="LightYellow" CssClass="input"
                                                                                                            MaxLength="25" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtAmountTaxableNew" runat="server" BackColor="LightYellow" CssClass="input"
                                                                                                            MaxLength="25" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr class="input">
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Taxfree Amount:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtAmountTaxfreeOld" runat="server" BackColor="LightYellow" CssClass="input"
                                                                                                            MaxLength="25" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtAmountTaxfreeNew" runat="server" BackColor="LightYellow" CssClass="input"
                                                                                                            MaxLength="25" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="trSalOthInc" runat="server" visible="true" class="input">
                                                                                                    <td align="left" style="width: 40%;" class="tdlabel input">Variables:</td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtSalOthInc" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>

                                                                                                </tr>
                                                                                                <tr id="trOthInc" runat="server" visible="true" class="input">
                                                                                                    <td align="left" style="width: 40%;" class="tdlabel input">Other Income:</td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtOthInc" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table6" class="input">
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Annual Rent:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtRent" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="margin-bottom: 3px; text-align: right;"></asp:TextBox><br />
                                                                                                        <asp:DropDownList ID="drpRent" runat="server" CssClass="input">
                                                                                                            <asp:ListItem Value="0">[Select Metro/Non-Metro]</asp:ListItem>
                                                                                                            <asp:ListItem Value="1">Metro</asp:ListItem>
                                                                                                            <asp:ListItem Value="2">Non-Metro</asp:ListItem>
                                                                                                        </asp:DropDownList>
                                                                                                        <%--<asp:Button ID="btnCalculateRent" runat="server" Text="Calculate" 
                                                                    onclick="btnCalculateRent_Click" />--%>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel" style="width: 40%;" class="input">Rent Exemption:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtRentExemption" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="margin-bottom: 3px; text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table9" class="input">
                                                                                                <tr>
                                                                                                    <td align="left" style="width: 40%;" class="tdlabel input">Standard Deduction:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtStandardDeduction" runat="server" CssClass="input" MaxLength="25"
                                                                                                            Text="50000.00" ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtStandardDeductionNew" runat="server" CssClass="input" MaxLength="25"
                                                                                                            Text="50000.00" ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <%--<tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" style="width: 20%;" class="tdlabel">HRA Exemption:</td>
                                                                            <td align="left">--%>
                                                                                                <%--</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>--%>
                                                                                                <tr>
                                                                                                    <td align="left" style="width: 40%;" class="tdlabel input">Housing Interest (Max Rs.2,00,000/-):
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtHousingInterest" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Visible="false" Style="text-align: right;"></asp:TextBox>
                                                                                                        <asp:TextBox ID="txtHousingInterestAbs" runat="server" CssClass="input" MaxLength="25"
                                                                                                            Visible="true" ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                        <asp:TextBox ID="txtHRAExemption" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" Visible="false" Style="text-align: right;"></asp:TextBox>
                                                                                                        <%--<asp:Button ID="btnCalculateHousingInterest" runat="server" Text="Calculate" 
                                                                        onclick="btnCalculateHousingInterest_Click" />--%>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <%--<tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" style="width: 20%;" class="tdlabel">Income from Let Out Property:</td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtLetOutIncome" runat="server" CssClass="input" MaxLength="25"></asp:TextBox>
                                                                                <asp:Button ID="btnLetOutIncome" runat="server" Text="Calculate" 
                                                                                    onclick="btnLetOutIncome_Click" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>--%>
                                                                                                <tr runat="server" visible="false">
                                                                                                    <td align="left" style="width: 40%;" class="tdlabel input">Gross Total Income:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtTotalGrossIncome" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <%--<tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <caption>
                                                                            <hr />
                                                                            <tr>
                                                                                <td align="left" class="tdlabel" style="width: 50%;">
                                                                                    Investments under Section 80C:</td>
                                                                                <td align="left">
                                                                                </td>
                                                                            </tr>
                                                                        </caption>
                                                                    </table>
                                                                </td>
                                                            </tr>--%>
                                                                                            </table>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table7" class="input">
                                                                                                <tr runat="server" visible="false" class="input">
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Section 80C:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txt80C" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Exemption under Section 80C (Max Rs.1,50,000/-):
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txt80CEx" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <%--<tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <caption>
                                                                            <hr />
                                                                            <tr>
                                                                                <td align="left" class="tdlabel" style="width: 50%;">
                                                                                    Investments under Section 80D:</td>
                                                                                <td align="left">
                                                                                </td>
                                                                            </tr>
                                                                        </caption>
                                                                    </table>
                                                                </td>
                                                            </tr>--%>
                                                                                                <%--<tr>
                                                                <td>
                                                                    <table width="100%">
                                                                        <tr>
                                                                            <td align="left" style="width: 40%;" class="tdlabel">Section 80D:</td>
                                                                            <td align="left">
                                                                                <asp:TextBox ID="txtTotalInv" runat="server" CssClass="input" MaxLength="25" 
                                                                                    ReadOnly="true" BackColor="LightYellow"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>--%>
                                                                                            </table>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table8" class="input">
                                                                                                <tr>
                                                                                                    <td align="left" style="width: 40%;" class="tdlabel input">Exemption allowed under Section 80D:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%; class="input"">
                                                                                                        <asp:TextBox ID="txtTotalInvExemption" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%; class="input"">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <%--<tr>
                                                                <td align="left" style="width: 40%;" class="tdlabel">National Pension Scheme (Employee) (Max Rs. 50,000/-):</td>
                                                                <td align="center" style="width: 30%;">
                                                                    <asp:TextBox ID="txtNPS" runat="server" CssClass="input" MaxLength="25" 
                                                                        ReadOnly="true" BackColor="LightYellow" style="text-align: right;"></asp:TextBox>
                                                                </td>
                                                                <td align="center" style="width: 30%;">
                                                                    Not allowed in New Regime
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td align="left" style="width: 40%;" class="tdlabel">Other Exemption:</td>
                                                                <td align="center" style="width: 30%;">
                                                                    <asp:TextBox ID="txtOtherExemption" runat="server" CssClass="input" MaxLength="25" 
                                                                        ReadOnly="true" BackColor="LightYellow" style="text-align: right;"></asp:TextBox>
                                                                </td>
                                                                <td align="center" style="width: 30%;">
                                                                    Not allowed in New Regime
                                                                </td>
                                                            </tr>--%>
                                                                                                <tr id="trNP" runat="server" visible="false" class="input">
                                                                                                    <td align="left" style="width: 40%;" class="tdlabel input">National Pension Scheme (Employee) (Max Rs. 50,000/-):
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtNPS" Visible="false" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr runat="server" visible="true" class="input">
                                                                                                    <td align="left" style="width: 40%;" class="tdlabel input">Other Exemption:
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">
                                                                                                        <asp:TextBox ID="txtOtherExemption" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="center" style="width: 30%;" class="input">Not allowed in New Regime
                                                                                                    </td>
                                                                                                </tr>

                                                                                            </table>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table4" class="input">
                                                                                                <tr>
                                                                                                    <td>
                                                                                                        <table width="100%" class="input">
                                                                                                            <caption>
                                                                                                                <tr>
                                                                                                                    <td align="center" class="input">
                                                                                                                        <asp:Button ID="btnCalculateTax" runat="server" OnClick="btnCalculateTax_Click" Style="height: 50px;"
                                                                                                                            Text="CALCULATE TAX" />
                                                                                                                    </td>
                                                                                                                </tr>
                                                                                                            </caption>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table10" class="input">
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel" style="width: 40%;"></td>
                                                                                                    <td align="left" style="width: 30%; text-align: center; font-weight: bold;">As per Old Regime
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center; font-weight: bold;">As per New Regime
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table12" class="input">
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Total Taxable Income:
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtTotalTaxableIncome" runat="server" BackColor="LightYellow" CssClass="input"
                                                                                                            MaxLength="25" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtTotalTaxableIncomeNew" runat="server" BackColor="LightYellow"
                                                                                                            CssClass="input" MaxLength="25" ReadOnly="true" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Tax on Total Income:
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtTaxOnIncome" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtTaxOnIncomeNew" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr id="divBreakup" runat="server" visible="false" class="input">
                                                                                                    <td colspan="3">
                                                                                                        <table width="100%" border="0" cellpadding="3" cellspacing="1" class="table4">
                                                                                                            <tr valign="top">
                                                                                                                <td valign="top">
                                                                                                                    <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table2">
                                                                                                                        <tr>
                                                                                                                            <td valign="top">
                                                                                                                                <asp:GridView ID="gvCTC" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                                                                                    CssClass="table4" HorizontalAlign="Left" ToolTip="Flexipay">
                                                                                                                                    <Columns>
                                                                                                                                        <asp:TemplateField Visible="true" HeaderText="Breakup (Old Regime)">
                                                                                                                                            <ItemStyle CssClass="GridViewItem" />
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("RANGE_OLD") %>' />
                                                                                                                                            </ItemTemplate>
                                                                                                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:TemplateField Visible="true" HeaderText="Tax (Old Regime)">
                                                                                                                                            <ItemStyle CssClass="GridViewItem" />
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("SLAB_TAX") %>' />
                                                                                                                                            </ItemTemplate>
                                                                                                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:TemplateField Visible="true" HeaderText="Breakup (New Regime)">
                                                                                                                                            <ItemStyle CssClass="GridViewItem" />
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("RANGE_NEW") %>' />
                                                                                                                                            </ItemTemplate>
                                                                                                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                                                                                                        </asp:TemplateField>
                                                                                                                                        <asp:TemplateField Visible="true" HeaderText="Tax (New Regime)">
                                                                                                                                            <ItemStyle CssClass="GridViewItem" />
                                                                                                                                            <ItemTemplate>
                                                                                                                                                <asp:Label ID="lblhead" runat="Server" Text='<%# Eval("SLAB_TAX_NEW") %>' />
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
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                        </table>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Tax Credit:
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtTaxCredit" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtTaxCreditNew" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Surcharge:
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtSurcharge" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtSurchargeNew" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Education Cess:
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtEduCess" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtEduCessNew" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Total Annual Tax:
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtAnnualTax" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtAnnualTaxNew" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Total Monthly Tax:
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtMonthlyTax" runat="server" CssClass="input" MaxLength="25" ReadOnly="true"
                                                                                                            BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                    <td align="left" style="width: 30%; text-align: center;" class="input">
                                                                                                        <asp:TextBox ID="txtMonthlyTaxNew" runat="server" CssClass="input" MaxLength="25"
                                                                                                            ReadOnly="true" BackColor="LightYellow" Style="text-align: right;"></asp:TextBox>
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                            <div id="instructionsDiv" runat="server" visible="true" style="border: 2px solid red; margin-top: 5px; padding: 10px;">
                                                                                                <b><u><span style="font-size: 20px;">Existing Employees:</span></u></b><br />
                                                                                                <ol>
                                                                                                    <li style="font-weight: bold;">Other section in the OLD Regime like 80 DD, 80 E etc.
                                                                    are allowed. To keep the calculation simple we have not included in this list. However
                                                                    while arriving at your correct tax slab we will plot all the old exemption list
                                                                    as displayed in last year in invesment portal.</li>
                                                                                                    <li style="font-weight: bold;">The tax calculator is shared to choose the tax regime.
                                                                    Kindly note employee need to update their investment declaration in the portal after
                                                                    choosing the Old tax regime.</li>
                                                                                                    <li style="font-weight: bold;">Employees who choose the NEW Tax regime need not update
                                                                    the investment details in investment portal.</li>
                                                                                                    <li style="color: Red; font-weight: bold;">Please note that once a tax regime is selected,
                                                                    it can not be changed in the financial year.</li>
                                                                                                    <li style="color: Red; font-weight: bold;">In absence of any selection of Tax Regime,
                                                                    the default regime i.e NEW regime will be considered.</li>
                                                                                                </ol>
                                                                                                <b><u><span style="font-size: 20px;">New Employees:</span></u></b><br />
                                                                                                <ol>
                                                                                                    <li style="font-weight: bold;">New joinees in between current Financial Year are requested
                                                                    to opt the tax regime which was opted / followed in your previous employer.</li>
                                                                                                    <li style="font-weight: bold;">Kindly note any mid term changing of Tax Regime is not
                                                                    allowed as Income Tax Act.</li>
                                                                                                    <%--<li style="font-weight: bold;">Changing of Tax Regime in the middle of any Financial Year will lead to problems while filing your Income Tax Returns.</li>--%>
                                                                                                </ol>
                                                                                            </div>
                                                                                            <hr />
                                                                                            <table width="100%" cellspacing="6" cellpadding="0" id="Table11"class="input">
                                                                                                <tr>
                                                                                                    <td align="left" class="tdlabel input" style="width: 40%;">Select Option:
                                                                                                    </td>
                                                                                                    <%--<td align="left" style="width: 30%; text-align: center; font-weight: bold;" >
                                                                                                        <asp:RadioButton ID="rdbOld" runat="server" GroupName="TaxOption" Text="Old Regime" CssClass="vertical-align-center"/>
                                                                                                    </td>--%>

                                                                                                    <td align="left" style="width: 30%; text-align: center; font-weight: bold; vertical-align: middle;">
                                                                                                        <asp:RadioButton ID="rdbOld" runat="server" GroupName="TaxOption" Text="Old Regime" CssClass="vertical-align-center"/>
</td>
                                                                                                    <td align="left" style="width: 30%; text-align: center; font-weight: bold;">
                                                                                                        <asp:RadioButton ID="rdbNew" runat="server" GroupName="TaxOption" Text="New Regime" />
                                                                                                    </td>
                                                                                                </tr>
                                                                                            </table>
                                                                                        </div>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <hr />
                                                                            <table id="Table13" cellpadding="0" cellspacing="6" width="100%">
                                                                                <tr>
                                                                                    <td align="center" colspan="3">
                                                                                        <asp:Button ID="btnSaveOption" runat="server" OnClick="btnSaveOption_Click" Style="height: 50px;"
                                                                                            Text="SAVE SELECTED TAX OPTION" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                            <hr />
                                                                            </div>
                                        <%--</ContentTemplate>
                                                    </asp:UpdatePanel>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                                </td> </tr> </table>
                            <asp:HiddenField ID="hdnBasic" runat="server" />
                                                                <asp:HiddenField ID="hdnHRA" runat="server" />
                                                            </ContentTemplate>
                                                        </asp:UpdatePanel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </header>
                    </section>
                </div>
            </div>
        </section>
    </section>
</asp:Content>

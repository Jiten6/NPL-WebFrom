<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="Letters.aspx.cs" Inherits="NewPortal2023.ESS.Letters" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="styles.css">
    <style type="text/css">
        .wrap {
            white-space: normal;
            width: 200px;
        }

        .adv-table {
            border: 1px solid black; /* You can adjust the border width and color as needed */
        }

        .header-style-back-color {
            background-color: darkcyan;
        }

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

        body {
            margin: 0;
            font-family: Arial, sans-serif;
        }

        #animation-container {
            opacity: 0; /* Set initial opacity to 0 */
            animation: fadeIn 1s ease-out forwards; /* Apply the fadeIn animation */
        }

        @keyframes fadeIn {
            to {
                opacity: 1; /* Set final opacity to 1 */
            }
        }
    </style>

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            // Code to run after the DOM has loaded
            document.getElementById('animation-container').style.opacity = 1;
        });
    </script>

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
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="styles.css">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="animation-container">
        <section id="main-content">
            <section class="wrapper">
                <div class="row">
                    <div class="col-sm-12">
                        <section class="panel">
                            <header class="panel-heading" style="background-color: darkcyan">
                                <h3 style="color: white">Letters</h3>
                            </header>
                            <div class="panel-body" style="background-color: aliceblue">
                                <asp:ScriptManager ID="smInv" runat="server">
                                    <Scripts>
                                        <asp:ScriptReference Path="~/Assets/jquery.blockUI.js" />
                                        <asp:ScriptReference Path="~/Assets/blockUI.js" />
                                    </Scripts>
                                </asp:ScriptManager>

                                <div class="form-group">
                                    <asp:LinkButton ID="lnkOther" runat="server" CssClass="tableTitle" ForeColor="darkcyan"
                                        Visible="false">LETTERS</asp:LinkButton>
                                    <h3 class="page-header form-group" style="text-align: left;">
                                        <asp:Label ID="lblOtherLabel" runat="server" CssClass="tableTitle" ForeColor="darkcyan"
                                            Text="LETTERS"></asp:Label>
                                    </h3>


                                    <asp:MultiView ID="mv" runat="server" ActiveViewIndex="0">
                                        <asp:View ID="vwList" runat="server">
                                            <div class="adv-table" style="border: 1px solid darkcyan">
                                                <table width="100%" border="0" cellspacing="3" cellpadding="0" class="table5" id="Table17">
                                                    <tr>
                                                        <td class="title"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="margin-left: 40px">
                                                            <asp:Label ID="lblMessage" runat="server" Font-Bold="True" ForeColor="#FF3300"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table width="100%" border="0" cellpadding="3" cellspacing="1" class="table4">
                                                                <tr id="year" runat="server" visible="false">
                                                                    <td style="height: 30px; width: 200px">Select Year :
                                                                <asp:DropDownList ID="drpMonth" runat="server" Width="123px" CssClass="input" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr valign="top">
                                                                    <td valign="top">
                                                                        <%--<asp:UpdatePanel ID="UpdpnlCost" runat="server" UpdateMode="Conditional">--%>
                                                                        <%--<ContentTemplate>--%>
                                                                        <div id="InvDetails" visible="true" runat="server">
                                                                            <table width="100%" border="0" cellspacing="6" cellpadding="0" id="Table18">
                                                                                <tr>
                                                                                    <td valign="top">
                                                                                        <asp:GridView ID="gvViewDocDetails" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                                                            CssClass="table4" DataKeyNames="FILENAME" Width="795px">
                                                                                            <Columns>
                                                                                                <asp:TemplateField Visible="false">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:Label ID="lblTSFileStorageName" runat="Server" Text='<%# Eval("FILEPATH") %>' />
                                                                                                    </ItemTemplate>
                                                                                                </asp:TemplateField>
                                                                                                <asp:TemplateField HeaderText="Download" HeaderStyle-BackColor="DarkCyan" HeaderStyle-ForeColor="White" HeaderStyle-BorderColor="DarkCyan">
                                                                                                    <ItemTemplate>
                                                                                                        <asp:LinkButton ID="lnkBtnOpenFile" runat="server" Text='<%# Eval("FILENAME") %>'
                                                                                                            OnClick="lnkBtnOpenFile_Click" />
                                                                                                    </ItemTemplate>
                                                                                                    <ItemStyle CssClass="GridViewItem" BackColor="white" BorderColor="DarkCyan" />
                                                                                                    <HeaderStyle CssClass="GridViewHeader" />
                                                                                                </asp:TemplateField>
                                                                                            </Columns>
                                                                                            <EmptyDataTemplate>
                                                                                                <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                                                                    <tr>
                                                                                                        <td class="GridViewHeader" style="width: 10%">
                                                                                                            <asp:Literal ID="Literal6" runat="server" Text="Download" />
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
                                                                        <%--</ContentTemplate>--%>
                                                                        <%-- <Triggers>
                <asp:AsyncPostBackTrigger ControlID="drpMonth" 
                    EventName="SelectedIndexChanged" />
                </Triggers>--%>
                                                                        <%--</asp:UpdatePanel>--%>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:View>
                                    </asp:MultiView>
                                </div>
                            </div>
                        </section>
                    </div>
                </div>
            </section>
        </section>
    </div>

</asp:Content>

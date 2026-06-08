<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="AttendanceSummeryReport.aspx.cs" Inherits="NewPortal2023.ESS.AttendanceSummeryReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <script language="javascript" src="Common.js" type="text/javascript"></script>

    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loader1").style.display = 'block';
        }

    </script>
    <script type="text/javascript" language="javascript">
        function ValidFile() {
            var file = $("#fupldDocument").val();
            if (file == "") {
                IN4_DisplayErrorMessage("Browse document.");
                return false;
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function ToggleVisible() {
            var trdate = document.getElementById("trdate");
            var trtime = document.getElementById("trtime");
            if (trdate.style.display == 'block') {
                trdate.style.display = 'none';
                trtime.style.display = 'none';
            }
            else {
                trdate.style.display = 'block';
                trtime.style.display = 'block';
            }
        }
    </script>
    <link href="../js/litepicker.css" rel="stylesheet" type="text/css" />
    <script src="../js/litepicker.js" type="text/javascript"></script>

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

        .tableTitleSubHeader {
            font-weight: bold;
            font-size: 9pt;
            vertical-align: middle;
            color: #205a94;
            font-family: Tahoma;
            height: 18px;
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
            font-family: Tahoma;
            /* height: 20px; */
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
                            <h3 style="color: white">Attendance Summary Report</h3>
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

                                        <div class="form-group row" style="margin: 5px">

                                            <label class="col-sm-3 labels">Select Month :-</label>
                                            <div class="form-group col-sm-3">
                                                <asp:DropDownList ID="drpMonth" runat="server" CssClass="form-control input-sm-3" Width="150px" OnSelectedIndexChanged="drpMonth_SelectedIndexChanged"
                                                    AutoPostBack="true" ForeColor="#205A94">

                                                    <asp:ListItem Value="0">Select Month</asp:ListItem>
                                                    <asp:ListItem Value="01">JAN</asp:ListItem>
                                                    <asp:ListItem Value="02">FEB</asp:ListItem>
                                                    <asp:ListItem Value="03">MAR</asp:ListItem>
                                                    <asp:ListItem Value="04">APR</asp:ListItem>
                                                    <asp:ListItem Value="05">MAY</asp:ListItem>
                                                    <asp:ListItem Value="06">JUN</asp:ListItem>
                                                    <asp:ListItem Value="07">JUL</asp:ListItem>
                                                    <asp:ListItem Value="08">AUG</asp:ListItem>
                                                    <asp:ListItem Value="09">SEPT</asp:ListItem>
                                                    <asp:ListItem Value="10">OCT</asp:ListItem>
                                                    <asp:ListItem Value="11">NOV</asp:ListItem>
                                                    <asp:ListItem Value="12">DEC</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                            <label class="col-sm-3 labels">Select Year :-</label>
                                            <div class="form-group col-sm-3">
                                                <asp:DropDownList ID="drpYear" runat="server" CssClass="form-control input-sm-3" Width="150px" OnSelectedIndexChanged="DropYear_SelectedIndexChanged"
                                                    AutoPostBack="true" ForeColor="#205A94">
                                                    <asp:ListItem Value="0">Select Year</asp:ListItem>
                                                    <asp:ListItem Value="2024">2024</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>


                                        </div>

                                        <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                            <asp:Button ID="btnGenerateAttendanceReport" runat="server" CssClass="btn btn-success" Text="Generate Attendance Sammary Report" OnClick="btnGenerateAttendanceReport_Click" OnClientClick="showLoader();" />
                                        </div>

                                        <div class="col-sm-12" style="text-align: center; margin-top: 20px">
                                            <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-primary" Text="Export" OnClick="btnExport_Click" />
                                        </div>

                                        <div class="form-group" id="trViewList" visible="true" runat="server">
                                            <div id="DivgvMultipleList" visible="true" runat="server" style="overflow-x: scroll;">
                                                <%--<asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                    Width="100%">--%>
                                                <asp:GridView ID="gvList" runat="server" AutoGenerateColumns="False"
                                                    HorizontalAlign="Left" CellPadding="5" OnRowDataBound="gvList_RowDataBound"
                                                    GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Emp Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lnkAppNo" runat="server" Text='<%# Eval("EMP_MID") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="5%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Name" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblName" runat="Server" Text='<%# Eval("Emp_Name") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Month" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblMonth" runat="Server" Text='<%# Eval("MONTHS") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="5%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Year" Visible="true">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblYear" runat="Server" Text='<%# Eval("YEAR") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="5%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Used TI">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPL" runat="Server" Text='<%# Eval("TI_Count") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Used PL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPL" runat="Server" Text='<%# Eval("PL_Count") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Used SL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSL" runat="Server" Text='<%# Eval("SL_Count") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Used CL">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCL" runat="Server" Text='<%# Eval("CL_Count") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Used CO">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblCO" runat="Server" Text='<%# Eval("CO_Count") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Used SP">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSP" runat="Server" Text='<%# Eval("SP_Count") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Weekly Off">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWeeklyoff" runat="Server" Text='<%# Eval("WEEKLY_OFF") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="7%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Working On Holiday">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblWorkingonHO" runat="Server" Text='<%# Eval("WORKING_ON_HOLIDAY") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="7%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Holiday">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaidHldy" runat="Server" Text='<%# Eval("PAID_HOLIDAY") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Present Days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPrsnt" runat="Server" Text='<%# Eval("PRESENT_DAYS") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Total Absent Days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblAbsnt" runat="Server" Text='<%# Eval("Total_Absent_Days") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="10%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Total Days">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTtldays" runat="Server" Text='<%# Eval("TOTAL_DAYS") %>' />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="GridViewItem" Width="8%" BackColor="white" HorizontalAlign="Center" />
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                    <EmptyDataTemplate>
                                                        <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                            <tr>
                                                                <td class="GridViewHeader" style="width: 10%">
                                                                    <asp:Literal ID="Literal6" runat="server" Text="Click on 'New Claim'" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnGenerateAttendanceReport" />
                                    <asp:PostBackTrigger ControlID="btnExport" />
                                    <%--<asp:PostBackTrigger ControlID="chk3" />
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

                        <div>
                            <rsweb:ReportViewer ID="rptPrint" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                interactivedeviceinfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                                BackColor="#CCCCFF" Height="600px" Width="100%" ZoomMode="PageWidth" PageCountMode="Actual">
                            </rsweb:ReportViewer>
                        </div>
                    </section>
                </div>
            </div>
        </section>
    </section>


</asp:Content>

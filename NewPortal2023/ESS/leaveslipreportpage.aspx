<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="leaveslipreportpage.aspx.cs" Inherits="NewPortal2023.ESS.leaveslipreportpage" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
Namespace="Microsoft.Reporting.WebForms"
TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   
    <script type="text/javascript">
        $(function () {

            var currentDate = new Date();

            $('[id*=txtFromDate1]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-mm-yyyy",
                autoclose: true,
                endDate: currentDate
            });

            $('[id*=txtToDate1]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-mm-yyyy",
                autoclose: true
            });

        });

     
        function ToggleEmpField() {
            var type = document.getElementById('<%= ddlreportdrop.ClientID %>').value;
            var txt = document.getElementById('<%= txtReportInput.ClientID %>');

            if (type === "Employeewise") {
                txt.disabled = false;
            } else {
                txt.value = "";
                txt.disabled = true;
            }
        }
    </script>

    <style>
        .border-container {
            border: 0.5px solid rgba(204, 204, 204, 0.5);
            padding: 25px;
            margin: 10px;
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
                            <h3 style="color: white">Leave SlipS Report</h3>
                        </header>

                        <div class="panel-body">

                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                            <!-- ALERT -->
                            <div id="divAlert" class="alert alert-danger fade in" runat="server" visible="false">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>

                            <!-- MAIN FORM -->
                            <div class="border-container">

                                <!-- Row 1 -->
                                <div class="form-group row">

                                    <!-- Report Type -->
                                    <div class="col-sm-3">
                                        <label>Select Report Type</label>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlreportdrop" runat="server"
                                            CssClass="form-control"
                                            onchange="ToggleEmpField()">
                                            <asp:ListItem Value="">--Select Type--</asp:ListItem>
                                            <asp:ListItem Value="All">All</asp:ListItem>
                                            <asp:ListItem Value="Employeewise">Employeewise</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <!-- Employee Input -->
                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtReportInput" runat="server"
                                            CssClass="form-control"
                                            Placeholder="Enter Employee ID"
                                            Enabled="false">
                                        </asp:TextBox>
                                    </div>

                                </div>

                                <br />

                                <!-- Row 2 -->
                                <div class="form-group row">

                                    <!-- From Date -->
                                    <div class="col-sm-3">
                                        <label>From Date</label>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtFromDate1" runat="server"
                                            CssClass="form-control datepicker"
                                            Placeholder="dd-mm-yyyy">
                                        </asp:TextBox>
                                    </div>

                                    <!-- To Date -->
                                    <div class="col-sm-3">
                                        <label>To Date</label>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:TextBox ID="txtToDate1" runat="server"
                                            CssClass="form-control datepicker"
                                            Placeholder="dd-mm-yyyy">
                                        </asp:TextBox>
                                    </div>

                                </div>

                                <br />

                                <!-- Row 3 -->
                                <div class="form-group row">

                                    <!-- Month -->
                                    <div class="col-sm-3">
                                        <label>Select Month</label>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="">--Select Month--</asp:ListItem>
                                            <asp:ListItem Value="JAN">January</asp:ListItem>
                                            <asp:ListItem Value="FEB">February</asp:ListItem>
                                            <asp:ListItem Value="MAR">March</asp:ListItem>
                                            <asp:ListItem Value="APR">April</asp:ListItem>
                                            <asp:ListItem Value="MAY">May</asp:ListItem>
                                            <asp:ListItem Value="JUN">June</asp:ListItem>
                                            <asp:ListItem Value="JUL">July</asp:ListItem>
                                            <asp:ListItem Value="AUG">August</asp:ListItem>
                                            <asp:ListItem Value="SEP">September</asp:ListItem>
                                            <asp:ListItem Value="OCT">October</asp:ListItem>
                                            <asp:ListItem Value="NOV">November</asp:ListItem>
                                            <asp:ListItem Value="DEC">December</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>

                                    <!-- Year -->
                                    <div class="col-sm-3">
                                        <label>Select Year</label>
                                    </div>

                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>

                                </div>

                                <br />

                                <!-- Button -->
                                <div class="form-group row">
                                    <div class="col-sm-12 text-center">
                                        <asp:Button ID="btnGenerateReport" runat="server"
                                            CssClass="btn btn-success"
                                            Text="Generate Report"
                                            OnClick="btnGenerate_Click" />
                                    </div>
                                </div>

                            </div>

                            <br />

                            <!-- REPORT VIEWER -->
                            <rsweb:ReportViewer ID="rptPrint" runat="server"
                                Font-Names="Verdana"
                                Font-Size="8pt"
                                Width="100%"
                                Height="600px"
                                ZoomMode="PageWidth">
                            </rsweb:ReportViewer>

                        </div>
                    </section>

                </div>
            </div>
        </section>
    </section>

</asp:Content>

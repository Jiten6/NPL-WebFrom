<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="ReportsPB.aspx.cs" Inherits="NewPortal2023.ESS.ReportsPB" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {

            var currentDate = new Date();

            $('[id*=txtFromDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",
                autoclose: true,
                endDate: currentDate // Disable dates after the current date
            });
        });
        $(function () {
            $('[id*=txtToDate]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",

                //autoclose: true,
                //endDate: currentDate // Disable dates after the current date
            });
        });
    </script>
    <%--   <script>
        function showAlert() {
            var textboxValue = document.getElementById('txtTravelAmt').value;
            alert("Text changed to: " + textboxValue);
        }
    </script>
    <script>
        function validateDates() {
            var fromDate = document.getElementById('<%= txtFromDate.ClientID %>').value;
            var toDate = document.getElementById('<%= txtToDate.ClientID %>').value;

            if (new Date(fromDate) > new Date(toDate)) {
                alert("From Date cannot be less than To Date.");
                // Clear the TO Date field
                document.getElementById('<%= txtToDate.ClientID %>').value = '';
            }
        }

    </script>--%>

    <%-- dnyaneshwar--%>

    <script type="text/javascript">
        $(function () {

            var currentDate = new Date();

            $('[id*=txtFromDate1]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",
                autoclose: true,
                endDate: currentDate // Disable dates after the current date
            });
        });
        $(function () {
            $('[id*=txtToDate1]').datepicker({
                changeMonth: true,
                changeYear: true,
                format: "dd-MM-yyyy",
                language: "tr",

                //autoclose: true,
                //endDate: currentDate // Disable dates after the current date
            });
        });
    </script>
    <%--<script>
        function validateDates1() {
            var fromDate = document.getElementById('<%= txtFromDate1.ClientID %>').value;
            var toDate = document.getElementById('<%= txtToDate1.ClientID %>').value;
            var fromDate2 = document.getElementById('<%= txtFromDate2.ClientID %>').value;
            var toDate2 = document.getElementById('<%= txtToDate2.ClientID %>').value;

            if (new Date(fromDate) > new Date(toDate)) {
                alert("From Date cannot be less than To Date.");
                // Clear the TO Date field
                document.getElementById('<%= txtToDate1.ClientID %>').value = '';
            }

            if (new Date(fromDate2) > new Date(toDate2)) {
                alert("From Date cannot be less than To Date.");
                // Clear the TO Date field
                document.getElementById('<%= txtToDate2.ClientID %>').value = '';
            }


        }

    </script>--%>

    <%-- dnyaneshwar--%>

    <style>
        .border-container {
            border: 0.5px solid rgba(204, 204, 204, 0.5); /* Adds a 1px solid black border */
            padding: 25px; /* Optional: Adds padding inside the border */
            margin: 10px; /* Optional: Adds margin outside the border */
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
                            <h3 style="color: white">Reports</h3>
                        </header>

                        <div class="panel-body">
                            <asp:ScriptManager ID="smInv" runat="server">
                                <Scripts>
                                    <asp:ScriptReference Path="~/ESS/jquery.blockUI.js" />
                                    <asp:ScriptReference Path="~/ESS/blockUI.js" />
                                </Scripts>
                            </asp:ScriptManager>

                            <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                <asp:Label ID="lblMessage" runat="server"></asp:Label>
                            </div>

                            <section class="content-header" runat="server" id="SectionList">
                                <div class="container-fluid">
                                    <!-- SELECT2 EXAMPLE -->
                                    <div class="card card-default">
                                        <div class="row mb-2" style="margin-top: 10px; margin-right: 2px;">
                                            <div class="col-sm-6">
                                                <!-- Content for the left side -->
                                            </div>
                                            <%-- <div class="col-sm-6">
                                        <ol class="breadcrumb float-sm-right">
                                            <li class="breadcrumb-item"><a href="#">Expense Report</a></li>
                                            <li class="breadcrumb-item active">Report From</li>
                                        </ol>
                                    </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </section>

                            <div id="divFrom" runat="server" visible="true">


                                <%--      
                                 
                                <div class="card card-default">
                                    <div class="card-body" >
                                       
                                        <div class="border-container form-group">
                                         <div class="col-12" visible="false">                                      
                                                    <asp:Label ID="Label3" runat="server" Text="Claim Detail Report" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                            </div>
                                                    &emsp;
                                            <fieldset>                                         

                                                <div class="form-group row">
                                                    <label for="drpType" class="col-sm-3 col-form-label">Type :</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                                            <asp:ListItem Value="">[Select Expense Type]</asp:ListItem>
                                                            <asp:ListItem Value="Domestic" Text="Domestic"></asp:ListItem>
                                                            <asp:ListItem Value="Local" Text="Local"></asp:ListItem>
                                                            <asp:ListItem Value="Telephone" Text="Telephone"></asp:ListItem>
                                                             <asp:ListItem Value="Miscellaneous" Text="Miscellaneous"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <label for="drpEmpType" class="col-sm-3 col-form-label">Employee Wise:</label>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="drpEmpType" runat="server" CssClass="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpEmpType_SelectedIndexChanged"></asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label for="txtFromDate" class="col-sm-3 col-form-label">From Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox class="form-control" ID="txtFromDate" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label for="txtToDate" class="col-sm-3 col-form-label">To Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox CssClass="form-control" ID="txtToDate" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                                                        <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate" CssClass="btn btn-warning" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>
                                      

                                        <div class="border-container"  ID="approved" visible="false">
                                             <div class="col-12" >
                                                    <asp:Label ID="lblExpTitle" runat="server" Text="Approved Claim Report" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                    &emsp;
                                            <fieldset>
                                                <div class="form-group row">
                                                    <label for="txtFromDate1" class="col-sm-3 col-form-label">From Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox class="form-control" ID="txtFromDate1" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label for="txtToDate1" class="col-sm-3 col-form-label">To Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox CssClass="form-control" ID="txtToDate1" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                                                        <asp:Button ID="btnSampleKRA" runat="server" Style="margin-left: 10px;" OnClick="lnkDownloadrdlc_Click" Text="Download" CssClass="btn btn-warning" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>

                                        <div class="border-container"  ID="update" visible="false">
                                             <div class="col-12" >
                                                    <asp:Label ID="Label1" runat="server" Text="Update Paid Claim" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                    &emsp;
                                            <fieldset>
                                                <div class="form-group row">
                                                    <label for="fupldDocument" class="col-sm-3 col-form-label">Select File to upload</label>
                                                    <div class="col-sm-3">
                                                        <asp:FileUpload ID="fupldDocument" runat="server" CssClass="btn fileinput-button" />
                                                        <asp:LinkButton ID="buttonTemplate" runat="server" Style="color: blue; font-weight: bold" OnClick="lnkDownloadTemplate_Click" AutoPostBack="true">Download Template</asp:LinkButton>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-12" style="text-align: center; margin-top: 30px">
                                                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Upload" AutoPostBack="true" OnClick="btnInsert_Click" />
                                                    </div>
                                                </div>
                                            </fieldset>
                                        </div>

                                        <div class="border-container"  ID="paid" visible="false">
                                             <div class="col-12" >
                                                    <asp:Label ID="Label2" runat="server" Text="Paid Claim Report" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                    &emsp;
                                            <fieldset>
                                                <div class="form-group row">
                                                    <label for="txtFromDate2" class="col-sm-3 col-form-label">From Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox class="form-control" ID="txtFromDate2" runat="server"></asp:TextBox>
                                                    </div>
                                                    <label for="txtToDate2" class="col-sm-3 col-form-label">To Date :</label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox CssClass="form-control" ID="txtToDate2" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                                                        <asp:Button ID="Button1" runat="server" Style="margin-left: 10px;" OnClick="lnkDownloadapaidrdlcreport_Click" Text="Download" CssClass="btn btn-warning" />
                                                    </div>
                                                </div>


                                            </fieldset>
                                        </div>--%>





                                <div class="col-sm-12">
                                    <label class="col-sm-3 col-form-label" style="font-size: 20px;">Select Report Type</label>
                                    <div class="col-sm-3">
                                        <asp:DropDownList ID="ddlreportdrop" runat="server" CssClass="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpType_Report_Selectedchanges" AutoPostBack="true">
                                            <asp:ListItem Value="">[Select Report Type]</asp:ListItem>
                                            <asp:ListItem Value="Employee Age Profiling" Text="Employee Age Profiling"></asp:ListItem>
                                            <asp:ListItem Value="Approved Claim Report" Text="Employee Tenure"></asp:ListItem>
                                            <asp:ListItem Value="Paid CTC" Text="Paid CTC"></asp:ListItem>
                                            <asp:ListItem Value="Paid Claim Report" Text="Ot Hours And Ot Amount"></asp:ListItem>
                                            <asp:ListItem Value="New joinee And Quit Employees" Text="New joinee And Attrition"></asp:ListItem>
                                             <asp:ListItem Value="Monthly head count" Text="Monthly head count"></asp:ListItem>
                                             <asp:ListItem Value="Today's Attendence" Text="Today's Attendence"></asp:ListItem>
                                            <asp:ListItem Value="Previous Month Average Cost" Text="Previous Month Average Cost"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>

                                <br />
                                <br />


                                <div class="container-fluid">
                                    <!-- Claim Detail Report Panel -->
                                    <asp:Panel ID="claimDetailPanel" runat="server" Visible="false" CssClass="card card-default">
                                        <div class="card-body">
                                            <div class="border-container form-group">
                                                <div class="col-12">
                                                    <asp:Label ID="Label3" runat="server" Text="Employee Age Profiling" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                &emsp;
                <fieldset>
                    <div class="form-group row">
                        <label for="drpType" class="col-sm-3 col-form-label">Type :</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="drpType" runat="server" CssClass="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="">Select Age Profiling</asp:ListItem>
                                <asp:ListItem Value="ALL" Text="ALL"></asp:ListItem>
                                <asp:ListItem Value="More_than_25" Text="Less than 25"></asp:ListItem>
                                <asp:ListItem Value="Between_25_to_35" Text="Between 25 to 35"></asp:ListItem>
                                <asp:ListItem Value="Between_35_to_45" Text="Between 35 to 45"></asp:ListItem>
                                <asp:ListItem Value="Between_45_to_55" Text="Between 45 to 55"></asp:ListItem>
                                <asp:ListItem Value="More_than_55" Text="More than 55"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <%--  <label for="drpEmpType" class="col-sm-3 col-form-label">Employee Wise:</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="drpEmpType" runat="server" CssClass="form-control select2" Style="width: 100%;">
                                <asp:ListItem Value="">Select Employee</asp:ListItem>
                                <asp:ListItem Value="E0000023" Text="Executive"></asp:ListItem>
                                <asp:ListItem Value="E0000026" Text="Workman"></asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>
                    </div>
                    <%--   <div class="form-group row">
                        <label for="txtFromDate" class="col-sm-3 col-form-label">From Date :</label>
                        <div class="col-sm-3">
                            <asp:TextBox CssClass="form-control" ID="txtFromDate" runat="server"></asp:TextBox>
                        </div>
                        <label for="txtToDate" class="col-sm-3 col-form-label">To Date :</label>
                        <div class="col-sm-3">
                            <asp:TextBox CssClass="form-control" ID="txtToDate" runat="server"></asp:TextBox>
                        </div>
                    </div>--%>
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                            <asp:Button ID="btnGenerate" runat="server" OnClick="btnGenerate_Click" Text="Generate" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </fieldset>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <!-- Approved Claim Report Panel -->
                                    <asp:Panel ID="approvedClaimPanel" runat="server" Visible="false" CssClass="card card-default">
                                        <div class="card-body">
                                            <div class="border-container form-group">
                                                <div class="col-12">
                                                    <asp:Label ID="lblExpTitle" runat="server" Text="EMPLOYEE TENURE" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                &emsp;
                <fieldset>
                    <div class="form-group row">
                        <label for="txtFromDate1" class="col-sm-3 col-form-label">Tenure :</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="drpTenure" runat="server" CssClass="form-control select2" Style="width: 100%;" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem Value="">Select tenure</asp:ListItem>
                                <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                                <asp:ListItem Value="2YRS" Text="Less than 2 YRS"></asp:ListItem>
                                <asp:ListItem Value="2YRS_to_5YRS" Text="2YRS - 5YRS"></asp:ListItem>
                                <asp:ListItem Value="5YRS_to_10YRS" Text="5YRS - 10 YRS"></asp:ListItem>
                                <asp:ListItem Value="10+_YRS" Text="10+ YRS"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <%--  <label for="drpEmpType" class="col-sm-3 col-form-label">Employee Wise:</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="drpEmpTypeTenure" runat="server" CssClass="form-control select2" Style="width: 100%;">
                                <asp:ListItem Value="">Select Employee</asp:ListItem>
                                <asp:ListItem Value="E0000023" Text="Executive"></asp:ListItem>
                                <asp:ListItem Value="E0000026" Text="Workman"></asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                            <asp:Button ID="btnSampleKRA" runat="server" Style="margin-left: 10px;" OnClick="btnGeneratetenure_Click" Text="Download" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </fieldset>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <!-- Update Paid Claim Panel -->
                                    <%--<asp:Panel ID="updatePaidClaimPanel" runat="server" Visible="false" CssClass="card card-default">
        <div class="card-body">
            <div class="border-container form-group">
                <div class="col-12">
                    <asp:Label ID="Label1" runat="server" Text="Update Paid Claim" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                </div>
                &emsp;
                <fieldset>
                     <div class="form-group row">
                        <label for="fupldDocument" class="col-sm-3 col-form-label">Select File to upload</label>
                        <div class="col-sm-3">
                           <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control select2" Style="width: 100%;">
                                <asp:ListItem Value="">Select Employee</asp:ListItem>
                                <asp:ListItem Value="E0000023" Text="DOWNLOAD EMPLOYEE DATA WHO LEFT"></asp:ListItem>
                                <asp:ListItem Value="E0000026" Text="Workman"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <label for="fupldDocument" class="col-sm-3 col-form-label">Select File to upload</label>
                        <div class="col-sm-3">
                           <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control select2" Style="width: 100%;">
                                <asp:ListItem Value="">Select Employee</asp:ListItem>
                                <asp:ListItem Value="E0000023" Text="Executive"></asp:ListItem>
                                <asp:ListItem Value="E0000026" Text="Workman"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 30px">
                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Text="Upload" AutoPostBack="true" OnClick="btnInsert_Click" />
                        </div>
                         <asp:Label ID="lblmsg" runat="server"></asp:Label>
                    </div>
                </fieldset>
            </div>
        </div>
    </asp:Panel>--%>

                                    <!-- Paid Claim Report Panel -->
                                    <asp:Panel ID="paidClaimReportPanel" runat="server" Visible="false" CssClass="card card-default">
                                        <div class="card-body">
                                            <div class="border-container form-group">
                                                <div class="col-12">
                                                    <asp:Label ID="Label2" runat="server" Text="Ot Hours And Ot Amount" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                &emsp;
                <fieldset>
                    <div class="form-group row">
                        <label for="txtFromDate2" class="col-sm-3 col-form-label">Select Month :</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="ddlMonths" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value="JAN 2025" Text="Jan 2025"></asp:ListItem>
                                <asp:ListItem Value="FEB 2025" Text="Feb 2025"></asp:ListItem>
                                <asp:ListItem Value="MAR 2025" Text="Mar 2025"></asp:ListItem>
                                <asp:ListItem Value="APR 2025" Text="Apr 2025"></asp:ListItem>
                                 <asp:ListItem Value="MAY 2025" Text="MAY 2025"></asp:ListItem>
                                  <asp:ListItem Value="JUN 2025" Text="JUN 2025"></asp:ListItem>
                                 <asp:ListItem Value="JUL 2025" Text="JUL 2025"></asp:ListItem>
                                 <asp:ListItem Value="AUG 2025" Text="AUG 2025"></asp:ListItem>
                                  <asp:ListItem Value="SEP 2025" Text="SEP 2025"></asp:ListItem>
                                <asp:ListItem Value="OCT 2025" Text="OCT 2025"></asp:ListItem>
                                 <asp:ListItem Value="NOV 2025" Text="NOV 2025"></asp:ListItem>
                                <asp:ListItem Value="DEC 2025" Text="DEC 2025"></asp:ListItem>
                                 <asp:ListItem Value="JAN 2026" Text="JAN 2026"></asp:ListItem>
                                <asp:ListItem Value="APR 2024" Text="Apr 2024"></asp:ListItem>
                                <asp:ListItem Value="MAY 2024" Text="May 2024"></asp:ListItem>
                                <asp:ListItem Value="JUN 2024" Text="Jun 2024"></asp:ListItem>
                                <asp:ListItem Value="JUL 2024" Text="Jul 2024"></asp:ListItem>
                                <asp:ListItem Value="AUG 2024" Text="Aug 2024"></asp:ListItem>
                                <asp:ListItem Value="SEP 2024" Text="Sep 2024"></asp:ListItem>
                                <asp:ListItem Value="OCT 2024" Text="Oct 2024"></asp:ListItem>
                                <asp:ListItem Value="NOV 2024" Text="Nov 2024"></asp:ListItem>
                                <asp:ListItem Value="DEC 2024" Text="Dec 2024"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <label for="txtToDate2" class="col-sm-3 col-form-label">Select Employee Type :</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="drpEmpType3" runat="server" CssClass="form-control select2" Style="width: 100%;">
                                <asp:ListItem Value="">Select Employee</asp:ListItem>
                                <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                                <asp:ListItem Value="E0000023" Text="Executive"></asp:ListItem>
                                <asp:ListItem Value="E0000026" Text="Workman"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                            <asp:Button ID="Button1" runat="server" Style="margin-left: 10px;" OnClick="btnGenerateOtandAmt_Click" Text="Download" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </fieldset>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                    <!-- Paid ctc-->
                                    <asp:Panel ID="PaidCtcpanel" runat="server" Visible="false" CssClass="card card-default">
                                        <div class="card-body">
                                            <div class="border-container form-group">
                                                <div class="col-12">
                                                    <asp:Label ID="Label1" runat="server" Text="Paid CTC" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                &emsp;
                <fieldset>
                    <div class="form-group row">
                        <label for="txtFromDate2" class="col-sm-3 col-form-label">Select Month :</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value="JAN 2025" Text="Jan 2025"></asp:ListItem>
                                <asp:ListItem Value="FEB 2025" Text="Feb 2025"></asp:ListItem>
                                <asp:ListItem Value="MAR 2025" Text="Mar 2025"></asp:ListItem>
                                <asp:ListItem Value="APR 2025" Text="Apr 2025"></asp:ListItem>
                                 <asp:ListItem Value="MAY 2025" Text="MAY 2025"></asp:ListItem>
                                 <asp:ListItem Value="JUN 2025" Text="JUN 2025"></asp:ListItem>
                                 <asp:ListItem Value="JUL 2025" Text="JUL 2025"></asp:ListItem>
                                <asp:ListItem Value="AUG 2025" Text="AUG 2025"></asp:ListItem>
                                <asp:ListItem Value="SEP 2025" Text="SEP 2025"></asp:ListItem>
                                <asp:ListItem Value="OCT 2025" Text="OCT 2025"></asp:ListItem>
                                 <asp:ListItem Value="NOV 2025" Text="NOV 2025"></asp:ListItem>
                                 <asp:ListItem Value="DEC 2025" Text="DEC 2025"></asp:ListItem>
                                 <asp:ListItem Value="JAN 2026" Text="JAN 2026"></asp:ListItem>
                                <asp:ListItem Value="APR 2024" Text="Apr 2024"></asp:ListItem>
                                <asp:ListItem Value="MAY 2024" Text="May 2024"></asp:ListItem>
                                <asp:ListItem Value="JUN 2024" Text="Jun 2024"></asp:ListItem>
                                <asp:ListItem Value="JUL 2024" Text="Jul 2024"></asp:ListItem>
                                <asp:ListItem Value="AUG 2024" Text="Aug 2024"></asp:ListItem>
                                <asp:ListItem Value="SEP 2024" Text="Sep 2024"></asp:ListItem>
                                <asp:ListItem Value="OCT 2024" Text="Oct 2024"></asp:ListItem>
                                <asp:ListItem Value="NOV 2024" Text="Nov 2024"></asp:ListItem>
                                <asp:ListItem Value="DEC 2024" Text="Dec 2024"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <%-- <label for="txtToDate2" class="col-sm-3 col-form-label">Select Employee Type :</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control select2" Style="width: 100%;">
                                <asp:ListItem Value="">Select Employee</asp:ListItem>
                                     <asp:ListItem Value="ALL" Text="All"></asp:ListItem>
                                <asp:ListItem Value="E0000023" Text="Executive"></asp:ListItem>
                                <asp:ListItem Value="E0000026" Text="Workman"></asp:ListItem>
                            </asp:DropDownList>
                        </div>--%>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                            <asp:Button ID="Button2" runat="server" Style="margin-left: 10px;" OnClick="btnGeneratePaidctc_Click" Text="Download" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </fieldset>
                                            </div>
                                        </div>
                                    </asp:Panel>


                                    <!--New joinee and quit employees -->
                                    <asp:Panel ID="PanelNaA" runat="server" Visible="false" CssClass="card card-default">
                                        <div class="card-body">
                                            <div class="border-container form-group">
                                                <div class="col-12">
                                                    <asp:Label ID="Label4" runat="server" Text="New joinee and Attrition" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                &emsp;
                 <fieldset>
                    <div class="form-group row">
                        <label for="select type" class="col-sm-3 col-form-label">Select Report :</label>
                        <div class="col-sm-3">
                            <asp:DropDownList ID="DropDownList2" runat="server" CssClass="form-control select2">
                                <asp:ListItem Value="">[Select Report Type]</asp:ListItem>
                                <asp:ListItem Value="New_joiners" Text="New joiners"></asp:ListItem>
                                <asp:ListItem Value="Attrition" Text="Attrition"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                            <asp:Button ID="Button3" runat="server" Style="margin-left: 10px;" OnClick="btnGenerateNaA_Click" Text="Download" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </fieldset>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                     <!--active and attendence -->
                                    <asp:Panel ID="Panelact" runat="server" Visible="false" CssClass="card card-default">
                                        <div class="card-body">
                                            <div class="border-container form-group">
                                                <div class="col-12">
                                                    <asp:Label ID="Label5" runat="server" Text="Monthly head count" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                &emsp;
                 <fieldset>
                    <div class="form-group row">
                      <label for="select type" class="col-sm-3 col-form-label">Select Report :</label>
                         <div class="col-sm-3">
                             <asp:DropDownList ID="ddlmonthyear" runat="server" CssClass="form-control select2">
                                 <asp:ListItem Value="2025-1" Text="Jan 2025"></asp:ListItem>
                                 <asp:ListItem Value="2025-2" Text="Feb 2025"></asp:ListItem>
                                 <asp:ListItem Value="2025-3" Text="Mar 2025"></asp:ListItem>
                                  <asp:ListItem Value="2025-4" Text="Apr 2025"></asp:ListItem>
                                   <asp:ListItem Value="2025-5" Text="May 2025"></asp:ListItem>
                                  <asp:ListItem Value="2025-6" Text="Jun 2025"></asp:ListItem>
                                 <asp:ListItem Value="2025-7" Text="Jul 2025"></asp:ListItem>
                                  <asp:ListItem Value="2025-8" Text="Aug 2025"></asp:ListItem>
                                  <asp:ListItem Value="2025-9" Text="Sep 2025"></asp:ListItem>
                                 <asp:ListItem Value="2025-10" Text="Oct 2025"></asp:ListItem>
                                    <asp:ListItem Value="2025-11" Text="Nov 2025"></asp:ListItem>
                                   <asp:ListItem Value="2025-12" Text="Dec 2025"></asp:ListItem>
                                 <asp:ListItem Value="2026-1" Text="Jan 2026"></asp:ListItem>
                                 <asp:ListItem Value="2024-4" Text="Apr 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-5" Text="May 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-6" Text="Jun 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-7" Text="Jul 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-8" Text="Aug 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-9" Text="Sep 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-10" Text="Oct 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-11" Text="Nov 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-12" Text="Dec 2024"></asp:ListItem>
                             </asp:DropDownList>
                         </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                            <asp:Button ID="Button4" runat="server" Style="margin-left: 10px;" OnClick="btnGenerAct_Click" Text="Download" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </fieldset>
                                            </div>
                                        </div>
                                    </asp:Panel>


                                     <!--attendence -->
                                      <asp:Panel ID="PanelAttendence" runat="server" Visible="false" CssClass="card card-default">
                                        <div class="card-body">
                                            <div class="border-container form-group">
                                                <div class="col-12">
                                                    <asp:Label ID="Label6" runat="server" Text="Today's Attendence" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                &emsp;
                 <fieldset>
                  <%--  <div class="form-group row">
                      <label for="select type" class="col-sm-3 col-form-label">Select Report :</label>
                         <div class="col-sm-3">
                             <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-control select2">
                                 <asp:ListItem Value="2025-1" Text="Jan 2025"></asp:ListItem>
                                 <asp:ListItem Value="2025-2" Text="Feb 2025"></asp:ListItem>
                                 <asp:ListItem Value="2025-3" Text="Mar 2025"></asp:ListItem>
                                 <asp:ListItem Value="2024-4" Text="Apr 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-5" Text="May 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-6" Text="Jun 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-7" Text="Jul 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-8" Text="Aug 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-9" Text="Sep 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-10" Text="Oct 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-11" Text="Nov 2024"></asp:ListItem>
                                 <asp:ListItem Value="2024-12" Text="Dec 2024"></asp:ListItem>
                             </asp:DropDownList>
                         </div>
                    </div>--%>
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                            <asp:Button ID="Button5" runat="server" Style="margin-left: 10px;" OnClick="btnGenerateAttendence_Click" Text="Download" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </fieldset>
                                            </div>
                                        </div>
                                    </asp:Panel>


                                     <!--previous month avg -->
                                      <asp:Panel ID="Panelpreavg" runat="server" Visible="false" CssClass="card card-default">
                                        <div class="card-body">
                                            <div class="border-container form-group">
                                                <div class="col-12">
                                                    <asp:Label ID="Label7" runat="server" Text="Previous Month Average Cost" CssClass="card-title" Style="font-size: 24px;"></asp:Label>
                                                </div>
                                                &emsp;
                 <fieldset>
                
                    <div class="form-group row">
                        <div class="col-sm-12" style="text-align: center; margin-top: 40px">
                            <asp:Button ID="Button6" runat="server" Style="margin-left: 10px;" OnClick="btnGenerateAvgCost_Click" Text="Download" CssClass="btn btn-warning" />
                        </div>
                    </div>
                </fieldset>
                                            </div>
                                        </div>
                                    </asp:Panel>
                                </div>




                                <rsweb:ReportViewer ID="rptPrint" runat="server" Font-Names="Verdana" Font-Size="8pt"
                                    interactivedeviceinfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
                                    BackColor="#CCCCFF" Height="600px" Width="100%" ZoomMode="PageWidth" PageCountMode="Actual">
                                </rsweb:ReportViewer>

                            </div>

                        </div>
                    </section>
                </div>

            </div>
        </section>
        <%-- </div>
    </div>--%>
    </section>

    <%--  </section>--%>
</asp:Content>

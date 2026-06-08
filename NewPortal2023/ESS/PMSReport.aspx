<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="PMSReport.aspx.cs" Inherits="NewPortal2023.ESS.PMSReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function showLoader() {
            document.getElementById("loadingOverlay").style.display = 'block';
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">
                        <div class="d-flex flex-wrap">
                            <div class="flex-grow-1">
                                <header class="panel-heading" style="background-color: darkcyan">
                                    <h3 style="color: white">PMS Report</h3>
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
                                            <div class="col-lg-12 row">
                                                <div class="col-sm-3" style="margin-left: -20px">
                                                    <asp:Label ID="Label2" runat="server" Text="Financial Year :" CssClass="card-title" Style="font-size: 20px;"></asp:Label>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="drpFinancialYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="drpFinancialYear_SelectedIndexChanged" CssClass="form-control input-sm-3" Enabled="true" onchange="showLoader();">
                                                            <asp:ListItem Value="">Select Year</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <br />
                                                </div>
                                            </div>

                                            <div class="col-12" id="divKRAFin" runat="server" visible="false">
                                                <div class="col-12" style="margin-left: -5px; display: flex; justify-content: center; align-items: center;">
                                                    <asp:Label ID="lblCycle" runat="server" Text="" CssClass="card-title" Style="font-size: 30px;"></asp:Label>
                                                </div>

                                                <div class="col-lg-12">
                                                    <div class="form-group">
                                                        <hr />
                                                    </div>
                                                </div>
                                                <div id="form1" runat="server">
                                                    <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                                        <button data-dismiss="alert" class="close close-sm" type="button">
                                                            <i class="fa fa-times"></i>
                                                        </button>
                                                        <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                                    </div>

                                                    <div id="divReportType" runat="server" class="form-group row" style="margin: 10px">
                                                        <div class="form-group row">
                                                            <div class="col-sm-3">
                                                                <label style="font-size: 15px;">Select Report Type :</label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="drpReportType" runat="server" OnSelectedIndexChanged="drpReportType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="200px" onchange="showLoader();">
                                                                    <asp:ListItem Value="" Text="Select Report Type"></asp:ListItem>
                                                                    <asp:ListItem Value="KRA" Text="KRA"></asp:ListItem>
                                                                    <asp:ListItem Value="KeyAccomp" Text="Key Accomplishment"></asp:ListItem>
                                                                    <asp:ListItem Value="TrainAndDevlp" Text="Training And Development"></asp:ListItem>
                                                                    <asp:ListItem Value="PLPPayout" Text="PLP Payout"></asp:ListItem>
                                                                    <asp:ListItem Value="OverallRating" Text="Overall Rating"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divEmpType" runat="server" visible="false" class="form-group row" style="margin: 10px">
                                                        <div class="form-group row">
                                                            <div class="col-sm-3">
                                                                <label style="font-size: 15px;">Select Type :</label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="drpType" runat="server" OnSelectedIndexChanged="drpType_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="200px" onchange="showLoader();">
                                                                    <asp:ListItem Value="" Text="All Employees"></asp:ListItem>
                                                                    <asp:ListItem Value="Employee Wise" Text="Employee Wise"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>

                                                    <div id="divAboveSr" runat="server" class="form-group row" style="margin: 10px">
                                                        <div id="divEmp" runat="server" class="form-group row" visible="false">
                                                            <div class="col-sm-3">
                                                                <asp:Label ID="lblDpt" runat="server" Text="Select Department :" Style="font-size: 15px;"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:DropDownList ID="drpDptList" runat="server" OnSelectedIndexChanged="drpDptList_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="200px" onchange="showLoader();">
                                                                </asp:DropDownList>
                                                            </div>

                                                            <div class="col-sm-3">
                                                                <asp:Label ID="lblEmpCode" runat="server" Text="Employee Code :" Style="font-size: 15px;"></asp:Label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <asp:TextBox ID="txtEmpCode" runat="server" Visible="true" AutoCompleteType="Disabled" CssClass="form-control input-sm" Width="200px"></asp:TextBox>
                                                                <asp:DropDownList ID="drpEmpCode" runat="server" OnSelectedIndexChanged="drpEmpCode_SelectedIndexChanged" Visible="false" AutoPostBack="true" CssClass="form-control input-sm-3" onchange="showLoader();" Width="200px">
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>

                                                        &emsp;

                                                        <div id="divPMS" runat="server" class="form-group col-sm-12" style="text-align: center">
                                                            <asp:Button ID="btnGenerateReport" runat="server" AutoPostBack="true" OnClick="btnGenerateReport_Click" OnClientClick="showLoader();" Text="Generate PMS Report" CssClass="btn btn-success" Width="300px" />
                                                        </div>

                                                        <div id="divPMSAll" runat="server" class="form-group col-sm-12" style="text-align: center">
                                                            <asp:Button ID="btnGenerateReportAllEmp" runat="server" AutoPostBack="true" OnClick="btnGenerateReportAllEmp_Click" OnClientClick="showLoader(); setTimeout(hideLoader, 500); return true;" Text="Generate PMS Report(All Employee)" CssClass="btn btn-success" Width="300px" />
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="drpDptList" />
                                            <asp:PostBackTrigger ControlID="drpEmpCode" />
                                            <asp:PostBackTrigger ControlID="btnGenerateReport" />
                                            <asp:PostBackTrigger ControlID="drpType" />
                                            <asp:PostBackTrigger ControlID="btnGenerateReportAllEmp" />
                                            <asp:PostBackTrigger ControlID="drpFinancialYear" />
                                            <asp:PostBackTrigger ControlID="drpReportType" />
                                            <%-- <asp:PostBackTrigger ControlID="chk7" />
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
                            </div>
                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>


</asp:Content>

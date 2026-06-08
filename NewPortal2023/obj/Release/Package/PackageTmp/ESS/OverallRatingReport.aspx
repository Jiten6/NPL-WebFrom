<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="OverallRatingReport.aspx.cs" Inherits="NewPortal2023.ESS.OverallRatingReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">Overall Rating Report</h3>
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
                                        <div id="divAlert" class="alert alert-block alert-danger fade in" runat="server" visible="false">
                                            <button data-dismiss="alert" class="close close-sm" type="button">
                                                <i class="fa fa-times"></i>
                                            </button>
                                            <asp:Label ID="lblMessage" runat="server"></asp:Label>
                                        </div>

                                        <div id="div1" runat="server" class="form-group row" style="margin: 10px">
                                            <div class="form-group row">
                                                <div class="col-sm-3">
                                                    <asp:Label ID="Label1" runat="server" Text="Select Type :" Style="font-size: 15px;"></asp:Label>
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

                                            <div class="form-group col-sm-12" style="text-align: center">
                                                <asp:Button ID="btnGenerateReport" runat="server" AutoPostBack="true" OnClick="btnGenerateReport_Click" OnClientClick="showLoader();" Text="Generate Overall Rating Report" CssClass="btn btn-success" Width="300px" />
                                            </div>

                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="drpDptList" />
                                    <asp:PostBackTrigger ControlID="drpEmpCode" />
                                    <asp:PostBackTrigger ControlID="btnGenerateReport" />
                                    <asp:PostBackTrigger ControlID="drpType" />
                                    <%--<asp:PostBackTrigger ControlID="chk5" />
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

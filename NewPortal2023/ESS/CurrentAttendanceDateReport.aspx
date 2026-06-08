<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="CurrentAttendanceDateReport.aspx.cs" Inherits="NewPortal2023.ESS.CurrentAttendanceDateReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   <style>
    .GridViewHeader {
        background-color: orange;

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
                            <h3 style="color: white">Today Attendance Report</h3>
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

                                        <div id="trSubmit1" runat="server">
                                            <div class="form-group row" style="margin: 10px">

                                                <%-- <div class="form-group row">
                                                    <asp:RadioButton ID="rbPresent" Text="Present" runat="server" OnCheckedChanged="rbPresent_CheckedChanged" AutoPostBack="true" GroupName="AttendanceType"></asp:RadioButton>
                                                    <asp:RadioButton ID="rbAbsent" Text="Absent" runat="server" OnCheckedChanged="rbPresent_CheckedChanged" AutoPostBack="true" GroupName="AttendanceType"></asp:RadioButton>
                                                </div>--%>



                                                <div class="form-group row">
                                                    <asp:Label ID="lblfDt" runat="server" class="col-sm-3" Style="font-size: 15px;">Executive<span style="color: Red;">*</span> :</asp:Label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtExecutive" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm" placeholder="Date" Width="200px"></asp:TextBox>
                                                    </div>

                                                    <asp:Label ID="lbltDt" runat="server" class="col-sm-3" Style="font-size: 15px;">WorkMen<span style="color: Red;">*</span> :</asp:Label>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtWorkMen" runat="server" AutoCompleteType="Disabled" CssClass="form-control input-sm" placeholder="Date" Width="200px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <br />
                                                <br />

                                                <hr />

                                                <asp:GridView ID="gvLeave" runat="server" AutoGenerateColumns="False"
                                    HorizontalAlign="Left" CellPadding="5"
                                     Width="100%"  BorderColor="#CCCCCC"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                    <Columns> <%-- OnRowDataBound="gvLeave_RowDataBound"--%>
                                                   
                                                        <asp:TemplateField HeaderText="EMP CODE">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDate" runat="Server" Text='<%# Eval("EMP_CODE") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="NAME">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblcode" runat="Server" Text='<%# Eval("EMP_NAME") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TIME IN">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTimein" runat="Server" Text='<%# Eval("Time_in") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="TIME OUT">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="100px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTimeout" runat="Server" Text='<%# Eval("TIME_OUT") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="DAY">
                                                            <ItemStyle CssClass="GridViewItem" HorizontalAlign="Center" Width="50px" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="Server" Text='<%# Eval("DAY") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle CssClass="GridViewHeader" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                            <tr>
                                                                <td class="GridViewHeader" style="width: 10%">
                                                                    <asp:Literal ID="Literal6" runat="server" Text="Records Not Founds" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </EmptyDataTemplate>
                                                </asp:GridView>


                                                <%-- <div id="divEmp" runat="server" class="form-group row" visible="false">
                                                    <div class="col-sm-3">
                                                        <asp:Label ID="lblDpt" runat="server" Text="Select Department :" Style="font-size: 15px;"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:DropDownList ID="drpDptList" runat="server" OnSelectedIndexChanged="drpDptList_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="200px" onchange="showLoader();">
                                                        </asp:DropDownList>
                                                    </div>

                                                    <div class="col-sm-3">
                                                        <asp:Label ID="Label1" runat="server" Text="Employee Code :" Style="font-size: 15px;"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:TextBox ID="txtEmpCode" runat="server" Visible="true" AutoCompleteType="Disabled" CssClass="form-control input-sm" Width="200px"></asp:TextBox>
                                                        <asp:DropDownList ID="drpEmpCode" runat="server" Visible="false" AutoPostBack="true" CssClass="form-control input-sm-3" onchange="showLoader();" Width="200px">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                                    <asp:Button ID="btnGenerateAttendanceReport" runat="server" CssClass="btn btn-success" Text="Generate Attendance Details Report"
                                                        OnClick="btnGenerateAttendanceReport_Click" OnClientClick="showLoader();" />
                                                </div>

                                                <div class="col-sm-12" style="text-align: center; margin-top: 20px">
                                                    <asp:Button ID="btnExport" runat="server" Visible="false" CssClass="btn btn-primary" Text="Export" OnClick="btnExport_Click" />
                                                </div>--%>
                                            </div>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <%--                   <asp:PostBackTrigger ControlID="drpDptList" />
                                    <asp:PostBackTrigger ControlID="drpEmpCode" />
                                    <asp:PostBackTrigger ControlID="btnGenerateReport" />
                                    <asp:PostBackTrigger ControlID="drpType" />
                                    <asp:PostBackTrigger ControlID="btnGenerateReport" />
                                    <asp:PostBackTrigger ControlID="btnGenerateReportAllEmp" />
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
                           <%-- <rsweb:reportviewer id="rptPrint" runat="server" font-names="Verdana" font-size="8pt"
                                interactivedeviceinfos="(Collection)" waitmessagefont-names="Verdana" waitmessagefont-size="14pt"
                                backcolor="#CCCCFF" height="600px" width="100%" zoommode="PageWidth" pagecountmode="Actual">
                            </rsweb:reportviewer>--%>
                        </div>
                    </section>
                </div>

            </div>
        </section>
    </section>
</asp:Content>

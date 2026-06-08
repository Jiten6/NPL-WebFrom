<%@ Page Title="" Language="C#" MasterPageFile="~/ESS/ESS.Master" AutoEventWireup="true" CodeBehind="PLEncashmentReport.aspx.cs" Inherits="NewPortal2023.ESS.PLEncashmentReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
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
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section id="main-content">
        <section class="wrapper">
            <div class="row">
                <div class="col-sm-12">
                    <section class="panel">

                        <header class="panel-heading" style="background-color: darkcyan">
                            <h3 style="color: white">PL Encashment Report</h3>
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

                                        <div class="form-group">
                                            <div class="form-group">
                                                <label class="col-sm-3 labels" style="font-size: 16PX;">Month :</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpSelectMonth" runat="server" OnSelectedIndexChanged="drpSelectMonth_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>
                                                </div>
                                                <%--<asp:Label ID="lblMonth" runat="server" Text="" CssClass="input"></asp:Label>--%>
                                            </div>

                                            <div class="form-group">
                                                <label class="col-sm-3 labels" style="font-size: 16PX;">Year :</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpSelectFinancialYear" runat="server" OnSelectedIndexChanged="drpSelectFinancialYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>
                                                </div>
                                                <%--<asp:Label ID="lblYear" runat="server" Text="" CssClass="input"></asp:Label>--%>
                                            </div>


                                            <%--<label class="col-sm-3 labels" style="font-size: 16PX;">Year :</label>
                                                <div class="col-sm-3">
                                                    <asp:DropDownList ID="drpSelectFinancialYear" runat="server" OnSelectedIndexChanged="drpSelectFinancialYear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control input-sm-3" Width="150px">
                                                    </asp:DropDownList>
                                                </div>
                                                <asp:Label ID="lblYear" runat="server" Text="" CssClass="input"></asp:Label>--%>
                                        </div>

                                        <div class="col-sm-12" style="text-align: center; margin-top: 50px">
                                            <asp:Button ID="btnSearch" CssClass="btn btn-info" runat="server" Text="Search" OnClick="btnSearch_Click" />

                                        </div>

                                        <div class="col-sm-12" style="text-align: center; margin-top: 10px">
                                            <asp:Button ID="btnExport" runat="server" CssClass="btn btn-warning" Text="Export" OnClick="btnExport_Click" />
                                        </div>

                                        <div id="trHr" runat="server" visible="false" class="form-group">
                                            <asp:TextBox ID="txtEmpCode" Visible="false" runat="server" CssClass="tableTitle" ForeColor="#205A94"></asp:TextBox>

                                            <asp:Button ID="btnLeaveDetails" Visible="false" runat="server" Text="Search" CssClass="tableTitle"
                                                Width="80px" Height="27px"
                                                ForeColor="#205A94" OnClick="btnLeaveDetails_Click" />
                                        </div>

                                        <div id="OtherDetails" visible="true" runat="server" style="overflow-x: scroll;">
                                            <%--visible="false"--%>

                                           <%-- <asp:GridView ID="gvLeaveCards" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                                                ToolTip=" PL Encashment Report">--%>
                                            <asp:GridView ID="gvLeaveCards" runat="server" AutoGenerateColumns="False"
                                                HorizontalAlign="Left" CellPadding="5" 
                                                GridLines="None" Width="100%" BackColor="White" BorderColor="#CCCCCC"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Names="Arial" Font-Size="12px" class="table table-bordered table-condensed">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Emp Code">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblFrom" runat="Server" Text='<%# Eval("EMP_MID") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Employee_Name">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblId" runat="Server" Text='<%# Eval("EMPLOYEE_NAME") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Year">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Year") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Month">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Month") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Leave Type">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTo" runat="Server" Text='<%# Eval("Leave_Code") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Encashment Days">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Encashed") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <%-- <asp:TemplateField HeaderText="Debit">--%>

                                                    <%-- <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Debit") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Encashed">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Encashed") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Availed">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Availed") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Closing_Bal">

                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLeave" runat="Server" Text='<%# Eval("Closing_Bal") %>' />
                                                        </ItemTemplate>
                                                        <ItemStyle CssClass="GridViewItem" BackColor="white" HorizontalAlign="Left" />
                                                        <HeaderStyle CssClass="GridViewHeader" />
                                                    </asp:TemplateField>--%>
                                                </Columns>
                                                <HeaderStyle BackColor="Orange" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                                <EmptyDataTemplate>
                                                    <table cellpadding="0" cellspacing="0" class="GridViewEmpty">
                                                        <tr>
                                                            <td class="GridViewHeader" style="width: 10%">
                                                                <asp:Literal ID="Literal6" runat="server" Text="No Records Found" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                            </asp:GridView>

                                        </div>

                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnSearch" />
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
                    </section>
                </div>

            </div>
        </section>
        <rsweb:ReportViewer ID="rptPrint" runat="server" Font-Names="Verdana" Font-Size="8pt"
            InteractiveDeviceInfos="(Collection)" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"
            BackColor="#CCCCFF" Height="600px" Width="100%" ZoomMode="PageWidth" PageCountMode="Actual">
        </rsweb:ReportViewer>
    </section>
</asp:Content>
